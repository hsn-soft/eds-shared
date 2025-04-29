using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibServiceDto.GibEnvelope
{
    [DataContract]
    public class SendEnvelopeDocumentResponse
    {
        #region GIB Send Package Response Fields

        [DataMember]
        public int GIBResultCode { get; set; }

        [DataMember]
        public string GIBResultDescription { get; set; }

        #endregion

        [DataMember]
        public bool ResponseStatus { get; set; }

        [DataMember]
        public string ResponseErrorMessage { get; set; }

        [DataMember]
        public bool DontReSend { get; set; }

        [DataMember]
        public DateTime ProcessTime { get; set; }

        [DataMember]
        public string SendEnvelopeIdentifier { get; set; }
    }
}
