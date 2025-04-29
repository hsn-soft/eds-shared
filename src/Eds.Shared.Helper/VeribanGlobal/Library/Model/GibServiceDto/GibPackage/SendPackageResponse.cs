using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibServiceDto.GibPackage
{
    [DataContract]
    public class SendPackageResponse
    {
        #region GIB Send Package Response Fields

        [DataMember]
        public string GIBResultContent { get; set; }

        #endregion

        [DataMember]
        public string StatusCode { get; set; }

        [DataMember]
        public string StatusDescription { get; set; }

        [DataMember]
        public bool ResponseStatus { get; set; }

        [DataMember]
        public bool DontReSend { get; set; }

        [DataMember]
        public DateTime ProcessTime { get; set; }

        [DataMember]
        public string SendPackageUUID { get; set; }
    }
}
