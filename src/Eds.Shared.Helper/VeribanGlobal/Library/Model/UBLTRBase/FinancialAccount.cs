using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Hesap bilgilerinin tutulduğu bölümdür.
    [XmlType("FinancialAccount", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class FinancialAccount
    {
        // Hesap numarası metin olarak girilir.
        public CombineId ID { get; set; }

        // Hesabın para birimi kodu girilir. "TRY",
        public string CurrencyCode { get; set; }

        // Ödeme ile ilgili açıklama serbest metin olarak girilir.
        public string PaymentNote { get; set; }

        //Hesabın bulunduğu banka ve şube bilgileri girilebilir. Bknz. Branch
        [XmlElement("FinancialInstitutionBranch", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Branch FinancialInstitutionBranch { get; set; }
    }
}
