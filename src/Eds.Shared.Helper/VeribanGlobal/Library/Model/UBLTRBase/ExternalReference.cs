using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Belgelerde ilişkilendirilmek istenen dokümanların referanslarının yer aldığı elemandır.
    [XmlType("ExternalReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class ExternalReference
    {
        // İlişkilendirilmek istenen dokumanın URI formatında referansını tutar. Örnek: "#12345"
        [XmlElement("URI")]
        public string URI { get; set; }
    }
}
