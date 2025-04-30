using System.Runtime.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.VeribanServiceDto.Transfer
{
    [DataContract]
    public class EArchiveSelfEmploymentFile : TransferFile
    {
        [DataMember]
        public string[] ReceiverMailTargetAddresses { get; set; }

        [DataMember]
        public EArchiveEnums.InvoiceTransportationTypes ReceiptTransportationType { get; set; }
    }
}