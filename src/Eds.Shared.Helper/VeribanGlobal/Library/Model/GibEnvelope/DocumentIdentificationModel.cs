using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibEnvelope
{
    [XmlType("DocumentIdentification")]
    public class DocumentIdentificationModel
    {
        // Zarf içerisinde kullanılan doküman standartını belirtir. Belirtilen standarta göre, mesajın içerisinde kullanılan etiketlerin
        // doğruluğunun kontrol edilebilmesini sağlar. Bu elemana “UBLTR” yazılacaktır.
        [XmlElement("Standard")]
        public string Standard { get; set; }

        // Bu elemana “1.0” değeri yazılacaktır.
        [XmlElement("TypeVersion")]
        public string TypeVersion { get; set; }

        // Bu elemanda gönderici (zarfı düzenleyen) tarafın oluşturacağı ve e-fatura uygulaması içerisinde biricik
        // (unique) olması zorunlu olan GUID formatındaki alfanümerik değer bulunacaktır.
        [XmlElement("InstanceIdentifier")]
        public string InstanceIdentifier { get; set; }

        // Bu elemana zarfın türü yazılacaktır. (“SENDERENVELOPE”, “POSTBOXENVELOPE” veya “SYSTEMENVELOPE”)
        [XmlElement("Type")]
        public string Type { get; set; }

        // Mevcut durumda zarfın içerisine farklı türde belge konulmasına izin verilmemektedir. Zarfın içerisine
        // birbirinden farklı türde belge konulmasına izin verilmesi halinde, bu eleman “True” değerini alır, eğer tek türde doküman
        // içeriyorsa “False” değerini alır. Örneğin, aynı zarf içerisinde hem uygulama yanıtı, hem de iade faturası gönderiliyorsa bu
        // elemanın alacağı değerin “True” olması gerekmektedir, eğer birden çok uygulama yanıtı gönderiliyorsa “False” değerini alması gerekmektedir.
        [XmlIgnore()]
        public bool MultipleType { get; set; }

        // Bu elemana zarfın oluşturulma tarihi ve zamanı yazılacaktır. Bu değer “Xs:dateTime” tipinde olacaktır.
        [XmlElement("CreationDateAndTime")]
        public string CreationDateAndTime { get; set; }
    }
}
