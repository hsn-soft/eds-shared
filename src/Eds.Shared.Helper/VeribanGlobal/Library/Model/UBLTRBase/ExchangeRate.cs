using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Kur bilgileri ve kurun tarihi girilir.
    [XmlType("ExchangeRate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class ExchangeRate
    {
        // Kaynak Para Birimi Kodu. Bknz. Kod Listeleri
        public string SourceCurrencyCode { get; set; }

        // Hedef Para Birimi Kodu. Bknz. Kod Listeleri
        public string TargetCurrencyCode { get; set; }

        // Döviz kuru girilir.
        public double CalculationRate { get; set; }

        // Kurun tarihi yıl-ay-gün şeklinde girilir.
        public string Date { get; set; }
    }
}
