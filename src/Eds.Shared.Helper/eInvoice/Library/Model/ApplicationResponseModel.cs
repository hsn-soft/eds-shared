using System.Xml.Serialization;
using Eds.Shared.Helper.eInvoice.Library.Model.Utils;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase;

namespace Eds.Shared.Helper.eInvoice.Library.Model
{
    [XmlRoot("ApplicationResponse", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:ApplicationResponse-2")]
    public class ApplicationResponseModel
    {
        public ApplicationResponseModel()
        {
            xmlns.Add("", "urn:oasis:names:specification:ubl:schema:xsd:ApplicationResponse-2");
            xmlns.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            xmlns.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

            //imza için gerekli
            xmlns.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");

            this.UBLVersionID = UblTr2HandlerEInvoice.UblVersion;
            this.CustomizationID = UblTr2HandlerEInvoice.CustomizationVersion;
        }

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces xmlns { get; set; } = new XmlSerializerNamespaces();

        [XmlAttribute("schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string schemaLocation { get; set; } = "urn:oasis:names:specification:ubl:schema:xsd:ApplicationResponse-2 ../xsd/maindoc/UBL" + (UblTr2HandlerEInvoice.UblVersion == "2.0" ? "TR" : "") + "-ApplicationResponse-" + UblTr2HandlerEInvoice.UblVersion + ".xsd";

        // (Zorunlu değil!) Bu alana XAdES formatında mali mühür/ elektronik imza bilgileri yazılacaktır.
        private List<UblExtension> _UBLExtensions;
        [XmlElement("UBLExtensions", Type = typeof(List<UblExtension>), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
        public virtual List<UblExtension> UBLExtensions
        {
            get { return this._UBLExtensions; }
            set { if (value != null) this._UBLExtensions = value; else this._UBLExtensions = new List<UblExtension>(); }
        }

        // XSD dokümanının UBL versiyonu yazılacaktır.Bu değer için “2.0” kullanılacaktır.
        [XmlElement("UBLVersionID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string UBLVersionID { get; set; }

        // UBL’ in özelleştirme numarasıdır. Bu değer için “TR1.2” kullanılacaktır.
        [XmlElement("CustomizationID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string CustomizationID { get; set; }

        // Kullanılan senaryodur. "TEMELFATURA" veya "TICARIFATURA" olmalıdır.
        [XmlElement("ProfileID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string ProfileID { get; set; }

        // Üç haneli harf grubunu ifade eden birim kod ile 13 haneli müteselsil numaranın birleşiminden meydana gelen
        // Uygulama Yanıtı Numarası bu elemana yazılacaktır. Müteselsil numaranın ilk dört hanesi uygulama yanıtının düzenlendiği yılı
        // kalan dokuz hane ise müteselsil numarayı ifade etmektedir. Düzenleyen bünyesinde aynı uygulama yanıtı numarası birden fazla kullanılamaz.
        // Örnek: "GIB2009000000001"
        [XmlElement("ID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string ID { get; set; }

        // Evrensel Tekil Tanımlama Numarası (ETTN), düzenlenen Uygulama Yanıtının evrensel eşsizliğini sağlayan numaradır. Bu numara
        // Uygulama Yanıtı düzenleyen tarafından standartlara uygun olarak üretilip Uygulama Yanıtlarında kullanılacaktır.
        // Örnek: "e093a490-dd99-11dd-ad8b-0800200c9a66"
        [XmlElement("UUID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string UUID { get; set; }

        // Uygulama Yanıtının düzenleme tarihi yazılacaktır.Yıl-Ay-Gün (YYYY-AA-GG)
        [XmlElement("IssueDate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string IssueDate { get; set; }

        // (Zorunlu değil!) Bu elemana Uygulama Yanıtının düzenleme saati yazılabilecektir.Saat:Dakika:Saniye
        [XmlElement("IssueTime", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string IssueTime { get; set; }

        // (Zorunlu değil!) Uygulama Yanıtında yer verilmek istenen genel açıklamalar için bu eleman kullanılacaktır. Birden fazla açıklama yapılmak
        // istenmesi halinde elemanın tekrar kullanımı mümkündür.
        private List<String> _notes;
        [XmlElement("Note", Type = typeof(String), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public virtual List<String> Notes
        {
            get { return this._notes; }
            set { if (value != null) this._notes = value; else this._notes = new List<String>(); }
        }

        // (Zorunlu değil!) Uygulama Yanıtında kullanılan mali mühür veya elektronik imza ile sertifikalarına ilişkin bilgiler bu elemanda yer alacaktır.
        // Uygulama Yanıtı Mesajı, sistem düzeyinde ya da belge düzeyinde kullanım durumlarına göre farklı kardinalite değerleri almaktadır.
        //      • Uygulama yanıtı sistem düzeyinde kullanılacak ise mühür veya imza elemanı seçimli olarak kullanılır.
        //      • Uygulama yanıtı belge düzeyinde kullanılacak ise mühür veya imza elemanı zorunlu olarak kullanılır.
        // Dolayısıyla, bu eleman ‘schematron’ düzeyinde de kontrol edilecektir.
        private List<CustomSignature> _signatures;
        [XmlElement("Signature", Type = typeof(CustomSignature), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<CustomSignature> Signatures
        {
            get { return this._signatures; }
            set { if (value != null) this._signatures = value; else this._signatures = new List<CustomSignature>(); }
        }

        // Bu elemanda Uygulama Yanıtı gönderen tarafın bilgileri yer alacaktır.
        private Party _senderParty;
        [XmlElement("SenderParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Party SenderParty
        {
            get { return this._senderParty; }
            set { if (value != null) this._senderParty = value; else this._senderParty = new Party(); }
        }

        // Bu elemanda Uygulama Yanıtının gönderildiği tarafa ait bilgiler yer alacaktır.
        private Party _receiverParty;
        [XmlElement("ReceiverParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Party ReceiverParty
        {
            get { return this._receiverParty; }
            set { if (value != null) this._receiverParty = value; else this._receiverParty = new Party(); }
        }

        // Belgelerin kabul veya red edilmesine ilişkin mesajlar bu elemana girilecektir.
        private DocumentResponse _documentResponse;
        [XmlElement("DocumentResponse", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual DocumentResponse DocumentResponse
        {
            get { return this._documentResponse; }
            set { if (value != null) this._documentResponse = value; else this._documentResponse = new DocumentResponse(); }
        }
    }
}
