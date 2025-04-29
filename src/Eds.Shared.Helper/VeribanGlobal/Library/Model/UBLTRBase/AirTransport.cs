using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    //Hava taşımacılığında kullanılan hava aracının numarasını tanımlamak için kullanılır
    [XmlType("AirTransport", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class AirTransport
    {
        //Kullanılan hava aracının numarasınıtanımlamak için kullanılır.
        [XmlElement("AircraftID ")]
        public string AircraftID { get; set; }
    }
}
