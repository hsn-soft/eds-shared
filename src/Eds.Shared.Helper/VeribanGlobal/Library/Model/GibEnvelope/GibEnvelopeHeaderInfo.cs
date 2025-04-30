namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibEnvelope
{
    public class GibEnvelopeHeaderInfo
    {
        public string SenderRegisterNumber { get; set; }
        public string SenderAlias { get; set; }
        public string SenderTitle { get; set; }
        public string ReceiverRegisterNumber { get; set; }
        public string ReceiverAlias { get; set; }
        public string ReceiverTitle { get; set; }
        public string EnvelopeIdentifier { get; set; }
        public DateTime EnvelopeCreationTime { get; set; }
        public string EnvelopeDirectionType { get; set; }
        public string EnvelopeElementType { get; set; }
        public int EnvelopeElementCount { get; set; }
        public byte EnvelopeDocumentType { get; set; }

        public List<object> ElementObjectList { get; set; }
    }

    public class GibEnvelopeElementDocInfo
    {
        public string DocumentXmlContent { get; set; }

        public string DocumentID { get; set; }
        public string DocumentUUID { get; set; }
        public DateTime IssueTime { get; set; }
    }

    public class GibEnvelopeElementInvoiceAnswerInfo
    {
        public string AnswerDocumentXmlContent { get; set; }

        public string AnswerDocumentID { get; set; }
        public string AnswerDocumentUUID { get; set; }
        public DateTime AnswerTime { get; set; }
        public string AnswerDocumentNote { get; set; }

        public string ReferenceDocumentUUID { get; set; }
        public string ReferenceDocumentTypeCode { get; set; }
        public string ReferenceDocumentType { get; set; }
        public string AnswerName { get; set; }
        public string AnswerDescription { get; set; }

        public string GTB_REFNO { get; set; }
        public string GTB_GCB_TESCILNO { get; set; }
        public string GTB_FIILI_IHRACAT_TARIHI { get; set; }
    }

    public class GibEnvelopeElementDespatchAnswerInfo
    {
        public string DespatchAnswerDocumentXmlContent { get; set; }

        public string DespatchAnswerDocumentID { get; set; }
        public string DespatchAnswerDocumentUUID { get; set; }
        public DateTime DespatchAnswerTime { get; set; }
        public string DespatchAnswerDocumentNote { get; set; }

        public string DespatchDocumentReferenceUniqueId { get; set; }

        public List<GibEnvelopeElementDespatchAnswerDetailInfo> DespatchAnswerDespatchLines { get; set; }
    }
    public class GibEnvelopeElementDespatchAnswerDetailInfo
    {
        public int LineID { get; set; }

        public decimal ReceivedQuantity { get; set; }
        public string TimingComplaint { get; set; }

        public decimal RejectedQuantity { get; set; }
        public string RejectReason { get; set; }

        public decimal ShortQuantity { get; set; }
        public decimal OversupplyQuantity { get; set; }
    }

    public class GibEnvelopeElementSysInfo
    {
        public string SystemResponseUUID { get; set; }

        public string ReferenceEnvelopeIdentifier { get; set; }
        public string ReferenceEnvelopeIssueDate { get; set; }
        public string ReferenceDocumentTypeCode { get; set; }
        public string ReferenceDocumentType { get; set; }

        public string ResultCode { get; set; }
        public string ResultDescription { get; set; }

        public string GTBRefNumber { get; set; }
    }
}
