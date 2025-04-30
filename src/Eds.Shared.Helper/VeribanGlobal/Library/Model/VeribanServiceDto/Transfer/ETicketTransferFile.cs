using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.VeribanServiceDto.Transfer
{
    [DataContract]
    public class ETicketTransferFile : TransferFile
    {
        [DataMember]
        public string[] ReceiverMailTargetAddresses { get; set; }
    }
}