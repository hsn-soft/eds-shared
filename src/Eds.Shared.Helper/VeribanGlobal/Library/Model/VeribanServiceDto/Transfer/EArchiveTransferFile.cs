using System.Runtime.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.VeribanServiceDto.Transfer
{
    [DataContract]
    public class EArchiveTransferFile : TransferFile
    {
        [DataMember]
        public string[] ReceiverMailTargetAddresses { get; set; }

        [DataMember]
        public string ReceiverGsmNo { get; set; }

        [DataMember]
        public EArchiveEnums.InvoiceTransportationTypes InvoiceTransportationType { get; set; }

        [DataMember]
        public bool IsInvoiceCreatedAtDelivery { get; set; }

        [DataMember]
        public bool IsInternetSalesInvoice { get; set; }
    }
}