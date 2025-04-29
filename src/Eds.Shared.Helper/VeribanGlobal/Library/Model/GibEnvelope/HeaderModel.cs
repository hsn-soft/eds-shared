using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibEnvelope
{
    [XmlType("StandardBusinessDocumentHeader")]
    public class HeaderModel
    {
        public HeaderModel()
        {
            this.HeaderVersion = "1.0";
        }

        // Versiyon değeri için “1.0” yazılacaktır.
        [XmlElement("HeaderVersion")]
        public string HeaderVersion { get; set; }

        // Zarf’ı gönderen tarafa ait bilgiler bu alana yazılacaktır.
        private List<PartnerModel> _senders;
        [XmlElement("Sender", Type = typeof(PartnerModel))]
        public virtual List<PartnerModel> Senders
        {
            get { return this._senders; }
            set { if (value != null) this._senders = value; else this._senders = new List<PartnerModel>(); }
        }

        // Zarf’ı alan tarafa ait bilgiler, bu alana yazılacaktır.
        private List<PartnerModel> _receivers;
        [XmlElement("Receiver", Type = typeof(PartnerModel))]
        public virtual List<PartnerModel> Receivers
        {
            get { return this._receivers; }
            set { if (value != null) this._receivers = value; else this._receivers = new List<PartnerModel>(); }
        }

        // Bu alana, Zarf’a ait bilgiler yazılacaktır.
        private DocumentIdentificationModel _documentIdentification;
        [XmlElement("DocumentIdentification")]
        public virtual DocumentIdentificationModel DocumentIdentification
        {
            get { return this._documentIdentification; }
            set { if (value != null) this._documentIdentification = value; else this._documentIdentification = new DocumentIdentificationModel(); }
        }

        private ManifestModel _manifest;
        [XmlElement("Manifest")]
        public virtual ManifestModel Manifest
        {
            get { return this._manifest; }
            set { if (value != null) this._manifest = value; else this._manifest = new ManifestModel(); }
        }

        private List<ScopeModel> _businessScopes;
        [XmlElement("BusinessScope", Type = typeof(ScopeModel))]
        public virtual List<ScopeModel> BusinessScopes
        {
            get { return this._businessScopes; }
            set { if (value != null) this._businessScopes = value; else this._businessScopes = new List<ScopeModel>(); }
        }
    }
}
