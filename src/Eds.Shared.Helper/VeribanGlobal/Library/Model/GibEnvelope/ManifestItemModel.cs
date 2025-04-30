using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibEnvelope
{
    [XmlType("ManifestItem")]
    public class ManifestItemModel
    {
        [XmlElement("MimeTypeQualifierCode")]
        public string MimeTypeQualifierCode { get; set; }

        [XmlElement("UniformResourceIdentifier")]
        public string UniformResourceIdentifier { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("LanguageCode")]
        public string LanguageCode { get; set; }
    }
}
