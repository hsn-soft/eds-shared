using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    [XmlType("CommodityClassification", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class CommodityClassification
    {
        [XmlElement("ItemClassificationCode")]
        public ItemIdentificationCode ItemClassificationCode { get; set; }
    }
    public class ItemIdentificationCode
    {
        [XmlAttribute("listID")]
        public string listID { get; set; }

        [XmlAttribute("listAgencyID")]
        public string listAgencyID { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
