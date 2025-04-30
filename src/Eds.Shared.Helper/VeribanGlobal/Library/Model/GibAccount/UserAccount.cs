using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibAccount
{
    [XmlType("UserAccount", Namespace = "http://www.hr-xml.org/3")]
    public class UserAccount
    {
        [XmlElement("UserID")]
        public CombineId UserId { get; set; }

        [XmlElement("PersonName")]
        public PersonName PersonName { get; set; }

        [XmlElement("UserRole")]
        public UserRole UserRole { get; set; }

        [XmlElement("AuthorizedWorkScope")]
        public AuthorizedWorkScope AuthorizedWorkScope { get; set; }

        [XmlElement("AccountConfiguration")]
        public AccountConfiguration AccountConfiguration { get; set; }
    }

    [XmlType("PersonName")]
    public class PersonName
    {
        // Tüzel kişilik ise ticari sicil gazetesinde bulunan şirket ünvanı, gerçek kişi ise Nüfus kayıtlarında bulunan Adı ve Soyadı yazılmalıdır. 
        [XmlElement("FormattedName")]
        public string FormattedName { get; set; }

        // Gerçek Kişiler için kişinin ilk adı yazılacaktır. Tüzel kişilikler için kullanılmasına gerek yoktur.
        [XmlElement("GivenName", Type = typeof(String), Namespace = "http://www.openapplications.org/oagis/9")]
        //[XmlElement("GivenName")]
        public string GivenName { get; set; }

        // Gerçek Kişiler için kişinin ortanca adı yazılacaktır. Tüzel kişilikler için kullanılmayacaktır.
        [XmlElement("MiddleName")]
        public string MiddleName { get; set; }

        // Gerçek Kişiler için kişinin soyadı yazılmalıdır. Tüzel kişilikler için kullanılmasına gerek yoktur.
        [XmlElement("FamilyName")]
        public string FamilyName { get; set; }
    }

    [XmlType("UserRole")]
    public class UserRole
    {
        // Gönderici birim rolü için GB, Posta kutusu rolü için PK yazılmalıdır.
        [XmlElement("RoleCode")]
        public string RoleCode { get; set; }

        // Gönderici birim rolü için “Gönderici Birim”, Posta kutusu rolü için “Posta Kutusu” sabit değeri yazılmalıdır. 
        [XmlElement("RoleName")]
        public string RoleName { get; set; }
    }

    [XmlType("AuthorizedWorkScope")]
    public class AuthorizedWorkScope
    {
        // Kullanıcı için belirlenen etiket (alias) değeri yazılmalıdır. 
        [XmlElement("WorkScopeCode")]
        public string WorkScopeCode { get; set; }

        // Kullanıcı için belirlenen etiket (alias)'in açıklaması yazılmalıdır. WorkScopeCode ile aynı değer yazılabilir.
        [XmlElement("WorkScopeName")]
        public string WorkScopeName { get; set; }
    }

    [XmlType("AccountConfiguration")]
    public class AccountConfiguration
    {
        // İşlem yapılan kullanıcı kamu ise 1, özel ise 2 değeri girilir. Sadece fatura saklama hizmeti verilecek olanlar için işlem 
        // yapılan kullanıcı kamu ise 11, özel ise 12 değeri girilir.
        [XmlElement("UserOptionCode")]
        public string UserOptionCode { get; set; }
    }
}
