using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Satıcı bilgilerini tutan elemandır.
    [XmlType("BillingReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class BillingReference
    {
        // Önceki ilişkili fatura belgelerine referans bilgisi girilir.
        [XmlElement("InvoiceDocumentReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual DocumentReference InvoiceDocumentReference { get; set; }

        //Yurt dışında bir kurum kendine fatura kesebilmektedir. Bu eleman bu belgeye referans için kullanılmaktadır.
        [XmlElement("SelfBilledInvoiceDocumentReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual DocumentReference SelfBilledInvoiceDocumentReference { get; set; }

        //İlgili CreditNote (Satıcı tarafından düzenlenip alıcının borcunu düşürmek için kullanılan belge) dokümanına referans bilgisini tutar.
        [XmlElement("CreditNoteDocumentReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual DocumentReference CreditNoteDocumentReference { get; set; }

        //İlgili DebitNote (Alıcı tarafından düzenlenip alıcının borcunu düşürmek için kullanılan belge) dokümanına referans bilgisini tutar.
        [XmlElement("DebitNoteDocumentReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual DocumentReference DebitNoteDocumentReference { get; set; }

        //İlgili hatırlatma belgesine referans girilir.
        [XmlElement("ReminderDocumentReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual DocumentReference ReminderDocumentReference { get; set; }

        //Fatura ile ilgili belgelerin kalemlerine referans eklemek için kullanılır.
        private List<BillingReferenceLine> _billingReferenceLines;
        [XmlElement("BillingReferenceLine", Type = typeof(BillingReferenceLine), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<BillingReferenceLine> References
        {
            get { return this._billingReferenceLines; }
            set { if (value != null) this._billingReferenceLines = value; else this._billingReferenceLines = new List<BillingReferenceLine>(); }
        }

        // NOT: 433 nolu VUK genel tebliği kapsamında düzenlenen ÖKC bilgi fişi bilgileri kullanılmışsa bu elemanın altında AdditionalDocumentReference elemanına yazılacaktır.
        //Bilgiler aşağıdaki şekilde yazılmalıdır.
        //AdditionalDocumentReference/ID: Fiş Numarası yazılacaktır.
        //AdditionalDocumentReference/IssueDate: Bilgi fişi tarihi yazılacaktır. 
        //AdditionalDocumentReference/DocumentTypeCode: OKCBF sabit değeri yazılmalıdır. 
        //AdditionalDocumentReference/DocumentType: OKCBilgiFisi sabit değeri yazılmalıdır. 
        //AdditionalDocumentReference/DocumentDescription: Bilgi Fişi Tipi yazılmalıdır. Kabul edilecek değerler şunlardır:  AVANS, YEMEK_FIS, E-FATURA, E-FATURA_IRSALIYE, E-ARSIV, E-ARSIV_IRSALIYE, FATURA, OTOPARK, FATURA_TAHSILAT, FATURA_TAHSILAT_KOMISYONLU 
        //AdditionalDocumentReference/Attachment/ExternalReference/URI: Z Raporu numarası yazılacaktır. 
        //AdditionalDocumentReference/ValidityPeriod/StartDate: İrsaliye yerine geçmesi durumunda bilgi fişi tarihi yazılmalıdır. 
        //AdditionalDocumentReference/ValidityPeriod/cbc:StartTime: İrsaliye yerine geçmesi durumunda bilgi fişi zamanı yazılmalıdır. 
        //AdditionalDocumentReference/IssuerParty/EndpointID: ÖKC Seri numarası yazılacaktır
        [XmlElement("AdditionalDocumentReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual DocumentReference AdditionalDocumentReference { get; set; }
    }
}
