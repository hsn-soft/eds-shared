namespace Eds.Shared.Helper.eInvoice.Library.Model.Utils
{
    public class EInvoiceTaxDetailInfoModel
    {
        public decimal LineExtensionAmount { get; set; }
        public decimal AllowanceTotalAmount { get; set; }
        public decimal TaxExclusiveAmount { get; set; }
        public decimal KDV0M { get; set; }
        public decimal KDV0 { get; set; }
        public decimal KDV1M { get; set; }
        public decimal KDV1 { get; set; }
        public decimal KDV8M { get; set; }
        public decimal KDV8 { get; set; }
        public decimal KDV18M { get; set; }
        public decimal KDV18 { get; set; }
        public decimal OTV { get; set; }
        public decimal TaxTotalAmount { get; set; }
        public decimal PayableAmount { get; set; }
        public string CurrencyCode { get; set; }

        public EInvoiceTaxDetailInfoModel(CreditNoteModel dataModel)
        {
            try
            {
                if (dataModel != null && dataModel.LegalMonetaryTotal != null)
                {
                    this.LineExtensionAmount = dataModel.LegalMonetaryTotal.LineExtensionAmount != null ? dataModel.LegalMonetaryTotal.LineExtensionAmount.Value : 0;

                    this.AllowanceTotalAmount = dataModel.LegalMonetaryTotal.AllowanceTotalAmount != null ? dataModel.LegalMonetaryTotal.AllowanceTotalAmount.Value : 0;

                    this.TaxExclusiveAmount = this.LineExtensionAmount - this.AllowanceTotalAmount;

                    this.TaxTotalAmount = dataModel.LegalMonetaryTotal.TaxInclusiveAmount != null && dataModel.LegalMonetaryTotal.TaxExclusiveAmount != null
                        ? dataModel.LegalMonetaryTotal.TaxInclusiveAmount.Value - dataModel.LegalMonetaryTotal.TaxExclusiveAmount.Value : 0;

                    if (dataModel.TaxTotals != null && dataModel.TaxTotals.Count > 0)
                    {
                        //genel vergiler
                        var taxTotal = dataModel.TaxTotals[0];
                        if (this.TaxTotalAmount == 0)
                        {
                            this.TaxTotalAmount = taxTotal.TaxAmount != null ? taxTotal.TaxAmount.Value : 0;
                        }

                        var KDVTaxList = taxTotal.TaxSubtotals.Where
                        (
                            x => x.TaxAmount != null
                            && x.Percent != null
                            && x.TaxCategory != null
                            && x.TaxCategory.TaxScheme != null
                            && x.TaxCategory.TaxScheme.TaxTypeCode != null
                            && x.TaxCategory.TaxScheme.TaxTypeCode.Equals("0015")
                        ).ToList();
                        if (KDVTaxList != null && KDVTaxList.Count > 0)
                        {
                            this.KDV0 = 0;
                            this.KDV0M = KDVTaxList.Where(x => x.Percent == 0 && x.TaxableAmount != null).Sum(s => s.TaxableAmount.Value);

                            this.KDV1 = KDVTaxList.Where(x => x.Percent == 1).Sum(s => s.TaxAmount.Value);
                            this.KDV1M = (this.KDV1 * 100) / 1;

                            this.KDV8 = KDVTaxList.Where(x => x.Percent == 8).Sum(s => s.TaxAmount.Value);
                            this.KDV8M = (this.KDV8 * 100) / 8;

                            this.KDV18 = KDVTaxList.Where(x => x.Percent == 18).Sum(s => s.TaxAmount.Value);
                            this.KDV18M = (this.KDV18 * 100) / 18;
                        }
                        else
                        {
                            this.KDV0 = 0;
                            this.KDV0M = 0;

                            this.KDV1 = 0;
                            this.KDV1M = 0;

                            this.KDV8 = 0;
                            this.KDV8M = 0;

                            this.KDV18 = 0;
                            this.KDV18M = 0;
                        }

                        var OTVTaxList = taxTotal.TaxSubtotals.Where
                        (
                            x => x.TaxAmount != null
                            && x.TaxCategory != null
                            && x.TaxCategory.TaxScheme != null
                            && x.TaxCategory.TaxScheme.TaxTypeCode != null
                            &&
                            (
                                x.TaxCategory.TaxScheme.TaxTypeCode.Equals("0071")      //OTV 1. LISTE
                                || x.TaxCategory.TaxScheme.TaxTypeCode.Equals("0073")   //OTV 3. LISTE
                                || x.TaxCategory.TaxScheme.TaxTypeCode.Equals("0074")   //OTV 4. LISTE
                                || x.TaxCategory.TaxScheme.TaxTypeCode.Equals("0075")   //OTV 3A LISTE
                                || x.TaxCategory.TaxScheme.TaxTypeCode.Equals("0076")   //OTV 3B LISTE
                                || x.TaxCategory.TaxScheme.TaxTypeCode.Equals("0077")   //OTV 3C LISTE
                            )
                        ).ToList();
                        if (OTVTaxList != null && OTVTaxList.Count > 0)
                        {
                            this.OTV = OTVTaxList.Sum(s => s.TaxAmount.Value);
                        }
                        else
                        {
                            this.OTV = 0;
                        }
                    }

                    this.PayableAmount = dataModel.LegalMonetaryTotal.PayableAmount != null ? dataModel.LegalMonetaryTotal.PayableAmount.Value : 0;

                    this.CurrencyCode = dataModel.DocumentCurrencyCode != null ? dataModel.DocumentCurrencyCode.Name : string.Empty;
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
        }
        public EInvoiceTaxDetailInfoModel(InvoiceModel dataModel)
        {
            try
            {
                if (dataModel != null && dataModel.LegalMonetaryTotal != null)
                {
                    this.LineExtensionAmount = dataModel.LegalMonetaryTotal.LineExtensionAmount != null ? dataModel.LegalMonetaryTotal.LineExtensionAmount.Value : 0;

                    this.AllowanceTotalAmount = dataModel.LegalMonetaryTotal.AllowanceTotalAmount != null ? dataModel.LegalMonetaryTotal.AllowanceTotalAmount.Value : 0;

                    this.TaxExclusiveAmount = this.LineExtensionAmount - this.AllowanceTotalAmount;

                    this.TaxTotalAmount = dataModel.LegalMonetaryTotal.TaxInclusiveAmount != null && dataModel.LegalMonetaryTotal.TaxExclusiveAmount != null
                        ? dataModel.LegalMonetaryTotal.TaxInclusiveAmount.Value - dataModel.LegalMonetaryTotal.TaxExclusiveAmount.Value : 0;

                    if (dataModel.TaxTotals != null && dataModel.TaxTotals.Count > 0)
                    {
                        //genel vergiler
                        var taxTotal = dataModel.TaxTotals[0];
                        if (this.TaxTotalAmount == 0)
                        {
                            this.TaxTotalAmount = taxTotal.TaxAmount != null ? taxTotal.TaxAmount.Value : 0;
                        }

                        var KDVTaxList = taxTotal.TaxSubtotals.Where
                        (
                            x => x.TaxAmount != null
                            && x.Percent != null
                            && x.TaxCategory != null
                            && x.TaxCategory.TaxScheme != null
                            && x.TaxCategory.TaxScheme.TaxTypeCode != null
                            && x.TaxCategory.TaxScheme.TaxTypeCode.Equals("0015")
                        ).ToList();
                        if (KDVTaxList != null && KDVTaxList.Count > 0)
                        {
                            this.KDV0 = 0;
                            this.KDV0M = KDVTaxList.Where(x => x.Percent == 0 && x.TaxableAmount != null).Sum(s => s.TaxableAmount.Value);

                            this.KDV1 = KDVTaxList.Where(x => x.Percent == 1).Sum(s => s.TaxAmount.Value);
                            this.KDV1M = (this.KDV1 * 100) / 1;

                            this.KDV8 = KDVTaxList.Where(x => x.Percent == 8).Sum(s => s.TaxAmount.Value);
                            this.KDV8M = (this.KDV8 * 100) / 8;

                            this.KDV18 = KDVTaxList.Where(x => x.Percent == 18).Sum(s => s.TaxAmount.Value);
                            this.KDV18M = (this.KDV18 * 100) / 18;
                        }
                        else
                        {
                            this.KDV0 = 0;
                            this.KDV0M = 0;

                            this.KDV1 = 0;
                            this.KDV1M = 0;

                            this.KDV8 = 0;
                            this.KDV8M = 0;

                            this.KDV18 = 0;
                            this.KDV18M = 0;
                        }

                        var OTVTaxList = taxTotal.TaxSubtotals.Where
                        (
                            x => x.TaxAmount != null
                            && x.TaxCategory != null
                            && x.TaxCategory.TaxScheme != null
                            && x.TaxCategory.TaxScheme.TaxTypeCode != null
                            &&
                            (
                                x.TaxCategory.TaxScheme.TaxTypeCode.Equals("0071")      //OTV 1. LISTE
                                || x.TaxCategory.TaxScheme.TaxTypeCode.Equals("0073")   //OTV 3. LISTE
                                || x.TaxCategory.TaxScheme.TaxTypeCode.Equals("0074")   //OTV 4. LISTE
                                || x.TaxCategory.TaxScheme.TaxTypeCode.Equals("0075")   //OTV 3A LISTE
                                || x.TaxCategory.TaxScheme.TaxTypeCode.Equals("0076")   //OTV 3B LISTE
                                || x.TaxCategory.TaxScheme.TaxTypeCode.Equals("0077")   //OTV 3C LISTE
                            )
                        ).ToList();
                        if (OTVTaxList != null && OTVTaxList.Count > 0)
                        {
                            this.OTV = OTVTaxList.Sum(s => s.TaxAmount.Value);
                        }
                        else
                        {
                            this.OTV = 0;
                        }
                    }

                    this.PayableAmount = dataModel.LegalMonetaryTotal.PayableAmount != null ? dataModel.LegalMonetaryTotal.PayableAmount.Value : 0;

                    this.CurrencyCode = dataModel.DocumentCurrencyCode != null ? dataModel.DocumentCurrencyCode.Name : string.Empty;
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
        }
    }
}
