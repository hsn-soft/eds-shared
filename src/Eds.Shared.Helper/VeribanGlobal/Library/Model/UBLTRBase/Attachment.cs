using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Belgelerde referans verilmek istenen referansların ya da belgelere eklenmek istenen dokümanların yer aldığı elemandır.
    [XmlType("Attachment")]
    public class Attachment
    {
        // İki çeşit kullanımı mevcuttur:
        //
        //      1. ExternalReference: İlişkilendirilmek istenen dokümanın URI formatında referansını tutar. Eğer
        //      Attachment elemanı, bir “DigitalSignatureAttachment” ise (diğer bir deyişle Signature Elemanının içerisine yeralıyorsa)
        //      ExternalReference zorunlu bir elemandır.
        //      Örnek : "#12345"
        //
        //      2. EmbeddedDocumentBinaryObject: İlişiklendirilmiş dokümanı base64Encoded formatında tutar.

        [XmlElement("ExternalReference")]
        public virtual ExternalReference ExternalReference { get; set; }

        [XmlElement("EmbeddedDocumentBinaryObject", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public EmbeddedDocumentBinaryObject EmbeddedDocumentBinaryObject { get; set; }
    }

    [XmlType("EmbeddedDocumentBinaryObject")]
    public class EmbeddedDocumentBinaryObject
    {
        public EmbeddedDocumentBinaryObject()
        {
            this.CharacterSetCode = "UTF-8";
            this.EncodingCode = "Base64";
            this.MimeCode = "application/xml";
        }

        [XmlText()]
        public string Name { get; set; }

        [XmlAttribute("characterSetCode")]
        public string CharacterSetCode { get; set; }

        [XmlAttribute("encodingCode")]
        public string EncodingCode { get; set; }

        [XmlAttribute("mimeCode")]
        public string MimeCode { get; set; }

        [XmlAttribute("filename")]
        public string Filename { get; set; }

        [XmlAttribute(DataType = "base64Binary")]
        public byte[] Value { get; set; }
    }
}
