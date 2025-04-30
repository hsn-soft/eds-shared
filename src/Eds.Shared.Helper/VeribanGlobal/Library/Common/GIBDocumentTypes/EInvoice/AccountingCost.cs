namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes.EInvoice
{
    public sealed class AccountingCost
    {
        private readonly byte _Value;
        private readonly string _Name;
        private readonly string _Description;

        public static readonly AccountingCost SAGLIK_ECZ = new AccountingCost(1, "SAGLIK_ECZ", "Eczane");
        public static readonly AccountingCost SAGLIK_HAS = new AccountingCost(2, "SAGLIK_HAS", "Hastane");
        public static readonly AccountingCost SAGLIK_OPT = new AccountingCost(3, "SAGLIK_OPT", "Optik");
        public static readonly AccountingCost SAGLIK_MED = new AccountingCost(4, "SAGLIK_MED", "Medikal");
        public static readonly AccountingCost ABONELIK = new AccountingCost(5, "ABONELIK", "Abonelik");
        public static readonly AccountingCost MAL_HIZMET = new AccountingCost(6, "MAL_HIZMET", "Mal Hizmet");
        public static readonly AccountingCost DIGER = new AccountingCost(7, "DIGER", "Diğer");

        public static readonly List<AccountingCost> TYPE_LIST = new List<AccountingCost>
        {
            SAGLIK_ECZ, SAGLIK_HAS, SAGLIK_OPT, SAGLIK_MED, ABONELIK, MAL_HIZMET , DIGER
        };

        private AccountingCost(byte value, string name, string description)
        {
            this._Value = value;
            this._Name = name;
            this._Description = description;
        }

        public override string ToString()
        {
            return _Name;
        }

        public byte GetValue()
        {
            return _Value;
        }

        public string GetName()
        {
            return _Name;
        }

        public string GetDescription()
        {
            return _Description;
        }
    }
}
