using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.VeribanServiceDto.Query
{
    [DataContract]
    public class TransferQueryResult : DocumentQueryResult
    {
        [DataMember]
        public DateTime? InsertTime { get; set; }
    }
}