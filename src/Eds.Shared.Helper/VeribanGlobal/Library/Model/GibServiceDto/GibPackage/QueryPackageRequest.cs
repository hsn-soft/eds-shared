using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibServiceDto.GibPackage
{
    [DataContract]
    public class QueryPackageRequest
    {
        #region GIB Query Package Request Fields

        [DataMember]
        public string PackageIdentifier { get; set; }

        #endregion
    }
}
