using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.VeribanServiceDto.Download
{
    [DataContract]
    public class DownloadFile
    {
        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string FileExtension { get; set; }

        [DataMember]
        public string FileHash { get; set; }

        [DataMember]
        public byte[] FileData { get; set; }
    }
}