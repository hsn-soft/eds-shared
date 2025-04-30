using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibServiceDto.GibArchive
{
    [DataContract]
    public class QueryArchiveDocumentRequest
    {
        [DataMember]
        public string ArchiveDocumentId { get; set; }
    }
}
