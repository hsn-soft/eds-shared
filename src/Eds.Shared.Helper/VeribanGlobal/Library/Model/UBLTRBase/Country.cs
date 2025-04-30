using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Ülke bilgisi girilecektir.
    [XmlType("Country", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class Country
    {
        public Country()
        {
            this.IdentificationCode = "TR";
            this.Name = String.Empty;
        }

        // Ülkeleri tanımlamak için kullanılan kodlu elemandır. Bu eleman değer
        // kümesini ISO 3166-1-alpha-2 Ülke Kodları listesinden almalıdır.
        [XmlElement("IdentificationCode")]
        public string IdentificationCode { get; set; }

        // Ülkeleri tanımlamak için kullanılan metin elemanıdır.
        [XmlElement("Name")]
        public string Name { get; set; }
    }
}
