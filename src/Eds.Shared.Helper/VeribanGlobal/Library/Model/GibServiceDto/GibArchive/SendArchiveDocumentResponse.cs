using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibServiceDto.GibArchive
{
    [DataContract]
    public class SendArchiveDocumentResponse
    {
        public SendArchiveDocumentResponse()
        {

        }

        #region #region GIB Send Archive Document Response Fields

        //Donen mesaj parse edilerek mesaj kodu alınacak
        [DataMember]
        public string ResultDescription { get; set; }

        #endregion

        [DataMember]
        public string ResultCode { get; set; }

        [DataMember]
        public bool ResponseStatus { get; set; }

        [DataMember]
        public bool DontReSend { get; set; }

        [DataMember]
        public string ProcessTime { get; set; }

        [DataMember]
        public string ArchiveDocumentUUID { get; set; }

        [DataMember]
        public string ArchiveDocumentFullPath { get; set; }
    }
}
