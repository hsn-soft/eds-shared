using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Vergi ve diğer yasal yükümlülüklerin hesaplaması ile ilgili bilgilere yer verilecektir.
    [XmlType("TaxSubtotal", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class TaxSubtotal
    {
        // Verginin üzerinden hesaplandığı tutar (matrah) bilgisi girilecektir.
        [XmlElement("TaxableAmount")]
        public virtual UblBaseCurrency TaxableAmount { get; set; }

        // Hesaplanan Vergi Tutarıdır.
        [XmlElement("TaxAmount")]
        public virtual UblBaseCurrency TaxAmount { get; set; }

        // Vergi hesaplamasında belli bir sıra izlenmesi veya birden fazla vergi hesaplaması yapılması
        // halinde ilgili sıra numarası girilecektir.
        [XmlElement("CalculationSequenceNumeric")]
        public decimal? CalculationSequenceNumeric { get; set; }
        public bool ShouldSerializeCalculationSequenceNumeric() { return CalculationSequenceNumeric.HasValue; }

        // Belge para birimi cinsinden toplam vergi tutarıdır.
        [XmlElement("TransactionCurrencyTaxAmount")]
        public virtual UblBaseCurrency TransactionCurrencyTaxAmount { get; set; }

        // Vergi oranı girilebilecektir.
        [XmlElement("Percent")]
        public decimal? Percent { get; set; }
        public bool ShouldSerializePercent() { return Percent.HasValue; }

        // Vergileme ölçüsü olarak miktar(kilogram, metre vb.) kullanılması halinde ilgili tarife bilgileri bu elemana girilecektir.
        [XmlElement("BaseUnitMeasure")]
        public virtual BaseUnit BaseUnitMeasure { get; set; }

        // Vergileme ölçüsü olarak tutar(perakende satış fiyatı gibi.) kullanılması halinde ilgili tarife bilgileri bu elemana girilecektir.
        [XmlElement("PerUnitAmount")]
        public virtual UblBaseCurrency PerUnitAmount { get; set; }

        // Verginin türü ile ilgili bilgiler girilecektir.
        [XmlElement("TaxCategory", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual TaxCategory TaxCategory { get; set; }
    }
}
