using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibServiceDto.GibArchive
{
    [DataContract]
    public class SendArchiveDocumentRequest
    {
        public SendArchiveDocumentRequest()
        {

        }

        #region #region GIB Send Archive Document Request Fields

        [DataMember]
        public string ArchiveFileName { get; set; }

        [DataMember]
        public byte[] BinaryDataValue { get; set; }

        #endregion

        [DataMember]
        public string ArchiveDocumentUUID { get; set; }

        [DataMember]
        public string ArchiveDocumentFullPath { get; set; }
    }
}
