using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Bu elemana irtibat bilgileri yazılabilecektir.
    [XmlType("Contact", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class Contact
    {
        // Telefon numarası metin olarak girilecektir.
        [XmlElement("Telephone")]
        public string Telephone { get; set; }

        // Fax numarası metin olarak girilecektir.
        [XmlElement("Telefax")]
        public string Telefax { get; set; }

        // Elektronik posta adresi metin olarak girilecektir.
        [XmlElement("ElectronicMail")]
        public string ElectronicMail { get; set; }

        //Serbest metin açıklama girilebilecektir.
        [XmlElement("Note")]
        public string Note { get; set; }

        // Başka iletişim kanalı veya ilave telefon, fax ve elektronik posta kullanılıyor ise bu eleman kanalın tanımlanmasında kullanılacaktır.
        private List<Communication> _otherCommunications;
        [XmlElement("OtherCommunication", Type = typeof(Communication))]
        public virtual List<Communication> OtherCommunications
        {
            get { return this._otherCommunications; }
            set { if (value != null) this._otherCommunications = value; else this._otherCommunications = new List<Communication>(); }
        }
    }
}
