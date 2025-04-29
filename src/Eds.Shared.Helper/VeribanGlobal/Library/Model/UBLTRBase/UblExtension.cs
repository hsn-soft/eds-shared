using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // UBL’e eklenecek genişletme alanlarını içerir. UBL-TR Fatura formatında bu alanda
    // XAdES formatında Mali mühür/elektronik imza bilgileri yazılacaktır.
    [XmlType("UBLExtension")]
    public class UblExtension
    {
        // UBL-TR Fatura formatında bu alanda XAdES formatında mali mühür/elektronik imza bilgileri yazılacaktır.
        [XmlElement("ExtensionContent")]
        public virtual ExtensionContent ExtensionContent { get; set; }
    }
}
