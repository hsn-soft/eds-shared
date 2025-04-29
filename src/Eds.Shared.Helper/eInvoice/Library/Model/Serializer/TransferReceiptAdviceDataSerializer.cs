using Eds.Shared.Helper.eInvoice.Library.Model.Utils;
using Eds.Shared.Helper.VeribanGlobal.Library.Common;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes.EInvoice;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Serializer;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase;

namespace Eds.Shared.Helper.eInvoice.Library.Model.Serializer
{
    public class TransferReceiptAdviceDataSerializer : BaseSerializer
    {
        public string SerializeAndGetXmlContent(ReceiptAdviceModel receiptAdviceModel, bool byPassModelControl = false, bool clearXmlDocumentHeaderTag = false)
        {
            if (receiptAdviceModel == null)
                throw new ArgumentNullException("receiptAdviceModel", "receiptAdviceModel must have a reference");

            if (!byPassModelControl)
            {
                KeyValuePair<bool, List<string>> controlData = TransferReceiptAdviceDataModelControl(receiptAdviceModel);
                if (!controlData.Key)
                {
                    throw new ArgumentException("MODEL CONTROL FAIL : " + ControlledDataHandle(controlData.Value));
                }
            }

            //ADD GIB DEFAULT DATA
            receiptAdviceModel = ReceiptAdviceAppendGibTag(receiptAdviceModel);

            //CLEAR XMLNS
            receiptAdviceModel.xmlns = (new ReceiptAdviceModel().xmlns);

            return SerializeAndGetXmlContentBase(new List<ReceiptAdviceModel>() { receiptAdviceModel }, !clearXmlDocumentHeaderTag);
        }

        public bool SerializeAndSaveXmlFile(string saveFileFullPath, ReceiptAdviceModel receiptAdviceModel, bool byPassModelControl = false)
        {
            if (receiptAdviceModel == null)
                throw new ArgumentNullException("receiptAdviceModel", "receiptAdviceModel must have a reference");

            if (!byPassModelControl)
            {
                KeyValuePair<bool, List<string>> controlData = TransferReceiptAdviceDataModelControl(receiptAdviceModel);
                if (!controlData.Key)
                {
                    throw new ArgumentException("MODEL CONTROL FAIL : " + ControlledDataHandle(controlData.Value));
                }
            }

            File.WriteAllText(saveFileFullPath, SerializeAndGetXmlContentBase(new List<ReceiptAdviceModel>() { receiptAdviceModel }, true));

            return File.Exists(saveFileFullPath);
        }

        public ReceiptAdviceModel DeserializeFromXmlFile(string receiptAdviceFileFullPath, bool byPassModelControl = false)
        {
            if (string.IsNullOrEmpty(receiptAdviceFileFullPath))
                throw new ArgumentNullException("receiptAdviceFileFullPath", "receiptAdviceFileFullPath must have a reference");

            string xmlContent = string.Empty;
            try
            {
                string content = File.ReadAllText(receiptAdviceFileFullPath, System.Text.Encoding.UTF8);

                xmlContent = SafeLoadFromXmlFile(content);
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message.ToString()); }

            if (string.IsNullOrEmpty(xmlContent))
                throw new ArgumentException("xmlContent can not read from path");

            return DeserializeFromXmlContent(xmlContent, byPassModelControl);
        }

        public ReceiptAdviceModel DeserializeFromXmlContent(string receiptAdviceXmlContent, bool byPassModelControl = false)
        {
            if (string.IsNullOrEmpty(receiptAdviceXmlContent))
                throw new ArgumentNullException("receiptAdviceXmlContent", "receiptAdviceXmlContent must have a reference");

            ReceiptAdviceModel deserializeReceiptModel = DeserializeFromXmlContentBase<ReceiptAdviceModel>(receiptAdviceXmlContent);

            //ADD DEFAULT GIB DATA
            deserializeReceiptModel = ReceiptAdviceAppendGibTag(deserializeReceiptModel);

            if (!byPassModelControl)
            {
                KeyValuePair<bool, List<string>> controlData = TransferReceiptAdviceDataModelControl(deserializeReceiptModel);
                if (!controlData.Key)
                {
                    throw new ArgumentException("MODEL CONTROL FAIL : " + ControlledDataHandle(controlData.Value));
                }
            }

            return deserializeReceiptModel;
        }

        public KeyValuePair<bool, List<string>> TransferReceiptAdviceDataModelControl(ReceiptAdviceModel receiptAdviceModel)
        {
            List<string> errorList = new List<string>();

            string errorMessage = string.Empty;
            if (!ReceiptAdviceModelControl(receiptAdviceModel, out errorMessage))
                errorList.Add(errorMessage);

            if (errorList.Count > 0)
                return new KeyValuePair<bool, List<string>>(false, errorList);
            else
                return new KeyValuePair<bool, List<string>>(true, null);
        }

        #region OLD CONTROL
        private bool ReceiptAdviceModelControl(ReceiptAdviceModel receiptAdviceModel, out string errorMessage)
        {
            errorMessage = string.Empty;
            #region Rule: inv:ReceiptAdvice/cac:DespatchSupplierParty/cac:Party/cac:PartyIdentification/cbc:ID -> PartyIdentificationSchemeIDCheck
            if (receiptAdviceModel.DespatchSupplierParty != null)
            {
                if (receiptAdviceModel.DespatchSupplierParty.Party != null)
                {
                    if (receiptAdviceModel.DespatchSupplierParty.Party.PartyIdentification != null && receiptAdviceModel.DespatchSupplierParty.Party.PartyIdentification.Count > 0)
                    {
                        List<string> schemeId = new List<string>();
                        foreach (var partIdentificationReceiptAdvice in receiptAdviceModel.DespatchSupplierParty.Party.PartyIdentification)
                        {
                            if (partIdentificationReceiptAdvice.ID != null)
                            {
                                if (!string.IsNullOrEmpty(partIdentificationReceiptAdvice.ID.SchemeId) && PartySchemeIdType.TYPE_LIST.FirstOrDefault(x => x.GetName() == partIdentificationReceiptAdvice.ID.SchemeId) != null)
                                {
                                    #region Rule: inv:ReceiptAdvice/cac:DespatchSupplierParty/cac:Party/cac:PartyIdentification -> PartyIdentificationTCKNVKNCheck
                                    if (partIdentificationReceiptAdvice.ID.SchemeId == PartySchemeIdType.VKN.ToString())
                                    {
                                        if (partIdentificationReceiptAdvice.ID.Id.Length != 10)
                                        {
                                            errorMessage = "receiptAdvice.DespatchSupplierParty.Party.PartyIdentification.ID (VKN) must be 10 character"; return false;
                                        }
                                    }
                                    else if (partIdentificationReceiptAdvice.ID.SchemeId == PartySchemeIdType.TCKN.ToString() && partIdentificationReceiptAdvice.ID.Id.Length != 11)
                                    {
                                        errorMessage = "receiptAdvice.DespatchSupplierParty.Party.PartyIdentification.ID (TCKN) must be 11 character"; return false;
                                    }
                                    #endregion
                                    //PartyIdentificationPartyNamePersonCheck için
                                    schemeId.Add(partIdentificationReceiptAdvice.ID.SchemeId);
                                }
                                else { errorMessage = "receiptAdvice.DespatchSupplierParty.Party.PartyIdentification(" + partIdentificationReceiptAdvice.ID.Id + ").SchemeId is not PartyIdentificationID"; return false; }
                            }
                            else { errorMessage = "receiptAdvice.DespatchSupplierParty.Party.PartyIdentification.ID"; return false; }
                        }

                        #region Rule: inv:ReceiptAdvice/cac:DespatchSupplierParty/cac:Party -> PartyIdentificationPartyNamePersonCheck
                        if (schemeId.Contains(PartySchemeIdType.TCKN.ToString()) && schemeId.Contains(PartySchemeIdType.VKN.ToString()))
                        {
                            errorMessage = "receiptAdvice.DespatchSupplierParty.Party.PartyIdentification.ID => VKN and TCKN can not coexist"; return false;
                        }
                        else
                        {
                            if (schemeId.Contains(PartySchemeIdType.TCKN.ToString()) || schemeId.Contains(PartySchemeIdType.VKN.ToString()))
                            {
                                if (schemeId.Contains(PartySchemeIdType.VKN.ToString()))
                                {
                                    if (receiptAdviceModel.DespatchSupplierParty.Party.PartyName != null)
                                    {
                                        if (string.IsNullOrEmpty(receiptAdviceModel.DespatchSupplierParty.Party.PartyName.Name))
                                        { errorMessage = "receiptAdvice.DespatchSupplierParty.Party.PartyName can not be empty"; return false; }
                                    }
                                    else { errorMessage = "receiptAdvice.DespatchSupplierParty.Party.PartyName"; return false; }
                                }
                                else if (schemeId.Contains(PartySchemeIdType.TCKN.ToString()))
                                {

                                    if (receiptAdviceModel.DespatchSupplierParty.Party.Person != null)
                                    {
                                        if (string.IsNullOrEmpty(receiptAdviceModel.DespatchSupplierParty.Party.Person.FirstName))
                                        {
                                            errorMessage = "receiptAdvice.DespatchSupplierParty.Party.Person.FirstName can not be empty"; return false;
                                        }

                                        if (string.IsNullOrEmpty(receiptAdviceModel.DespatchSupplierParty.Party.Person.FamilyName))
                                        {
                                            errorMessage = "receiptAdvice.DespatchSupplierParty.Party.Person.FamilyName can not be empty"; return false;
                                        }
                                    }
                                    else { errorMessage = "receiptAdvice.DespatchSupplierParty.Party.Person"; return false; }
                                }
                            }
                            else { errorMessage = "receiptAdvice.DespatchSupplierParty.Party.PartyIdentification.ID => VKN or TCKN to be found"; return false; }
                        }
                        #endregion

                    }
                    else { errorMessage = "receiptAdvice.DespatchSupplierParty.Party.PartyIdentification"; return false; }
                }
                else { errorMessage = "receiptAdvice.DespatchSupplierParty.Party"; return false; }
            }
            else { errorMessage = "receiptAdvice.DespatchSupplierParty"; return false; }
            #endregion

            #region Rule: inv:ReceiptAdvice/cac:DeliveryCustomerParty/cac:Party/cac:PartyIdentification/cbc:ID -> PartyIdentificationSchemeIDCheck
            if (receiptAdviceModel.DeliveryCustomerParty != null)
            {
                if (receiptAdviceModel.DeliveryCustomerParty.Party != null)
                {
                    if (receiptAdviceModel.DeliveryCustomerParty.Party.PartyIdentification != null && receiptAdviceModel.DeliveryCustomerParty.Party.PartyIdentification.Count > 0)
                    {
                        List<string> schemeId = new List<string>();
                        foreach (var partIdentificationRecAdvice in receiptAdviceModel.DeliveryCustomerParty.Party.PartyIdentification)
                        {
                            if (partIdentificationRecAdvice.ID != null)
                            {
                                if (!string.IsNullOrEmpty(partIdentificationRecAdvice.ID.SchemeId) && PartySchemeIdType.TYPE_LIST.FirstOrDefault(x => x.GetName() == partIdentificationRecAdvice.ID.SchemeId) != null)
                                {
                                    #region Rule: inv:ReceiptAdvice/cac:DeliveryCustomerParty/cac:Party/cac:PartyIdentification -> PartyIdentificationTCKNVKNCheck
                                    if (partIdentificationRecAdvice.ID.SchemeId == PartySchemeIdType.VKN.ToString())
                                    {
                                        if (partIdentificationRecAdvice.ID.Id == null)
                                        {
                                            errorMessage = "receiptAdvice.DeliveryCustomerParty.Party.PartyIdentification.ID (VKN) can not be null"; return false;
                                        }
                                        else
                                        {
                                            if (partIdentificationRecAdvice.ID.Id.Length != 10)
                                            {
                                                errorMessage = "receiptAdvice.DeliveryCustomerParty.Party.PartyIdentification.ID (VKN) must be 10 character"; return false;
                                            }
                                        }
                                    }
                                    else if (partIdentificationRecAdvice.ID.SchemeId == PartySchemeIdType.TCKN.ToString() && partIdentificationRecAdvice.ID.Id.Length != 11)
                                    {
                                        errorMessage = "receiptAdvice.DeliveryCustomerParty.Party.PartyIdentification.ID (TCKN) must be 11 character"; return false;
                                    }
                                    #endregion

                                    //PartyIdentificationPartyNamePersonCheck için
                                    schemeId.Add(partIdentificationRecAdvice.ID.SchemeId);
                                }
                                else { errorMessage = "receiptAdvice.DeliveryCustomerParty.Party.PartyIdentification(" + partIdentificationRecAdvice.ID.Id + ").SchemeId is not PartyIdentificationID"; return false; }

                            }
                            else { errorMessage = "receiptAdvice.DeliveryCustomerParty.Party.PartyIdentification.ID"; return false; }
                        }


                        #region Rule: inv:ReceiptAdvice/cac:DeliveryCustomerParty/cac:Party -> PartyIdentificationPartyNamePersonCheck
                        if (schemeId.Contains(PartySchemeIdType.TCKN.ToString()) && schemeId.Contains(PartySchemeIdType.VKN.ToString()))
                        {
                            errorMessage = "receiptAdvice.DeliveryCustomerParty.Party.PartyIdentification.ID => VKN and TCKN can not coexist"; return false;
                        }
                        else
                        {
                            if (schemeId.Contains(PartySchemeIdType.TCKN.ToString()) || schemeId.Contains(PartySchemeIdType.VKN.ToString()))
                            {
                                if (schemeId.Contains(PartySchemeIdType.VKN.ToString()))
                                {
                                    if (receiptAdviceModel.DeliveryCustomerParty.Party.PartyName != null)
                                    {
                                        if (string.IsNullOrEmpty(receiptAdviceModel.DeliveryCustomerParty.Party.PartyName.Name))
                                        {
                                            errorMessage = "receiptAdvice.DeliveryCustomerParty.Party.PartyName can not be empty"; return false;
                                        }
                                    }
                                    else { errorMessage = "receiptAdvice.DeliveryCustomerParty.Party.PartyName"; return false; }
                                }
                                else if (schemeId.Contains(PartySchemeIdType.TCKN.ToString()))
                                {

                                    if (receiptAdviceModel.DeliveryCustomerParty.Party.Person != null)
                                    {
                                        if (string.IsNullOrEmpty(receiptAdviceModel.DeliveryCustomerParty.Party.Person.FirstName))
                                        {
                                            errorMessage = "receiptAdvice.DeliveryCustomerParty.Party.Person.FirstName can not be empty"; return false;
                                        }

                                        if (string.IsNullOrEmpty(receiptAdviceModel.DeliveryCustomerParty.Party.Person.FamilyName))
                                        {
                                            errorMessage = "receiptAdvice.DeliveryCustomerParty.Party.Person.FamilyName can not be empty"; return false;
                                        }
                                    }
                                    else { errorMessage = "receiptAdvice.DeliveryCustomerParty.Party.Person"; return false; }
                                }
                            }
                            else { errorMessage = "receiptAdvice.DeliveryCustomerParty.Party.PartyIdentification.ID => VKN or TCKN to be found"; return false; }
                        }
                        #endregion
                    }
                    else { errorMessage = "receiptAdvice.DeliveryCustomerParty.Party.PartyIdentification"; return false; }
                }
                else { errorMessage = "receiptAdvice.DeliveryCustomerParty.Party"; return false; }
            }
            else { errorMessage = "receiptAdvice.DeliveryCustomerParty"; return false; }
            #endregion

            #region Rule: inv:ReceiptAdvice/cbc:UUID -> UUIDCheck
            if (string.IsNullOrEmpty(receiptAdviceModel.UUID) || (!string.IsNullOrEmpty(receiptAdviceModel.UUID) && (receiptAdviceModel.UUID.Length != Guid.NewGuid().ToString().Length || receiptAdviceModel.UUID.ToLower() == Guid.NewGuid().ToString().ToLower()))) { errorMessage = "invoice.UUID"; return false; }
            #endregion

            #region Rule: inv:ReceiptAdvice -> UBLVersionIDCheck
            if (!string.IsNullOrEmpty(receiptAdviceModel.UBLVersionID))
            {
                if (receiptAdviceModel.UBLVersionID != UblTr2HandlerEInvoice.UblVersion)
                { errorMessage = "receiptAdvice.UBLVersionID not equal " + UblTr2HandlerEInvoice.UblVersion; return false; }
            }
            else { errorMessage = "receiptAdvice.UBLVersionID"; return false; }
            #endregion

            #region Rule: inv:ReceiptAdvice -> CustomizationIDCheck
            if (!string.IsNullOrEmpty(receiptAdviceModel.CustomizationID))
            {
                if (!(receiptAdviceModel.CustomizationID == "TR1.2.1" || receiptAdviceModel.CustomizationID == "TR1.2"))
                { errorMessage = "receiptAdvice.CustomizationID not equal TR1.2.1"; return false; }
            }
            else { errorMessage = "receiptAdvice.CustomizationID"; return false; }
            #endregion

            #region Rule: inv:ReceiptAdvice -> ReceiptAdviceIDCheck
            if (!string.IsNullOrEmpty(receiptAdviceModel.ID))
            {
                if (receiptAdviceModel.ID.Length != 16)
                { errorMessage = "receiptAdvice.ID must be 16 character"; return false; }
            }
            else { errorMessage = "receiptAdvice.ID"; return false; }
            #endregion

            #region Rule: inv:ReceiptAdvice -> ReceiptAdviceIDCheck
            //Regex regex = new Regex(@"^[A-Z0-9]{3}20[0-9]{2}[0-9]{9}$")
            //Match match = regex.Match(receiptAdvice.ID)
            // (!match.Success)  errorMessage = "Geçersiz cbc:ID elemanı değeri. cbc:ID elemanı (ABC2009123456789) formatında olmalıdır"; return false
            #endregion

            #region Rule: inv:ReceiptAdvice -> CopyIndicatorCheck

            if (receiptAdviceModel.CopyIndicator)
            { errorMessage = "receiptAdvice.CopyIndicator not equal false"; return false; }

            #endregion

            #region Rule: inv:ReceiptAdvice -> TimeCheck
            if (string.IsNullOrEmpty(receiptAdviceModel.IssueDate))
            { errorMessage = "receiptAdvice.IssueDate"; return false; }
            else
            {
                DateTime dt;
                if (!DateTime.TryParse(receiptAdviceModel.IssueDate, out dt) || dt.Year < 2000)
                { errorMessage = "receiptAdvice.IssueDate"; return false; }
            }
            #endregion

            #region Rule: inv:ReceiptAdvice -> ReceiptAdviceTypeCodeCheck
            if (!string.IsNullOrEmpty(receiptAdviceModel.ReceiptAdviceTypeCode))
            {
                if (ReceiptAdviceTypeCode.TYPE_LIST.FirstOrDefault(x => x.GetName() == receiptAdviceModel.ReceiptAdviceTypeCode) == null)
                { errorMessage = "receiptAdvice.ReceiptAdviceTypeCode is not ReceiptAdviceTypeCode"; return false; }
            }
            else { errorMessage = "receiptAdvice.ReceiptAdviceTypeCode"; return false; }
            #endregion

            return true;
        }

        private ReceiptAdviceModel ReceiptAdviceAppendGibTag(ReceiptAdviceModel receiptAdviceModel)
        {
            #region DeliveryCustomerPartyPostallAddress
            try
            {
                if (receiptAdviceModel.DeliveryCustomerParty != null && receiptAdviceModel.DeliveryCustomerParty.Party != null)
                {
                    if (receiptAdviceModel.DeliveryCustomerParty.Party.PostalAddress != null)
                    {
                        if (string.IsNullOrEmpty(receiptAdviceModel.DeliveryCustomerParty.Party.PostalAddress.StreetName))
                        {
                            receiptAdviceModel.DeliveryCustomerParty.Party.PostalAddress.StreetName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(receiptAdviceModel.DeliveryCustomerParty.Party.PostalAddress.CitySubdivisionName))
                        {
                            receiptAdviceModel.DeliveryCustomerParty.Party.PostalAddress.CitySubdivisionName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(receiptAdviceModel.DeliveryCustomerParty.Party.PostalAddress.CityName))
                        {
                            receiptAdviceModel.DeliveryCustomerParty.Party.PostalAddress.CityName = string.Empty;
                        }
                    }

                    if (receiptAdviceModel.DeliveryCustomerParty.Party.AgentParty != null && receiptAdviceModel.DeliveryCustomerParty.Party.AgentParty.PostalAddress != null)
                    {
                        if (string.IsNullOrEmpty(receiptAdviceModel.DeliveryCustomerParty.Party.AgentParty.PostalAddress.StreetName))
                        {
                            receiptAdviceModel.DeliveryCustomerParty.Party.AgentParty.PostalAddress.StreetName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(receiptAdviceModel.DeliveryCustomerParty.Party.AgentParty.PostalAddress.CitySubdivisionName))
                        {
                            receiptAdviceModel.DeliveryCustomerParty.Party.AgentParty.PostalAddress.CitySubdivisionName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(receiptAdviceModel.DeliveryCustomerParty.Party.AgentParty.PostalAddress.CityName))
                        {
                            receiptAdviceModel.DeliveryCustomerParty.Party.AgentParty.PostalAddress.CityName = string.Empty;
                        }
                    }
                }

                if (receiptAdviceModel.DespatchSupplierParty != null && receiptAdviceModel.DespatchSupplierParty.Party != null)
                {
                    if (receiptAdviceModel.DespatchSupplierParty.Party.PostalAddress != null)
                    {
                        if (string.IsNullOrEmpty(receiptAdviceModel.DespatchSupplierParty.Party.PostalAddress.StreetName))
                        {
                            receiptAdviceModel.DespatchSupplierParty.Party.PostalAddress.StreetName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(receiptAdviceModel.DespatchSupplierParty.Party.PostalAddress.CitySubdivisionName))
                        {
                            receiptAdviceModel.DespatchSupplierParty.Party.PostalAddress.CitySubdivisionName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(receiptAdviceModel.DespatchSupplierParty.Party.PostalAddress.CityName))
                        {
                            receiptAdviceModel.DespatchSupplierParty.Party.PostalAddress.CityName = string.Empty;
                        }
                    }

                    if (receiptAdviceModel.DespatchSupplierParty.Party.AgentParty != null && receiptAdviceModel.DespatchSupplierParty.Party.AgentParty.PostalAddress != null)
                    {
                        if (string.IsNullOrEmpty(receiptAdviceModel.DespatchSupplierParty.Party.AgentParty.PostalAddress.StreetName))
                        {
                            receiptAdviceModel.DespatchSupplierParty.Party.AgentParty.PostalAddress.StreetName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(receiptAdviceModel.DespatchSupplierParty.Party.AgentParty.PostalAddress.CitySubdivisionName))
                        {
                            receiptAdviceModel.DespatchSupplierParty.Party.AgentParty.PostalAddress.CitySubdivisionName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(receiptAdviceModel.DespatchSupplierParty.Party.AgentParty.PostalAddress.CityName))
                        {
                            receiptAdviceModel.DespatchSupplierParty.Party.AgentParty.PostalAddress.CityName = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
            #endregion

            #region PartyTaxScheme
            if (receiptAdviceModel.DeliveryCustomerParty != null && receiptAdviceModel.DeliveryCustomerParty.Party != null)
            {
                if (receiptAdviceModel.DeliveryCustomerParty.Party.PartyTaxScheme == null)
                {
                    receiptAdviceModel.DeliveryCustomerParty.Party.PartyTaxScheme = new PartyTaxScheme()
                    {
                        TaxScheme = new TaxScheme()
                        {
                            Name = string.Empty
                        }
                    };
                }
                else
                {
                    if (receiptAdviceModel.DeliveryCustomerParty.Party.PartyTaxScheme.TaxScheme == null)
                    {
                        receiptAdviceModel.DeliveryCustomerParty.Party.PartyTaxScheme.TaxScheme = new TaxScheme()
                        {
                            Name = string.Empty
                        };
                    }
                }
            }
            #endregion

            #region IssueDate

            if (!string.IsNullOrEmpty(receiptAdviceModel.IssueDate))
            {
                DateTime dt;
                if (DateTime.TryParse(receiptAdviceModel.IssueDate, out dt))
                { receiptAdviceModel.IssueDate = dt.ToString(DateFormats.DateTimeGIBFormatShort); }
            }

            if (receiptAdviceModel.OrderReference != null && !string.IsNullOrEmpty(receiptAdviceModel.OrderReference.IssueDate))
            {
                DateTime dt;
                if (DateTime.TryParse(receiptAdviceModel.OrderReference.IssueDate, out dt))
                { receiptAdviceModel.OrderReference.IssueDate = dt.ToString(DateFormats.DateTimeGIBFormatShort); }
            }

            if (receiptAdviceModel.AdditionalDocumentReferences != null && receiptAdviceModel.AdditionalDocumentReferences.Count > 0)
            {
                foreach (var item in receiptAdviceModel.AdditionalDocumentReferences)
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

            return receiptAdviceModel;
        }
        #endregion
    }
}
