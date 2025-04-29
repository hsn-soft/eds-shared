using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibEnvelope
{
    [XmlRoot(ElementName = "StandardBusinessDocument", Namespace = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader")]
    public class GibEnvelopeDocument
    {
        public GibEnvelopeDocument()
        {
            xmlns = new XmlSerializerNamespaces();
            xmlns.Add("sh", "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader");
            xmlns.Add("ef", "http://www.efatura.gov.tr/package-namespace");
            xmlns.Add("xsd", "http://www.w3.org/2001/XMLSchema");

            schemaLocation = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader PackageProxy_1_2.xsd";
        }

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces xmlns { get; set; }

        [XmlAttribute("schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string schemaLocation { get;  set; }

        private HeaderModel _Header;
        [XmlElement("StandardBusinessDocumentHeader", Namespace = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader")]
        public virtual HeaderModel Header
        {
            get { return this._Header; }
            set { if (value != null) this._Header = value; else this._Header = new HeaderModel(); }
        }

        private PackageModel _Package;
        [XmlElement("Package", Namespace = "http://www.efatura.gov.tr/package-namespace")]
        public virtual PackageModel Package
        {
            get { return this._Package; }
            set { if (value != null) this._Package = value; else this._Package = new PackageModel(); }
        }
    }
}
