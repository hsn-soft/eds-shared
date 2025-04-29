using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Yanıta ait detaylar bu elemanda gösterilecektir.
    [XmlType("Response", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class Response
    {
        // Response elemanını tekil olarak tanımlayan numaradır.
        [XmlElement("ReferenceID")]
        public string ReferenceID { get; set; }

        // YanıtKodu.
        [XmlElement("ResponseCode")]
        public string ResponseCode { get; set; }

        // Yanıt ile ilgili açıklamalar bu elemana serbest metin olarak yazılabilecektir.
        [XmlElement("Description")]
        public string Description { get; set; }
    }
}
