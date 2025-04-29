using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.VeribanServiceDto.Transfer
{
    [DataContract]
    public class EBookTransferFile : TransferFile
    {
        [DataMember]
        public string[] ReceiverMailTargetAddresses { get; set; }

        [DataMember]
        public byte BookPeriod { get; set; }

        [DataMember]
        public int BookYear { get; set; }
    }
}