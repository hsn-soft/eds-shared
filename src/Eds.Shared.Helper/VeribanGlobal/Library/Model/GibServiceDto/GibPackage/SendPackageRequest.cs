using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibServiceDto.GibPackage
{
    [DataContract]
    public class SendPackageRequest
    {
        #region GIB Send Package Request Fields

        [DataMember]
        public string GIBPackageFileName { get; set; }

        [DataMember]
        public byte[] GIBBinaryDataValue { get; set; }

        #endregion

        [DataMember]
        public string PackageUUID { get; set; }
    }
}
