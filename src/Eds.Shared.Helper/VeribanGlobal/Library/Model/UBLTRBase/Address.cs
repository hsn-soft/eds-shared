using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Bu eleman adres bilgilerinin tanımlanmasında kullanılacaktır.
    [XmlType("Address", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class Address
    {
        public Address()
        {
            CitySubdivisionName = string.Empty;
            CityName = string.Empty;
        }

        // Mahalle, meydan, bulvar, cadde, sokak ve küme evlere karşılık gelecek şekilde, standart sayısal eşdeğer olarak
        // yetkili makamlar tarafından verilmiş olan “sabit tanımlama numarası” girilebilecektir.
        [XmlElement("ID")]
        public CombineId ID { get; set; }

        //Posta Kutusu girilecektir.
        [XmlElement("Postbox")]
        public string Postbox { get; set; }

        // İç kapı numarası girilecektir.
        [XmlElement("Room")]
        public string Room { get; set; }

        // Meydan/bulvar/cadde/sokak/küme evler/site adı bilgileri girilecektir. 
        [XmlElement("StreetName")]
        public string StreetName { get; set; }

        //Blok adı girilebilecektir.
        [XmlElement("BlockName")]
        public string BlockName { get; set; }

        // Bina ve/veya blok adı girilebilecektir.
        [XmlElement("BuildingName")]
        public string BuildingName { get; set; }

        // Bina veya bloğa ait dış kapı numarası girilecektir.
        [XmlElement("BuildingNumber")]
        public string BuildingNumber { get; set; }

        //***ZORUNLU*** İlçe/semt adı bilgileri girilecektir. 
        [XmlElement("CitySubdivisionName")]
        public string CitySubdivisionName { get; set; }

        //***ZORUNLU*** İl adı girilecektir.
        [XmlElement("CityName")]
        public string CityName { get; set; }

        // Posta kod numarası girilecektir.
        [XmlElement("PostalZone")]
        public string PostalZone { get; set; }

        // Kasaba/köy/mezra/mevkii bilgileri girilecektir. 
        [XmlElement("Region")]
        public string Region { get; set; }

        //Mahalle adı girilecektir.  
        [XmlElement("District")]
        public string District { get; set; }

        //***ZORUNLU*** Ülke bilgisi girilecektir.
        [XmlElement("Country", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Country Country { get; set; }
    }
}
