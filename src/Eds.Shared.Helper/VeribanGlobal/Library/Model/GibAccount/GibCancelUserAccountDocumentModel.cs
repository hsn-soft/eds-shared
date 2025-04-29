using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibAccount
{
    [XmlRoot("CancelUserAccount", Namespace = "http://www.hr-xml.org/3")]
    public class GibCancelUserAccountDocument
    {
        public GibCancelUserAccountDocument()
        {
            xmlns = new XmlSerializerNamespaces();
            xmlns.Add("", "http://www.hr-xml.org/3");
            xmlns.Add("xsd", "http://www.w3.org/2001/XMLSchema");
            xmlns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            xmlns.Add("oa", "http://www.openapplications.org/oagis/9");

            schemaLocation = "http://www.hr-xml.org/3 ../Developer/BODs/ProcessUserAccount.xsd";

            ReleaseId = "3.0";
        }

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces xmlns { get; set; }

        [XmlAttribute("schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string schemaLocation { get; set; }

        [XmlAttribute("releaseID")]
        public string ReleaseId { get; set; }

        private ApplicationArea _applicationArea;
        [XmlElement("ApplicationArea", Namespace = "http://www.openapplications.org/oagis/9")]
        public virtual ApplicationArea ApplicationArea
        {
            get { return this._applicationArea; }
            set { if (value != null) this._applicationArea = value; else this._applicationArea = new ApplicationArea(); }
        }

        private CancelUserAccountDataArea _dataArea;
        [XmlElement("DataArea")]
        public virtual CancelUserAccountDataArea DataArea
        {
            get { return this._dataArea; }
            set { if (value != null) this._dataArea = value; else this._dataArea = new CancelUserAccountDataArea(); }
        }
    }
}