using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibServiceDto.GibPackage
{
    [DataContract]
    public class QueryPackageResponse
    {
        #region GIB Query Package Response Fields

        [DataMember]
        public string PackageIdentifier { get; set; }

        [DataMember]
        public string StatusCode { get; set; }

        [DataMember]
        public string StatusDescription { get; set; }

        #endregion

        [DataMember]
        public bool ResponseStatus { get; set; }

        [DataMember]
        public bool DontReQuery { get; set; }

        [DataMember]
        public bool OperationCompleted { get; set; }

        [DataMember]
        public DateTime ProcessTime { get; set; }
    }
}
