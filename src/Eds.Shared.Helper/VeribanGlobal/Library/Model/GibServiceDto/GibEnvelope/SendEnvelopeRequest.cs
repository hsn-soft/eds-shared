using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibServiceDto.GibEnvelope
{
    [DataContract]
    public class SendEnvelopeRequest
    {
        #region GIB Send Request Fields

        //Bu kısma XML formatında oluşturulmuş ve daha sonra ZIP formatında sıkıştırılmış zarfın adı yazılacaktır. Zarf adının
        //uzantısının “zip” olması gerekmektedir. Zarf adının ise e-Fatura Uygulaması Teknik Kılavuzlar “Ek-1 e-Fatura Uygulaması Zarf
        //Şema Yapısı” dokümanında belirtilen “InstanceIdentifier” değeri ile aynı olmalıdır.
        [DataMember]
        public string EnvelopeFileName { get; set; }

        //Bu kısma XML formatında oluşturmuş zarfın, ZIP formatı ile sıkıştırılmış halinin Base64 ile encode edilmiş hali yazılacaktır.
        [DataMember]
        public string BinaryDataContentType { get; set; }

        [DataMember]
        public byte[] BinaryDataValue { get; set; }

        //Bu kısma XML formatında oluşturulmuş ve daha sonra ZIP formatı ile sıkıştırılmış zarfın MD5 özeti yazılacaktır.
        [DataMember]
        public string Hash { get; set; }

        #endregion

        [DataMember]
        public string EnvelopeID { get; set; }

        [DataMember]
        public string EnvelopeFullPath { get; set; }
    }
}
