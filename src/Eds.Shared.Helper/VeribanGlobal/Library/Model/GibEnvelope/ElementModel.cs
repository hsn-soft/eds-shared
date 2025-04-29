using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibEnvelope
{
    [XmlType("Elements")]
    public class ElementModel
    {
        //"INVOICE","BAPR","SAPR","PUA","CUA" 
        [XmlElement("ElementType")]
        public string PackageElementType { get; set; }

        [XmlElement("ElementCount")]
        public int PackageElementCount { get; set; }

        [XmlElement("ElementList")]
        public string PackageElementLines { get; set; }
    }
}
