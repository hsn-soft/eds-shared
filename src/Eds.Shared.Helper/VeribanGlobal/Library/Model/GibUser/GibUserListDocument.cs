using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibUser
{
    [XmlRoot("UserList")]
    public class GibUserListDocument
    {
        public GibUserListDocument()
        {

        }

        private List<GibUserInfo> _GibUserInfoList;
        [XmlElement("User", Type = typeof(GibUserInfo))]
        public virtual List<GibUserInfo> GibUserInfoList
        {
            get { return this._GibUserInfoList; }
            set { if (value != null) this._GibUserInfoList = value; else this._GibUserInfoList = new List<GibUserInfo>(); }
        }
    }

    [XmlType("User")]
    public class GibUserInfo
    {
        [XmlElement("Identifier")]
        public string Identifier { get; set; }

        [XmlElement("Title")]
        public string Title { get; set; }

        [XmlElement("Type")]
        public string Type { get; set; }

        [XmlElement("FirstCreationTime")]
        public string FirstCreationTime { get; set; }

        [XmlElement("AccountType")]
        public string AccountType { get; set; }

        private List<GibAliasDocument> _GIBAliasDocumentList;
        [XmlElement("Documents", Type = typeof(List<GibAliasDocument>))]
        public virtual List<GibAliasDocument> GIBAliasDocumentList
        {
            get { return this._GIBAliasDocumentList; }
            set { if (value != null) this._GIBAliasDocumentList = value; else this._GIBAliasDocumentList = new List<GibAliasDocument>(); }
        }
    }

    [XmlType("Document")]
    public class GibAliasDocument
    {
        [XmlAttribute("type")]
        public GibAliasDocumentTypes AliasType { get; set; }

        private List<GibAliasInfo> _GIBAliasInfoList;
        [XmlElement("Alias", Type = typeof(GibAliasInfo))]
        public virtual List<GibAliasInfo> GIBAliasInfoList
        {
            get { return this._GIBAliasInfoList; }
            set { if (value != null) this._GIBAliasInfoList = value; else this._GIBAliasInfoList = new List<GibAliasInfo>(); }
        }
    }

    [XmlType("Alias")]
    public class GibAliasInfo
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("CreationTime")]
        public string CreationTime { get; set; }

        [XmlElement("DeletionTime")]
        public string DeletionTime { get; set; }
    }

    [XmlType("AliasDocumentType")]
    public class AliasDocumentType
    {
        [XmlText()]
        public string Type { get; set; }

        [XmlAttribute("type")]
        public string AliasType { get; set; }
    }

    public enum GibAliasDocumentTypes
    {
        Invoice,
        DespatchAdvice,
    }
}