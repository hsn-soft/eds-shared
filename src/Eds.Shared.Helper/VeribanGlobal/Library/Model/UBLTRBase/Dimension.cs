using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    //Boyut Bilgileri
    [XmlType("Dimension", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class Dimension
    {
        //***ZORUNLU***
        //Hangi özelliğin ölçüldüğü girilir.
        [XmlElement("AttributeID")]
        public string AttributeID { get; set; }

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

        //Minimum ölçüm girilir
        [XmlElement("MinimumMeasure")]
        public virtual BaseUnit MinimumMeasure { get; set; }

        /// <summary>
        ///
        /// </summary>
        [XmlElement("MaximumMeasure")]
        public virtual BaseUnit MaximumMeasure { get; set; }
    }
}
