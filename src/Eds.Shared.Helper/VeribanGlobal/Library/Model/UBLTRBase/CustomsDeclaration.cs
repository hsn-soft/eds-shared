using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    [XmlType("CustomsDeclaration", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class CustomsDeclaration
    {
        [XmlElement("ID")]
        public CombineId ID { get; set; }

        [XmlElement("IssuerParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Party IssuerParty { get; set; }
    }
}
