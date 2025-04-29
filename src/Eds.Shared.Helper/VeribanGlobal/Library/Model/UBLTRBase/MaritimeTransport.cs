using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    //Deniz Taşımacılığı
    [XmlType("MaritimeTransport", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class MaritimeTransport
    {
        //Geminin varsa IMO ve MMSI numarası girilir.
        [XmlElement("VesselID ")]
        public string VesselID { get; set; }

        // Geminin adı girilir.
        [XmlElement("VesselName ")]
        public string VesselName { get; set; }

        //Geminin radyo çağrı adı girilir.
        [XmlElement("RadioCallSignID ")]
        public string RadioCallSignID { get; set; }

        //Geminin ihtiyaçları bu elemana girilir.
        private List<String> _ShipsRequirements;
        [XmlElement("ShipsRequirements", Type = typeof(String))]
        public virtual List<String> ShipsRequirements
        {
            get { return this._ShipsRequirements; }
            set { if (value != null) this._ShipsRequirements = value; else this._ShipsRequirements = new List<String>(); }
        }

        //Geminin brüt ağırlığı girilir.
        [XmlElement("GrossTonnageMeasure")]
        public virtual BaseUnit GrossTonnageMeasure { get; set; }

        //Geminin net ağırlığı girilir. 
        [XmlElement("NetTonnageMeasure")]
        public virtual BaseUnit NetTonnageMeasure { get; set; }

        //Geminin kayıt dokümanı referansı girilir.Bknz.DocumentReference
        [XmlElement("RegistryCertificateDocumentReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual DocumentReference RegistryCertificateDocumentReference { get; set; }

        //Geminin kayıt limanı bilgisi girilir.Bknz.Location
        [XmlElement("RegistryPortLocation", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Location RegistryPortLocation { get; set; }
    }
}
