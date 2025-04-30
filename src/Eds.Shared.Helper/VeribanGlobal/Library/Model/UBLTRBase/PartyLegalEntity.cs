using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    //Tarafın sicil bilgilerini veya merkez bilgilerini içerir.
    [XmlType("PartyLegalEntity", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class PartyLegalEntity
    {
        //Kayıt ismi girilir.
        [XmlElement("RegistrationName")]
        public string RegistrationName { get; set; }

        //Kayıt numarası girilir.
        [XmlElement("CompanyID")]
        public string CompanyID { get; set; }

        //Kayıt tarihi girilir.
        [XmlElement("RegistrationDate")]
        public string RegistrationDate { get; set; }

        //Tek bir kişiye ait olup olmadığını belirtir.
        [XmlElement("SolePrioprietorshipIndicator")]
        public string SolePrioprietorshipIndicator { get; set; }

        //Ödenmiş sermaye bilgisi girilir.
        [XmlElement("CorporateStockAmount")]
        public UblBaseCurrency CorporateStockAmount { get; set; }

        //Şirketin halka açık olup olmadığının göstergesi girilebilir.
        [XmlElement("FullyPaidSharesIndicator")]
        public string FullyPaidSharesIndicator { get; set; }

        //Kayıtlı olduğu yerin bilgilerini içerir. Bknz. CorporateRegistrationScheme
        [XmlElement("CorporateRegistrationScheme", Type = typeof(CorporateRegistrationScheme), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual CorporateRegistrationScheme CorporateRegistrationScheme { get; set; }

        //Merkez bilgilerini içerir. Bknz. Party
        [XmlElement("HeadParty", Type = typeof(Party), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Party HeadParty { get; set; }
    }
}
