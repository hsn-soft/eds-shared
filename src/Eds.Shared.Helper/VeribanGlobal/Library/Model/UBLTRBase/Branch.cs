using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    //Şube bilgisi girilir.
    [XmlType("Branch", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class Branch
    {
        //Şube adı girilir.
        public string Name { get; set; }

        //Banka bilgisi girilir. Bknz.
        [XmlElement("FinancialInstitution", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual FinancialInstitution FinancialInstitution { get; set; }
    }
}
