using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    //Teslimat koşulları girilir.
    [XmlType("DeliveryTerms", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class DeliveryTerms
    {
        //Teslim koşulları girilir (örneğin CIF, FOB).
        [XmlElement("ID")]
        public CombineId ID { get; set; }

        //Teslimat koşulları serbest metin olarak girilir.
        [XmlElement("SpecialTerms")]
        public string SpecialTerms { get; set; }

        //Teslimat koşullarının kapsadığı tutar girilebilir.
        [XmlElement("Amount")]
        public virtual UblBaseCurrency Amount { get; set; }
    }
}
