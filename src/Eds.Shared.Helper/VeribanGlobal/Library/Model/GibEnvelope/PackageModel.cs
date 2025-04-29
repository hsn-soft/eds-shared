using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibEnvelope
{
    [XmlType("Package", Namespace = "")]
    public class PackageModel
    {
        private List<ElementModel> _Elements;
        [XmlElement("Elements", Type = typeof(ElementModel))]
        public virtual List<ElementModel> Elements
        {
            get { return this._Elements; }
            set { if (value != null) this._Elements = value; else this._Elements = new List<ElementModel>(); }
        }
    }
}
