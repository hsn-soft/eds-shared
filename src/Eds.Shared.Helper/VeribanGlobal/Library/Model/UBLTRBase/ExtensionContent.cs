using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // XAdES formatında mali mühür/elektronik imza bilgileri yazılır.
    [XmlType("ExtensionContent")]
    public class ExtensionContent
    {
        // Mali mühür/elektronik imza bilgileri XAdES formatında(http://www.w3.org/2000/09/xmldsig#Signature:)
        // yazılacatır. Bknz: http://uri.etsi.org/01903/v1.3.2/XAdES.xsd, http://www.w3.org/TR/XAdES/
        [XmlElement("Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public virtual XAdES Signature { get; set; }
    }
}
