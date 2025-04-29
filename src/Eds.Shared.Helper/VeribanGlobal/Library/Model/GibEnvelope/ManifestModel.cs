using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibEnvelope
{
    [XmlType("Manifest")]
    public class ManifestModel
    {
        [XmlElement("NumberOfItems")]
        public int NumberOfItems { get; set; }

        private List<ManifestItemModel> _manifestItem;
        [XmlElement("ManifestItem", Type = typeof(ManifestItemModel))]
        public List<ManifestItemModel> ManifestItems
        {
            get { return this._manifestItem; }
            set { if (value != null) this._manifestItem = value; else this._manifestItem = new List<ManifestItemModel>(); }
        }
    }
}
