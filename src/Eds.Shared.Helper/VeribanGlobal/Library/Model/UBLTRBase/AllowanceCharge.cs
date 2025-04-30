using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Iskonto veya artırımların tanımlandığı elemandır.
    [XmlType("AllowanceCharge", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class AllowanceCharge
    {
        // Iskonto ise “false”, artırım ise “true” girilir.
        [XmlElement("ChargeIndicator")]
        public bool? ChargeIndicator { get; set; }
        public bool ShouldSerializeChargeIndicator() { return ChargeIndicator.HasValue; }

        // Iskonto veya artırımın sebebi serbest metin olarak girilir.
        [XmlElement("AllowanceChargeReason")]
        public string AllowanceChargeReason { get; set; }

        [XmlElement("MultiplierFactorNumeric")]
        public string MultiplierFactorNumeric { get; set; }

        //Birden fazla iskonto veya fiyat artırımı kullanılması durumunda sıra numarası girilir.
        [XmlElement("SequenceNumeric")]
        public string SequenceNumeric { get; set; }

        // Iskonto veya artırım miktarı numerik girilir.
        [XmlElement("Amount")]
        public UblBaseCurrency Amount { get; set; }

        // Iskonto veya artırım oranının uygulandığı tutar girilir.
        [XmlElement("BaseAmount")]
        public UblBaseCurrency BaseAmount { get; set; }

        //Ürün adetine göre iskonto veya artırımın uygulandığı durumlarda uygulanan ürün miktarını gösterir.
        [XmlElement("PerUnitAmount")]
        public UblBaseCurrency PerUnitAmount { get; set; }
    }
}
