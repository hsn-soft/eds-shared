using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.VeribanServiceDto.Page
{
    [DataContract]
    public class MailData
    {
        [DataMember]
        public string UniqueId { get; set; }
        [DataMember]
        public string TargetAddress { get; set; }
        [DataMember]
        public string SalesInvoiceUniqueId { get; set; }
        [DataMember]
        public string MailStateGlobalStateClass { get; set; }
        [DataMember]
        public string MailStateName { get; set; }
        [DataMember]
        public string MailStateErrorDesc { get; set; }
        [DataMember]
        public string SendTime { get; set; }
        [DataMember]
        public bool IsRead { get; set; }
        [DataMember]
        public bool IsDownload { get; set; }
        [DataMember]
        public bool IsReadyReSendEmail { get; set; }
    }
}
