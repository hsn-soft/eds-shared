using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    //Siparişin kalemlerine referans atmak için kullanılır.
    [XmlType("OrderLineReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class OrderLineReference
    {
        //Kalem numarası girilir.
        public string LineID { get; set; }

        //Alıcının verdiği kalem numarası verilir.
        public string SalesOrderLineID { get; set; }

        //Kalemin durumu girilir.
        public string LineStatusCode { get; set; }

        //İlgili sipariş belgesine referans verilir. Bknz. OrderReference
        [XmlElement("OrderReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual OrderReference OrderReference { get; set; }
    }
}
