using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibEnvelope
{
    [XmlType("ContactInformation")]
    public class ContactInformationModel
    {
        // ContactTypeIdentifier elemanına VKN_TCKN yazılması durumunda zarfı gönderen tarafın TCKN veya VKN’si bu elemana
        // yazılacaktır. ContactTypeIdentifier elemanına UNVAN veya diğer başlıkların yazılması durumunda ise ilgili bilgi yazılacaktır.
        [XmlElement("Contact")]
        public string Contact { get; set; }

        // Bu elemana VKN_TCKN değeri mutlaka yazılmalıdır. ContactInformation elemanın tekrarlaması durumunda ek
        // bilgi olarak UNVAN ve diğer başlıklar yazılabilir.
        [XmlElement("ContactTypeIdentifier")]
        public string ContactTypeIdentifier { get; set; }

        // Zarfı gönderen tarafın e-posta adresi bu elemana yazılabilecektir.
        [XmlElement("EmailAdress")]
        public string EmailAdress { get; set; }

        // Zarfı gönderenin fax numarası yazılabilecektir.
        [XmlElement("FaxNumber")]
        public string FaxNumber { get; set; }

        // Bu elemana zarfı gönderenin telefon numarası yazılabilecektir.
        [XmlElement("TelephoneNumber")]
        public string TelephoneNumber { get; set; }
    }
}
