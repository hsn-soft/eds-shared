using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibAccount
{
    [XmlType("Receiver")]
    public class Receiver
    {
        [XmlElement("LogicalID")]
        public string LogicalId { get; set; }

        [XmlElement("ComponentID")]
        public string ComponentId { get; set; }

        [XmlElement("TaskID")]
        public string TaskId { get; set; }

        [XmlElement("ReferenceID")]
        public string ReferenceId { get; set; }

        [XmlElement("ConfirmationCode")]
        public string ConfirmationCode { get; set; }

        [XmlElement("AuthorizationID")]
        public string AuthorizationId { get; set; }
    }
}
