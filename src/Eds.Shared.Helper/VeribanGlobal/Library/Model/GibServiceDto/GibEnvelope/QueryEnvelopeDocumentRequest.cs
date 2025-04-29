using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibServiceDto.GibEnvelope
{
    public class QueryEnvelopeDocumentRequest
    {
        #region GIB Query Request Fields

        [DataMember]
        public string EnvelopeIdentifier { get; set; }

        #endregion
    }
}
