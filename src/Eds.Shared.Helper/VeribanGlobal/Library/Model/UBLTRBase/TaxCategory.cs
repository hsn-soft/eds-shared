using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Belge üzerinde yer alan vergi türü, muafiyet ve istisnalara ilişkin bilgiler girilir.
    [XmlType("TaxCategory", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class TaxCategory
    {
        //Vergi muafiyet, istisna sebepleri bu alana kodlu olarak girilecektir. Bknz. Kod listeleri.
        [XmlElement("TaxExemptionReasonCode")]
        public string TaxExemptionReasonCode { get; set; }

        // Vergi muafiyet, istisna sebepleri bu alana serbest metin olarak girilecektir.
        [XmlElement("TaxExemptionReason")]
        public string TaxExemptionReason { get; set; }

        // Uygulanan vergi türü hakkında bilgiler girilir.
        [XmlElement("TaxScheme", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual TaxScheme TaxScheme { get; set; }
    }
}
