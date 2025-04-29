using Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase;

namespace Eds.Shared.Helper.eInvoice.Library.Model.Utils
{
    public static class UblTr2HandlerEInvoice
    {
        public static readonly string GeneralVersion = "1.2";//1.0
        public static readonly string UblVersion = "2.1";//2.0
        public static readonly string CustomizationVersion = "TR1.2";//TR1.0
        public static readonly string CustomizationVersion2 = "TR1.2.1";//TR1.0
        public static readonly string TlCurrency = "TL";
        public static readonly string TrlCurrency = "TRL";
        public static readonly string TryCurrency = "TRY";//TRL

        public static void SetHandleInvoice(ref InvoiceModel invoice)
        {
            try
            {
                if (invoice != null)
                {
                    invoice.UBLVersionID = UblVersion;
                    invoice.CustomizationID = CustomizationVersion;
                    if (UblVersion == "2.1")
                    {
                        invoice.schemaLocation = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2 UBL-Invoice-2.1.xsd";
                    }
                    if (UblVersion == "2.0")
                    {
                        invoice.schemaLocation = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2 UBLTR-Invoice-2.0.xsd";
                    }
                    if (invoice.DocumentCurrencyCode != null && (invoice.DocumentCurrencyCode.Name == TrlCurrency || invoice.DocumentCurrencyCode.Name == TlCurrency))
                    {
                        invoice.DocumentCurrencyCode.Name = TryCurrency;
                    }

                    //invoice.AccountingSupplierParty.Party.PostalAddress.Country
                    //Ülke Adı Türkiye değilse default TR yapısını kaldır.
                    if (invoice.AccountingSupplierParty != null
                        && invoice.AccountingSupplierParty.Party != null
                        && invoice.AccountingSupplierParty.Party.PostalAddress != null
                        && invoice.AccountingSupplierParty.Party.PostalAddress.Country != null
                        && !string.IsNullOrEmpty(invoice.AccountingSupplierParty.Party.PostalAddress.Country.Name)
                        && !invoice.AccountingSupplierParty.Party.PostalAddress.Country.Name.Equals("Türkiye")
                        && !invoice.AccountingSupplierParty.Party.PostalAddress.Country.Name.Equals("Turkey")
                        && invoice.AccountingSupplierParty.Party.PostalAddress.Country.IdentificationCode.Equals("TR"))
                    {
                        invoice.AccountingSupplierParty.Party.PostalAddress.Country.IdentificationCode = null;
                    }

                    //invoice.AccountingCustomerParty.Party.PostalAddress.Country
                    //Ülke Adı Türkiye değilse default TR yapısını kaldır.
                    if (invoice.AccountingCustomerParty != null
                        && invoice.AccountingCustomerParty.Party != null
                        && invoice.AccountingCustomerParty.Party.PostalAddress != null
                        && invoice.AccountingCustomerParty.Party.PostalAddress.Country != null
                        && !string.IsNullOrEmpty(invoice.AccountingCustomerParty.Party.PostalAddress.Country.Name)
                        && !invoice.AccountingCustomerParty.Party.PostalAddress.Country.Name.Equals("Türkiye")
                        && !invoice.AccountingCustomerParty.Party.PostalAddress.Country.Name.Equals("Turkey")
                        && invoice.AccountingCustomerParty.Party.PostalAddress.Country.IdentificationCode.Equals("TR"))
                    {
                        invoice.AccountingCustomerParty.Party.PostalAddress.Country.IdentificationCode = null;
                    }

                    //invoice.BuyerCustomerParty.Party.PostalAddress.Country
                    //Ülke Adı Türkiye değilse default TR yapısını kaldır.
                    if (invoice.BuyerCustomerParty != null
                        && invoice.BuyerCustomerParty.Party != null
                        && invoice.BuyerCustomerParty.Party.PostalAddress != null
                        && invoice.BuyerCustomerParty.Party.PostalAddress.Country != null
                        && !string.IsNullOrEmpty(invoice.BuyerCustomerParty.Party.PostalAddress.Country.Name)
                        && !invoice.BuyerCustomerParty.Party.PostalAddress.Country.Name.Equals("Türkiye")
                        && !invoice.BuyerCustomerParty.Party.PostalAddress.Country.Name.Equals("Turkey")
                        && invoice.BuyerCustomerParty.Party.PostalAddress.Country.IdentificationCode.Equals("TR"))
                    {
                        invoice.BuyerCustomerParty.Party.PostalAddress.Country.IdentificationCode = null;
                    }

                    // AllowanceCharge
                    if (invoice.AllowanceCharges != null && invoice.AllowanceCharges.Count > 0)
                    {
                        if (invoice.AllowanceCharges[0].Amount != null && !String.IsNullOrEmpty(invoice.AllowanceCharges[0].Amount.CurrencyID) && (invoice.AllowanceCharges[0].Amount.CurrencyID == TrlCurrency || invoice.AllowanceCharges[0].Amount.CurrencyID == TlCurrency))
                        {
                            invoice.AllowanceCharges[0].Amount.CurrencyID = TryCurrency;
                        }
                        if (invoice.AllowanceCharges[0].BaseAmount != null && !String.IsNullOrEmpty(invoice.AllowanceCharges[0].BaseAmount.CurrencyID) && (invoice.AllowanceCharges[0].BaseAmount.CurrencyID == TrlCurrency || invoice.AllowanceCharges[0].BaseAmount.CurrencyID == TlCurrency))
                        {
                            invoice.AllowanceCharges[0].BaseAmount.CurrencyID = TryCurrency;
                        }
                        if (invoice.AllowanceCharges[0].PerUnitAmount != null && !String.IsNullOrEmpty(invoice.AllowanceCharges[0].PerUnitAmount.CurrencyID) && (invoice.AllowanceCharges[0].PerUnitAmount.CurrencyID == TrlCurrency || invoice.AllowanceCharges[0].PerUnitAmount.CurrencyID == TlCurrency))
                        {
                            invoice.AllowanceCharges[0].PerUnitAmount.CurrencyID = TryCurrency;
                        }
                    }
                    // LegalMonetaryTotal
                    if (invoice.LegalMonetaryTotal != null)
                    {
                        if (invoice.LegalMonetaryTotal.LineExtensionAmount != null && !String.IsNullOrEmpty(invoice.LegalMonetaryTotal.LineExtensionAmount.CurrencyID) && (invoice.LegalMonetaryTotal.LineExtensionAmount.CurrencyID == TrlCurrency || invoice.LegalMonetaryTotal.LineExtensionAmount.CurrencyID == TlCurrency))
                        {
                            invoice.LegalMonetaryTotal.LineExtensionAmount.CurrencyID = TryCurrency;
                        }
                        if (invoice.LegalMonetaryTotal.TaxExclusiveAmount != null && !String.IsNullOrEmpty(invoice.LegalMonetaryTotal.TaxExclusiveAmount.CurrencyID) && (invoice.LegalMonetaryTotal.TaxExclusiveAmount.CurrencyID == TrlCurrency || invoice.LegalMonetaryTotal.TaxExclusiveAmount.CurrencyID == TlCurrency))
                        {
                            invoice.LegalMonetaryTotal.TaxExclusiveAmount.CurrencyID = TryCurrency;
                        }
                        if (invoice.LegalMonetaryTotal.TaxInclusiveAmount != null && !String.IsNullOrEmpty(invoice.LegalMonetaryTotal.TaxInclusiveAmount.CurrencyID) && (invoice.LegalMonetaryTotal.TaxInclusiveAmount.CurrencyID == TrlCurrency || invoice.LegalMonetaryTotal.TaxInclusiveAmount.CurrencyID == TlCurrency))
                        {
                            invoice.LegalMonetaryTotal.TaxInclusiveAmount.CurrencyID = TryCurrency;
                        }
                        if (invoice.LegalMonetaryTotal.AllowanceTotalAmount != null && !String.IsNullOrEmpty(invoice.LegalMonetaryTotal.AllowanceTotalAmount.CurrencyID) && (invoice.LegalMonetaryTotal.AllowanceTotalAmount.CurrencyID == TrlCurrency || invoice.LegalMonetaryTotal.AllowanceTotalAmount.CurrencyID == TlCurrency))
                        {
                            invoice.LegalMonetaryTotal.AllowanceTotalAmount.CurrencyID = TryCurrency;
                        }
                        if (invoice.LegalMonetaryTotal.ChargeTotalAmount != null && !String.IsNullOrEmpty(invoice.LegalMonetaryTotal.ChargeTotalAmount.CurrencyID) && (invoice.LegalMonetaryTotal.ChargeTotalAmount.CurrencyID == TrlCurrency || invoice.LegalMonetaryTotal.ChargeTotalAmount.CurrencyID == TlCurrency))
                        {
                            invoice.LegalMonetaryTotal.ChargeTotalAmount.CurrencyID = TryCurrency;
                        }
                        if (invoice.LegalMonetaryTotal.PayableRoundingAmount != null && !String.IsNullOrEmpty(invoice.LegalMonetaryTotal.PayableRoundingAmount.CurrencyID) && (invoice.LegalMonetaryTotal.PayableRoundingAmount.CurrencyID == TrlCurrency || invoice.LegalMonetaryTotal.PayableRoundingAmount.CurrencyID == TlCurrency))
                        {
                            invoice.LegalMonetaryTotal.PayableRoundingAmount.CurrencyID = TryCurrency;
                        }
                        if (invoice.LegalMonetaryTotal.PayableAmount != null && !String.IsNullOrEmpty(invoice.LegalMonetaryTotal.PayableAmount.CurrencyID) && (invoice.LegalMonetaryTotal.PayableAmount.CurrencyID == TrlCurrency || invoice.LegalMonetaryTotal.PayableAmount.CurrencyID == TlCurrency))
                        {
                            invoice.LegalMonetaryTotal.PayableAmount.CurrencyID = TryCurrency;
                        }
                    }
                    //PaymentMeans
                    if (invoice.PaymentMeans != null && invoice.PaymentMeans.Count > 0)
                    {
                        foreach (var PayMean in invoice.PaymentMeans)
                        {
                            if (PayMean != null && PayMean.PayeeFinancialAccount != null && !String.IsNullOrEmpty(PayMean.PayeeFinancialAccount.CurrencyCode) && (PayMean.PayeeFinancialAccount.CurrencyCode == TrlCurrency || PayMean.PayeeFinancialAccount.CurrencyCode == TlCurrency))
                            {
                                PayMean.PayeeFinancialAccount.CurrencyCode = TryCurrency;
                            }
                        }
                    }
                    //PaymentTerms
                    if (invoice.PaymentTerms != null)
                    {
                        if (invoice.PaymentTerms.Amount != null && !String.IsNullOrEmpty(invoice.PaymentTerms.Amount.CurrencyID) && (invoice.PaymentTerms.Amount.CurrencyID == TrlCurrency || invoice.PaymentTerms.Amount.CurrencyID == TlCurrency))
                        {
                            invoice.PaymentTerms.Amount.CurrencyID = TryCurrency;
                        }
                        if (invoice.PaymentTerms.PenaltyAmount != null && !String.IsNullOrEmpty(invoice.PaymentTerms.PenaltyAmount.CurrencyID) && (invoice.PaymentTerms.PenaltyAmount.CurrencyID == TrlCurrency || invoice.PaymentTerms.PenaltyAmount.CurrencyID == TlCurrency))
                        {
                            invoice.PaymentTerms.PenaltyAmount.CurrencyID = TryCurrency;
                        }
                    }
                    //PaymentAlternativeExchangeRate
                    if (invoice.PaymentAlternativeExchangeRate != null)
                    {
                        if (!String.IsNullOrEmpty(invoice.PaymentAlternativeExchangeRate.SourceCurrencyCode) && (invoice.PaymentAlternativeExchangeRate.SourceCurrencyCode == TrlCurrency || invoice.PaymentAlternativeExchangeRate.SourceCurrencyCode == TlCurrency))
                        {
                            invoice.PaymentAlternativeExchangeRate.SourceCurrencyCode = TryCurrency;
                        }
                        if (!String.IsNullOrEmpty(invoice.PaymentAlternativeExchangeRate.TargetCurrencyCode) && (invoice.PaymentAlternativeExchangeRate.TargetCurrencyCode == TrlCurrency || invoice.PaymentAlternativeExchangeRate.TargetCurrencyCode == TlCurrency))
                        {
                            invoice.PaymentAlternativeExchangeRate.TargetCurrencyCode = TryCurrency;
                        }

                    }
                    bool GurmenInvoiceTypeCode = false;
                    bool GurmenTaxExemptionReasonCode = false;
                    if (invoice.AccountingSupplierParty != null
                        && invoice.AccountingSupplierParty.Party != null
                        &&
                        (
                            (
                                invoice.AccountingSupplierParty.Party.PartyIdentification != null
                                && invoice.AccountingSupplierParty.Party.PartyIdentification.Any(PI => PI.ID != null && PI.ID.SchemeId == "4460023855")
                            )
                            ||
                            (
                                invoice.AccountingSupplierParty.Party.WebSiteURI != null
                                && invoice.AccountingSupplierParty.Party.WebSiteURI.Contains("http://www.gurmen.com.tr")
                            )
                            ||
                            (
                                invoice.AccountingSupplierParty.Party.Contact != null
                                && invoice.AccountingSupplierParty.Party.Contact.ElectronicMail != null
                                && invoice.AccountingSupplierParty.Party.Contact.ElectronicMail.Contains("muhasebe@gurmen.com.tr")
                            )
                         )
                    )
                    {
                        GurmenTaxExemptionReasonCode = true;
                    }
                    // TaxTotals
                    if (invoice.TaxTotals != null && invoice.TaxTotals.Count > 0)
                    {
                        foreach (var MainTax in invoice.TaxTotals)
                        {
                            if (MainTax != null)
                            {
                                if (MainTax.TaxAmount != null && !String.IsNullOrEmpty(MainTax.TaxAmount.CurrencyID) && (MainTax.TaxAmount.CurrencyID == TrlCurrency || MainTax.TaxAmount.CurrencyID == TlCurrency))
                                {
                                    MainTax.TaxAmount.CurrencyID = TryCurrency;
                                }
                                if (MainTax.TaxSubtotals != null && MainTax.TaxSubtotals.Count > 0)
                                {
                                    foreach (var TaxSubTotal in MainTax.TaxSubtotals)
                                    {
                                        if (TaxSubTotal != null)
                                        {
                                            // TaxExemptionReasonCode default 301 atama
                                            if (TaxSubTotal.TaxAmount != null && TaxSubTotal.TaxCategory != null && !string.IsNullOrEmpty(TaxSubTotal.TaxCategory.TaxExemptionReason) && UblVersion == "2.1")
                                            {
                                                if (GurmenTaxExemptionReasonCode)
                                                {
                                                    GurmenInvoiceTypeCode = true;
                                                    TaxSubTotal.TaxCategory.TaxExemptionReasonCode = !String.IsNullOrEmpty(TaxSubTotal.TaxCategory.TaxExemptionReasonCode) ? TaxSubTotal.TaxCategory.TaxExemptionReasonCode : "301";
                                                }
                                                else
                                                {
                                                    TaxSubTotal.TaxCategory.TaxExemptionReasonCode = !String.IsNullOrEmpty(TaxSubTotal.TaxCategory.TaxExemptionReasonCode) ? TaxSubTotal.TaxCategory.TaxExemptionReasonCode : "351";
                                                }
                                            }

                                            if (TaxSubTotal.TaxableAmount != null && !String.IsNullOrEmpty(TaxSubTotal.TaxableAmount.CurrencyID) && (TaxSubTotal.TaxableAmount.CurrencyID == TrlCurrency || TaxSubTotal.TaxableAmount.CurrencyID == TlCurrency))
                                            {
                                                TaxSubTotal.TaxableAmount.CurrencyID = TryCurrency;
                                            }
                                            if (TaxSubTotal.TaxAmount != null && !String.IsNullOrEmpty(TaxSubTotal.TaxAmount.CurrencyID) && (TaxSubTotal.TaxAmount.CurrencyID == TrlCurrency || TaxSubTotal.TaxAmount.CurrencyID == TlCurrency))
                                            {
                                                TaxSubTotal.TaxAmount.CurrencyID = TryCurrency;
                                            }
                                            if (TaxSubTotal.TransactionCurrencyTaxAmount != null && !String.IsNullOrEmpty(TaxSubTotal.TransactionCurrencyTaxAmount.CurrencyID) && (TaxSubTotal.TransactionCurrencyTaxAmount.CurrencyID == TrlCurrency || TaxSubTotal.TransactionCurrencyTaxAmount.CurrencyID == TlCurrency))
                                            {
                                                TaxSubTotal.TransactionCurrencyTaxAmount.CurrencyID = TryCurrency;
                                            }
                                            if (TaxSubTotal.PerUnitAmount != null && !String.IsNullOrEmpty(TaxSubTotal.PerUnitAmount.CurrencyID) && (TaxSubTotal.PerUnitAmount.CurrencyID == TrlCurrency || TaxSubTotal.PerUnitAmount.CurrencyID == TlCurrency))
                                            {
                                                TaxSubTotal.PerUnitAmount.CurrencyID = TryCurrency;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (GurmenInvoiceTypeCode && UblVersion == "2.1")
                    {
                        invoice.InvoiceTypeCode = InvoiceTypeCode.ISTISNA.ToString();
                    }
                    // InvoiceLine
                    if (invoice.InvoiceLines != null && invoice.InvoiceLines.Count > 0)
                    {
                        foreach (var Line in invoice.InvoiceLines)
                        {
                            if (Line != null)
                            {
                                // TaxTotal
                                if (Line.TaxTotal != null)
                                {
                                    if (Line.TaxTotal.TaxAmount != null && !String.IsNullOrEmpty(Line.TaxTotal.TaxAmount.CurrencyID) && (Line.TaxTotal.TaxAmount.CurrencyID == TrlCurrency || Line.TaxTotal.TaxAmount.CurrencyID == TlCurrency))
                                    {
                                        Line.TaxTotal.TaxAmount.CurrencyID = TryCurrency;
                                    }
                                    if (Line.TaxTotal.TaxSubtotals != null && Line.TaxTotal.TaxSubtotals.Count > 0)
                                    {
                                        foreach (var TaxSub in Line.TaxTotal.TaxSubtotals)
                                        {
                                            if (TaxSub != null)
                                            {
                                                //TaxExemptionReasonCode default 301 atama
                                                if (TaxSub.TaxAmount != null && TaxSub.TaxCategory != null && !string.IsNullOrEmpty(TaxSub.TaxCategory.TaxExemptionReason) && UblVersion == "2.1")
                                                {
                                                    if (GurmenTaxExemptionReasonCode)
                                                    {
                                                        TaxSub.TaxCategory.TaxExemptionReasonCode = !String.IsNullOrEmpty(TaxSub.TaxCategory.TaxExemptionReasonCode) ? TaxSub.TaxCategory.TaxExemptionReasonCode : "301";
                                                    }
                                                    else
                                                    {
                                                        TaxSub.TaxCategory.TaxExemptionReasonCode = !String.IsNullOrEmpty(TaxSub.TaxCategory.TaxExemptionReasonCode) ? TaxSub.TaxCategory.TaxExemptionReasonCode : "351";
                                                    }
                                                }
                                                if (TaxSub.TaxableAmount != null && !String.IsNullOrEmpty(TaxSub.TaxableAmount.CurrencyID) && (TaxSub.TaxableAmount.CurrencyID == TrlCurrency || TaxSub.TaxableAmount.CurrencyID == TlCurrency))
                                                {
                                                    TaxSub.TaxableAmount.CurrencyID = TryCurrency;
                                                }
                                                if (TaxSub.TaxAmount != null && !String.IsNullOrEmpty(TaxSub.TaxAmount.CurrencyID) && (TaxSub.TaxAmount.CurrencyID == TrlCurrency || TaxSub.TaxAmount.CurrencyID == TlCurrency))
                                                {
                                                    TaxSub.TaxAmount.CurrencyID = TryCurrency;
                                                }
                                                if (TaxSub.TransactionCurrencyTaxAmount != null && !String.IsNullOrEmpty(TaxSub.TransactionCurrencyTaxAmount.CurrencyID) && (TaxSub.TransactionCurrencyTaxAmount.CurrencyID == TrlCurrency || TaxSub.TransactionCurrencyTaxAmount.CurrencyID == TlCurrency))
                                                {
                                                    TaxSub.TransactionCurrencyTaxAmount.CurrencyID = TryCurrency;
                                                }
                                                if (TaxSub.PerUnitAmount != null && !String.IsNullOrEmpty(TaxSub.PerUnitAmount.CurrencyID) && (TaxSub.PerUnitAmount.CurrencyID == TrlCurrency || TaxSub.PerUnitAmount.CurrencyID == TlCurrency))
                                                {
                                                    TaxSub.PerUnitAmount.CurrencyID = TryCurrency;
                                                }
                                            }
                                        }
                                    }
                                }

                                if (Line.LineExtensionAmount != null && !String.IsNullOrEmpty(Line.LineExtensionAmount.CurrencyID) && (Line.LineExtensionAmount.CurrencyID == TrlCurrency || Line.LineExtensionAmount.CurrencyID == TlCurrency))
                                {
                                    Line.LineExtensionAmount.CurrencyID = TryCurrency;
                                }
                                if (Line.AllowanceCharges != null && Line.AllowanceCharges.Count > 0)
                                {
                                    foreach (AllowanceCharge AllowCharge in Line.AllowanceCharges)
                                    {
                                        if (AllowCharge != null)
                                        {
                                            if (AllowCharge.Amount != null && !String.IsNullOrEmpty(AllowCharge.Amount.CurrencyID) && (AllowCharge.Amount.CurrencyID == TrlCurrency || AllowCharge.Amount.CurrencyID == TlCurrency))
                                            {
                                                AllowCharge.Amount.CurrencyID = TryCurrency;
                                            }
                                            if (AllowCharge.BaseAmount != null && !String.IsNullOrEmpty(AllowCharge.BaseAmount.CurrencyID) && (AllowCharge.BaseAmount.CurrencyID == TrlCurrency || AllowCharge.BaseAmount.CurrencyID == TlCurrency))
                                            {
                                                AllowCharge.BaseAmount.CurrencyID = TryCurrency;
                                            }
                                            if (AllowCharge.PerUnitAmount != null && !String.IsNullOrEmpty(AllowCharge.PerUnitAmount.CurrencyID) && (AllowCharge.PerUnitAmount.CurrencyID == TrlCurrency || AllowCharge.PerUnitAmount.CurrencyID == TlCurrency))
                                            {
                                                AllowCharge.PerUnitAmount.CurrencyID = TryCurrency;
                                            }
                                        }
                                    }
                                }
                                if (Line.Price != null && Line.Price.PriceAmount != null && !String.IsNullOrEmpty(Line.Price.PriceAmount.CurrencyID) && (Line.Price.PriceAmount.CurrencyID == TrlCurrency || Line.Price.PriceAmount.CurrencyID == TlCurrency))
                                {
                                    Line.Price.PriceAmount.CurrencyID = TryCurrency;
                                }

                                //Line.Deliveries.DeliveryAddress.Country
                                //Ülke Adı ve Ülke Koduyoksa TR yazıyorum.
                                if (Line.Deliveries != null && Line.Deliveries.Count > 0)
                                {
                                    foreach (Delivery delivery in Line.Deliveries)
                                    {
                                        //delivery.DeliveryAddress.Country
                                        //Ülke Adı Türkiye değilse default TR yapısını kaldır.
                                        if (delivery.DeliveryAddress != null
                                            && delivery.DeliveryAddress.Country != null
                                            && !string.IsNullOrEmpty(delivery.DeliveryAddress.Country.Name)
                                            && !delivery.DeliveryAddress.Country.Name.Equals("Türkiye")
                                            && !delivery.DeliveryAddress.Country.Name.Equals("Turkey")
                                            && delivery.DeliveryAddress.Country.IdentificationCode.Equals("TR"))
                                        {
                                            delivery.DeliveryAddress.Country.IdentificationCode = null;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //PricingExchangeRate
                    if (invoice.PricingExchangeRate != null)
                    {
                        if (!String.IsNullOrEmpty(invoice.PricingExchangeRate.SourceCurrencyCode) && (invoice.PricingExchangeRate.SourceCurrencyCode == TrlCurrency || invoice.PricingExchangeRate.SourceCurrencyCode == TlCurrency))
                        {
                            invoice.PricingExchangeRate.SourceCurrencyCode = TryCurrency;
                        }
                        if (!String.IsNullOrEmpty(invoice.PricingExchangeRate.TargetCurrencyCode) && (invoice.PricingExchangeRate.TargetCurrencyCode == TrlCurrency || invoice.PricingExchangeRate.TargetCurrencyCode == TlCurrency))
                        {
                            invoice.PricingExchangeRate.TargetCurrencyCode = TryCurrency;
                        }
                    }
                    //TaxExchangeRate
                    if (invoice.TaxExchangeRate != null)
                    {
                        if (!String.IsNullOrEmpty(invoice.TaxExchangeRate.SourceCurrencyCode) && (invoice.TaxExchangeRate.SourceCurrencyCode == TrlCurrency || invoice.TaxExchangeRate.SourceCurrencyCode == TlCurrency))
                        {
                            invoice.TaxExchangeRate.SourceCurrencyCode = TryCurrency;
                        }
                        if (!String.IsNullOrEmpty(invoice.TaxExchangeRate.TargetCurrencyCode) && (invoice.TaxExchangeRate.TargetCurrencyCode == TrlCurrency || invoice.TaxExchangeRate.TargetCurrencyCode == TlCurrency))
                        {
                            invoice.TaxExchangeRate.TargetCurrencyCode = TryCurrency;
                        }
                    }
                    //PaymentExchangeRate
                    if (invoice.PaymentExchangeRate != null)
                    {
                        if (!String.IsNullOrEmpty(invoice.PaymentExchangeRate.SourceCurrencyCode) && (invoice.PaymentExchangeRate.SourceCurrencyCode == TrlCurrency || invoice.PaymentExchangeRate.SourceCurrencyCode == TlCurrency))
                        {
                            invoice.PaymentExchangeRate.SourceCurrencyCode = TryCurrency;
                        }
                        if (!String.IsNullOrEmpty(invoice.PaymentExchangeRate.TargetCurrencyCode) && (invoice.PaymentExchangeRate.TargetCurrencyCode == TrlCurrency || invoice.PaymentExchangeRate.TargetCurrencyCode == TlCurrency))
                        {
                            invoice.PaymentExchangeRate.TargetCurrencyCode = TryCurrency;
                        }
                    }
                    //TaxCurrencyCode
                    if (invoice.TaxCurrencyCode != null && !String.IsNullOrEmpty(invoice.TaxCurrencyCode.Name) && (invoice.TaxCurrencyCode.Name == TrlCurrency || invoice.TaxCurrencyCode.Name == TlCurrency))
                    {
                        invoice.TaxCurrencyCode.Name = TryCurrency;
                    }
                    //PricingCurrencyCode
                    if (invoice.PricingCurrencyCode != null && !String.IsNullOrEmpty(invoice.PricingCurrencyCode.Name) && (invoice.PricingCurrencyCode.Name == TrlCurrency || invoice.PricingCurrencyCode.Name == TlCurrency))
                    {
                        invoice.PricingCurrencyCode.Name = TryCurrency;
                    }
                    //PaymentCurrencyCode
                    if (invoice.PaymentCurrencyCode != null && !String.IsNullOrEmpty(invoice.PaymentCurrencyCode.Name) && (invoice.PaymentCurrencyCode.Name == TrlCurrency || invoice.PaymentCurrencyCode.Name == TlCurrency))
                    {
                        invoice.PaymentCurrencyCode.Name = TryCurrency;
                    }
                    //PaymentAlternativeCurrencyCode
                    if (invoice.PaymentAlternativeCurrencyCode != null && !String.IsNullOrEmpty(invoice.PaymentAlternativeCurrencyCode.Name) && (invoice.PaymentAlternativeCurrencyCode.Name == TrlCurrency || invoice.PaymentAlternativeCurrencyCode.Name == TlCurrency))
                    {
                        invoice.PaymentAlternativeCurrencyCode.Name = TryCurrency;
                    }
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
        }
    }
}
