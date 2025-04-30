using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    [XmlType("ItemIdentification", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class ItemIdentification
    {
        [XmlElement("ID")]
        public CombineId ID { get; set; }
    }
}
