using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    //Taşıma Yükleme-Boşaltma Üniteleri
    [XmlType("TransportHandlingUnit", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class TransportHandlingUnit
    {
        //Taşıma ünitesi numarası girilir.
        [XmlElement("ID")]
        public CombineId ID { get; set; }

        //Taşıma ünitesi tipi kodlu olarak girilir.
        [XmlElement("TransportHandlingUnitTypeCode")]
        public string TransportHandlingUnitTypeCode { get; set; }

        //Nasıl paketlenip taşınacağı kodlu olarak tanımlar.
        [XmlElement("HandlingCode")]
        public string HandlingCode { get; set; }

        //Nasıl paketlenip taşınacağıserbest metin olarak tanımlar.
        [XmlElement("HandlingInstructions")]
        public string HandlingInstructions { get; set; }

        //Ürün tehlikeli madde kategorisinde sayılıp sayılamayacağını gösteren bilgi.
        [XmlElement("HazardousRiskIndicator")]
        public string HazardousRiskIndicator { get; set; }

        //Toplam ürün miktarı girilir.
        [XmlElement("TotalGoodsItemQuantity")]
        public virtual BaseUnit TotalGoodsItemQuantity { get; set; }

        //Toplam paket miktarı girilir.
        [XmlElement("TotalPackageQuantity")]
        public virtual BaseUnit TotalPackageQuantity { get; set; }

        //Zarar bilgisi girilir.
        private List<String> _DamageRemarks;
        [XmlElement("DamageRemarks", Type = typeof(String))]
        public virtual List<String> DamageRemarks
        {
            get { return this._DamageRemarks; }
            set { if (value != null) this._DamageRemarks = value; else this._DamageRemarks = new List<String>(); }
        }

        //Takip numarası girilir.
        [XmlElement("TraceID")]
        public string TraceID { get; set; }

        //İçerdiği paket bilgileri girilir. Bknz. Package
        private List<Package> _actualPackages;
        [XmlElement("ActualPackage", Type = typeof(Package), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<Package> ActualPackages
        {
            get { return this._actualPackages; }
            set { if (value != null) this._actualPackages = value; else this._actualPackages = new List<Package>(); }
        }

        //TransportEquipment TransportEquipment  Ekipman bilgisi girilir.Bknz.TransportEquipment
        private List<TransportEquipment> _TransportEquipments;
        [XmlElement("TransportEquipment", Type = typeof(TransportEquipment), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<TransportEquipment> TransportEquipments
        {
            get { return this._TransportEquipments; }
            set { if (value != null) this._TransportEquipments = value; else this._TransportEquipments = new List<TransportEquipment>(); }
        }

        //TransportMeans TransportMeans  Taşıma şekli bilgisi girilir.Bknz.TransportMeans.
        private List<TransportMeans> _TransportMeans;
        [XmlElement("TransportMeans", Type = typeof(TransportMeans), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<TransportMeans> TransportMeans
        {
            get { return this._TransportMeans; }
            set { if (value != null) this._TransportMeans = value; else this._TransportMeans = new List<TransportMeans>(); }
        }

        //HazardousGoodsTransit HazardousGoodsTransit   Taşıma sırasındaki tehlikeli malların bilgisi girilir.Bknz.HazardousGoodsTransit
        private List<HazardousGoodsTransit> _HazardousGoodsTransits;
        [XmlElement("HazardousGoodsTransit", Type = typeof(HazardousGoodsTransit), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<HazardousGoodsTransit> HazardousGoodsTransits
        {
            get { return this._HazardousGoodsTransits; }
            set { if (value != null) this._HazardousGoodsTransits = value; else this._HazardousGoodsTransits = new List<HazardousGoodsTransit>(); }
        }

        //Ürünlerin diğer ölçümleri girilir. Bknz. Dimension
        private List<Dimension> _MeasurementDimensions;
        [XmlElement("MeasurementDimension", Type = typeof(Dimension), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<Dimension> MeasurementDimensions
        {
            get { return this._MeasurementDimensions; }
            set { if (value != null) this._MeasurementDimensions = value; else this._MeasurementDimensions = new List<Dimension>(); }
        }

        //Taşıma ünitesi için minimum sıcaklık girilir. Bknz.Temperature
        private List<Temperature> _MinimumTemperatures;
        [XmlElement("MinimumTemperature", Type = typeof(Temperature), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<Temperature> MinimumTemperatures
        {
            get { return this._MinimumTemperatures; }
            set { if (value != null) this._MinimumTemperatures = value; else this._MinimumTemperatures = new List<Temperature>(); }
        }

        //Taşıma ünitesi için maksimum sıcaklık girilir. Bknz.Temperature
        private List<Temperature> _MaximumTemperatures;
        [XmlElement("MaximumTemperature", Type = typeof(Temperature), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<Temperature> MaximumTemperatures
        {
            get { return this._MaximumTemperatures; }
            set { if (value != null) this._MaximumTemperatures = value; else this._MaximumTemperatures = new List<Temperature>(); }
        }

        //Yerde kapladığı alan bilgisi girilir.Bknz.Dimension
        [XmlElement("FloorSpaceMeasurementDimension", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Dimension FloorSpaceMeasurementDimension { get; set; }

        //Palette kapladığı alan blgisi girilir.Bknz.Dimension
        [XmlElement("PalletSpaceMeasurementDimension", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Dimension PalletSpaceMeasurementDimension { get; set; }

        // İlgili gönderi belgesine referans girilir.Bknz.DocumentReference
        private List<DocumentReference> _ShipmentDocumentReferences;
        [XmlElement("ShipmentDocumentReference", Type = typeof(DocumentReference), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<DocumentReference> DespatchDocumentReferences
        {
            get { return this._ShipmentDocumentReferences; }
            set { if (value != null) this._ShipmentDocumentReferences = value; else this._ShipmentDocumentReferences = new List<DocumentReference>(); }
        }

        //Gümrük numaralandırma bilgisi girilir. Bknz. CustomsDeclaration
        private List<CustomsDeclaration> _CustomsDeclarations;
        [XmlElement("CustomsDeclaration", Type = typeof(CustomsDeclaration), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<CustomsDeclaration> CustomsDeclarations
        {
            get { return this._CustomsDeclarations; }
            set { if (value != null) this._CustomsDeclarations = value; else this._CustomsDeclarations = new List<CustomsDeclaration>(); }
        }
    }
}
