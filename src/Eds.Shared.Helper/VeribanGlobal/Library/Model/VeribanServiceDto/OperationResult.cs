using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.VeribanServiceDto
{
    [DataContract]
    public class OperationResult
    {
        [DataMember]
        public bool OperationCompleted { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
