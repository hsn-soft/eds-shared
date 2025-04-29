using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.VeribanServiceDto.Transfer
{
    [DataContract]
    public class EInvoiceTransferFile : TransferFile
    {
        [DataMember]
        public string CustomerAlias { get; set; }

        [DataMember]
        public bool IsDirectSend { get; set; }
    }
}
