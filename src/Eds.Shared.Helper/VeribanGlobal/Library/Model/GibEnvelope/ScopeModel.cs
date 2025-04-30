using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibEnvelope
{
    [XmlType("Scope")]
    public class ScopeModel
    {
        [XmlElement("Type")]
        public string Type { get; set; }

        [XmlElement("InstanceIdentifier")]
        public string InstanceIdentifier { get; set; }

        [XmlElement("Identifier")]
        public string Identifier { get; set; }

        [XmlElement("ScopeInformation")]
        public object[] ScopeInformation { get; set; }
    }
}
