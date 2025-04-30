using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Vergi ve diğer yasal yükümlülüklerin hesaplaması ile ilgili bilgiler ile belge üzerinde
    // hesaplanan toplam vergi ve yasal yükümlülük tutarı girilecektir.
    [XmlType("TaxTotal", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class TaxTotal
    {
        // İki çeşit kullanımı mevcuttur:
        //  1. “Invoice/TaxTotal”: Hesaplanan vergilerin toplam tutarı girilir. Bu alan zorunludur.
        //      • TaxAmount: Toplam vergi tutarı girilir.
        //      • TaxSubtotal: Vergi hesaplaması ile ilgili bilgilere yer verilir. Birden fazla vergi türü veya aynı vergi türü
        //      içerisinde farklı oranlarda yapılan hesaplamalarla ilgili bilgilere de bu alanda yer verilecektir.
        //  2. “Invoice/InvoiceLine/TaxTotal”: Hesaplanan vergilerin kalem bazlı hesaplanması durumunda bu alan kullanılır. Bu alan seçimlidir.

        // Kalem için hesaplanan toplam vergi tutarı girilir.
        [XmlElement("TaxAmount")]
        public UblBaseCurrency TaxAmount { get; set; }

        // Kalem bazında vergi hesaplaması söz konusu olması halinde ilgili bilgilere yer verilebilecektir.
        private List<TaxSubtotal> _taxSubtotals;
        [XmlElement("TaxSubtotal", Type = typeof(TaxSubtotal), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<TaxSubtotal> TaxSubtotals
        {
            get { return this._taxSubtotals; }
            set { if (value != null) this._taxSubtotals = value; else this._taxSubtotals = new List<TaxSubtotal>(); }
        }
    }
}
