namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes
{
    // Faturalar düzenleme amaçlarına göre iki tipte tanımlanmıştır. Her türlü mal ve
    // hizmet satışı ile ilgili düzenlenen faturalar için “SATIS” değeri; bir malın iadesi
    // amacıyla alıcı tarafından düzenlenen faturalar ise “IADE” değerini alacaktır.
    public sealed class InvoiceTypeCode
    {
        private readonly byte _Value;
        private readonly string _Name;
        private readonly string _Description;

        public static readonly InvoiceTypeCode SATIS = new InvoiceTypeCode(1, "SATIS", "Satış");
        public static readonly InvoiceTypeCode IADE = new InvoiceTypeCode(2, "IADE", "İade");
        public static readonly InvoiceTypeCode TEVKIFAT = new InvoiceTypeCode(3, "TEVKIFAT", "Tevkifat");
        public static readonly InvoiceTypeCode ISTISNA = new InvoiceTypeCode(4, "ISTISNA", "İstisna");
        public static readonly InvoiceTypeCode ARACTESCIL = new InvoiceTypeCode(5, "ARACTESCIL", "Araç Tescil");
        public static readonly InvoiceTypeCode OZELMATRAH = new InvoiceTypeCode(6, "OZELMATRAH", "Özel Matrah");
        public static readonly InvoiceTypeCode IHRACKAYITLI = new InvoiceTypeCode(7, "IHRACKAYITLI", "İhraç Kayıtlı");
        public static readonly InvoiceTypeCode SGK = new InvoiceTypeCode(8, "SGK", "Sgk");
        public static readonly InvoiceTypeCode SATISIPTALIADE = new InvoiceTypeCode(9, "SATISIPTALIADE", "Satış İptal İade");
        public static readonly InvoiceTypeCode KOMISYONCU = new InvoiceTypeCode(10, "KOMISYONCU", "Komisyoncu");

        public static readonly List<InvoiceTypeCode> TYPE_LIST = new List<InvoiceTypeCode>
        {
            SATIS, IADE, TEVKIFAT, ISTISNA, ARACTESCIL, OZELMATRAH, IHRACKAYITLI, SGK, SATISIPTALIADE, KOMISYONCU
        };

        private InvoiceTypeCode(byte value, string name, string description)
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
