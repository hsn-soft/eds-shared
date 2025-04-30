using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Kalem ile ilgili tanımlayıcı bilgilere bu elemanda yer verilecektir.
    [XmlType("LineReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class LineReference
    {
        // Kalem Numarası. Kalemin sıra numarası girilecektir.
        [XmlElement("LineID")]
        public string LineID { get; set; }

        // Referans Belge
        [XmlElement("DocumentReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual DocumentReference DocumentReference { get; set; }
    }
}
