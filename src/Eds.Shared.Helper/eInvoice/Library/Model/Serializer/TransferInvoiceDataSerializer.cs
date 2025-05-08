using System.Text.RegularExpressions;
using Eds.Shared.Helper.eInvoice.Library.Model.Utils;
using Eds.Shared.Helper.VeribanGlobal.Library.Common;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.ConstRepository;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes.EInvoice;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.GlobalTypes.EInvoice;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Serializer;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase;

namespace Eds.Shared.Helper.eInvoice.Library.Model.Serializer
{
    public class TransferInvoiceDataSerializer : BaseSerializer
    {
        public string SerializeAndGetXmlContent(InvoiceModel invoiceModel, bool byPassModelControl = false, bool clearXmlDocumentHeaderTag = false)
        {
            if (invoiceModel == null)
                throw new ArgumentNullException("invoiceModel", "invoiceModel must have a reference");

            if (!byPassModelControl)
            {
                KeyValuePair<bool, List<string>> controlData = TransferInvoiceDataModelControl(invoiceModel);
                if (!controlData.Key)
                {
                    throw new ArgumentException("MODEL CONTROL FAIL : " + ControlledDataHandle(controlData.Value));
                }
            }

            //FIX DEFAULT DATA
            UblTr2HandlerEInvoice.SetHandleInvoice(ref invoiceModel);

            //ADD GIB DEFAULT DATA
            invoiceModel = InvoiceAppendGibTag(invoiceModel);

            //CLEAR XMLNS
            invoiceModel.xmlns = new InvoiceModel().xmlns;

            return SerializeAndGetXmlContentBase(new List<InvoiceModel>() { invoiceModel }, !clearXmlDocumentHeaderTag);
        }

        public bool SerializeAndSaveXmlFile(string saveFileFullPath, InvoiceModel invoiceModel, bool byPassModelControl = false)
        {
            if (invoiceModel == null)
                throw new ArgumentNullException("invoiceModel", "invoiceModel must have a reference");

            if (!byPassModelControl)
            {
                KeyValuePair<bool, List<string>> controlData = TransferInvoiceDataModelControl(invoiceModel);
                if (!controlData.Key)
                {
                    throw new ArgumentException("MODEL CONTROL FAIL : " + ControlledDataHandle(controlData.Value));
                }
            }

            File.WriteAllText(saveFileFullPath, SerializeAndGetXmlContentBase(new List<InvoiceModel>() { invoiceModel }, true));

            return File.Exists(saveFileFullPath);
        }

        public InvoiceModel DeserializeFromXmlFile(string invoiceFileFullPath, bool byPassModelControl = false)
        {
            if (string.IsNullOrEmpty(invoiceFileFullPath))
                throw new ArgumentNullException("invoiceFileFullPath", "invoiceFileFullPath must have a reference");

            string xmlContent = string.Empty;
            try
            {
                string content = File.ReadAllText(invoiceFileFullPath, System.Text.Encoding.UTF8);

                xmlContent = SafeLoadFromXmlFile(content);
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message.ToString()); }

            if (string.IsNullOrEmpty(xmlContent))
                throw new ArgumentException("xmlContent can not read from path");

            return DeserializeFromXmlContent(xmlContent, byPassModelControl);
        }

        public InvoiceModel DeserializeFromXmlContent(string invoiceXmlContent, bool byPassModelControl = false)
        {
            if (string.IsNullOrEmpty(invoiceXmlContent))
                throw new ArgumentNullException("invoiceXmlContent", "invoiceXmlContent must have a reference");

            InvoiceModel deserializeDespatchModel = DeserializeFromXmlContentBase<InvoiceModel>(invoiceXmlContent);

            //ADD DEFAULT GIB DATA
            deserializeDespatchModel = InvoiceAppendGibTag(deserializeDespatchModel);

            if (!byPassModelControl)
            {
                KeyValuePair<bool, List<string>> controlData = TransferInvoiceDataModelControl(deserializeDespatchModel);
                if (!controlData.Key)
                {
                    throw new Exception("MODEL CONTROL FAIL : " + ControlledDataHandle(controlData.Value));
                }
            }

            return deserializeDespatchModel;
        }

        public KeyValuePair<bool, List<string>> TransferInvoiceDataModelControl(InvoiceModel invoiceModel)
        {
            List<string> errorList = new List<string>();

            string errorMessage = string.Empty;
            if (!InvoiceModelControl(invoiceModel, out errorMessage))
                errorList.Add(errorMessage);

            if (errorList.Count > 0)
                return new KeyValuePair<bool, List<string>>(false, errorList);
            else
                return new KeyValuePair<bool, List<string>>(true, null);
        }

        #region OLD CONTROL
        private bool InvoiceModelControl(InvoiceModel invoiceModel, out string errorMessage)
        {
            errorMessage = string.Empty;
            foreach (InvoiceLine invoiceLine in invoiceModel.InvoiceLines)
            {
                if (invoiceLine.WithholdingTaxTotals != null && invoiceLine.TaxTotal != null && invoiceLine.TaxTotal.TaxSubtotals != null)
                {
                    foreach (TaxSubtotal taxSubTotal in invoiceLine.TaxTotal.TaxSubtotals)
                    {
                        if (taxSubTotal.TaxCategory != null && taxSubTotal.TaxCategory.TaxScheme != null && string.IsNullOrEmpty(taxSubTotal.TaxCategory.TaxScheme.TaxTypeCode) && taxSubTotal.TaxCategory.TaxScheme.TaxTypeCode == "601")
                        {
                            if (taxSubTotal.Percent != 30)
                                errorMessage = "601 numaralı Yapım işleri ile bu işlerle birlikte ifa edilen mühendislik için tevkifat oranı 20 (2/10) değil 30 (3/10) yazmanız gerekmektedir."; return false;
                        }
                    }
                }
            }

            #region Rule: inv:Invoice/cac:AdditionalDocumentReferences/cbc:DocumentTypeCode -> DocumentTypeCodeCheck
            if (invoiceModel.AdditionalDocumentReferences != null && invoiceModel.AdditionalDocumentReferences.Count > 0)
            {
                foreach (DocumentReference docRef in invoiceModel.AdditionalDocumentReferences)
                {
                    if (string.IsNullOrEmpty(docRef.DocumentTypeCode) && docRef.DocumentTypeCode == "601")
                    {
                        if (docRef.DocumentType != "30")
                            errorMessage = "601 numaralı Yapım işleri ile bu işlerle birlikte ifa edilen mühendislik için tevkifat oranı 20 (2/10) değil 30 (3/10) yazmanız gerekmektedir."; return false;
                    }
                }
            }
            #endregion

            #region Rule: inv:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PartyIdentification/cbc:ID -> PartyIdentificationSchemeIDCheck
            if (invoiceModel.AccountingSupplierParty != null)
            {
                if (invoiceModel.AccountingSupplierParty.Party != null)
                {
                    if (invoiceModel.AccountingSupplierParty.Party.PartyIdentification != null && invoiceModel.AccountingSupplierParty.Party.PartyIdentification.Count > 0)
                    {
                        List<string> schemeId = new List<string>();
                        foreach (var partIdentificationInvoice in invoiceModel.AccountingSupplierParty.Party.PartyIdentification)
                        {
                            if (partIdentificationInvoice.ID != null)
                            {
                                if (!string.IsNullOrEmpty(partIdentificationInvoice.ID.SchemeId) && PartySchemeIdType.TYPE_LIST.FirstOrDefault(x => x.GetName() == partIdentificationInvoice.ID.SchemeId) != null)
                                {
                                    #region Rule: inv:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PartyIdentification -> PartyIdentificationTCKNVKNCheck
                                    if (partIdentificationInvoice.ID.SchemeId == PartySchemeIdType.VKN.ToString())
                                    {
                                        if (partIdentificationInvoice.ID.Id.Length != 10)
                                        {
                                            errorMessage = "invoice.AccountingSupplierParty.Party.PartyIdentification.ID (VKN) must be 10 character"; return false;
                                        }
                                    }

                                    else if (partIdentificationInvoice.ID.SchemeId == PartySchemeIdType.TCKN.ToString())
                                    {
                                        if (partIdentificationInvoice.ID.Id.Length != 11)
                                        {
                                            errorMessage = "invoice.AccountingSupplierParty.Party.PartyIdentification.ID (TCKN) must be 11 character"; return false;
                                        }
                                    }
                                    else if (partIdentificationInvoice.ID.SchemeId == PartySchemeIdType.TICARETSICILNO.ToString())
                                    {
                                        if (partIdentificationInvoice.ID.Id == null)
                                        {
                                            errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyIdentification.ID (TICARETSICILNO) can not be null"; return false;
                                        }
                                    }
                                    else if (partIdentificationInvoice.ID.SchemeId == PartySchemeIdType.ALIAS.ToString())
                                    {
                                        if (partIdentificationInvoice.ID.Id == null)
                                        {
                                            errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyIdentification.ID (ALIAS) can not be null"; return false;
                                        }
                                    }
                                    else if (partIdentificationInvoice.ID.SchemeId == PartySchemeIdType.BAYINO.ToString())
                                    {
                                        if (partIdentificationInvoice.ID.Id == null)
                                        {
                                            errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyIdentification.ID (BAYINO) can not be null"; return false;
                                        }
                                    }
                                    else if (partIdentificationInvoice.ID.SchemeId == PartySchemeIdType.SUBENO.ToString())
                                    {
                                        if (partIdentificationInvoice.ID.Id == null)
                                        {
                                            errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyIdentification.ID (SUBENO) can not be null"; return false;
                                        }
                                    }
                                    else if (partIdentificationInvoice.ID.SchemeId == PartySchemeIdType.MERSISNO.ToString())
                                    {
                                        if (partIdentificationInvoice.ID.Id == null)
                                        {
                                            errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyIdentification.ID (MERSISNO) can not be null"; return false;
                                        }
                                    }
                                    else if (partIdentificationInvoice.ID.SchemeId == PartySchemeIdType.GTBGCBTESCILNO.ToString() && partIdentificationInvoice.ID.Id == null)
                                    {
                                        errorMessage = "despatchAdvice.DespatchSupplierParty.Party.PartyIdentification.ID (GTBGCBTESCILNO) can not be null"; return false;
                                    }
                                    #endregion
                                    //PartyIdentificationPartyNamePersonCheck için
                                    schemeId.Add(partIdentificationInvoice.ID.SchemeId);
                                }
                                else { errorMessage = "invoice.AccountingSupplierParty.Party.PartyIdentification(" + partIdentificationInvoice.ID.Id + ").SchemeId is not PartyIdentificationID"; return false; }
                            }
                            else { errorMessage = "invoice.AccountingSupplierParty.Party.PartyIdentification.ID"; return false; }
                        }

                        #region Rule: inv:Invoice/cac:AccountingSupplierParty/cac:Party -> PartyIdentificationPartyNamePersonCheck
                        if (schemeId.Contains(PartySchemeIdType.TCKN.ToString()) && schemeId.Contains(PartySchemeIdType.VKN.ToString()))
                        {
                            errorMessage = "invoice.AccountingSupplierParty.Party.PartyIdentification.ID => VKN and TCKN can not coexist"; return false;
                        }
                        else
                        {
                            if (schemeId.Contains(PartySchemeIdType.TCKN.ToString()) || schemeId.Contains(PartySchemeIdType.VKN.ToString()))
                            {
                                if (schemeId.Contains(PartySchemeIdType.VKN.ToString()))
                                {
                                    if (invoiceModel.AccountingSupplierParty.Party.PartyName != null)
                                    {
                                        if (string.IsNullOrEmpty(invoiceModel.AccountingSupplierParty.Party.PartyName.Name))
                                        { errorMessage = "invoice.AccountingSupplierParty.Party.PartyName can not be empty"; return false; }
                                    }
                                    else { errorMessage = "invoice.AccountingSupplierParty.Party.PartyName"; return false; }
                                }
                                else if (schemeId.Contains(PartySchemeIdType.TCKN.ToString()))
                                {

                                    if (invoiceModel.AccountingSupplierParty.Party.Person != null)
                                    {
                                        if (string.IsNullOrEmpty(invoiceModel.AccountingSupplierParty.Party.Person.FirstName))
                                        {
                                            errorMessage = "invoice.AccountingSupplierParty.Party.Person.FirstName can not be empty"; return false;
                                        }

                                        if (string.IsNullOrEmpty(invoiceModel.AccountingSupplierParty.Party.Person.FamilyName))
                                        {
                                            errorMessage = "invoice.AccountingSupplierParty.Party.Person.FamilyName can not be empty"; return false;
                                        }
                                    }
                                    else { errorMessage = "invoice.AccountingSupplierParty.Party.Person"; return false; }
                                }
                            }
                            else { errorMessage = "invoice.AccountingSupplierParty.Party.PartyIdentification.ID => VKN or TCKN to be found"; return false; }
                        }
                        #endregion

                    }
                    else { errorMessage = "invoice.AccountingSupplierParty.Party.PartyIdentification"; return false; }
                }
                else { errorMessage = "invoice.AccountingSupplierParty.Party"; return false; }
            }
            else { errorMessage = "invoice.AccountingSupplierParty"; return false; }
            #endregion

            #region Rule: inv:Invoice/cac:AccountingCustomerParty/cac:Party/cac:PartyIdentification/cbc:ID -> PartyIdentificationSchemeIDCheck
            if (invoiceModel.AccountingCustomerParty != null)
            {
                if (invoiceModel.AccountingCustomerParty.Party != null)
                {
                    if (invoiceModel.AccountingCustomerParty.Party.PartyIdentification != null && invoiceModel.AccountingCustomerParty.Party.PartyIdentification.Count > 0)
                    {
                        List<string> schemeId = new List<string>();
                        foreach (var partIdentification in invoiceModel.AccountingCustomerParty.Party.PartyIdentification)
                        {
                            if (partIdentification.ID != null)
                            {
                                if (!string.IsNullOrEmpty(partIdentification.ID.SchemeId) && PartySchemeIdType.TYPE_LIST.FirstOrDefault(x => x.GetName() == partIdentification.ID.SchemeId) != null)
                                {
                                    #region Rule: inv:Invoice/cac:AccountingCustomerParty/cac:Party/cac:PartyIdentification -> PartyIdentificationTCKNVKNCheck
                                    if (partIdentification.ID.SchemeId == PartySchemeIdType.VKN.ToString())
                                    {
                                        if (partIdentification.ID.Id == null)
                                        {
                                            errorMessage = "invoice.AccountingCustomerParty.Party.PartyIdentification.ID (VKN) can not be null"; return false;
                                        }
                                        else
                                        {
                                            if (partIdentification.ID.Id.Length != 10)
                                            {
                                                errorMessage = "invoice.AccountingCustomerParty.Party.PartyIdentification.ID (VKN) must be 10 character"; return false;
                                            }
                                        }
                                    }
                                    else if (partIdentification.ID.SchemeId == PartySchemeIdType.TCKN.ToString() && partIdentification.ID.Id.Length != 11)
                                    {
                                        errorMessage = "invoice.AccountingCustomerParty.Party.PartyIdentification.ID (TCKN) must be 11 character"; return false;
                                    }
                                    #endregion

                                    //PartyIdentificationPartyNamePersonCheck için
                                    schemeId.Add(partIdentification.ID.SchemeId);
                                }
                                else { errorMessage = "invoice.AccountingCustomerParty.Party.PartyIdentification(" + partIdentification.ID.Id + ").SchemeId is not PartyIdentificationID"; return false; }

                            }
                            else { errorMessage = "invoice.AccountingCustomerParty.Party.PartyIdentification.ID"; return false; }
                        }


                        #region Rule: inv:Invoice/cac:AccountingCustomerParty/cac:Party -> PartyIdentificationPartyNamePersonCheck
                        if (schemeId.Contains(PartySchemeIdType.TCKN.ToString()) && schemeId.Contains(PartySchemeIdType.VKN.ToString()))
                        {
                            errorMessage = "invoice.AccountingCustomerParty.Party.PartyIdentification.ID => VKN and TCKN can not coexist"; return false;
                        }
                        else
                        {
                            if (schemeId.Contains(PartySchemeIdType.TCKN.ToString()) || schemeId.Contains(PartySchemeIdType.VKN.ToString()))
                            {
                                if (schemeId.Contains(PartySchemeIdType.VKN.ToString()))
                                {
                                    if (invoiceModel.AccountingCustomerParty.Party.PartyName != null)
                                    {
                                        if (string.IsNullOrEmpty(invoiceModel.AccountingCustomerParty.Party.PartyName.Name))
                                        {
                                            errorMessage = "invoice.AccountingCustomerParty.Party.PartyName can not be empty"; return false;
                                        }
                                    }
                                    else { errorMessage = "invoice.AccountingCustomerParty.Party.PartyName"; return false; }
                                }
                                else if (schemeId.Contains(PartySchemeIdType.TCKN.ToString()))
                                {

                                    if (invoiceModel.AccountingCustomerParty.Party.Person != null)
                                    {
                                        if (string.IsNullOrEmpty(invoiceModel.AccountingCustomerParty.Party.Person.FirstName))
                                        {
                                            errorMessage = "invoice.AccountingCustomerParty.Party.Person.FirstName can not be empty"; return false;
                                        }

                                        if (string.IsNullOrEmpty(invoiceModel.AccountingCustomerParty.Party.Person.FamilyName))
                                        {
                                            errorMessage = "invoice.AccountingCustomerParty.Party.Person.FamilyName can not be empty"; return false;
                                        }
                                    }
                                    else { errorMessage = "invoice.AccountingCustomerParty.Party.Person"; return false; }
                                }
                            }
                            else { errorMessage = "invoice.AccountingCustomerParty.Party.PartyIdentification.ID => VKN or TCKN to be found"; return false; }
                        }
                        #endregion
                    }
                    else { errorMessage = "invoice.AccountingCustomerParty.Party.PartyIdentification"; return false; }
                }
                else { errorMessage = "invoice.AccountingCustomerParty.Party"; return false; }
            }
            else { errorMessage = "invoice.AccountingCustomerParty"; return false; }
            #endregion

            #region Rule: inv:Invoice/cbc:UUID -> UUIDCheck
            if (string.IsNullOrEmpty(invoiceModel.UUID) || (!string.IsNullOrEmpty(invoiceModel.UUID) && (invoiceModel.UUID.Length != Guid.NewGuid().ToString().Length || invoiceModel.UUID.ToLower() == Guid.NewGuid().ToString().ToLower()))) { errorMessage = "invoice.UUID"; return false; }
            #endregion

            #region Rule: inv:Invoice/cac:TaxTotal/cac:TaxSubtotal/cac:TaxCategory/cac:TaxScheme/cbc:TaxTypeCode -> TaxTypeCheck
            if (invoiceModel.TaxTotals != null && invoiceModel.TaxTotals.Count > 0)
            {
                foreach (var tax in invoiceModel.TaxTotals)
                {
                    if (tax.TaxSubtotals != null && tax.TaxSubtotals.Count > 0)
                    {
                        foreach (var taxSub in tax.TaxSubtotals)
                        {
                            if (taxSub.TaxCategory != null)
                            {
                                if (taxSub.TaxCategory.TaxScheme != null)
                                {
                                    //ilker: Bu alan gib tarafında zorunlu değil bir müşteriden mail geldi o sebepten kapatıldı.
                                }
                                else { errorMessage = "invoice.TaxTotals.TabSubTotal.TaxCategory.TaxScheme"; return false; }
                            }
                            else { errorMessage = "invoice.TaxTotals.TabSubTotal.TaxCategory"; return false; }
                        }
                    }
                    else { errorMessage = "invoice.TaxTotals.TabSubTotal"; return false; }
                }
            }
            else { errorMessage = "invoice.TaxTotals"; return false; }
            #endregion

            #region Rule: inv:Invoice/cac:InvoiceLine/cac:TaxTotal/cac:TaxSubtotal/cac:TaxCategory/cac:TaxScheme/cbc:TaxTypeCode - > TaxTypeCheck
            if (invoiceModel.InvoiceLines != null && invoiceModel.InvoiceLines.Count > 0)
            {
                foreach (var iLine in invoiceModel.InvoiceLines)
                {
                    if (iLine.TaxTotal != null)
                    {
                        if (iLine.TaxTotal.TaxSubtotals != null && iLine.TaxTotal.TaxSubtotals.Count > 0)
                        {
                            foreach (var iLineSubTotal in iLine.TaxTotal.TaxSubtotals)
                            {
                                if (iLineSubTotal.TaxCategory != null)
                                {
                                    if (iLineSubTotal.TaxCategory.TaxScheme != null)
                                    {
                                        if (!string.IsNullOrEmpty(iLineSubTotal.TaxCategory.TaxScheme.TaxTypeCode) && !Tax.TaxTypeList.Contains(iLineSubTotal.TaxCategory.TaxScheme.TaxTypeCode))
                                        {
                                            errorMessage = "invoice.InvoiceLines.InvoiceLine(" + iLine.ID.Id + ").TaxTotal.TabSubTotal.TaxCategory.TaxScheme.TaxTypeCode is not TaxType"; return false;
                                        }
                                    }
                                    else { errorMessage = "invoice.InvoiceLines.InvoiceLine(" + iLine.ID.Id + ").TaxTotal.TabSubTotal.TaxCategory.TaxScheme"; return false; }
                                }
                                else { errorMessage = "invoice.InvoiceLines.InvoiceLine(" + iLine.ID.Id + ").TaxTotal.TabSubTotal.TaxCategory"; return false; }
                            }
                        }
                        else { errorMessage = "invoice.InvoiceLines.InvoiceLine(" + iLine.ID.Id + ").TaxTotal.TabSubTotal"; return false; }
                    }
                }
            }
            else { errorMessage = "invoice.InvoiceLines"; return false; }
            #endregion

            #region Rule: inv:Invoice/cac:TaxTotal/cac:TaxSubtotal -> TaxExemptionReasonCheck
            if (invoiceModel.TaxTotals != null)
            {
                foreach (var tax in invoiceModel.TaxTotals)
                {
                    if (tax.TaxSubtotals != null && tax.TaxSubtotals.Count > 0)
                    {
                        foreach (var taxSubTotal in tax.TaxSubtotals)
                        {
                            if (taxSubTotal.TaxAmount != null)
                            {
                                if (taxSubTotal.TaxAmount.Value == 0
                                    && taxSubTotal.TaxCategory.TaxScheme.TaxTypeCode == Tax.TaxType_0015.GetCode()
                                    && invoiceModel.AccountingSupplierParty != null
                                    && invoiceModel.AccountingSupplierParty.Party != null
                                    && invoiceModel.AccountingSupplierParty.Party.PartyIdentification != null
                                    && invoiceModel.AccountingSupplierParty.Party.PartyIdentification.Count > 0)
                                {
                                    foreach (var partIdentification in invoiceModel.AccountingSupplierParty.Party.PartyIdentification)
                                    {
                                        if (partIdentification.ID != null
                                            && !string.IsNullOrEmpty(partIdentification.ID.SchemeId)
                                            && PartySchemeIdType.TYPE_LIST.FirstOrDefault(x => x.GetName() == partIdentification.ID.SchemeId) != null
                                            && partIdentification.ID.SchemeId == PartySchemeIdType.VKN.ToString()
                                            && string.IsNullOrEmpty(taxSubTotal.TaxCategory.TaxExemptionReason)
                                            && InvoiceTypeCode.OZELMATRAH.GetName() != invoiceModel.InvoiceTypeCode)
                                        {
                                            errorMessage = "invoice.TaxTotals.TabSubTotal.TaxCategory.TaxExemptionReason"; return false;
                                        }
                                    }
                                }
                            }
                            else { errorMessage = "invoice.TaxTotals.TabSubTotal.TaxAmount"; return false; }
                        }
                    }
                    else { errorMessage = "invoice.TaxTotals.TabSubTotal"; return false; }
                }
            }
            else { errorMessage = "invoice.TaxTotals"; return false; }
            #endregion

            #region Rule: inv:Invoice/cac:InvoiceLine/cac:TaxTotal/cac:TaxSubtotal -> TaxExemptionReasonCheck
            if (invoiceModel.InvoiceLines != null && invoiceModel.InvoiceLines.Count > 0)
            {
                foreach (var iLine in invoiceModel.InvoiceLines)
                {
                    if (iLine.TaxTotal != null)
                    {
                        if (iLine.TaxTotal.TaxSubtotals != null && iLine.TaxTotal.TaxSubtotals.Count > 0)
                        {
                            foreach (var taxSub in iLine.TaxTotal.TaxSubtotals)
                            {
                                if (taxSub.TaxAmount != null)
                                {
                                    //
                                }
                                else { errorMessage = "invoice.InvoiceLines(" + iLine.ID + ").TaxTotals.TabSubTotal.TaxAmount"; return false; }
                            }
                        }
                        else { errorMessage = "invoice.InvoiceLines(" + iLine.ID + ").TaxTotal.TaxSubTotals"; return false; }
                    }
                }
            }
            else { errorMessage = "invoice.InvoiceLines"; return false; }
            #endregion

            #region Rule: inv:Invoice/cac:LegalMonetaryTotal/cbc:LineExtensionAmount -> decimalCheck
            if (invoiceModel.LegalMonetaryTotal != null)
            {
                if (invoiceModel.LegalMonetaryTotal.LineExtensionAmount != null && !GibCheckDecimal(invoiceModel.LegalMonetaryTotal.LineExtensionAmount))
                {
                    errorMessage = "invoice.LegalMonetaryTotal.LineExtensionAmount not valid"; return false;
                }
            }
            else { errorMessage = "invoice.LegalMonetaryTotal"; return false; }
            #endregion

            #region Rule: inv:Invoice/cac:LegalMonetaryTotal/cbc:TaxExclusiveAmount -> decimalCheck
            if (invoiceModel.LegalMonetaryTotal != null)
            {
                if (invoiceModel.LegalMonetaryTotal.TaxExclusiveAmount != null && !GibCheckDecimal(invoiceModel.LegalMonetaryTotal.TaxExclusiveAmount))
                {
                    errorMessage = "invoice.LegalMonetaryTotal.TaxExclusiveAmount not valid"; return false;
                }
            }
            else { errorMessage = "invoice.LegalMonetaryTotal"; return false; }
            #endregion

            #region Rule: inv:Invoice/cac:LegalMonetaryTotal/cbc:TaxInclusiveAmount -> decimalCheck
            if (invoiceModel.LegalMonetaryTotal != null)
            {
                if (invoiceModel.LegalMonetaryTotal.TaxInclusiveAmount != null && !GibCheckDecimal(invoiceModel.LegalMonetaryTotal.TaxInclusiveAmount))
                {
                    errorMessage = "invoice.LegalMonetaryTotal.TaxInclusiveAmount not valid"; return false;
                }
            }
            else { errorMessage = "invoice.LegalMonetaryTotal"; return false; }
            #endregion

            #region Rule: inv:Invoice/cac:LegalMonetaryTotal/cbc:AllowanceTotalAmount -> decimalCheck
            if (invoiceModel.LegalMonetaryTotal != null)
            {
                if (invoiceModel.LegalMonetaryTotal.AllowanceTotalAmount != null && !GibCheckDecimal(invoiceModel.LegalMonetaryTotal.AllowanceTotalAmount))
                {
                    errorMessage = "invoice.LegalMonetaryTotal.AllowanceTotalAmount not valid"; return false;
                }
            }
            else { errorMessage = "invoice.LegalMonetaryTotal"; return false; }
            #endregion

            #region Rule: inv:Invoice/cac:LegalMonetaryTotal/cbc:PayableAmount -> decimalCheck
            if (invoiceModel.LegalMonetaryTotal != null)
            {
                if (invoiceModel.LegalMonetaryTotal.PayableAmount != null && !GibCheckDecimal(invoiceModel.LegalMonetaryTotal.PayableAmount))
                {
                    errorMessage = "invoice.LegalMonetaryTotal.PayableAmount not valid"; return false;
                }
            }
            else { errorMessage = "invoice.LegalMonetaryTotal"; return false; }
            #endregion

            #region Rule: inv:Invoice -> UBLVersionIDCheck
            if (!string.IsNullOrEmpty(invoiceModel.UBLVersionID))
            {
                if (invoiceModel.UBLVersionID != UblTr2HandlerEInvoice.UblVersion)
                { errorMessage = "invoice.UBLVersionID not equal " + UblTr2HandlerEInvoice.UblVersion; return false; }
            }
            else { errorMessage = "invoice.UBLVersionID"; return false; }
            #endregion

            #region Rule: inv:Invoice -> CustomizationIDCheck
            if (!string.IsNullOrEmpty(invoiceModel.CustomizationID))
            {
                if (invoiceModel.CustomizationID != UblTr2HandlerEInvoice.CustomizationVersion &&
                    invoiceModel.CustomizationID != UblTr2HandlerEInvoice.CustomizationVersion2)
                {
                    errorMessage = "invoice.CustomizationID not equal " +
                        UblTr2HandlerEInvoice.CustomizationVersion + " or " + UblTr2HandlerEInvoice.CustomizationVersion2;
                    return false;
                }
            }
            else { errorMessage = "invoice.CustomizationID"; return false; }
            #endregion

            #region Rule: inv:Invoice -> ProfileIDCheck
            if (!string.IsNullOrEmpty(invoiceModel.ProfileID))
            {
                List<string> profileList = new List<string>();
                foreach (var item in VeribanGlobal.Library.Common.GIBDocumentTypes.ProfileIdType.TYPE_LIST)
                {
                    profileList.Add(item.GetName());
                }
                if (!profileList.Contains(invoiceModel.ProfileID))
                { errorMessage = "invoice.ProfileID is not ProfileIDType (" + invoiceModel.ProfileID + ")"; return false; }
            }
            else { errorMessage = "invoice.ProfileID"; return false; }
            #endregion

            #region Rule: inv:Invoice -> InvoiceIDCheck
            if (!string.IsNullOrEmpty(invoiceModel.ID))
            {
                if (invoiceModel.ID.Length != 16)
                { errorMessage = "invoice.ID must be 16 character"; return false; }
                else
                {
                    //string part1 = invoiceModel.ID.Substring(0, 3)
                    //string part2 = invoiceModel.ID.Substring(3, 4)
                    string part3 = invoiceModel.ID.Substring(4, 9);

                    double oReturn = 0;
                    if (!double.TryParse(part3, out oReturn))
                    {
                        errorMessage = "invoice.ID is not numeric (ABC2014123456789)";
                        return false;
                    }

                }
            }
            else { errorMessage = "invoice.ID"; return false; }
            #endregion

            #region Rule: inv:Invoice -> InvoiceIDCheck
            Regex regex = new Regex(@"^[A-Z0-9]{3}20[0-9]{2}[0-9]{9}$");
            Match match = regex.Match(invoiceModel.ID);
            if (!match.Success) { errorMessage = "Geçersiz cbc:ID elemanı değeri. cbc:ID elemanı (ABC2009123456789) formatında olmalıdır"; return false; }
            #endregion

            #region Rule: inv:Invoice -> CopyIndicatorCheck

            if (invoiceModel.CopyIndicator)
            { errorMessage = "invoice.CopyIndicator not equal false"; return false; }

            #endregion

            #region Rule: inv:Invoice -> TimeCheck
            if (string.IsNullOrEmpty(invoiceModel.IssueDate))
            { errorMessage = "invoice.IssueDate"; return false; }
            else
            {
                DateTime dt;
                if (!DateTime.TryParse(invoiceModel.IssueDate, out dt) || dt == null || dt.Year < 2000)
                { errorMessage = "invoice.IssueDate"; return false; }
            }
            #endregion

            #region Rule: inv:Invoice -> InvoiceTypeCodeCheck
            if (!string.IsNullOrEmpty(invoiceModel.InvoiceTypeCode))
            {
                if (InvoiceTypeCode.TYPE_LIST.FirstOrDefault(x => x.GetName() == invoiceModel.InvoiceTypeCode) == null)
                { errorMessage = "invoice.InvoiceTypeCode is not InvoiceTypeCode"; return false; }

                //InvoiceTypeCode IADE ise, ProfileID TEMELFATURA olmalıdır. 02.03.2018
                if (invoiceModel.InvoiceTypeCode.Equals(InvoiceTypeCode.IADE.ToString()) && !invoiceModel.ProfileID.Equals(ProfileIdType.TEMELFATURA.ToString()))
                {
                    errorMessage = "invoice.ProfileID must be TEMELFATURA because InvoiceTypeCode is IADE"; return false;
                }
            }
            else { errorMessage = "invoice.InvoiceTypeCode"; return false; }
            #endregion

            #region Rule: inv:Invoice -> CurrencyCodeCheck
            if (invoiceModel.DocumentCurrencyCode == null)
            {
                errorMessage = "invoice.DocumentCurrencyCode"; return false;
            }
            else
            {
                if (!CurrencyCode.GetCurrencyCodeList().Contains(invoiceModel.DocumentCurrencyCode.Name))
                {
                    errorMessage = "invoice.DocumentCurrencyCode not valid"; return false;
                }
            }
            #endregion

            #region Rule: inv:InvoiceQuantity -> InvoiceQuantityCheck
            if (invoiceModel.InvoiceLines != null && invoiceModel.InvoiceLines.Count > 0)
            {
                foreach (var item in invoiceModel.InvoiceLines)
                {
                    if (string.IsNullOrEmpty(item.InvoicedQuantity.UnitCode))
                    { errorMessage = "item.InvoicedQuantity.UnitCode"; return false; }
                }
            }
            #endregion

            #region Rule: IHRACAT
            if (invoiceModel.ProfileID.Equals(ProfileIdType.IHRACAT.ToString()))
            {
                bool ihracatPartyControl = false;
                if (invoiceModel.BuyerCustomerParty != null && invoiceModel.BuyerCustomerParty.Party != null && invoiceModel.BuyerCustomerParty.Party.PartyLegalEntities != null && invoiceModel.BuyerCustomerParty.Party.PartyLegalEntities.Count > 0)
                {
                    bool isError = false;
                    foreach (var item in invoiceModel.BuyerCustomerParty.Party.PartyLegalEntities)
                    {
                        isError = string.IsNullOrEmpty(item.RegistrationName);
                        if (isError) break;
                    }

                    ihracatPartyControl = !isError;
                }
                if (!ihracatPartyControl)
                {
                    errorMessage = "BuyerCustomerParty.PartyLegalEntities.RegistrationName error"; return false;
                }

                foreach (var line in invoiceModel.InvoiceLines)
                {
                    if (line.Deliveries != null && line.Deliveries.Count > 0)
                    {
                        foreach (var delivery in line.Deliveries)
                        {
                            //Ülke
                            if (delivery.DeliveryAddress != null && delivery.DeliveryAddress.Country != null)
                            {
                                if (string.IsNullOrEmpty(delivery.DeliveryAddress.Country.Name))
                                {
                                    errorMessage = "Line: " + line.ID.Id + ", DeliveryAddress Country Name not null"; return false;
                                }
                            }
                            else
                            {
                                errorMessage = "Line: " + line.ID.Id + ", DeliveryAddress Country not found(Satır Ülke Bilgisi)"; return false;
                            }

                            //Teslim Şartı
                            foreach (var term in delivery.DeliveryTerms)
                            {
                                bool isChecked = false;
                                if (term.ID != null && !string.IsNullOrEmpty(term.ID.SchemeId) && !string.IsNullOrEmpty(term.ID.Id))
                                {
                                    var termDetail = VeribanGlobal.Library.Common.ConstRepository.Incoterms.GetIncotermsList().AsEnumerable().FirstOrDefault(o => o.Code == term.ID.Id);
                                    if (termDetail != null)
                                    {
                                        isChecked = true;
                                    }
                                }

                                if (!isChecked)
                                {
                                    errorMessage = "Line: " + line.ID.Id + ", item.delivery.term ID or Scheme error!(Teslim Şartı)"; return false;
                                }
                            }

                            //sevkiyat
                            if (delivery.Shipment != null)
                            {
                                //GTIP Numarası
                                if (delivery.Shipment.GoodsItems != null && delivery.Shipment.GoodsItems.Count > 0)
                                {
                                    foreach (var gi in delivery.Shipment.GoodsItems)
                                    {
                                        if (!(!string.IsNullOrEmpty(gi.RequiredCustomsID) && gi.RequiredCustomsID.Length == 12))
                                        {
                                            errorMessage = "Line: " + line.ID.Id + ", Delivery Shipment GTIP Number error"; return false;
                                        }
                                    }
                                }
                                else
                                {
                                    errorMessage = "Line: " + line.ID.Id + ", Delivery Shipment GoodsItems not found"; return false;
                                }

                                //Gönderilme şekli
                                if (delivery.Shipment.ShipmentStages != null && delivery.Shipment.ShipmentStages.Count > 0)
                                {
                                    foreach (var stage in delivery.Shipment.ShipmentStages)
                                    {
                                        bool isChecked = false;
                                        if (!string.IsNullOrEmpty(stage.TransportModeCode))
                                        {
                                            int code = 0; int.TryParse(stage.TransportModeCode, out code);
                                            var transport = VeribanGlobal.Library.Common.ConstRepository.TransportModeCode.GetTransportModeCodeList().FirstOrDefault(o => o.Id == code);
                                            if (transport != null)
                                            {
                                                isChecked = true;
                                            }
                                        }

                                        if (!isChecked)
                                        {
                                            errorMessage = "Line: " + line.ID.Id + ", Delivery ShipmentStages TransportModeCode error(Gönderilme şekli)"; return false;
                                        }
                                    }
                                }

                            }
                            else
                            {
                                errorMessage = "Line: " + line.ID.Id + ", Delivery Shipment not found"; return false;
                            }
                        }
                    }
                    else
                    {
                        errorMessage = "Line: " + line.ID.Id + ", item.Deliveries not found"; return false;
                    }
                }
            }
            #endregion

            #region Rule: SGK

            foreach (var partIdentification in invoiceModel.AccountingCustomerParty.Party.PartyIdentification)
            {
                if (partIdentification.ID != null && partIdentification.ID.SchemeId == PartySchemeIdType.VKN.ToString() && partIdentification.ID.Id == "7750409379")
                {
                    if (!string.IsNullOrEmpty(invoiceModel.AccountingCost))
                    {
                        if (!invoiceModel.AccountingCost.Equals(AccountingCost.DIGER.ToString()))
                        {
                            if (invoiceModel.AccountingCost.Equals(AccountingCost.MAL_HIZMET.ToString())
                                || invoiceModel.AccountingCost.Equals(AccountingCost.ABONELIK.ToString()))
                            {
                                if (invoiceModel.AdditionalDocumentReferences != null && invoiceModel.AdditionalDocumentReferences.Count > 0)
                                {
                                    bool findDocNo = false;
                                    foreach (DocumentReference documentReference in invoiceModel.AdditionalDocumentReferences)
                                    {
                                        if (documentReference.DocumentTypeCode == "DOSYA_NO")
                                        {
                                            if (string.IsNullOrEmpty(documentReference.IssueDate))
                                            {
                                                errorMessage = "invoice.AdditionalDocumentReferences DOSYA_NO IssueDate is null"; return false;
                                            }
                                            else if (string.IsNullOrEmpty(documentReference.DocumentType))
                                            {
                                                errorMessage = "invoice.AdditionalDocumentReferences DOSYA_NO DocumentType is null"; return false;
                                            }
                                            else if (documentReference.DocumentDescriptions == null || documentReference.DocumentDescriptions.Count <= 0)
                                            {
                                                errorMessage = "invoice.AdditionalDocumentReferences DOSYA_NO DocumentDescriptions is null"; return false;
                                            }

                                            findDocNo = true;
                                            break;
                                        }
                                    }

                                    if (!findDocNo)
                                    {
                                        errorMessage = "invoice.AdditionalDocumentReferences DOSYA_NO is null"; return false;
                                    }
                                }
                                else { errorMessage = "invoice.AdditionalDocumentReferences is null"; return false; }
                            }
                            else if (invoiceModel.AccountingCost.Equals(AccountingCost.SAGLIK_MED.ToString())
                                || invoiceModel.AccountingCost.Equals(AccountingCost.SAGLIK_ECZ.ToString())
                                || invoiceModel.AccountingCost.Equals(AccountingCost.SAGLIK_HAS.ToString())
                                || invoiceModel.AccountingCost.Equals(AccountingCost.SAGLIK_OPT.ToString()))
                            {
                                if (invoiceModel.AdditionalDocumentReferences != null && invoiceModel.AdditionalDocumentReferences.Count > 0)
                                {
                                    bool findDocNo = false;
                                    bool findMukAd = false;
                                    bool findMukKod = false;

                                    foreach (DocumentReference documentReference in invoiceModel.AdditionalDocumentReferences)
                                    {
                                        if (documentReference.DocumentTypeCode == "DOSYA_NO")
                                        {
                                            if (string.IsNullOrEmpty(documentReference.IssueDate))
                                            {
                                                errorMessage = "invoice.AdditionalDocumentReferences DOSYA_NO IssueDate is null"; return false;
                                            }
                                            else if (string.IsNullOrEmpty(documentReference.DocumentType))
                                            {
                                                errorMessage = "invoice.AdditionalDocumentReferences DOSYA_NO DocumentType is null"; return false;
                                            }
                                            else if (documentReference.DocumentDescriptions == null || documentReference.DocumentDescriptions.Count <= 0)
                                            {
                                                errorMessage = "invoice.AdditionalDocumentReferences DOSYA_NO DocumentDescriptions is null"; return false;
                                            }

                                            findDocNo = true;
                                        }
                                        else if (documentReference.DocumentTypeCode == "MUKELLEF_ADI")
                                        {
                                            if (string.IsNullOrEmpty(documentReference.IssueDate))
                                            {
                                                errorMessage = "invoice.AdditionalDocumentReferences MUKELLEF_ADI IssueDate is null"; return false;
                                            }
                                            else if (string.IsNullOrEmpty(documentReference.DocumentType))
                                            {
                                                errorMessage = "invoice.AdditionalDocumentReferences MUKELLEF_ADI DocumentType is null"; return false;
                                            }
                                            else if (documentReference.DocumentDescriptions == null || documentReference.DocumentDescriptions.Count <= 0)
                                            {
                                                errorMessage = "invoice.AdditionalDocumentReferences MUKELLEF_ADI DocumentDescriptions is null"; return false;
                                            }

                                            findMukAd = true;
                                        }
                                        else if (documentReference.DocumentTypeCode == "MUKELLEF_KODU")
                                        {
                                            if (string.IsNullOrEmpty(documentReference.IssueDate))
                                            {
                                                errorMessage = "invoice.AdditionalDocumentReferences MUKELLEF_KODU IssueDate is null"; return false;
                                            }
                                            else if (string.IsNullOrEmpty(documentReference.DocumentType))
                                            {
                                                errorMessage = "invoice.AdditionalDocumentReferences MUKELLEF_KODU DocumentType is null"; return false;
                                            }
                                            else if (documentReference.DocumentDescriptions == null || documentReference.DocumentDescriptions.Count <= 0)
                                            {
                                                errorMessage = "invoice.AdditionalDocumentReferences MUKELLEF_KODU DocumentDescriptions is null"; return false;
                                            }

                                            findMukKod = true;
                                        }
                                    }

                                    if (!findDocNo)
                                    {
                                        errorMessage = "invoice.AdditionalDocumentReferences DOSYA_NO is null"; return false;
                                    }
                                    else if (!findMukAd)
                                    {
                                        errorMessage = "invoice.AdditionalDocumentReferences MUKELLEF_ADI is null"; return false;
                                    }
                                    else if (!findMukKod)
                                    {
                                        errorMessage = "invoice.AdditionalDocumentReferences MUKELLEF_KODU is null"; return false;
                                    }

                                }
                                else { errorMessage = "invoice.AdditionalDocumentReferences is null"; return false; }

                            }
                            else { errorMessage = "Sgk CompanyType is unknown"; return false; }
                            if (!invoiceModel.AccountingCost.Equals(AccountingCost.MAL_HIZMET.ToString()) && !invoiceModel.AccountingCost.Equals(AccountingCost.DIGER.ToString()))
                                if (invoiceModel.InvoicePeriod != null)
                                {
                                    if (string.IsNullOrEmpty(invoiceModel.InvoicePeriod.StartDate))
                                    {
                                        errorMessage = "invoice.InvoicePeriod StartDate is null"; return false;
                                    }
                                    else if (string.IsNullOrEmpty(invoiceModel.InvoicePeriod.EndDate))
                                    {
                                        errorMessage = "invoice.InvoicePeriod EndDate is null"; return false;
                                    }

                                }
                                else
                                { errorMessage = "invoice.InvoicePeriod is null"; return false; }
                        }
                    }
                    else { errorMessage = "invoice.AccountingCost"; return false; }
                }
            }
            #endregion

            #region Rule: YOLCUBERABERFATURA
            if (invoiceModel.ProfileID.Equals(ProfileIdType.YOLCUBERABERFATURA.ToString()))
            {
                bool TaxRepresentativePartyCheck = false;

                if (invoiceModel.TaxRepresentativeParty != null)
                {
                    if (invoiceModel.TaxRepresentativeParty.PartyIdentification != null)
                    {
                        //ARACIKURUMVKN KONTROLÜ
                        var ARACIKURUMVKN = invoiceModel.TaxRepresentativeParty.PartyIdentification.
                            Any(x =>
                                 x.ID.SchemeId == "ARACIKURUMVKN" &&
                                (x.ID.Id.Length == 10 || x.ID.Id.Length == 11) &&
                                 long.TryParse(x.ID.Id, out long res)
                            );
                        //ARACIKURUMETIKET KONTROLÜ
                        var ARACIKURUMETIKET = invoiceModel.TaxRepresentativeParty.PartyIdentification.
                             Any(x =>
                                  x.ID.SchemeId == "ARACIKURUMETIKET" &&
                                  x.ID.Id.Length > 0
                             );
                        TaxRepresentativePartyCheck = ARACIKURUMVKN && ARACIKURUMETIKET;
                    }

                    if (!TaxRepresentativePartyCheck)
                    {
                        errorMessage = "TaxRepresentativeParty.ARACIKURUMVKN/ARACIKURUMETIKET error"; return false;
                    }
                }

                bool TaxFreeNationalityIDCheck = false;
                if (invoiceModel.BuyerCustomerParty != null &&
                    invoiceModel.BuyerCustomerParty.Party != null &&
                    invoiceModel.BuyerCustomerParty.Party.PartyIdentification != null &&
                    invoiceModel.BuyerCustomerParty.Party.Person != null &&
                    invoiceModel.BuyerCustomerParty.Party.Person.NationalityID != null
                    )
                {
                    //ARACIKURUMETIKET KONTROLÜ
                    var PARTYTYPE = invoiceModel.BuyerCustomerParty.Party.PartyIdentification.
                         Any(x =>
                              x.ID.SchemeId == "PARTYTYPE" &&
                              x.ID.Id == "TAXFREE"
                         );
                    var COUNTRY = VeribanGlobal.Library.Common.ConstRepository.CountryTypeForIhracat.GetCountryList().
                        Any(x => x.CountryCode == invoiceModel.BuyerCustomerParty.Party.Person.NationalityID);
                    TaxFreeNationalityIDCheck = PARTYTYPE && COUNTRY;
                }
                if (!TaxFreeNationalityIDCheck)
                {
                    errorMessage = "cac:Party/cac:Person/cbc:NationalityID  error"; return false;
                }

                bool PassportIDCheck = false;
                if (invoiceModel.BuyerCustomerParty != null &&
                    invoiceModel.BuyerCustomerParty.Party != null &&
                    invoiceModel.BuyerCustomerParty.Party.PartyIdentification != null &&
                    invoiceModel.BuyerCustomerParty.Party.Person != null &&
                    invoiceModel.BuyerCustomerParty.Party.Person.IdentityDocumentReference != null &&
                    invoiceModel.BuyerCustomerParty.Party.Person.IdentityDocumentReference.ID != null &&
                    invoiceModel.BuyerCustomerParty.Party.Person.IdentityDocumentReference.ID.Id != null &&
                    invoiceModel.BuyerCustomerParty.Party.Person.IdentityDocumentReference.ID.Id.Length > 0
                    )
                {
                    PassportIDCheck = invoiceModel.BuyerCustomerParty.Party.PartyIdentification.
                         Any(x =>
                              x.ID.SchemeId == "PARTYTYPE" &&
                              x.ID.Id == "TAXFREE"
                         );
                }
                if (!PassportIDCheck)
                {
                    errorMessage = "cac:Party/cac:Person/cac:IdentityDocumentReference  error"; return false;
                }
            }
            #endregion

            #region Rule: KAMU
            if (invoiceModel.ProfileID.Equals(ProfileIdType.KAMU.ToString()))
            {
                #region Rule: inv:Invoice/cac:PaymentMeans/cac:PayeeFinancialAccount/cbc:ID || cbc:CurrencyCode
                var paymentMeansInfo = invoiceModel.PaymentMeans.FirstOrDefault();
                if (paymentMeansInfo != null)
                {
                    if (!paymentMeansInfo.PayeeFinancialAccount.ID.Id.IsIbanValid())
                    {
                        errorMessage = "inv:Invoice/cac:PaymentMeans/cac:PayeeFinancialAccount/cbc:ID elemanı zorunludur ve geçerli iban değeri içermelidir";
                        return false;
                    }

                    if (string.IsNullOrEmpty(paymentMeansInfo.PayeeFinancialAccount.CurrencyCode))
                    {
                        errorMessage = "inv:Invoice/cac:PaymentMeans/cac:PayeeFinancialAccount/cbc:CurrencyCode elemanı zorunludur ve boş değer içermemelidir";
                        return false;
                    }
                }
                else
                {
                    errorMessage = "inv:Invoice/cac:PaymentMeans/cac:PayeeFinancialAccount/cbc:ID elemanı zorunludur ve geçerli iban değeri içermelidir";
                    return false;
                }

                #endregion

                #region Rule: inv:invoice/cac:BuyerCustomerParty/cac:Party/cac:PartyIdentification/cbc:ID
                if (invoiceModel.BuyerCustomerParty != null)
                {
                    if (invoiceModel.BuyerCustomerParty.Party != null)
                    {
                        if (invoiceModel.BuyerCustomerParty.Party.PartyIdentification != null)
                        {
                            var partyIdentificationInfo = invoiceModel
                                 .BuyerCustomerParty
                                 .Party
                                 .PartyIdentification
                                 .FirstOrDefault(p=> p.ID.SchemeId == "VKN");
                            if (partyIdentificationInfo != null)
                            {
                                if (partyIdentificationInfo.ID.Id.Length != 10)
                                {
                                    errorMessage = "inv:Invoice/cac:BuyerCustomerParty/cac:Party/cac:PartyIdentification/cbc: ID elemanının schemeID özelliği 'VKN' olan 1 nesne içermesi zorunludur ve değeri 10 haneli olan bir sayıdan oluşmalıdır.";
                                    return false;
                                }
                            }
                            else
                            {
                                errorMessage = "inv:Invoice/cac:BuyerCustomerParty/cac:Party/cac:PartyIdentification/cbc: ID elemanının schemeID özelliği 'VKN' olan 1 nesne içermesi zorunludur ve değeri 10 haneli olan bir sayıdan oluşmalıdır.";
                                return false;
                            }

                        }
                        else
                        {
                            errorMessage = "inv:Invoice/cac:BuyerCustomerParty/cac:Party/cac:PartyIdentification elemanı zorunludur.";
                            return false;
                        }
                    }
                    else
                    {
                        errorMessage = "inv:Invoice/cac:BuyerCustomerParty/cac:Party elemanı zorunludur.";
                        return false;
                    }
                }
                else
                {
                    errorMessage = "inv:Invoice/cac:BuyerCustomerParty elemanı zorunludur.";
                    return false;
                }
                #endregion
            }
            #endregion


            return true;
        }

        private InvoiceModel InvoiceAppendGibTag(InvoiceModel invoiceModel)
        {
            #region CustomerPartyPostallAddress
            try
            {
                if (invoiceModel.AccountingCustomerParty != null && invoiceModel.AccountingCustomerParty.Party != null)
                {
                    if (invoiceModel.AccountingCustomerParty.Party.PostalAddress != null)
                    {

                        if (string.IsNullOrEmpty(invoiceModel.AccountingCustomerParty.Party.PostalAddress.StreetName))
                        {
                            invoiceModel.AccountingCustomerParty.Party.PostalAddress.StreetName = string.Empty;
                        }

                        if (string.IsNullOrEmpty(invoiceModel.AccountingCustomerParty.Party.PostalAddress.CitySubdivisionName))
                        {
                            invoiceModel.AccountingCustomerParty.Party.PostalAddress.CitySubdivisionName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(invoiceModel.AccountingCustomerParty.Party.PostalAddress.CityName))
                        {
                            invoiceModel.AccountingCustomerParty.Party.PostalAddress.CityName = string.Empty;
                        }
                    }

                    if (invoiceModel.AccountingCustomerParty.Party.AgentParty != null && invoiceModel.AccountingCustomerParty.Party.AgentParty.PostalAddress != null)
                    {
                        if (string.IsNullOrEmpty(invoiceModel.AccountingCustomerParty.Party.AgentParty.PostalAddress.StreetName))
                        {
                            invoiceModel.AccountingCustomerParty.Party.AgentParty.PostalAddress.StreetName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(invoiceModel.AccountingCustomerParty.Party.AgentParty.PostalAddress.CitySubdivisionName))
                        {
                            invoiceModel.AccountingCustomerParty.Party.AgentParty.PostalAddress.CitySubdivisionName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(invoiceModel.AccountingCustomerParty.Party.AgentParty.PostalAddress.CityName))
                        {
                            invoiceModel.AccountingCustomerParty.Party.AgentParty.PostalAddress.CityName = string.Empty;
                        }
                    }
                }

                if (invoiceModel.AccountingSupplierParty != null && invoiceModel.AccountingSupplierParty.Party != null)
                {
                    if (invoiceModel.AccountingSupplierParty.Party.PostalAddress != null)
                    {
                        if (string.IsNullOrEmpty(invoiceModel.AccountingSupplierParty.Party.PostalAddress.StreetName))
                        {
                            invoiceModel.AccountingSupplierParty.Party.PostalAddress.StreetName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(invoiceModel.AccountingSupplierParty.Party.PostalAddress.CitySubdivisionName))
                        {
                            invoiceModel.AccountingSupplierParty.Party.PostalAddress.CitySubdivisionName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(invoiceModel.AccountingSupplierParty.Party.PostalAddress.CityName))
                        {
                            invoiceModel.AccountingSupplierParty.Party.PostalAddress.CityName = string.Empty;
                        }
                    }

                    if (invoiceModel.AccountingSupplierParty.Party.AgentParty != null && invoiceModel.AccountingSupplierParty.Party.AgentParty.PostalAddress != null)
                    {
                        if (string.IsNullOrEmpty(invoiceModel.AccountingSupplierParty.Party.AgentParty.PostalAddress.StreetName))
                        {
                            invoiceModel.AccountingSupplierParty.Party.AgentParty.PostalAddress.StreetName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(invoiceModel.AccountingSupplierParty.Party.AgentParty.PostalAddress.CitySubdivisionName))
                        {
                            invoiceModel.AccountingSupplierParty.Party.AgentParty.PostalAddress.CitySubdivisionName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(invoiceModel.AccountingSupplierParty.Party.AgentParty.PostalAddress.CityName))
                        {
                            invoiceModel.AccountingSupplierParty.Party.AgentParty.PostalAddress.CityName = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
            #endregion

            #region PartyTaxScheme
            if (invoiceModel.AccountingCustomerParty != null && invoiceModel.AccountingCustomerParty.Party != null)
            {
                if (invoiceModel.AccountingCustomerParty.Party.PartyTaxScheme == null)
                {
                    invoiceModel.AccountingCustomerParty.Party.PartyTaxScheme = new PartyTaxScheme()
                    {
                        TaxScheme = new TaxScheme()
                        {
                            Name = string.Empty
                        }
                    };
                }
                else
                {
                    if (invoiceModel.AccountingCustomerParty.Party.PartyTaxScheme.TaxScheme == null)
                    {
                        invoiceModel.AccountingCustomerParty.Party.PartyTaxScheme.TaxScheme = new TaxScheme()
                        {
                            Name = string.Empty
                        };
                    }
                }
            }

            #endregion

            #region IssueDate

            if (!string.IsNullOrEmpty(invoiceModel.IssueDate))
            {
                DateTime dt;
                if (DateTime.TryParse(invoiceModel.IssueDate, out dt))
                { invoiceModel.IssueDate = dt.ToString(DateFormats.DateTimeGIBFormatShort); }
            }

            if (invoiceModel.OrderReference != null && !string.IsNullOrEmpty(invoiceModel.OrderReference.IssueDate))
            {
                DateTime dt;
                if (DateTime.TryParse(invoiceModel.OrderReference.IssueDate, out dt))
                { invoiceModel.OrderReference.IssueDate = dt.ToString(DateFormats.DateTimeGIBFormatShort); }
            }

            if (invoiceModel.DespatchDocumentReferences != null && invoiceModel.DespatchDocumentReferences.Count > 0)
            {
                foreach (var item in invoiceModel.DespatchDocumentReferences)
                {
                    if (!string.IsNullOrEmpty(item.IssueDate))
                    {
                        DateTime dt;
                        if (DateTime.TryParse(item.IssueDate, out dt))
                        { item.IssueDate = dt.ToString(DateFormats.DateTimeGIBFormatShort); }
                    }
                }
            }

            if (invoiceModel.ReceiptDocumentReferences != null && invoiceModel.ReceiptDocumentReferences.Count > 0)
            {
                foreach (var item in invoiceModel.ReceiptDocumentReferences)
                {
                    if (!string.IsNullOrEmpty(item.IssueDate))
                    {
                        DateTime dt;
                        if (DateTime.TryParse(item.IssueDate, out dt))
                        { item.IssueDate = dt.ToString(DateFormats.DateTimeGIBFormatShort); }
                    }
                }
            }

            if (invoiceModel.AdditionalDocumentReferences != null && invoiceModel.AdditionalDocumentReferences.Count > 0)
            {
                foreach (var item in invoiceModel.AdditionalDocumentReferences)
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

            if (invoiceModel.PaymentMeans != null && invoiceModel.PaymentMeans.Count > 0)
            {
                foreach (var item in invoiceModel.PaymentMeans)
                {
                    if (string.IsNullOrEmpty(item.PaymentMeansCode))
                    {
                        item.PaymentMeansCode = "ZZZ";
                    }
                    else
                    {
                        item.PaymentMeansCode = item.PaymentMeansCode.TrimStart('0');
                    }
                }
            }

            return invoiceModel;
        }
        #endregion
    }
}
