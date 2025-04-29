using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    //Banka bilgisi girilebilir.
    [XmlType("FinancialInstitution", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class FinancialInstitution
    {
        //Banka ismi girilebilir.
        public string Name { get; set; }
    }
}
