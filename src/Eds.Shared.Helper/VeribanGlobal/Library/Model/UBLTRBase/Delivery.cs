using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Ürün tesliman bilgileri detaylı olarak girilir.
    [XmlType("Delivery", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class Delivery
    {
        //Teslimatı belge içerisinde tekil olarak tanımlar.
        [XmlElement("ID")]
        public CombineId ID { get; set; }

        //Ürün miktarı girilir.
        [XmlElement("Quantity")]
        public virtual BaseUnit Quantity { get; set; }

        //Gerçekleşen teslim tarihi yazılır.
        [XmlElement("ActualDeliveryDate")]
        public string ActualDeliveryDate { get; set; }

        //Gerçekleşen teslim zamanı yazılır.
        [XmlElement("ActualDeliveryTime")]
        public string ActualDeliveryTime { get; set; }

        //Son teslim tarihi girilir.
        [XmlElement("LatestDeliveryDate")]
        public string LatestDeliveryDate { get; set; }

        //Son teslim zamanı girilir.
        [XmlElement("LatestDeliveryTime")]
        public string LatestDeliveryTime { get; set; }

        //Takip numarası girilir.
        [XmlElement("TrackingID")]
        public string TrackingID { get; set; }

        //Teslimat adresi girilir. Bknz. Address.
        [XmlElement("DeliveryAddress", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Address DeliveryAddress { get; set; }

        //Alternatif teslim yeri girilir. Bknz. Location.
        [XmlElement("AlternativeDeliveryLocation", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Location AlternativeDeliveryLocation { get; set; }

        //Tahmini teslim dönemi girilir. Bknz. Period
        [XmlElement("EstimatedDeliveryPeriod", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Period EstimatedDeliveryPeriod { get; set; }

        //Taşıyıcı taraf girilir. Bknz. Party
        [XmlElement("CarrierParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Party CarrierParty { get; set; }

        //Teslimat yapılacak (ürünleri teslim alacak) taraf girilir. Bknz. Party
        [XmlElement("DeliveryParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Party DeliveryParty { get; set; }

        //Gönderi bilgisi girilir. Bknz. Despatch
        [XmlElement("Despatch", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Despatch Despatch { get; set; }

        //Teslimat şartları girilir. Bknz. DeliveryTerms
        private List<DeliveryTerms> _deliveryTerms;
        [XmlElement("DeliveryTerms", Type = typeof(DeliveryTerms), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<DeliveryTerms> DeliveryTerms
        {
            get { return this._deliveryTerms; }
            set { if (value != null) this._deliveryTerms = value; else this._deliveryTerms = new List<DeliveryTerms>(); }
        }

        //Yük/kargo bilgileri girilir. Bknz. Shipment
        [XmlElement("Shipment", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Shipment Shipment { get; set; }
    }

    [XmlType("RoadTransport", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class RoadTransport
    {
        //Plaka numarası girilir. 
        [XmlElement("LicensePlateID")]
        public CombineId LicensePlateID { get; set; }
    }
}
