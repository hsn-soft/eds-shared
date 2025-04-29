using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    [XmlType("Package", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class Package
    {
        public Package()
        {
            ID = new CombineId();
        }

        //Paket numarası girilir.
        [XmlElement("ID")]
        public CombineId ID { get; set; }

        //Paket adedi girilir.
        [XmlElement("Quantity")]
        public virtual BaseUnit Quantity { get; set; }

        [XmlElement("ReturnableMaterialIndicator")]
        public string ReturnableMaterialIndicator { get; set; }

        [XmlElement("PackageLevelCode")]
        public string PackageLevelCode { get; set; }

        //Paketleme tipini belirtir.
        [XmlElement("PackagingTypeCode")]
        public string PackagingTypeCode { get; set; }

        private List<String> _PackagingMaterials;
        [XmlElement("PackagingMaterial", Type = typeof(String))]
        public virtual List<String> PackagingMaterials
        {
            get { return this._PackagingMaterials; }
            set { if (value != null) this._PackagingMaterials = value; else this._PackagingMaterials = new List<String>(); }
        }

        private List<Package> _ContainedPackages;
        [XmlElement("ContainedPackage", Type = typeof(Package), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<Package> ContainedPackages
        {
            get { return this._ContainedPackages; }
            set { if (value != null) this._ContainedPackages = value; else this._ContainedPackages = new List<Package>(); }
        }

        private List<GoodsItem> _goodsItems;
        [XmlElement("GoodsItem", Type = typeof(GoodsItem), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<GoodsItem> GoodsItems
        {
            get { return this._goodsItems; }
            set { if (value != null) this._goodsItems = value; else this._goodsItems = new List<GoodsItem>(); }
        }

        private List<Dimension> _MeasurementDimensions;
        [XmlElement("MeasurementDimension", Type = typeof(Dimension), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<Dimension> MeasurementDimensions
        {
            get { return this._MeasurementDimensions; }
            set { if (value != null) this._MeasurementDimensions = value; else this._MeasurementDimensions = new List<Dimension>(); }
        }
    }
}
