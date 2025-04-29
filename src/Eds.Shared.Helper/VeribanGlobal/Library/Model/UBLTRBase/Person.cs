using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Şahısla ilgili bilgiler girilecektir.
    [XmlType("Person", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class Person
    {
        // Şahsın ilk adı girilecektir.
        public string FirstName { get; set; }

        // Şahsın soyadı girilecektir.
        public string FamilyName { get; set; }

        // Şahsın ünvanı girilecektir.
        public string Title { get; set; }

        // Şahsın diğer isimleri yazılacaktır.
        public string MiddleName { get; set; }

        // Şahsın adının ön eki varsa bu alana girilecektir.
        public string NameSuffix { get; set; }

        //Şahsın milliyeti girilecektir.
        public string NationalityID { get; set; }

        //Şahsın hesap bilgileri girilecektir. Bknz. FinancialAccount
        [XmlElement("FinancialAccount", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual FinancialAccount FinancialAccount { get; set; }

        //Şahsın kimlik dokümanına (Örneğin pasaport numarası buraya yazılacaktır) referans girilebilecektir. Bknz. DocumentReference.
        [XmlElement("IdentityDocumentReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual DocumentReference IdentityDocumentReference { get; set; }
    }
}
