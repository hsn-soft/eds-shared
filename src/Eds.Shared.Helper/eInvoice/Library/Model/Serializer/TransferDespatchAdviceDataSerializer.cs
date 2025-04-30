using System.Text.RegularExpressions;
using Eds.Shared.Helper.eInvoice.Library.Model.Utils;
using Eds.Shared.Helper.VeribanGlobal.Library.Common;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes.EInvoice;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Serializer;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase;

namespace Eds.Shared.Helper.eInvoice.Library.Model.Serializer
{
    public class TransferDespatchAdviceDataSerializer : BaseSerializer
    {
        public string SerializeAndGetXmlContent(DespatchAdviceModel despatchAdviceModel, bool byPassModelControl = false, bool clearXmlDocumentHeaderTag = false)
        {
            if (despatchAdviceModel == null)
                throw new ArgumentNullException("despatchAdviceModel", "despatchAdviceModel must have a reference");

            if (!byPassModelControl)
            {
                KeyValuePair<bool, List<string>> controlData = TransferDespatchAdviceDataModelControl(despatchAdviceModel);
                if (!controlData.Key)
                {
                    throw new ArgumentException("MODEL CONTROL FAIL : " + ControlledDataHandle(controlData.Value));
                }
            }

            //ADD DEFAULT DESPATCH ADVICE DATA
            despatchAdviceModel = DespatchAdviceAppendGibTag(despatchAdviceModel);

            //CLEAR XMLNS
            despatchAdviceModel.xmlns = (new DespatchAdviceModel().xmlns);

            return SerializeAndGetXmlContentBase(new List<DespatchAdviceModel>() { despatchAdviceModel }, !clearXmlDocumentHeaderTag);
        }

        public bool SerializeAndSaveXmlFile(string saveFileFullPath, DespatchAdviceModel despatchAdviceModel, bool byPassModelControl = false)
        {
            if (despatchAdviceModel == null)
                throw new ArgumentNullException("despatchAdviceModel", "despatchAdviceModel must have a reference");

            if (!byPassModelControl)
            {
                KeyValuePair<bool, List<string>> controlData = TransferDespatchAdviceDataModelControl(despatchAdviceModel);
                if (!controlData.Key)
                {
                    throw new ArgumentException("MODEL CONTROL FAIL : " + ControlledDataHandle(controlData.Value));
                }
            }

            File.WriteAllText(saveFileFullPath, SerializeAndGetXmlContentBase(new List<DespatchAdviceModel>() { despatchAdviceModel }, true));

            return File.Exists(saveFileFullPath);
        }

        public DespatchAdviceModel DeserializeFromXmlFile(string despatchAdviceFileFullPath, bool byPassModelControl = false)
        {
            if (string.IsNullOrEmpty(despatchAdviceFileFullPath))
                throw new ArgumentNullException("despatchAdviceFileFullPath", "despatchAdviceFileFullPath must have a reference");

            string xmlContent = string.Empty;
            try
            {
                string content = File.ReadAllText(despatchAdviceFileFullPath, System.Text.Encoding.UTF8);

                xmlContent = SafeLoadFromXmlFile(content);
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message.ToString()); }

            if (string.IsNullOrEmpty(xmlContent))
                throw new ArgumentException("xmlContent can not read from path");

            return DeserializeFromXmlContent(xmlContent, byPassModelControl);
        }

        public DespatchAdviceModel DeserializeFromXmlContent(string despatchAdviceXmlContent, bool byPassModelControl = false)
        {
            if (string.IsNullOrEmpty(despatchAdviceXmlContent))
                throw new ArgumentNullException("despatchAdviceXmlContent", "despatchAdviceXmlContent must have a reference");

            DespatchAdviceModel deserializeDespatchModel = DeserializeFromXmlContentBase<DespatchAdviceModel>(despatchAdviceXmlContent);

            //ADD GIB DEFAULT DATA
            deserializeDespatchModel = DespatchAdviceAppendGibTag(deserializeDespatchModel);

            if (!byPassModelControl)
            {
                KeyValuePair<bool, List<string>> controlData = TransferDespatchAdviceDataModelControl(deserializeDespatchModel);
                if (!controlData.Key)
                {
                    throw new ArgumentException("MODEL CONTROL FAIL : " + ControlledDataHandle(controlData.Value));
                }
            }

            return deserializeDespatchModel;
        }

        public KeyValuePair<bool, List<string>> TransferDespatchAdviceDataModelControl(DespatchAdviceModel despatchAdviceModel)
        {
            List<string> errorList = new List<string>();

            string errorMessage = string.Empty;
            if (!DespatchAdviceModelControl(despatchAdviceModel, out errorMessage))
                errorList.Add(errorMessage);

            if (errorList.Count > 0)
                return new KeyValuePair<bool, List<string>>(false, errorList);
            else
                return new KeyValuePair<bool, List<string>>(true, null);
        }

        #region OLD CONTROL
        private bool DespatchAdviceModelControl(DespatchAdviceModel despatchAdvice, out string errorMessage)
        {
            errorMessage = string.Empty;
            #region Rule: inv:DespatchAdvice/cac:DespatchSupplierParty/cac:Party/cac:PartyIdentification/cbc:ID -> PartyIdentificationSchemeIDCheck
            if (despatchAdvice.DespatchSupplierParty != null)
            {
                if (despatchAdvice.DespatchSupplierParty.Party != null)
                {
                    if (despatchAdvice.DespatchSupplierParty.Party.PartyIdentification != null && despatchAdvice.DespatchSupplierParty.Party.PartyIdentification.Count > 0)
                    {
                        List<string> schemeId = new List<string>();
                        foreach (var partIdentificationDespatchAdvice in despatchAdvice.DespatchSupplierParty.Party.PartyIdentification)
                        {
                            if (partIdentificationDespatchAdvice.ID != null)
                            {
                                if (!string.IsNullOrEmpty(partIdentificationDespatchAdvice.ID.SchemeId) && PartySchemeIdType.TYPE_LIST.FirstOrDefault(x => x.GetName() == partIdentificationDespatchAdvice.ID.SchemeId) != null)
                                {
                                    #region Rule: inv:DespatchAdvice/cac:DespatchSupplierParty/cac:Party/cac:PartyIdentification -> PartyIdentificationTCKNVKNCheck
                                    if (partIdentificationDespatchAdvice.ID.SchemeId == PartySchemeIdType.VKN.ToString())
                                    {
                                        if (partIdentificationDespatchAdvice.ID.Id.Length != 10)
                                        {
                                            errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyIdentification.ID (VKN) must be 10 character"; return false;
                                        }
                                    }
                                    else if (partIdentificationDespatchAdvice.ID.SchemeId == PartySchemeIdType.TCKN.ToString())
                                    {
                                        if (partIdentificationDespatchAdvice.ID.Id.Length != 11)
                                        {
                                            errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyIdentification.ID (TCKN) must be 11 character"; return false;
                                        }
                                    }
                                    else if (partIdentificationDespatchAdvice.ID.SchemeId == PartySchemeIdType.TICARETSICILNO.ToString())
                                    {
                                        if (partIdentificationDespatchAdvice.ID.Id == null)
                                        {
                                            errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyIdentification.ID (TICARETSICILNO) can not be null"; return false;
                                        }
                                    }
                                    else if (partIdentificationDespatchAdvice.ID.SchemeId == PartySchemeIdType.ALIAS.ToString())
                                    {
                                        if (partIdentificationDespatchAdvice.ID.Id == null)
                                        {
                                            errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyIdentification.ID (ALIAS) can not be null"; return false;
                                        }
                                    }
                                    else if (partIdentificationDespatchAdvice.ID.SchemeId == PartySchemeIdType.BAYINO.ToString())
                                    {
                                        if (partIdentificationDespatchAdvice.ID.Id == null)
                                        {
                                            errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyIdentification.ID (BAYINO) can not be null"; return false;
                                        }
                                    }
                                    else if (partIdentificationDespatchAdvice.ID.SchemeId == PartySchemeIdType.SUBENO.ToString())
                                    {
                                        if (partIdentificationDespatchAdvice.ID.Id == null)
                                        {
                                            errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyIdentification.ID (SUBENO) can not be null"; return false;
                                        }
                                    }
                                    else if (partIdentificationDespatchAdvice.ID.SchemeId == PartySchemeIdType.MERSISNO.ToString())
                                    {
                                        if (partIdentificationDespatchAdvice.ID.Id == null)
                                        {
                                            errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyIdentification.ID (MERSISNO) can not be null"; return false;
                                        }
                                    }
                                    else if (partIdentificationDespatchAdvice.ID.SchemeId == PartySchemeIdType.GTBGCBTESCILNO.ToString() && partIdentificationDespatchAdvice.ID.Id == null)
                                    {
                                        errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyIdentification.ID (GTBGCBTESCILNO) can not be null"; return false;
                                    }
                                    #endregion
                                    //PartyIdentificationPartyNamePersonCheck için
                                    schemeId.Add(partIdentificationDespatchAdvice.ID.SchemeId);
                                }
                                else { errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyIdentification(" + partIdentificationDespatchAdvice.ID.Id + ").SchemeId is not PartyIdentificationID"; return false; }
                            }
                            else { errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyIdentification.ID"; return false; }
                        }

                        #region Rule: inv:despatchAdvice/cac:DespatchSupplierParty/cac:Party -> PartyIdentificationPartyNamePersonCheck
                        if (schemeId.Contains(PartySchemeIdType.TCKN.ToString()) && schemeId.Contains(PartySchemeIdType.VKN.ToString()))
                        {
                            errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyIdentification.ID => VKN and TCKN can not coexist"; return false;
                        }
                        else
                        {
                            if (schemeId.Contains(PartySchemeIdType.TCKN.ToString()) || schemeId.Contains(PartySchemeIdType.VKN.ToString()))
                            {
                                if (schemeId.Contains(PartySchemeIdType.VKN.ToString()))
                                {
                                    if (despatchAdvice.DespatchSupplierParty.Party.PartyName != null)
                                    {
                                        if (string.IsNullOrEmpty(despatchAdvice.DespatchSupplierParty.Party.PartyName.Name))
                                        { errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyName can not be empty"; return false; }
                                    }
                                    else { errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyName"; return false; }
                                }
                                else if (schemeId.Contains(PartySchemeIdType.TCKN.ToString()))
                                {

                                    if (despatchAdvice.DespatchSupplierParty.Party.Person != null)
                                    {
                                        if (string.IsNullOrEmpty(despatchAdvice.DespatchSupplierParty.Party.Person.FirstName))
                                        {
                                            errorMessage = "despatchAdvice.DespatchSupplierParty.Party.Person.FirstName can not be empty"; return false;
                                        }

                                        if (string.IsNullOrEmpty(despatchAdvice.DespatchSupplierParty.Party.Person.FamilyName))
                                        {
                                            errorMessage = "despatchAdvice.DespatchSupplierParty.Party.Person.FamilyName can not be empty"; return false;
                                        }
                                    }
                                    else { errorMessage = "despatchAdvice.DespatchSupplierParty.Party.Person"; return false; }
                                }
                            }
                            else { errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyIdentification.ID => VKN or TCKN to be found"; return false; }
                        }
                        #endregion

                    }
                    else { errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyIdentification"; return false; }
                }
                else { errorMessage = "despatchAdvice.DespatchSupplierParty.Party"; return false; }
            }
            else { errorMessage = "despatchAdvice.DespatchSupplierParty"; return false; }
            #endregion

            #region Rule: inv:DespatchAdvice/cac:DeliveryCustomerParty/cac:Party/cac:PartyIdentification/cbc:ID -> PartyIdentificationSchemeIDCheck
            if (despatchAdvice.DeliveryCustomerParty != null)
            {
                if (despatchAdvice.DeliveryCustomerParty.Party != null)
                {
                    if (despatchAdvice.DeliveryCustomerParty.Party.PartyIdentification != null && despatchAdvice.DeliveryCustomerParty.Party.PartyIdentification.Count > 0)
                    {
                        List<string> schemeId = new List<string>();
                        foreach (var partIdentificationDespatch in despatchAdvice.DeliveryCustomerParty.Party.PartyIdentification)
                        {
                            if (partIdentificationDespatch.ID != null)
                            {
                                if (!string.IsNullOrEmpty(partIdentificationDespatch.ID.SchemeId) && PartySchemeIdType.TYPE_LIST.FirstOrDefault(x => x.GetName() == partIdentificationDespatch.ID.SchemeId) != null)
                                {
                                    #region Rule: inv:DespatchAdvice/cac:DeliveryCustomerParty/cac:Party/cac:PartyIdentification -> PartyIdentificationTCKNVKNCheck
                                    if (partIdentificationDespatch.ID.SchemeId == PartySchemeIdType.VKN.ToString())
                                    {
                                        if (partIdentificationDespatch.ID.Id == null)
                                        {
                                            errorMessage = "despatchAdvice.DeliveryCustomerParty.Party.PartyIdentification.ID (VKN) can not be null"; return false;
                                        }
                                        else
                                        {
                                            if (partIdentificationDespatch.ID.Id.Length != 10)
                                            {
                                                errorMessage = "despatchAdvice.DeliveryCustomerParty.Party.PartyIdentification.ID (VKN) must be 10 character"; return false;
                                            }
                                        }
                                    }
                                    else if (partIdentificationDespatch.ID.SchemeId == PartySchemeIdType.TCKN.ToString() && partIdentificationDespatch.ID.Id.Length != 11)
                                    {
                                        errorMessage = "despatchAdvice.DeliveryCustomerParty.Party.PartyIdentification.ID (TCKN) must be 11 character"; return false;
                                    }
                                    #endregion

                                    //PartyIdentificationPartyNamePersonCheck için
                                    schemeId.Add(partIdentificationDespatch.ID.SchemeId);
                                }
                                else { errorMessage = "despatchAdvice.DeliveryCustomerParty.Party.PartyIdentification(" + partIdentificationDespatch.ID.Id + ").SchemeId is not PartyIdentificationID"; return false; }

                            }
                            else { errorMessage = "despatchAdvice.DeliveryCustomerParty.Party.PartyIdentification.ID"; return false; }
                        }


                        #region Rule: inv:DespatchAdvice/cac:DeliveryCustomerParty/cac:Party -> PartyIdentificationPartyNamePersonCheck
                        if (schemeId.Contains(PartySchemeIdType.TCKN.ToString()) && schemeId.Contains(PartySchemeIdType.VKN.ToString()))
                        {
                            errorMessage = "despatchAdvice.DeliveryCustomerParty.Party.PartyIdentification.ID => VKN and TCKN can not coexist"; return false;
                        }
                        else
                        {
                            if (schemeId.Contains(PartySchemeIdType.TCKN.ToString()) || schemeId.Contains(PartySchemeIdType.VKN.ToString()))
                            {
                                if (schemeId.Contains(PartySchemeIdType.VKN.ToString()))
                                {
                                    if (despatchAdvice.DeliveryCustomerParty.Party.PartyName != null)
                                    {
                                        if (string.IsNullOrEmpty(despatchAdvice.DeliveryCustomerParty.Party.PartyName.Name))
                                        {
                                            errorMessage = "despatchAdvice.DeliveryCustomerParty.Party.PartyName can not be empty"; return false;
                                        }
                                    }
                                    else { errorMessage = "despatchAdvice.DeliveryCustomerParty.Party.PartyName"; return false; }
                                }
                                else if (schemeId.Contains(PartySchemeIdType.TCKN.ToString()))
                                {

                                    if (despatchAdvice.DeliveryCustomerParty.Party.Person != null)
                                    {
                                        if (string.IsNullOrEmpty(despatchAdvice.DeliveryCustomerParty.Party.Person.FirstName))
                                        {
                                            errorMessage = "despatchAdvice.DeliveryCustomerParty.Party.Person.FirstName can not be empty"; return false;
                                        }

                                        if (string.IsNullOrEmpty(despatchAdvice.DeliveryCustomerParty.Party.Person.FamilyName))
                                        {
                                            errorMessage = "despatchAdvice.DeliveryCustomerParty.Party.Person.FamilyName can not be empty"; return false;
                                        }
                                    }
                                    else { errorMessage = "despatchAdvice.DeliveryCustomerParty.Party.Person"; return false; }
                                }
                            }
                            else { errorMessage = "despatchAdvice.DeliveryCustomerParty.Party.PartyIdentification.ID => VKN or TCKN to be found"; return false; }
                        }
                        #endregion
                    }
                    else { errorMessage = "despatchAdvice.DeliveryCustomerParty.Party.PartyIdentification"; return false; }
                }
                else { errorMessage = "despatchAdvice.DeliveryCustomerParty.Party"; return false; }
            }
            else { errorMessage = "despatchAdvice.DeliveryCustomerParty"; return false; }
            #endregion

            #region Rule: inv:DespatchAdvice/cbc:UUID -> UUIDCheck
            if (string.IsNullOrEmpty(despatchAdvice.UUID) || (!string.IsNullOrEmpty(despatchAdvice.UUID) && (despatchAdvice.UUID.Length != Guid.NewGuid().ToString().Length || despatchAdvice.UUID.ToLower() == Guid.NewGuid().ToString().ToLower()))) { errorMessage = "invoice.UUID"; return false; }
            #endregion

            #region Rule: inv:DespatchAdvice -> UBLVersionIDCheck
            if (!string.IsNullOrEmpty(despatchAdvice.UBLVersionID))
            {
                if (despatchAdvice.UBLVersionID != UblTr2HandlerEInvoice.UblVersion)
                { errorMessage = "despatchAdvice.UBLVersionID not equal " + UblTr2HandlerEInvoice.UblVersion; return false; }
            }
            else { errorMessage = "despatchAdvice.UBLVersionID"; return false; }
            #endregion

            #region Rule: inv:DespatchAdvice -> DespatchAdviceIDCheck
            if (!string.IsNullOrEmpty(despatchAdvice.ID))
            {
                if (despatchAdvice.ID.Length != 16)
                { errorMessage = "despatchAdvice.ID must be 16 character"; return false; }
                else
                {
                    //string part1 = despatchAdvice.ID.Substring(0, 3)
                    //string part2 = despatchAdvice.ID.Substring(3, 4)
                    string part3 = despatchAdvice.ID.Substring(4, 9);

                    double oReturn = 0;
                    if (!double.TryParse(part3, out oReturn))
                    {
                        errorMessage = "despatchAdvice.ID is not numeric (ABC2014123456789)";
                        return false;
                    }

                }
            }
            else { errorMessage = "despatchAdvice.ID"; return false; }
            #endregion

            #region Rule: inv:DespatchAdvice -> DespatchAdviceIDCheck
            Regex regex = new Regex(@"^[A-Z0-9]{3}20[0-9]{2}[0-9]{9}$");
            Match match = regex.Match(despatchAdvice.ID);
            if (!match.Success) { errorMessage = "Geçersiz cbc:ID elemanı değeri. cbc:ID elemanı (ABC2009123456789) formatında olmalıdır"; return false; }
            #endregion

            #region Rule: inv:DespatchAdvice -> CopyIndicatorCheck

            if (despatchAdvice.CopyIndicator)
            { errorMessage = "despatchAdvice.CopyIndicator not equal false"; return false; }

            #endregion

            #region Rule: inv:DespatchAdvice -> TimeCheck
            if (string.IsNullOrEmpty(despatchAdvice.IssueDate))
            { errorMessage = "despatchAdvice.IssueDate"; return false; }
            else
            {
                DateTime dt;
                if (!DateTime.TryParse(despatchAdvice.IssueDate, out dt) || dt.Year < 2000)
                { errorMessage = "despatchAdvice.IssueDate"; return false; }
            }
            #endregion

            #region Rule: inv:DespatchAdvice -> DespatchAdviceTypeCodeCheck
            if (!string.IsNullOrEmpty(despatchAdvice.DespatchAdviceTypeCode))
            {
                if (DespatchAdviceTypeCode.TYPE_LIST.FirstOrDefault(x => x.GetName() == despatchAdvice.DespatchAdviceTypeCode) == null)
                { errorMessage = "despatchAdvice.DespatchAdviceTypeCode is not DespatchAdviceTypeCode"; return false; }
            }
            else { errorMessage = "despatchAdvice.DespatchAdviceTypeCode"; return false; }
            #endregion

            #region Rule: inv:DeliveredQuantity -> DeliveredQuantityCheck
            if (despatchAdvice.DespatchLines != null && despatchAdvice.DespatchLines.Count > 0)
            {
                foreach (var item in despatchAdvice.DespatchLines)
                {
                    if (string.IsNullOrEmpty(item.DeliveredQuantity.UnitCode))
                    { errorMessage = "item.DeliveredQuantity.UnitCode"; return false; }
                }
            }
            #endregion

            return true;
        }

        private DespatchAdviceModel DespatchAdviceAppendGibTag(DespatchAdviceModel despatchAdviceModel)
        {
            #region DeliveryCustomerPartyPostallAddress
            try
            {
                if (despatchAdviceModel.DeliveryCustomerParty != null && despatchAdviceModel.DeliveryCustomerParty.Party != null)
                {
                    if (despatchAdviceModel.DeliveryCustomerParty.Party.PostalAddress != null)
                    {
                        if (string.IsNullOrEmpty(despatchAdviceModel.DeliveryCustomerParty.Party.PostalAddress.StreetName))
                        {
                            despatchAdviceModel.DeliveryCustomerParty.Party.PostalAddress.StreetName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(despatchAdviceModel.DeliveryCustomerParty.Party.PostalAddress.CitySubdivisionName))
                        {
                            despatchAdviceModel.DeliveryCustomerParty.Party.PostalAddress.CitySubdivisionName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(despatchAdviceModel.DeliveryCustomerParty.Party.PostalAddress.CityName))
                        {
                            despatchAdviceModel.DeliveryCustomerParty.Party.PostalAddress.CityName = string.Empty;
                        }
                    }

                    if (despatchAdviceModel.DeliveryCustomerParty.Party.AgentParty != null && despatchAdviceModel.DeliveryCustomerParty.Party.AgentParty.PostalAddress != null)
                    {
                        if (string.IsNullOrEmpty(despatchAdviceModel.DeliveryCustomerParty.Party.AgentParty.PostalAddress.StreetName))
                        {
                            despatchAdviceModel.DeliveryCustomerParty.Party.AgentParty.PostalAddress.StreetName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(despatchAdviceModel.DeliveryCustomerParty.Party.AgentParty.PostalAddress.CitySubdivisionName))
                        {
                            despatchAdviceModel.DeliveryCustomerParty.Party.AgentParty.PostalAddress.CitySubdivisionName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(despatchAdviceModel.DeliveryCustomerParty.Party.AgentParty.PostalAddress.CityName))
                        {
                            despatchAdviceModel.DeliveryCustomerParty.Party.AgentParty.PostalAddress.CityName = string.Empty;
                        }
                    }
                }

                if (despatchAdviceModel.DespatchSupplierParty != null && despatchAdviceModel.DespatchSupplierParty.Party != null)
                {
                    if (despatchAdviceModel.DespatchSupplierParty.Party.PostalAddress != null)
                    {
                        if (string.IsNullOrEmpty(despatchAdviceModel.DespatchSupplierParty.Party.PostalAddress.StreetName))
                        {
                            despatchAdviceModel.DespatchSupplierParty.Party.PostalAddress.StreetName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(despatchAdviceModel.DespatchSupplierParty.Party.PostalAddress.CitySubdivisionName))
                        {
                            despatchAdviceModel.DespatchSupplierParty.Party.PostalAddress.CitySubdivisionName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(despatchAdviceModel.DespatchSupplierParty.Party.PostalAddress.CityName))
                        {
                            despatchAdviceModel.DespatchSupplierParty.Party.PostalAddress.CityName = string.Empty;
                        }
                    }

                    if (despatchAdviceModel.DespatchSupplierParty.Party.AgentParty != null && despatchAdviceModel.DespatchSupplierParty.Party.AgentParty.PostalAddress != null)
                    {
                        if (string.IsNullOrEmpty(despatchAdviceModel.DespatchSupplierParty.Party.AgentParty.PostalAddress.StreetName))
                        {
                            despatchAdviceModel.DespatchSupplierParty.Party.AgentParty.PostalAddress.StreetName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(despatchAdviceModel.DespatchSupplierParty.Party.AgentParty.PostalAddress.CitySubdivisionName))
                        {
                            despatchAdviceModel.DespatchSupplierParty.Party.AgentParty.PostalAddress.CitySubdivisionName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(despatchAdviceModel.DespatchSupplierParty.Party.AgentParty.PostalAddress.CityName))
                        {
                            despatchAdviceModel.DespatchSupplierParty.Party.AgentParty.PostalAddress.CityName = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
            #endregion

            #region PartyTaxScheme
            if (despatchAdviceModel.DeliveryCustomerParty != null && despatchAdviceModel.DeliveryCustomerParty.Party != null)
            {
                if (despatchAdviceModel.DeliveryCustomerParty.Party.PartyTaxScheme == null)
                {
                    despatchAdviceModel.DeliveryCustomerParty.Party.PartyTaxScheme = new PartyTaxScheme()
                    {
                        TaxScheme = new TaxScheme()
                        {
                            Name = string.Empty
                        }
                    };
                }
                else
                {
                    if (despatchAdviceModel.DeliveryCustomerParty.Party.PartyTaxScheme.TaxScheme == null)
                    {
                        despatchAdviceModel.DeliveryCustomerParty.Party.PartyTaxScheme.TaxScheme = new TaxScheme()
                        {
                            Name = string.Empty
                        };
                    }
                }
            }
            #endregion

            #region IssueDate

            if (!string.IsNullOrEmpty(despatchAdviceModel.IssueDate))
            {
                DateTime dt;
                if (DateTime.TryParse(despatchAdviceModel.IssueDate, out dt))
                { despatchAdviceModel.IssueDate = dt.ToString(DateFormats.DateTimeGIBFormatShort); }
            }

            if (despatchAdviceModel.OrderReference != null && !string.IsNullOrEmpty(despatchAdviceModel.OrderReference.IssueDate))
            {
                DateTime dt;
                if (DateTime.TryParse(despatchAdviceModel.OrderReference.IssueDate, out dt))
                { despatchAdviceModel.OrderReference.IssueDate = dt.ToString(DateFormats.DateTimeGIBFormatShort); }
            }

            if (despatchAdviceModel.AdditionalDocumentReferences != null && despatchAdviceModel.AdditionalDocumentReferences.Count > 0)
            {
                foreach (var item in despatchAdviceModel.AdditionalDocumentReferences)
                {
                    if (!string.IsNullOrEmpty(item.IssueDate))
                    {
                        DateTime dt;
                        if (DateTime.TryParse(item.IssueDate, out dt))
                        { item.IssueDate = dt.ToString(DateFormats.DateTimeGIBFormatShort); }
                    }
                }
            }
            #endregion

            return despatchAdviceModel;
        }
        #endregion
    }
}
