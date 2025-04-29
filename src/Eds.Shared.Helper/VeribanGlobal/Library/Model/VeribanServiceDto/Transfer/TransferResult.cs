using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.VeribanServiceDto.Transfer
{
    [DataContract]
    public class TransferResult : OperationResult
    {
        [DataMember]
        public string TransferFileUniqueId { get; set; }
    }
}