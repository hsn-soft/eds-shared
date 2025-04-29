using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibServiceDto.GibEnvelope
{
    [DataContract]
    public class SendEnvelopeResponse
    {
        #region #region GIB Send Response Fields

        //Donen mesaj parse edilerek mesaj kodu alınacak
        [DataMember]
        public string ResultCode { get; set; }

        //Donen mesaj parse edilerek mesaj acıklaması alınacak
        [DataMember]
        public string ResultDescription { get; set; }

        [DataMember]
        public string Hash { get; set; }

        #endregion

        public SendEnvelopeResponse()
        {
            this._sendElements = new List<SendElementDto>();
        }

        [DataMember]
        public string EnvelopeID { get; set; }

        [DataMember]
        public bool ResponseStatus { get; set; }

        [DataMember]
        public string ProcessTime { get; set; }

        [DataMember]
        public string EnvelopeFileFullPath { get; set; }

        private List<SendElementDto> _sendElements;
        [DataMember]
        public virtual List<SendElementDto> SendElements
        {
            get { return this._sendElements; }
            set { if (value != null) this._sendElements = value; else this._sendElements = new List<SendElementDto>(); }
        }
    }

    [DataContract]
    public class SendElementDto
    {
        [DataMember]
        public string ElementID { get; set; }

        [DataMember]
        public string ElementUUID { get; set; }

        [DataMember]
        public bool SerializeStatus { get; set; }

        [DataMember]
        public string ElementXmlContent { get; set; }

        [DataMember]
        public string ElementXmlFileFullPath { get; set; }
    }
}
