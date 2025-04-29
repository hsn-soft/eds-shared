using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibServiceDto.GibEnvelope
{
    [DataContract]
    public class QueryEnvelopeDocumentResponse
    {
        #region GIB Query Response Fields

        [DataMember]
        public string ResponseXMLContent { get; set; }

        [DataMember]
        public int GIBResultCode { get; set; }

        [DataMember]
        public string GIBResultDescription { get; set; }

        [DataMember]
        public string EnvelopeIssueDate { get; set; }

        #endregion

        [DataMember]
        public bool ResponseStatus { get; set; }

        [DataMember]
        public string ResponseErrorMessage { get; set; }

        [DataMember]
        public bool DontReQuery { get; set; }

        [DataMember]
        public bool OperationCompleted { get; set; }

        [DataMember]
        public DateTime ProcessTime { get; set; }

        [DataMember]
        public string QueryEnvelopeIdentifier { get; set; }
    }
}
