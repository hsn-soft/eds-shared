namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.FaultCodes
{
    //GIB WEB SERVICE FAULT EXCEPTION CODES
    public sealed class EInvoiceServiceFaultExceptionCode
    {
        private readonly int _Code;
        private readonly string _Name;
        private readonly string _Description;

        private EInvoiceServiceFaultExceptionCode(int code, string name, string description)
        {
            this._Code = code;
            this._Name = name;
            this._Description = description;
        }

        //EXTRA FAULT EXEPTION CODES
        public static readonly EInvoiceServiceFaultExceptionCode ZARF_KUYRUGA_EKLENDI = new EInvoiceServiceFaultExceptionCode(1000, "ZARF GIB KUYRUGA EKLENDI", "");

        public static readonly EInvoiceServiceFaultExceptionCode OZET_DEGERLER_ESIT_DEGIL = new EInvoiceServiceFaultExceptionCode(2000, "OZET DEGERLER ESIT DEGIL", "Alınan dosya ile gönderilen dosyanın özet (hash) değerleri eşit değil!");
        public static readonly EInvoiceServiceFaultExceptionCode ZARF_ID_SISTEMDE_MEVCUT = new EInvoiceServiceFaultExceptionCode(2001, "ZARF ID SISTEMDE MEVCUT", "Gönderilen zarf saha önceden sistem üzerinde arşivlenmiş!");
        public static readonly EInvoiceServiceFaultExceptionCode ZARF_ARSIVE_EKLENEMEDI = new EInvoiceServiceFaultExceptionCode(2002, "ZARF ARSIVE EKLENEMEDI", "Gönderilen zarf arşive eklenemedi!");
        public static readonly EInvoiceServiceFaultExceptionCode ZARF_KUYRUGA_EKLENEMEDI = new EInvoiceServiceFaultExceptionCode(2003, "ZARF KUYRUGA EKLENEMEDI", "Gönderilen zarf arşivden kuyruğa alınamadı!");
        public static readonly EInvoiceServiceFaultExceptionCode ZARF_ID_BULUNAMADI = new EInvoiceServiceFaultExceptionCode(2004, "ZARF ID BULUNAMADI", "Sorgulanan zarf sistem üzerinde bulunamadı!");
        public static readonly EInvoiceServiceFaultExceptionCode GIB_SISTEM_HATASI = new EInvoiceServiceFaultExceptionCode(2005, "GIB SISTEM HATASI", "");
        public static readonly EInvoiceServiceFaultExceptionCode GECERSIZ_ZARF_ADI = new EInvoiceServiceFaultExceptionCode(2006, "GECERSIZ ZARF ADI", "Gönderilen zarf adı Guid formatında olmalıdır!");
        public static readonly EInvoiceServiceFaultExceptionCode GIB_ISLEM_YETKINIZ_YOK = new EInvoiceServiceFaultExceptionCode(2007, "PAKET GÖNDERMEYE VE SORGULAMAYA YETKİNİZ GEÇİCİ OLARAK KALDIRILMIŞTIR.", "");

        public static readonly EInvoiceServiceFaultExceptionCode SUNUCU_ERISIMI_BASARISIZ = new EInvoiceServiceFaultExceptionCode(5000, "GIB SUNUCU ERISIMI BASARISIZ", "");
        public static readonly EInvoiceServiceFaultExceptionCode GIB_TANIMLANAMAYAN_HATA = new EInvoiceServiceFaultExceptionCode(5001, "GIB TANIMLANAMAYAN HATA", "");
        public static readonly EInvoiceServiceFaultExceptionCode VERIBAN_SERVIS_HATA = new EInvoiceServiceFaultExceptionCode(5002, "VERIBAN SERVIS HATA", "");
        public static readonly EInvoiceServiceFaultExceptionCode VERIBAN_SISTEM_HATASI = new EInvoiceServiceFaultExceptionCode(5003, "VERIBAN SISTEM HATASI", "");

        public override string ToString()
        {
            return _Name;
        }
        public string GetName()
        {
            return _Name;
        }
        public int GetCode()
        {
            return _Code;
        }
        public string GetDescription()
        {
            return _Description;
        }

        public static EInvoiceServiceFaultExceptionCode GetEInvoiceServiceFaultExceptionCode(int code)
        {
            var _CodeList = new List<EInvoiceServiceFaultExceptionCode>()
            {
                OZET_DEGERLER_ESIT_DEGIL,
                ZARF_ID_SISTEMDE_MEVCUT,
                ZARF_ARSIVE_EKLENEMEDI,
                ZARF_KUYRUGA_EKLENEMEDI,
                ZARF_ID_BULUNAMADI,
                GIB_SISTEM_HATASI,
                GECERSIZ_ZARF_ADI,
                GIB_ISLEM_YETKINIZ_YOK
            };

            var result = _CodeList.FirstOrDefault(o => o._Code == code);
            if (result == null) return GIB_TANIMLANAMAYAN_HATA;
            return result;
        }
    }
}
