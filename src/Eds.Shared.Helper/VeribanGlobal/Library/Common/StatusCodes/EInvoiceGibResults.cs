namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.StatusCodes
{
    public class EInvoiceGibResults
    {
        private readonly List<EInvoiceGibResult> results = null;

        public EInvoiceGibResults()
        {
            results = new List<EInvoiceGibResult>();
            results.Add(new EInvoiceGibResult("1000", "ZARF KUYRUGA EKLENDI", true));
            results.Add(new EInvoiceGibResult("1100", "ZARF ISLENIYOR", true));
            results.Add(new EInvoiceGibResult("1110", "ZIP DOSYASI DEGIL"));
            results.Add(new EInvoiceGibResult("1111", "ZARF ID UZUNLUGU GECERSIZ"));
            results.Add(new EInvoiceGibResult("1120", "ZARF ARSIVDEN_KOPYALANAMADI"));
            results.Add(new EInvoiceGibResult("1130", "ZIP ACILAMADI"));
            results.Add(new EInvoiceGibResult("1131", "ZIP BIR DOSYA ICERMELI"));
            results.Add(new EInvoiceGibResult("1132", "XML DOSYASI DEGIL"));
            results.Add(new EInvoiceGibResult("1133", "ZARF ID VE XML DOSYASININ ADI AYNI OLMALI"));
            results.Add(new EInvoiceGibResult("1140", "DOKUMAN AYRISTIRILAMADI"));
            results.Add(new EInvoiceGibResult("1141", "ZARF ID YOK"));
            results.Add(new EInvoiceGibResult("1142", "ZARF ID VE ZIP DOSYASI ADI AYNI OLMALI"));
            results.Add(new EInvoiceGibResult("1143", "GECERSIZ VERSIYON"));
            results.Add(new EInvoiceGibResult("1150", "SCHEMATRON KONTROL SONUCU HATALI"));
            results.Add(new EInvoiceGibResult("1160", "XML SEMA KONTROLUNDEN GECEMEDI"));
            results.Add(new EInvoiceGibResult("1161", "IMZA SAHIBI TCKN VKN ALINAMADI"));
            results.Add(new EInvoiceGibResult("1162", "IMZA KAYDEDILEMEDI"));
            results.Add(new EInvoiceGibResult("1163", "GONDERILEN ZARF SISTEMDE DAHA ONCE KAYITLI OLAN BIR FATURAYI ICERMEKTEDIR."));
            results.Add(new EInvoiceGibResult("1170", "YETKI KONTROL EDILEMEDI"));
            results.Add(new EInvoiceGibResult("1171", "GONDERICI BIRIM YETKISI YOK"));
            results.Add(new EInvoiceGibResult("1172", "POSTA KUTUSU YETKISI YOK"));
            results.Add(new EInvoiceGibResult("1175", "IMZA YETKISI KONTROL EDILEMEDI"));
            results.Add(new EInvoiceGibResult("1176", "IMZA SAHIBI YETKISIZ"));
            results.Add(new EInvoiceGibResult("1180", "ADRES KONTROL EDILEMEDI"));
            results.Add(new EInvoiceGibResult("1181", "ADRES BULUNAMADI"));
            results.Add(new EInvoiceGibResult("1190", "SISTEM YANITI HAZIRLANAMADI"));
            results.Add(new EInvoiceGibResult("1195", "SISTEM HATASI"));
            results.Add(new EInvoiceGibResult("1200", "ZARF BASARIYLA ISLENDI", true));
            results.Add(new EInvoiceGibResult("1210", "DOKUMAN BULUNAN ADRESE GONDERILEMEDI", true));
            results.Add(new EInvoiceGibResult("1215", "DOKUMAN GONDERIMI BASARISIZ. TERKAR GONDERME SONLANDI"));
            results.Add(new EInvoiceGibResult("1220", "HEDEFTEN SISTEM YANITI GELMEDI", true));
            results.Add(new EInvoiceGibResult("1230", "HEDEFTEN SISTEM YANITI BASARISIZ GELDI"));
            results.Add(new EInvoiceGibResult("1300", "BASARIYLA TAMAMLANDI", false, true));
        }

        public EInvoiceGibResult GetGibResult(string code, string description)
        {
            var result = results.FirstOrDefault(o => o.Code.Equals(code));
            if (result == null)
                result = new EInvoiceGibResult(code, description);
            return result;
        }
    }

    public class EInvoiceGibResult
    {
        /// <summary>
        ///     Gib tarafından gönderilen durum kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     Gib tarafından gönderilen durum açıklaması
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Tekrar sorgula = true
        /// </summary>
        public bool RepeatQuery { get; set; }

        /// <summary>
        ///     Durumu : 1300 "BASARIYLA TAMAMLANDI"
        ///     İşlem Tamamlandı
        /// </summary>
        public bool Completed { get; set; }

        public EInvoiceGibResult(string code, string description, bool repeatQuery = false, bool Completed = false)
        {
            this.Code = code;
            this.Description = description;
            this.RepeatQuery = repeatQuery;
            this.Completed = Completed;
        }
    }
}
