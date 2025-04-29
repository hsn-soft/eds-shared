using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    //Gönderi Fazları
    [XmlType("ShipmentStage", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class ShipmentStage
    {
        //Aşama bilgisi numarasi girilir.
        [XmlElement("ID")]
        public CombineId ID { get; set; }

        //Bu taşıma fazının hangi modda (hava, deniz, kara) gerçekleştiği bilgisi girilir.
        [XmlElement("TransportModeCode")]
        public string TransportModeCode { get; set; }

        //Bu taşıma fazının nasıl bir araç ile gerçekleştiği bilgisi girilir(örneğin, kamyon, tır, gemi)
        [XmlElement("TransportMeansTypeCode")]
        public string TransportMeansTypeCode { get; set; }

        //Bu fazda gerçekleştirilen taşımanın güzergahı kodlu olarak girilir.
        [XmlElement("TransitDirectionCode")]
        public string TransitDirectionCode { get; set; }

        // Fazla ilgili detay bilgi girilir (örneğin güzergah)
        private List<String> _Instructions;
        [XmlElement("Instructions", Type = typeof(String))]
        public virtual List<String> Instructionss
        {
            get { return this._Instructions; }
            set { if (value != null) this._Instructions = value; else this._Instructions = new List<String>(); }
        }

        //Fazın gerçekleşme zamanı girilir. Bknz. Period
        [XmlElement("TransitPeriod", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Period TransitPeriod { get; set; }

        //Taşımada kullanılan vasıta hakkında detay bilgi girilir(örneğin kamyon plaka numarası) Bknz.TransportMeans
        [XmlElement("TransportMeans", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual TransportMeans TransportMeans { get; set; }

        //Şoför bilgileri girilir.Bknz.Person
        private List<Person> _DriverPersons;
        [XmlElement("DriverPerson", Type = typeof(Person), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<Person> DriverPersons
        {
            get { return this._DriverPersons; }
            set { if (value != null) this._DriverPersons = value; else this._DriverPersons = new List<Person>(); }
        }
    }
}
