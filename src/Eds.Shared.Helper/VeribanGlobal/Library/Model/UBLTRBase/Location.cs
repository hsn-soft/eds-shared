using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    //Konum bilgisi girilir.
    [XmlType("Location", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class Location
    {
        //Mekanı tanımlamak için kullanılır (örneğin GLN numarası)
        [XmlElement("ID")]
        public CombineId ID { get; set; }

        //Addres bilgisini tutar. Bknz. Address.
        [XmlElement("Address", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Address Address { get; set; }
    }
}
