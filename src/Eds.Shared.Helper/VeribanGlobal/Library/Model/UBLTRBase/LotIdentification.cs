using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    [XmlType("LotIdentification", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class LotIdentification
    {
        [XmlElement("LotNumberID")]
        public CombineId LotNumberID { get; set; }

        [XmlElement("ExpiryDate")]
        public string ExpiryDate { get; set; }
    }
}
