using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Mal/hizmetin birim fiyatı girilir.
    [XmlType("Price", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class Price
    {
        // Mal/hizmetin birim fiyatı nümerik olarak girilir.
        [XmlElement("PriceAmount")]
        public UblBaseCurrency PriceAmount { get; set; }
    }
}
