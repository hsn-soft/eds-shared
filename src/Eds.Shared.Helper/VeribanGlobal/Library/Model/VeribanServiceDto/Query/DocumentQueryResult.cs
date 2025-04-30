using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.VeribanServiceDto.Query
{
    [DataContract]
    public class DocumentQueryResult
    {
        [DataMember]
        public int StateCode { get; set; }

        [DataMember]
        public string StateName { get; set; }

        [DataMember]
        public string StateDescription { get; set; }
    }
}