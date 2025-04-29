using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    //Gönderi
    [XmlType("Shipment", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class Shipment
    {
        public Shipment()
        {
            ID = new CombineId();
        }

        //Taşıma ünitesi numarası girilir.
        [XmlElement("ID")]
        public CombineId ID { get; set; }

        //Nasıl paketlenip taşınacağı kodlu olarak tanımlar.
        [XmlElement("HandlingCode")]
        public string HandlingCode { get; set; }

        //Nasıl paketlenip taşınacağıserbest metin olarak tanımlar.
        [XmlElement("HandlingInstructions")]
        public string HandlingInstructions { get; set; }

        //Brüt ağırlık girilir.
        [XmlElement("GrossWeightMeasure")]
        public virtual BaseUnit GrossWeightMeasure { get; set; }

        //Net ağırlık girilir.
        [XmlElement("NetWeightMeasure")]
        public virtual BaseUnit NetWeightMeasure { get; set; }

        //Brüt hacim girilir.
        [XmlElement("GrossVolumeMeasure")]
        public virtual BaseUnit GrossVolumeMeasure { get; set; }

        //Net hacim girilir.
        [XmlElement("NetVolumeMeasure")]
        public virtual BaseUnit NetVolumeMeasure { get; set; }

        [XmlElement("TotalGoodsItemQuantity")]
        public virtual BaseUnit TotalGoodsItemQuantity { get; set; }

        //Toplam taşıma ünitesi miktarı girilir.
        [XmlElement("TotalTransportHandlingUnitQuantity")]
        public virtual BaseUnit TotalTransportHandlingUnitQuantity { get; set; }

        //Sigorta tutarı girilir.
        [XmlElement("InsuranceValueAmount")]
        public virtual UblBaseCurrency InsuranceValueAmount { get; set; }

        //Gümrük değeri tutarı girilir.
        [XmlElement("DeclaredCustomsValueAmount")]
        public virtual UblBaseCurrency DeclaredCustomsValueAmount { get; set; }

        //Nakliye tutarı (navlun) girilir.
        [XmlElement("DeclaredForCarriageValueAmount")]
        public virtual UblBaseCurrency DeclaredForCarriageValueAmount { get; set; }

        //Ürünün GTIP kıymet değeri girilir.
        [XmlElement("DeclaredStatisticsValueAmount")]
        public virtual UblBaseCurrency DeclaredStatisticsValueAmount { get; set; }

        //FOB tutarı girilir.
        [XmlElement("FreeOnBoardValueAmount")]
        public virtual UblBaseCurrency FreeOnBoardValueAmount { get; set; }

        private List<String> _SpecialInstructions;
        [XmlElement("SpecialInstructions", Type = typeof(String), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public virtual List<String> SpecialInstructionss
        {
            get { return this._SpecialInstructions; }
            set { if (value != null) this._SpecialInstructions = value; else this._SpecialInstructions = new List<String>(); }
        }

        //Taşıması gerçekleşen mallar hakkındaki bilgileri içerir.
        private List<GoodsItem> _goodsItems;
        [XmlElement("GoodsItem", Type = typeof(GoodsItem), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<GoodsItem> GoodsItems
        {
            get { return this._goodsItems; }
            set { if (value != null) this._goodsItems = value; else this._goodsItems = new List<GoodsItem>(); }
        }

        //Bu elemana taşıma faz bilgileri yazılır.
        private List<ShipmentStage> _shipmentStages;
        [XmlElement("ShipmentStage", Type = typeof(ShipmentStage), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<ShipmentStage> ShipmentStages
        {
            get { return this._shipmentStages; }
            set { if (value != null) this._shipmentStages = value; else this._shipmentStages = new List<ShipmentStage>(); }
        }

        //DespatchAdvice dokümanı içerisinde kullanımında taşıyıcı firma, fiili sevk tarihi ve asıl teslim tarihi bilgileri girilir.
        [XmlElement("Delivery", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Delivery Delivery { get; set; }

        //Taşıma üniteleri bilgisi girilir. Bknz. TransportHandlingUnit
        private List<TransportHandlingUnit> _transportHandlingUnits;
        [XmlElement("TransportHandlingUnit", Type = typeof(TransportHandlingUnit), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<TransportHandlingUnit> TransportHandlingUnits
        {
            get { return this._transportHandlingUnits; }
            set { if (value != null) this._transportHandlingUnits = value; else this._transportHandlingUnits = new List<TransportHandlingUnit>(); }
        }

        [XmlElement("ReturnAddress", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Address ReturnAddress { get; set; }

        [XmlElement("FirstArrivalPortLocation", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Location FirstArrivalPortLocation { get; set; }

        [XmlElement("LastExitPortLocation", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Location LastExitPortLocation { get; set; }
    }
}
