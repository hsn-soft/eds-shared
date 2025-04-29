using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.VeribanServiceDto.Page
{
    [DataContract]
    public class SalesInvoiceModel
    {
    }

    [DataContract]
    public class SalesInvoiceActiveFilter
    {
        [DataMember]
        public string filter_insert_date_from { get; set; }
        [DataMember]
        public string filter_insert_date_to { get; set; }
        [DataMember]
        public byte? filter_document_number_type { get; set; }
        [DataMember]
        public string filter_document_number { get; set; }
        [DataMember]
        public string filter_issue_date_from { get; set; }
        [DataMember]
        public string filter_issue_date_to { get; set; }
        [DataMember]
        public byte? filter_earsiv_data_type { get; set; }
        [DataMember]
        public byte? filter_receiver_text_type { get; set; }
        [DataMember]
        public string filter_receiver_text { get; set; }
        [DataMember]
        public string filter_receiver_register { get; set; }
        [DataMember]
        public int? filter_payable_amount_from { get; set; }
        [DataMember]
        public int? filter_payable_amount_to { get; set; }
        [DataMember]
        public byte? filter_invoice_state { get; set; }
        [DataMember]
        public byte? filter_invoiceReport_state { get; set; }
        [DataMember]
        public byte? filter_invoiceMail_state { get; set; }
    }

    public class SalesInvoices
    {
        public Guid UniqueId { get; set; }
        public DateTime InsertTime { get; set; }

        public string InvoiceNumber { get; set; }
        public string DespatcheNumber { get; set; }
        public string IntegrationCode { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime? DespatcheDate { get; set; }

        public string Transportation { get; set; }
        public string InternetSales { get; set; }
        public string DeliverySales { get; set; }
        public bool IsDefaultCancel { get; set; }
        public bool IsAfterCancel { get; set; }
        public DateTime? CancelTime { get; set; }

        public string ReceiverRegisterName { get; set; }
        public string ReceiverRegisterNo { get; set; }

        public string TaxExclusiveAmount { get; set; }
        public string TaxTotalAmount { get; set; }
        public string PayableAmount { get; set; }
        public string CurrencyCode { get; set; }

        public string InvoiceStateGlobalStateClass { get; set; }
        public string InvoiceStateName { get; set; }
        public string InvoiceStateDesc { get; set; }

        public string ReportStateGlobalStateClass { get; set; }
        public string ReportStateName { get; set; }
        public string ReportStateDesc { get; set; }

        public string MessageStateGlobalStateClass { get; set; }
        public string MessageStateName { get; set; }

        public bool IsReadyBrowse { get; set; }
        public bool IsReadyFaktoring { get; set; }
    }
}
