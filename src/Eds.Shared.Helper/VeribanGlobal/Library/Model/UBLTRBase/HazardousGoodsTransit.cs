using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    [XmlType("HazardousGoodsTransit", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class HazardousGoodsTransit
    {
        //Taşıma sırasında her hangi bir tehlikeli durum olması durumunda nasıl müdahale edileceğini anlatan kod girilebilir.
        [XmlElement("TransportEmergencyCardCode")]
        public string TransportEmergencyCardCode { get; set; }

        //Paketleme kriterleri kodu girilir.
        [XmlElement("PackagingCriteriaCode")]
        public string PackagingCriteriaCode { get; set; }

        //Ürünün taşımasına yönelik kanun ve kuralları belirten kod girilir.
        [XmlElement("HazardousRegulationCode")]
        public string HazardousRegulationCode { get; set; }

        //ABD Ulaştırma Bakanlığı tarafından belirlenen Tehlikeli Maddeler için Soluma Toksisitesi Tehlike Bölgesini belirten kod girilir.
        [XmlElement("InhalationToxicityZoneCode")]
        public string InhalationToxicityZoneCode { get; set; }

        //Tehlikeli kargonun taşınmasının yetki kodu girilir.  
        [XmlElement("TransportAuthorizationCode")]
        public string TransportAuthorizationCode { get; set; }

        //Ürünü güvenle taşınması için gerekli maximum sıcaklık girilir. Bknz.Temperature 
        [XmlElement("MaximumTemperature", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Temperature MaximumTemperature { get; set; }

        //Ürünü güvenle taşınması için gerekli minimum sıcaklık girilir. Bknz.Temperature
        [XmlElement("MinimumTemperature", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Temperature MinimumTemperature { get; set; }

    }
}
