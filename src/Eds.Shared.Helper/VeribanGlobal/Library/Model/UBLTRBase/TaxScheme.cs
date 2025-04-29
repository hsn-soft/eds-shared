using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Bu eleman aracılığıyla vergi dairesi ile ilgili bilgiler verilebileceği gibi vergi ile ilgili bilgiler de verilebilir.
    [XmlType("TaxScheme", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class TaxScheme
    {
        // Bu eleman “Party” elemanı içerisinde kullanıldığında vergi dairesi adını içermektedir. Diğer elemanlar içerisinde
        // kullanımında Vergi Kod listesinde henüz yer almayan bir verginin söz konusu olması durumunda verginin adı girilecektir. 
        // Kontrolü Schematron kuralları ile yapılacaktır.
        [XmlElement("Name")]
        public string Name { get; set; }

        // Vergi Tipi Kodu girilecektir.
        [XmlElement("TaxTypeCode")]
        public string TaxTypeCode { get; set; }
    }
}
