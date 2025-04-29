using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Ödeme şeklinin girildiği elemandır.
    [XmlType("PaymentMeans", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class PaymentMeans
    {
        // Ödeme şeklinin kodu girilir. Bu eleman için UN/EDIFACT 4461 Ödeme Çeşitleri Kod Listesi kullanılacaktır.
        [XmlElement("PaymentMeansCode")]
        public string PaymentMeansCode { get; set; }

        // Son ödeme günü yıl-ay-gün formatında girilir.
        [XmlElement("PaymentDueDate")]
        public string PaymentDueDate { get; set; }

        // Ödeme kanalı kodu girilir.
        [XmlElement("PaymentChannelCode")]
        public string PaymentChannelCode { get; set; }

        // Ödeme ile ilgili açıklamalar serbest metin olarak girilir.
        [XmlElement("InstructionNote")]
        public string InstructionNote { get; set; }

        //Ödeme yapan tarafın hesap bilgileri girilir. Bknz. FinancialAccount.
        [XmlElement("PayerFinancialAccount", Type = typeof(FinancialAccount), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual FinancialAccount PayerFinancialAccount { get; set; }
        // Ödeme yapılacak hesap girilir.
        [XmlElement("PayeeFinancialAccount", Type = typeof(FinancialAccount), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual FinancialAccount PayeeFinancialAccount { get; set; }
    }
}
