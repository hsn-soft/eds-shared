using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.VeribanServiceDto.Query
{
    [DataContract]
    public class EArchiveOkcDocumentQueryResult : DocumentQueryResult
    {
        [DataMember]
        public int GIBReportStateCode { get; set; }

        [DataMember]
        public string GIBReportStateName { get; set; }

        [DataMember]
        public string GIBReportUUID { get; set; }
    }
}
