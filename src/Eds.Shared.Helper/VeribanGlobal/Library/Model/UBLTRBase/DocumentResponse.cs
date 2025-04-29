using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Belgelere ilişkin kabul, red ve diğer mesajlar bu elemana girilecektir.
    [XmlType("DocumentResponse")]
    public class DocumentResponse
    {
        // Uygulama Yanıtının sistem seviyesi yanıtı mı yoksa iş seviyesi yanıt mı olduğunu belirler. İş seviyesinde bu elemanın
        // içerdiği “cbc:ResponseCode” tekil elemanın değeri faturanın kabul edilme durumunda KABUL, faturanın reddi durumunda 
        // RED ve faturanın iade durumunda ise IADE olacaktır.
        [XmlElement("Response")]
        public virtual Response Response { get; set; }

        // Yanıt verilen belgeye referans bilgisi içermektedir.
        [XmlElement("DocumentReference")]
        public virtual DocumentReference DocumentReference { get; set; }

        // Satıra yanıt bilgilerini içerir. Dokümanın belli bir kalemi ile ilgili red ve düzeltme talebi olma durumunda
        private List<LineResponse> _lineResponse;
        [XmlElement("LineResponse", Type = typeof(LineResponse))]
        public virtual List<LineResponse> LineResponses
        {
            get { return this._lineResponse; }
            set { if (value != null) this._lineResponse = value; else this._lineResponse = new List<LineResponse>(); }
        }
    }
}
