using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibServiceDto.GibArchive
{
    [DataContract]
    public class QueryArchiveDocumentResponse
    {
        #region #region GIB Query Response Fields

        [DataMember]
        public string ArchiveDocumentId { get; set; }

        [DataMember]
        public int ResultCode { get; set; }

        [DataMember]
        public string ResultDescription { get; set; }

        [DataMember]
        public string TCRegisterNumber { get; set; }

        [DataMember]
        public string TaxRegisterNumber { get; set; }

        #endregion

        [DataMember]
        public bool ResponseStatus { get; set; }

        [DataMember]
        public bool DontReQuery { get; set; }

        [DataMember]
        public string ProcessTime { get; set; }
    }
}
