namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.FaultCodes
{
    //GIB WEB SERVICE SEND FAULT EXCEPTION CODES
    public sealed class EArchiveServiceSendFaultExceptionCode
    {
        public readonly string _Code;
        public readonly string _Name;
        public readonly string _Description;

        private EArchiveServiceSendFaultExceptionCode(string code, string name, string description)
        {
            this._Code = code;
            this._Name = name;
            this._Description = description;
        }

        public override string ToString()
        {
            return _Name;
        }

        public static readonly EArchiveServiceSendFaultExceptionCode GONDERIM_BASARILI = new EArchiveServiceSendFaultExceptionCode("000", "DOSYA KAYDEDILDI", "");

        public static readonly EArchiveServiceSendFaultExceptionCode SUNUCU_ERISIMI_BASARISIZ = new EArchiveServiceSendFaultExceptionCode("5000", "GIB SUNUCU ERISIMI BASARISIZ", "");
        public static readonly EArchiveServiceSendFaultExceptionCode GIB_TANIMLANAMAYAN_HATA = new EArchiveServiceSendFaultExceptionCode("5001", "GIB TANIMLANAMAYAN HATA", "");
        public static readonly EArchiveServiceSendFaultExceptionCode VERIBAN_SERVIS_HATA = new EArchiveServiceSendFaultExceptionCode("5002", "VERIBAN SERVIS HATA", "");
        public static readonly EArchiveServiceSendFaultExceptionCode VERIBAN_SISTEM_HATASI = new EArchiveServiceSendFaultExceptionCode("5003", "VERIBAN SISTEM HATASI", "");

        public static EArchiveServiceSendFaultExceptionCode GetEArchiveServiceSendFaultExceptionCode(string code)
        {
            var _CodeList = new List<EArchiveServiceSendFaultExceptionCode>()
            {
                new EArchiveServiceSendFaultExceptionCode("000", "DOSYA KAYDEDILDI", "Dosya Kaydedildi"),
                new EArchiveServiceSendFaultExceptionCode("001", "GONDERICI YETKISI YOK", "Gönderici imza yetkisi yok"),
                new EArchiveServiceSendFaultExceptionCode("002", "PAKET BOS OLAMAZ", "Attachment null olamaz"),
                new EArchiveServiceSendFaultExceptionCode("003", "PAKET ADI BOS OLAMAZ", "Paket ID boş olamaz"),
                new EArchiveServiceSendFaultExceptionCode("004", "PAKET DAHA ONCE GONDERILMIS", "Paket daha önceden gönderilmiş"),
                new EArchiveServiceSendFaultExceptionCode("005", "PAKET DOSYASI BOS OLAMAZ", "Paket dosyası boş olamaz"),
                new EArchiveServiceSendFaultExceptionCode("006", "DOSYA BULUNAMADI", "Dosya bulunamadı + (Açıklama)"),
                new EArchiveServiceSendFaultExceptionCode("007", "GIB I/O ERROR", "IO Error"),
                new EArchiveServiceSendFaultExceptionCode("008", "GIB ERROR", "Error"),
                new EArchiveServiceSendFaultExceptionCode("009", "DOSYA ISMI HATALI", "Dosya ismi 36 + .zip 40 karakter olmalıdır"),
                new EArchiveServiceSendFaultExceptionCode("010", "DOSYA ISMI UZANTISI HATALI", "Dosya ismi zip uzantılı olmalıdır"),
            };

            return _CodeList.FirstOrDefault(o => o._Code == code);
        }
    }

    //GIB WEB SERVICE QUERY FAULT EXCEPTION CODES
    public sealed class EArchiveServiceQueryFaultExceptionCode
    {
        public readonly string _Code;
        public readonly string _Name;
        public readonly string _Description;
        public readonly bool _DontReQuery;
        public readonly bool _Completed;

        private EArchiveServiceQueryFaultExceptionCode(string code, string name, string description, bool dontReQuery = false, bool completed = false)
        {
            this._Code = code;
            this._Name = name;
            this._Description = description;
            this._DontReQuery = dontReQuery;
            this._Completed = completed;
        }

        public override string ToString()
        {
            return _Name;
        }

        public static readonly EArchiveServiceQueryFaultExceptionCode SUNUCU_ERISIMI_BASARISIZ = new EArchiveServiceQueryFaultExceptionCode("5000", "GIB SUNUCU ERISIMI BASARISIZ", "");
        public static readonly EArchiveServiceQueryFaultExceptionCode GIB_TANIMLANAMAYAN_HATA = new EArchiveServiceQueryFaultExceptionCode("5001", "GIB TANIMLANAMAYAN HATA", "", true);
        public static readonly EArchiveServiceQueryFaultExceptionCode VERIBAN_SERVIS_HATA = new EArchiveServiceQueryFaultExceptionCode("5002", "VERIBAN SERVIS HATA", "", true);

        public static EArchiveServiceQueryFaultExceptionCode GetEArchiveServiceQueryFaultExceptionCode(string code)
        {
            var _CodeList = new List<EArchiveServiceQueryFaultExceptionCode>()
            {
                new EArchiveServiceQueryFaultExceptionCode("1", "GONDERICI YETKISI YOK", "Gönderici (SOAP Mesajı imzalayan Şajıs/Kurum) VKN/TCKN'nin sorgulama yetkisi yok.",true),
                new EArchiveServiceQueryFaultExceptionCode("2", "PAKET BULUNAMADI", "Sorgulanan Numaralı Paket bulunamadı.",true),

                new EArchiveServiceQueryFaultExceptionCode("10", "DOSYA KUYRUKTA", "Dosya kuyrukta"),
                new EArchiveServiceQueryFaultExceptionCode("15", "PAKET ISLENIYOR", "Paket işlenmeye başlandı"),

                new EArchiveServiceQueryFaultExceptionCode("30", "PAKET BASARIYLA ISLENDI", "Paket başarıyla sistemde işlendi.",true,true),

                new EArchiveServiceQueryFaultExceptionCode("150", "PAKET ONCEDEN YUKLENDI", "Aynı Paket ID",true),
                new EArchiveServiceQueryFaultExceptionCode("151", "DOSYA OKUNAMADI", "Dosya okunamadı",true),
                new EArchiveServiceQueryFaultExceptionCode("152", "ZIP ACILAMADI", "Zip dosyası açılamadı",true),
                new EArchiveServiceQueryFaultExceptionCode("153", "ZIP BIR DOSYA ICERMELI", "Pakette birden fazla dosya var",true),
                new EArchiveServiceQueryFaultExceptionCode("154", "IMZA DOGRULANAMADI", "İmza doğrulanamadı",true),
                new EArchiveServiceQueryFaultExceptionCode("155", "XML YAPISI BOZUK", "Başlık XML validasyon hatası açıklaması",true),
                new EArchiveServiceQueryFaultExceptionCode("156", "GECERSIZ XML VERSIYON", "Uyumsuz doküman versiyon tipi",true),
                new EArchiveServiceQueryFaultExceptionCode("157", "XML DOGRULAMA HATASI", "XML validasyon hatası açıklaması",true),
                new EArchiveServiceQueryFaultExceptionCode("158", "BASLIK ID ILE PAKET ID FARKLI", "Başlık paket ID ile paket adlandırılması uyumsuz",true),
                new EArchiveServiceQueryFaultExceptionCode("159", "IMZA BILGISI BULUNAMADI", "İmza bulunamadı",true),
                new EArchiveServiceQueryFaultExceptionCode("160", "GIB XML VERITABANI HATASI", "XML veritabanı yazım hatası",true),

                new EArchiveServiceQueryFaultExceptionCode("161", "GIB OKUMA HATASI", "Okuma hatası"),
                new EArchiveServiceQueryFaultExceptionCode("162", "GIB VERITABANI HATASI", "Veritabanı hatası"),
                new EArchiveServiceQueryFaultExceptionCode("163", "GIB SISTEM HATASI", "Sistem hatası",true),

                new EArchiveServiceQueryFaultExceptionCode("164", "PAKETTE DOSYA YOK", "Pakette dosya yok",true),
                new EArchiveServiceQueryFaultExceptionCode("165", "PAKET DOSYA BOYUTU HATASI", "Max dosya boyutu hatası",true),
                new EArchiveServiceQueryFaultExceptionCode("166", "SOAP ILE PAKET IMZASI UYUSMUYOR", "İstek imzası ve paket imzası uyuşmuyor",true),
                new EArchiveServiceQueryFaultExceptionCode("167", "IMZA SAHIBI ILE RAPORTOR KIMLIGI FARKLI", "İmza sahibi ile hazırlayan VKN/TCKN uyuşmuyor",true),
                new EArchiveServiceQueryFaultExceptionCode("168", "GONDERICI BU MUKELLEFE AIT YETKISI YOK", "Göndericinin bu mükellefin serbest meslek raporunu işletme yetkisi yok.",true),
                new EArchiveServiceQueryFaultExceptionCode("169", "RAPOR VERI UYUMSUZLUĞU (ESKI VE YENI)", "Yeni nesil ÖKC mali rapor bilgileri eski nesil rapor alanında (mRapor) gönderilemez.",true),
            };

            return _CodeList.FirstOrDefault(o => o._Code == code);
        }
    }
}

//getUserList
//001 Gönderici yetkisi yok
//002 Hatalı User List Parametresi.Beklenen: XML ya da CSV
//006 Dosya bulunamadı.