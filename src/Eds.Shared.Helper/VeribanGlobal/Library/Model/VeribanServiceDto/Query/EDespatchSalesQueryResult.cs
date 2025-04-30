using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.VeribanServiceDto.Query
{
    [DataContract]
    public class EDespatchSalesQueryResult : DocumentQueryResult
    {
        [DataMember]
        public byte AnswerStateCode { get; set; }

        [DataMember]
        public string AnswerStateName { get; set; }

        [DataMember]
        public string AnswerStateDescription { get; set; }


        [DataMember]
        public byte AnswerTypeCode { get; set; }

        [DataMember]
        public string AnswerTypeName { get; set; }

        [DataMember]
        public string AnswerTypeDescription { get; set; }


        [DataMember]
        public string EnvelopeIdentifier { get; set; }

        [DataMember]
        public int EnvelopeGIBCode { get; set; }

        [DataMember]
        public string EnvelopeGIBStateName { get; set; }

        [DataMember]
        public string EnvelopeCreationTime { get; set; }


        [DataMember]
        public string AnswerEnvelopeIdentifier { get; set; }

        [DataMember]
        public int AnswerEnvelopeGIBCode { get; set; }

        [DataMember]
        public string AnswerEnvelopeGIBStateName { get; set; }

        [DataMember]
        public string AnswerEnvelopeCreationTime { get; set; }

    }
}
