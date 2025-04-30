using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Referans verilen ya da eklenen belgelere ilişkin bilgiler girilecektir.
    [XmlType("DocumentReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class DocumentReference
    {
        // Referans verilen veya eklenen belgenin sıra numarası girilecektir.
        [XmlElement("ID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public CombineId ID { get; set; }

        // Belgenin düzenlenme tarihi girilecektir.Yıl-Ay-Gün (YYYY-AA-GG)
        [XmlElement("IssueDate")]
        public string IssueDate { get; set; }

        // Bu eleman belge seviyesinde kullanılmayacaktır. Kullanım alanı sistem seviyesinde dönen uygulama yanıtı (ApplicationResponse) belgesinin içindedir.
        [XmlElement("DocumentTypeCode")]
        public string DocumentTypeCode { get; set; }

        // Referans verilen veya eklenen belgenin tipi girilecektir. Örnek olarak “XSLT”, “REKLAM”, “PROFORMA”, “GÖRÜŞME DETAYI” ve benzeri değerler girilebilir.
        [XmlElement("DocumentType")]
        public string DocumentType { get; set; }

        //Referans verilen ya da eklenen belgelere ilişkin serbest metin açıklaması girilebilir.
        private List<String> _documentDescriptions;
        [XmlElement("DocumentDescription", Type = typeof(String), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public virtual List<String> DocumentDescriptions
        {
            get { return this._documentDescriptions; }
            set { if (value != null) this._documentDescriptions = value; else this._documentDescriptions = new List<String>(); }
        }

        // Belgelerde referans verilmek istenen referansların ya da belgelere eklenmek istenen dokümanların yer aldığı elemandır.
        [XmlElement("Attachment", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Attachment Attachment { get; set; }

        //Referans verilen ya da eklenen belgenin geçerlilik süresi girilebilir.
        [XmlElement("ValidityPeriod", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Period ValidityPeriod { get; set; }

        //Referans verilen ya da eklenen belgeyi yayınlayan taraf bilgisi girilebilir.
        [XmlElement("IssuerParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Party IssuerParty { get; set; }
    }
}