using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    [XmlType("PartyIdentification", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class PartyIdentification
    {
        [XmlElement("ID")]
        public CombineId ID { get; set; }
    }
}
