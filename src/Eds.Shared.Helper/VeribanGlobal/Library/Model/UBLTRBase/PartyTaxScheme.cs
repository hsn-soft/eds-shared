using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Bu eleman aracılığıyla Tarafın(Party) vergi dairesi ile ilgili bilgiler verilir.
    [XmlType("PartyTaxScheme", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class PartyTaxScheme
    {
        // “TaxScheme/Name” elemanı bu kullanım için zorunludur. Vergi Dairesi adı “TaxScheme/Name” içine metin olarak
        // girilmesi gerekir. XSD’de “TaxScheme/Name” seçimli olarak tanımlanmasına rağmen bu zorunluluk kontrolü ikinci faz
        // doğrulamada Schematron kuralları ile gerçekleştirilecektir.
        [XmlElement("TaxScheme", Type = typeof(TaxScheme), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual TaxScheme TaxScheme { get; set; }
    }
}
