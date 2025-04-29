using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibAccount
{
    [XmlType("DataArea")]
    public class ProcessUserAccountDataArea
    {
        //İçi boş bırakılmalıdır.
        private UAProcess _process;
        [XmlElement("Process", Type = typeof(UAProcess), Namespace = "http://www.openapplications.org/oagis/9")]
        public virtual UAProcess Process
        {
            get { return this._process; }
            set { if (value != null) this._process = value; else this._process = new UAProcess(); }
        }

        private List<UserAccount> _userAccounts;
        [XmlElement("UserAccount", Type = typeof(UserAccount), Namespace = "http://www.hr-xml.org/3")]
        public virtual List<UserAccount> UserAccounts
        {
            get { return this._userAccounts; }
            set { if (value != null) this._userAccounts = value; else this._userAccounts = new List<UserAccount>(); }
        }

    }
}
