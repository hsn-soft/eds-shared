namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.FaultCodes
{
    //GIB WEB SERVICE SEND FAULT EXCEPTION CODES
    public sealed class ETicketServiceSendFaultExceptionCode
    {
        public readonly string _Code;
        public readonly string _Name;
        public readonly string _Description;

        private ETicketServiceSendFaultExceptionCode(string code, string name, string description)
        {
            this._Code = code;
            this._Name = name;
            this._Description = description;
        }

        public override string ToString()
        {
            return _Name;
        }

        public static readonly ETicketServiceSendFaultExceptionCode GONDERIM_BASARILI = new ETicketServiceSendFaultExceptionCode("500", "PAKET KUYRUGA EKLENDI", "");

        public static readonly ETicketServiceSendFaultExceptionCode SUNUCU_ERISIMI_BASARISIZ = new ETicketServiceSendFaultExceptionCode("5000", "GIB SUNUCU ERISIMI BASARISIZ", "");
        public static readonly ETicketServiceSendFaultExceptionCode GIB_TANIMLANAMAYAN_HATA = new ETicketServiceSendFaultExceptionCode("5001", "GIB TANIMLANAMAYAN HATA", "");
        public static readonly ETicketServiceSendFaultExceptionCode VERIBAN_SERVIS_HATA = new ETicketServiceSendFaultExceptionCode("5002", "VERIBAN SERVIS HATA", "");
        public static readonly ETicketServiceSendFaultExceptionCode VERIBAN_SISTEM_HATASI = new ETicketServiceSendFaultExceptionCode("5003", "VERIBAN SISTEM HATASI", "");

        public static ETicketServiceSendFaultExceptionCode GetETicketServiceSendFaultExceptionCode(string code)
        {
            var _CodeList = new List<ETicketServiceSendFaultExceptionCode>()
            {
                new ETicketServiceSendFaultExceptionCode("401", "GIB ERISIM YETKILENDIRME HATASI", "Erişim yetkilendirme sistemi hatası."),
                new ETicketServiceSendFaultExceptionCode("402", "KURUM KAYITLI DEGIL", "Paketin adındaki kurum kayıtlı bir e-Bilet kullanıcısı değil."),
                new ETicketServiceSendFaultExceptionCode("403", "PAKET BOS", "Attachment null."),
                new ETicketServiceSendFaultExceptionCode("404", "PAKET ADI BOS", "Paket adı boş."),
                new ETicketServiceSendFaultExceptionCode("405", "PAKET ICERIGI BOS", "Paket içeriği boş."),
                new ETicketServiceSendFaultExceptionCode("406", "PAKET MAKSIMUM BOYUTU GECMIS", "Paket maksimum boyutu geçmiş."),
                new ETicketServiceSendFaultExceptionCode("407", "DATAHANDLER HATASI", "Datahandler hatası."),
                new ETicketServiceSendFaultExceptionCode("408", "PAKET ADI GECERLI DEGIL", "Paket adı geçerli değil."),
                new ETicketServiceSendFaultExceptionCode("409", "PAKET GONDERME YETKISI YOK", "VERİBAN özel entegratörün bu firma için paket yollama yetkisi yok."),
                new ETicketServiceSendFaultExceptionCode("410", "GIB VERITABANI HATASI", "Veritabanı hatası. "),
                new ETicketServiceSendFaultExceptionCode("411", "PAKET DAHA ONCEDEN YUKLENMIS", "Paket sisteme daha önce yüklenmiş."),
                new ETicketServiceSendFaultExceptionCode("412", "GIB DISK HATASI", "Disk hatası. "),
                new ETicketServiceSendFaultExceptionCode("413", "GIB ISLEM KUYRUGU HATASI", "İşlem kuyruğu hatası. "),
            };

            return _CodeList.FirstOrDefault(o => o._Code == code);
        }
    }

    //GIB WEB SERVICE QUERY FAULT EXCEPTION CODES
    public sealed class ETicketServiceQueryFaultExceptionCode
    {
        public readonly string _Code;
        public readonly string _Name;
        public readonly string _Description;
        public readonly bool _DontReQuery;
        public readonly bool _Completed;

        private ETicketServiceQueryFaultExceptionCode(string code, string name, string description, bool dontReQuery = false, bool completed = false)
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

        public static readonly ETicketServiceQueryFaultExceptionCode SUNUCU_ERISIMI_BASARISIZ = new ETicketServiceQueryFaultExceptionCode("5000", "GIB SUNUCU ERISIMI BASARISIZ", "");
        public static readonly ETicketServiceQueryFaultExceptionCode GIB_TANIMLANAMAYAN_HATA = new ETicketServiceQueryFaultExceptionCode("5001", "GIB TANIMLANAMAYAN HATA", "", true);
        public static readonly ETicketServiceQueryFaultExceptionCode VERIBAN_SERVIS_HATA = new ETicketServiceQueryFaultExceptionCode("5002", "VERIBAN SERVIS HATA", "", true);

        public static ETicketServiceQueryFaultExceptionCode GetETicketServiceQueryFaultExceptionCode(string code)
        {
            var _CodeList = new List<ETicketServiceQueryFaultExceptionCode>()
            {
                new ETicketServiceQueryFaultExceptionCode("0", "PAKET BASARIYLA ISLENDI", "Paket başarıyla sistemde işlendi.",true,true),
                new ETicketServiceQueryFaultExceptionCode("1", "PAKET KONTROLU HATALI", "Paket kontrolü hatalı.",true),
                new ETicketServiceQueryFaultExceptionCode("-1", "PAKET ISLENIYOR", "Paket üzerinde işlem yapılıyor..."),
                new ETicketServiceQueryFaultExceptionCode("-2", "PAKET KUYRUKTA", "Paket kuyrukta bekliyor..."),
                new ETicketServiceQueryFaultExceptionCode("-3", "GIB SISTEM HATASI", "Gib sistemi üzerinde paket hata aldı.",true),

                new ETicketServiceQueryFaultExceptionCode("503", "GIB VERITABANI HATASI", "Veritabanı hatası."),
                new ETicketServiceQueryFaultExceptionCode("504", "PAKET ADI GECERLI DEGIL", "Paket adı geçerli değil.",true),
                new ETicketServiceQueryFaultExceptionCode("505", "PAKET SORGULAMA YETKISI YOK", "Paket bulundu fakat, VERİBAN özel entegratörün bu firma için paket durumunu sorgulama yetkisi yoktur.",true),
                new ETicketServiceQueryFaultExceptionCode("506", "PAKET BULUNAMADI", "Paket bulunamadı.",true),
            };

            return _CodeList.FirstOrDefault(o => o._Code == code);
        }
    }
}
