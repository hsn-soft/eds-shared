using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibAccount
{
    [XmlType("DataArea")]
    public class CancelUserAccountDataArea
    {
        //İçi boş bırakılmalıdır.
        private UACancel _cancel;
        [XmlElement("Cancel", Type = typeof(UACancel), Namespace = "http://www.openapplications.org/oagis/9")]
        public virtual UACancel Cancel
        {
            get { return this._cancel; }
            set { if (value != null) this._cancel = value; else this._cancel = new UACancel(); }
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
