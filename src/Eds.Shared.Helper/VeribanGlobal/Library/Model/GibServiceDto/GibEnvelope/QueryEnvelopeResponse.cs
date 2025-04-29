using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibServiceDto.GibEnvelope
{
    [DataContract]
    public class QueryEnvelopeResponse
    {
        #region #region GIB Query Response Fields

        [DataMember]
        public string ResponseXMLContent { get; set; }

        [DataMember]
        public string EnvelopeIssueDate { get; set; }

        [DataMember]
        public string ResultCode { get; set; }

        [DataMember]
        public string ResultDescription { get; set; }

        #endregion

        [DataMember]
        public bool ResponseStatus { get; set; }

        [DataMember]
        public bool DontReQuery { get; set; }

        [DataMember]
        public bool OperationCompleted { get; set; }

        [DataMember]
        public string ProcessTime { get; set; }

        [DataMember]
        public string EnvelopeID { get; set; }
    }

}
