using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    //Sıcaklık
    [XmlType("Temperature", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class Temperature
    {
        //***ZORUNLU***
        //Sıcaklık nitelik numarası girilir.
        [XmlElement("AttributeID")]
        public string AttributeID { get; set; }

        //***ZORUNLU***
        //Ölçüm girilir.
        [XmlElement("Measure")]
        public virtual BaseUnit Measure { get; set; }

        //Açıklama girilir
        private List<String> _Descriptions;
        [XmlElement("Description", Type = typeof(String))]
        public virtual List<String> Descriptions
        {
            get { return this._Descriptions; }
            set { if (value != null) this._Descriptions = value; else this._Descriptions = new List<String>(); }
        }
    }
}
