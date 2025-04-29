using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Her türlü alternatif iletişim kanalının tanımlanmasında kullanılacaktır.
    [XmlType("Communication", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class Communication
    {
        // Bu eleman için UN/EDIFACT 3155 İletişim Numarası Kod Listesi kullanılmalıdır.
        public string ChannelCode { get; set; }

        // Bu eleman metin olarak kanal adı için kullanılacaktır.
        public string Channel { get; set; }

        // Bu eleman iletişim adresini metin olarak tutar.
        public string Value { get; set; }
    }
}
