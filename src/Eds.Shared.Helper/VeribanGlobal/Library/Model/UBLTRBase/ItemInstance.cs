using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    [XmlType("ItemInstance", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class ItemInstance
    {
        [XmlElement("ProductTraceID")]
        public string ProductTraceID { get; set; }

        [XmlElement("ManufactureDate")]
        public string ManufactureDate { get; set; }

        [XmlElement("ManufactureTime")]
        public string ManufactureTime { get; set; }

        [XmlElement("BestBeforeDate")]
        public string BestBeforeDate { get; set; }

        [XmlElement("RegistrationID")]
        public CombineId RegistrationID { get; set; }

        [XmlElement("SerialID")]
        public CombineId SerialID { get; set; }

        private List<ItemProperty> _additionalItemProperties;
        [XmlElement("AdditionalItemProperty", Type = typeof(ItemProperty), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<ItemProperty> AdditionalItemProperties
        {
            get { return this._additionalItemProperties; }
            set { if (value != null) this._additionalItemProperties = value; else this._additionalItemProperties = new List<ItemProperty>(); }
        }


        [XmlElement("LotIdentification", Type = typeof(LotIdentification), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual LotIdentification LotIdentification { get; set; }

    }
}
