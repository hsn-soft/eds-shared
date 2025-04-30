using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model
{
    public partial class VeribanServiceFault
    {
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FaultCode { get; set; }

        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FaultDescription { get; set; }
    }
}