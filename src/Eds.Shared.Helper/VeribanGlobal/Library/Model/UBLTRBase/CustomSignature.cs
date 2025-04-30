using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Bu elemana belgelerde kullanılan mali mühür/elektronik imza ile ilgili bilgiler girilir.
    [XmlType("Signature", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class CustomSignature
    {
        // Bu alana dokumana eklenecek elektronik imza ile ilgili bir referans numarası verilecektir.
        [XmlElement("ID")]
        public CombineId ID { get; set; }

        // Bu alana dokumanı imzalayan imza sahibinin bilgileri eklenecektir.
        [XmlElement("SignatoryParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Party SignatoryParty { get; set; }

        // Bu alana UBLExtensions alanına eklenen dijital imzaya referans eklenecektir.
        [XmlElement("DigitalSignatureAttachment", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Attachment DigitalSignatureAttachment { get; set; }
    }

    //[XmlType("CombineId")]
    [XmlType("CombineId", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class CombineId
    {
        [XmlText()]
        public string Id { get; set; }

        [XmlAttribute("schemeID")]
        public string SchemeId { get; set; }
    }
}
