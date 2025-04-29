namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes
{
    // Alıcı ve Satıcı’yı tanımlamak için “PartyIdentification/ID” elemanı kullanılmaktadır.
    // Gerçek kişi Alıcı ve Satıcılar için TC Kimlik Numarası, tüzel kişi alıcı ve satıcılar için ise
    // Vergi Kimlik Numarası girilmelidir. 
    // UBL-TR’de “PartyIdentification/ID” elemanının “schemeID” özelliği zorunludur. “schemeID” elemanı 
    // TC Kimlik Numarası ile Vergi Kimlik Numaralarına ek olarak aşağıda yer alan diğer değerleri alabilir:
    public sealed class PartySchemeIdType
    {
        private readonly byte _Value;
        private readonly string _Name;
        private readonly string _Description;

        public static readonly PartySchemeIdType VKN = new PartySchemeIdType(1, "VKN", "Vergi Kimlik Numarası");
        public static readonly PartySchemeIdType TCKN = new PartySchemeIdType(2, "TCKN", "TC Kimlik Numarası");
        public static readonly PartySchemeIdType HIZMETNO = new PartySchemeIdType(3, "HIZMETNO", "Hizmet No");
        public static readonly PartySchemeIdType MUSTERINO = new PartySchemeIdType(4, "MUSTERINO", "Müşteri No");
        public static readonly PartySchemeIdType TESISATNO = new PartySchemeIdType(5, "TESISATNO", "Tesisat No");
        public static readonly PartySchemeIdType TELEFONNO = new PartySchemeIdType(6, "TELEFONNO", "Telefon No");
        public static readonly PartySchemeIdType DISTRIBUTORNO = new PartySchemeIdType(7, "DISTRIBUTORNO", "Distribütör No");
        public static readonly PartySchemeIdType TICARETSICILNO = new PartySchemeIdType(8, "TICARETSICILNO", "Ticaret Sicil No");
        public static readonly PartySchemeIdType TAPDKNO = new PartySchemeIdType(9, "TAPDKNO", "TAPDK No");
        public static readonly PartySchemeIdType BAYINO = new PartySchemeIdType(10, "BAYINO", "Bayi No");
        public static readonly PartySchemeIdType ABONENO = new PartySchemeIdType(11, "ABONENO", "Abone No");
        public static readonly PartySchemeIdType SAYACNO = new PartySchemeIdType(12, "SAYACNO", "Sayaç No");
        public static readonly PartySchemeIdType URETICINO = new PartySchemeIdType(13, "URETICINO", "Üretici No");
        public static readonly PartySchemeIdType CIFTCINO = new PartySchemeIdType(14, "CIFTCINO", "Çiftçi No");
        public static readonly PartySchemeIdType IMALATCINO = new PartySchemeIdType(15, "IMALATCINO", "İmalatçı No");
        public static readonly PartySchemeIdType DOSYANO = new PartySchemeIdType(16, "DOSYANO", "Dosya No");
        public static readonly PartySchemeIdType HASTANO = new PartySchemeIdType(17, "HASTANO", "Hasta No");
        public static readonly PartySchemeIdType MERSISNO = new PartySchemeIdType(18, "MERSISNO", "Mersis No");
        public static readonly PartySchemeIdType EPDKNO = new PartySchemeIdType(19, "EPDKNO", "EPDK No");
        public static readonly PartySchemeIdType SUBENO = new PartySchemeIdType(20, "SUBENO", "Şube No");
        public static readonly PartySchemeIdType PASAPORTNO = new PartySchemeIdType(21, "PASAPORTNO", "Pasaport No");
        public static readonly PartySchemeIdType ARACIKURUMETIKET = new PartySchemeIdType(22, "ARACIKURUMETIKET", "Aracı Kurum Etiket No");
        public static readonly PartySchemeIdType ARACIKURUMVKN = new PartySchemeIdType(23, "ARACIKURUMVKN", "Aracı Kurum VKN No");
        public static readonly PartySchemeIdType GTBREFNO = new PartySchemeIdType(24, "GTB_REFNO", "GTB_REFNO");
        public static readonly PartySchemeIdType GTBGCBTESCILNO = new PartySchemeIdType(25, "GTB_GCB_TESCILNO", "GTB_GCB_TESCILNO");
        public static readonly PartySchemeIdType GTBFIILIIHRACATTARIHI = new PartySchemeIdType(26, "GTB_FIILI_IHRACAT_TARIHI", "GTB_FIILI_IHRACAT_TARIHI");
        public static readonly PartySchemeIdType ALIAS = new PartySchemeIdType(27, "ALIAS", "ALIAS");

        public static readonly List<PartySchemeIdType> TYPE_LIST = new List<PartySchemeIdType>
        {
            VKN, TCKN, HIZMETNO, MUSTERINO, TESISATNO, TELEFONNO, DISTRIBUTORNO,TICARETSICILNO,
            TAPDKNO, BAYINO, ABONENO, SAYACNO, URETICINO, CIFTCINO, IMALATCINO, DOSYANO, HASTANO,
            MERSISNO, EPDKNO, SUBENO, PASAPORTNO, ARACIKURUMETIKET, ARACIKURUMVKN, GTBREFNO, GTBGCBTESCILNO, GTBFIILIIHRACATTARIHI, ALIAS
        };

        private PartySchemeIdType(byte value, string name, string description)
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
