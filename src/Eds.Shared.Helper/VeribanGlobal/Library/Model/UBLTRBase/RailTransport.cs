using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    //Demiryolu Taşımacılığı
    [XmlType("RailTransport", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class RailTransport
    {
        //Tren numarası girilir. 
        [XmlElement("TrainID ")]
        public string TrainID { get; set; }

        //Vagon numarası girilir. 
        [XmlElement("RailCarID ")]
        public string RailCarID { get; set; }
    }
}
