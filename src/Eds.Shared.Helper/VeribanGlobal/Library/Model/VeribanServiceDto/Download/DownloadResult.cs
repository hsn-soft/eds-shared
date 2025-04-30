using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.VeribanServiceDto.Download
{
    [DataContract]
    public class DownloadResult
    {
        [DataMember]
        public string ReferenceCode { get; set; }

        [DataMember]
        public bool DownloadFileReady { get; set; }

        [DataMember]
        public string DownloadDescription { get; set; }

        [DataMember]
        public DownloadFile DownloadFile { get; set; }
    }
}