using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Bir belgenin belli bir kalemine yanıt verilirken bu eleman kullanılabilecektir.
    [XmlType("LineResponse")]
    public class LineResponse
    {
        // Kalem Bilgisi
        [XmlElement("LineReference")]
        public virtual LineReference LineReference { get; set; }

        // Yanıt
        [XmlElement("Response")]
        public virtual Response Response { get; set; }
    }
}
