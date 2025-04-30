using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibServiceDto.GibEnvelope
{
    [DataContract]
    public class QueryEnvelopeRequest
    {
        [DataMember]
        public string InstanceIdentifier { get; set; }
    }
}
