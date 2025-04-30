using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Mal/Hizmet bilgilerinin girildiği bölümdür.
    [XmlType("Item", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class Item
    {
        // Mal/Hizmet hakkında açıklama serbest metin olarak girilir.
        [XmlElement("Description")]
        public string Description { get; set; }

        // Mal/hizmet adı serbest metin olarak girilir.
        [XmlElement("Name")]
        public string Name { get; set; }

        // Mal/hizmet Parti No
        [XmlElement("Keyword")]
        public string Keyword { get; set; }

        // Mal/hizmet marka adı serbest metin olarak girilir.
        [XmlElement("BrandName")]
        public string BrandName { get; set; }

        // Mal/hizmet model adı serbest metin olarak girilir.
        [XmlElement("ModelName")]
        public string ModelName { get; set; }

        // Alıcının mal/hizmete verdiği tanımlama bilgisi girilir.
        [XmlElement("BuyersItemIdentification", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public ItemIdentification BuyersItemIdentification { get; set; }

        // Satıcının mal/hizmete verdiği tanımlama bilgisi girilir.
        [XmlElement("SellersItemIdentification", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public ItemIdentification SellersItemIdentification { get; set; }

        // Üreticinin mal/hizmete verdiği tanımlama bilgisi girilir.
        [XmlElement("ManufacturersItemIdentification", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public ItemIdentification ManufacturersItemIdentification { get; set; }

        //Mal/hizmet için diğer kullanılabilecek sınıflandırma bilgileri girilebilir. Bknz. ItemIdentification
        private List<ItemIdentification> _additionalItemIdentifications;
        [XmlElement("AdditionalItemIdentification", Type = typeof(ItemIdentification), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<ItemIdentification> AdditionalItemIdentifications
        {
            get { return this._additionalItemIdentifications; }
            set { if (value != null) this._additionalItemIdentifications = value; else this._additionalItemIdentifications = new List<ItemIdentification>(); }
        }

        // Emtia sınıflandırma bilgisi girilir.
        private List<CommodityClassification> _commodityClassifications;
        [XmlElement("CommodityClassification", Type = typeof(CommodityClassification), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<CommodityClassification> CommodityClassifications
        {
            get { return this._commodityClassifications; }
            set { if (value != null) this._commodityClassifications = value; else this._commodityClassifications = new List<CommodityClassification>(); }
        }

        // gümrük takip
        private List<ItemInstance> _itemInstance;
        [XmlElement("ItemInstance", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<ItemInstance> ItemInstance
        {
            get { return this._itemInstance; }
            set { if (value != null) this._itemInstance = value; else this._itemInstance = new List<ItemInstance>(); }
        }
    }
}
