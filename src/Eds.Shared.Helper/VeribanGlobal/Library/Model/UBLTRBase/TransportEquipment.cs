using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    //Taşıma Ekipmanı
    [XmlType("TransportEquipment", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class TransportEquipment
    {
        //: Tekil numarası girilir. Örneğin Dorse Plaka numarası.
        [XmlElement("ID")]
        public CombineId ID { get; set; }

        //Ekipman tipi kodu girilir
        [XmlElement("TransportEquipmentTypeCode")]
        public string TransportEquipmentTypeCode { get; set; }

        //Serbest metin açıklama girilir. 
        [XmlElement("Description")]
        public string Description { get; set; }
    }
}
