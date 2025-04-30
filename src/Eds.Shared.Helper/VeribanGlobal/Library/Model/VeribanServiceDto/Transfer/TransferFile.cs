using System.Runtime.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.VeribanServiceDto.Transfer
{
    [DataContract]
    public class TransferFile
    {
        [DataMember]
        public GlobalEnums.TransferDocumentDataTypes FileDataType { get; set; }

        [DataMember]
        public string FileNameWithExtension { get; set; }

        [DataMember]
        public byte[] BinaryData { get; set; }

        [DataMember]
        public string BinaryDataHash { get; set; }
    }
}