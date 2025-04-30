using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    //İstif Yeri
    [XmlType("Stowage", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class Stowage
    {
        //İstif yeri mekan numarası girilir.
        [XmlElement("LocationID")]
        public string LocationID { get; set; }

        //Mekan bilgisi detaylı olarak girilir.
        private List<Location> _Locations;
        [XmlElement("Location", Type = typeof(Location))]
        public virtual List<Location> Locations
        {
            get { return this._Locations; }
            set { if (value != null) this._Locations = value; else this._Locations = new List<Location>(); }
        }

        //İstif yeri ölçüleri girilir. Bknz. Dimension
        private List<Dimension> _MeasurementDimensions;
        [XmlElement("MeasurementDimension", Type = typeof(Dimension), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<Dimension> MeasurementDimensions
        {
            get { return this._MeasurementDimensions; }
            set { if (value != null) this._MeasurementDimensions = value; else this._MeasurementDimensions = new List<Dimension>(); }
        }
    }
}
