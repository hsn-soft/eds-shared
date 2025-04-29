using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    [XmlType("TransportMeans", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class TransportMeans
    {
        //Seyahat/Sefer numarası girilir.
        [XmlElement("JourneyID")]
        public string JourneyID { get; set; }

        //Kayıtlı olduğu ülke kodlu olarak girilir.
        [XmlElement("RegistrationNationalityID")]
        public string RegistrationNationalityID { get; set; }

        //Kayıtlı olduğu ülke serbest metin olarak girilir.
        [XmlElement("RegistrationNationality")]
        public string RegistrationNationality { get; set; }

        //Yön bilgisi kodlu olarak girilir.
        [XmlElement("DirectionCode")]
        public string DirectionCode { get; set; }

        //Taşıma şekli kodlu olarak girilir.
        [XmlElement("TransportMeansTypeCode")]
        public string TransportMeansTypeCode { get; set; }

        //Ticaret servisi kodlu olarak girilir.
        [XmlElement("TradeServiceCode")]
        public string TradeServiceCode { get; set; }

        //İstifleme bilgisi kodlu olarak girilir.Bknz.Stowage
        [XmlElement("Stowage", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Stowage Stowage { get; set; }

        //Hava taşımacılığı bilgisi girilir. Bknz.AirTransport
        [XmlElement("AirTransport", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual AirTransport AirTransport { get; set; }

        //Karayolu taşımacılığı bilgisi girilir. Bknz.RoadTransport
        [XmlElement("RoadTransport", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual RoadTransport RoadTransport { get; set; }

        //Demiryolu taşımacılığı bilgisi girilir. Bknz.RailTransport
        [XmlElement("RailTransport", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual RailTransport RailTransport { get; set; }

        //Deniz taşımacılığı bilgisi girilir. Bknz.MaritimeTransport
        [XmlElement("MaritimeTransport", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual MaritimeTransport MaritimeTransport { get; set; }

        //Bu araca sahip olan taraf bilgisi girilir.Bknz.Party
        [XmlElement("OwnerParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Party OwnerParty { get; set; }

        //Ölçüm bilgileri girilir.Bknz.Dimension
        private List<Dimension> _MeasurementDimensions;
        [XmlElement("MeasurementDimension", Type = typeof(Dimension), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<Dimension> MeasurementDimensions
        {
            get { return this._MeasurementDimensions; }
            set { if (value != null) this._MeasurementDimensions = value; else this._MeasurementDimensions = new List<Dimension>(); }
        }
    }
}
