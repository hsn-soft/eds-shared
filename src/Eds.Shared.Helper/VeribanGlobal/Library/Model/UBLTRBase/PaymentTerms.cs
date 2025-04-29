using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Ödeme koşullarının girildiği elemandır. 
    [XmlType("PaymentTerms", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class PaymentTerms
    {
        // Ödeme koşulları ile ilgili açıklama serbest metin olarak girilir.
        [XmlElement("Note")]
        public string Note { get; set; }

        // Ödemenin gecikmesi durumunda uygulanacak ceza oranı numerik olarak girilir.
        [XmlElement("PenaltySurchargePercent")]
        public Decimal PenaltySurchargePercent { get; set; }

        // Ödemenin gecikmesi durumunda uygulanacak ceza tutarı numerik olarak girilir.
        [XmlElement("Amount")]
        public UblBaseCurrency Amount { get; set; }

        //Ödemenin gecikmesi durumunda uygulanacak ceza tutarı numerik olarak girilir.
        [XmlElement("PenaltyAmount")]
        public UblBaseCurrency PenaltyAmount { get; set; }

        //Son ödeme günü yıl-ay-gün formatında girilir.
        [XmlElement("PaymentDueDate")]
        public string PaymentDueDate { get; set; }

        //Ödeme dönemi girilir. Bknz. Period
        [XmlElement("SettlementPeriod", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Period SettlementPeriod { get; set; }
    }
}
