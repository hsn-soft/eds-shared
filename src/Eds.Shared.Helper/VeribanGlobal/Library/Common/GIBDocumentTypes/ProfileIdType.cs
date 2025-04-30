namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes
{
    // Dokümanın ait olduğu süreci tanımlamak için “/Invoice/ProfileID” elemanı kullanılmaktadır. 
    // “ProfileID” elemanı aşağıdaki tabloda yer alan değerleri alabilir.
    public sealed class ProfileIdType
    {
        private readonly byte _Value;
        private readonly string _Name;
        private readonly string _Description;

        public static readonly ProfileIdType TEMELFATURA = new ProfileIdType(1, "TEMELFATURA", "Temel Fatura Profili");
        public static readonly ProfileIdType TICARIFATURA = new ProfileIdType(2, "TICARIFATURA", "Ticari Fatura Profili");
        public static readonly ProfileIdType IHRACAT = new ProfileIdType(3, "IHRACAT", "İhracat Faturası Profili");
        public static readonly ProfileIdType YOLCUBERABERFATURA = new ProfileIdType(4, "YOLCUBERABERFATURA", "Yolcu Beraberi Eşya İhracı Faturası");
        public static readonly ProfileIdType TEMELIRSALIYE = new ProfileIdType(5, "TEMELIRSALIYE", "Temel İrsaliye Profili");
        public static readonly ProfileIdType EARSIVFATURA = new ProfileIdType(6, "EARSIVFATURA", "E-arşiv Fatura Profili");
        public static readonly ProfileIdType OZELFATURA = new ProfileIdType(7, "OZELFATURA", "Özel Fatura Profili");
        public static readonly ProfileIdType KAMU = new ProfileIdType(8, "KAMU", "Kamu Fatura Profili");
        public static readonly ProfileIdType HKS = new ProfileIdType(9, "HKS", "Hal Komisyon Fatura Profili");
        public static readonly ProfileIdType STANDARTKODFATURA = new ProfileIdType(10, "STDKODFATURA", "Standart Kod Fatura");

        public static readonly List<ProfileIdType> TYPE_LIST = new List<ProfileIdType>
        {
            TEMELFATURA, TICARIFATURA, IHRACAT, YOLCUBERABERFATURA, TEMELIRSALIYE, EARSIVFATURA, OZELFATURA, KAMU, HKS, STANDARTKODFATURA
        };
        private ProfileIdType(byte value, string name, string description)
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
