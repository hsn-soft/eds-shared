namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.FaultCodes
{
    // WEB SERVICE FAULT MESSAGE STATUS
    public sealed class MessageFaultStatus
    {
        private readonly String name;
        private readonly Int32 code;
        private readonly String description;

        public static readonly MessageFaultStatus OZET_DEGERLER_ESIT_DEGIL = new MessageFaultStatus("OZET DEGERLER ESIT DEGIL", 2000, "Alınan dosya ile gönderilen dosyanın özet (hash) değerleri eşit değil!");
        public static readonly MessageFaultStatus ZARF_ID_SISTEMDE_MEVCUT = new MessageFaultStatus("ZARF ID SISTEMDE MEVCUT", 2001, "Gönderilen zarf saha önceden sistem üzerinde arşivlenmiş!");
        public static readonly MessageFaultStatus ZARF_ARSIVE_EKLENEMEDI = new MessageFaultStatus("ZARF ARSIVE EKLENEMEDI", 2002, "Gönderilen zarf arşive eklenemedi!");
        public static readonly MessageFaultStatus ZARF_KUYRUGA_EKLENEMEDI = new MessageFaultStatus("ZARF KUYRUGA EKLENEMEDI", 2003, "Gönderilen zarf arşivden kuyruğa alınamadı!");
        public static readonly MessageFaultStatus ZARF_ID_BULUNAMADI = new MessageFaultStatus("ZARF ID BULUNAMADI", 2004, "Sorgulanan zarf sistem üzerinde bulunamadı!");
        public static readonly MessageFaultStatus SISTEM_HATASI = new MessageFaultStatus("SISTEM HATASI", 2005, "Gönderilen parametreler yada data içeriği çözümlenemedi!");
        public static readonly MessageFaultStatus GECERSIZ_ZARF_ADI = new MessageFaultStatus("GECERSIZ ZARF ADI", 2006, "Gönderilen zarf adı Guid formatında olmalıdır!");

        public static readonly MessageFaultStatus SUNUCU_ERISIMI_BASARISIZ = new MessageFaultStatus("SUNUCU_ERISIMI_BASARISIZ", 5000, "Sunucuya erişim sağlanamadı!");

        private MessageFaultStatus(String name, Int32 code, String description)
        {
            this.name = name;
            this.code = code;
            this.description = description;
        }
        public override String ToString()
        {
            return name;
        }
        public String GetName()
        {
            return name;
        }
        public Int32 GetCode()
        {
            return code;
        }
        public String GetDescription()
        {
            return description;
        }
    }

    // SCHEME CONTROL MESSAGE STATUS
    public sealed class MessageSchemeStatus
    {
        private readonly String name;
        private readonly Int32 code;
        private readonly String description;

        public static readonly MessageSchemeStatus ZARF_KUYRUGA_EKLENDI = new MessageSchemeStatus("ZARF KUYRUGA EKLENDI", 1000, "ZARF KUYRUGA EKLENDI");
        public static readonly MessageSchemeStatus ZARF_KONTROL_EDILIYOR = new MessageSchemeStatus("ZARF KONTROL EDILIYOR", 1001, "ZARF KONTROL EDILIYOR");
        public static readonly MessageSchemeStatus ZARF_KONTROL_EDILDI_ISLENMEYI_BEKLIYOR = new MessageSchemeStatus("ZARF KONTROL EDILDI ISLENMEYI BEKLIYOR", 1099, "ZARF KONTROL EDILDI ISLENMEYI BEKLIYOR");
        public static readonly MessageSchemeStatus ZARF_ISLENIYOR = new MessageSchemeStatus("ZARF ISLENIYOR", 1100, "ZARF ISLENIYOR");
        public static readonly MessageSchemeStatus ZIP_DOSYASI_DEGIL = new MessageSchemeStatus("ZIP DOSYASI DEGIL", 1110, "ZIP DOSYASI DEGIL");
        public static readonly MessageSchemeStatus ZARF_ID_UZUNLUGU_GECERSIZ = new MessageSchemeStatus("ZARF ID UZUNLUGU GECERSIZ", 1111, "ZARF ID UZUNLUGU GECERSIZ");
        public static readonly MessageSchemeStatus ZARF_ARSIVDEN_KOPYALANAMADI = new MessageSchemeStatus("ZARF ARSIVDEN KOPYALANAMADI", 1120, "ZARF ARSIVDEN KOPYALANAMADI");
        public static readonly MessageSchemeStatus ZIP_ACILAMADI = new MessageSchemeStatus("ZIP ACILAMADI", 1130, "ZIP ACILAMADI");
        public static readonly MessageSchemeStatus ZIP_BIR_DOSYA_ICERMELI = new MessageSchemeStatus("ZIP BIR DOSYA ICERMELI", 1131, "ZIP BIR DOSYA ICERMELI");
        public static readonly MessageSchemeStatus XML_DOSYASI_DEGIL = new MessageSchemeStatus("XML DOSYASI DEGIL", 1132, "XML DOSYASI DEGIL");
        public static readonly MessageSchemeStatus ZARF_ID_VE_XML_DOSYASININ_ADI_AYNI_OLMALI = new MessageSchemeStatus("ZARF ID VE XML DOSYASININ ADI AYNI OLMALI", 1133, "ZARF ID VE XML DOSYASININ ADI AYNI OLMALI");
        public static readonly MessageSchemeStatus DOKUMAN_AYRISTIRILAMADI = new MessageSchemeStatus("DOKUMAN AYRISTIRILAMADI", 1140, "DOKUMAN AYRISTIRILAMADI");
        public static readonly MessageSchemeStatus ZARF_ID_YOK = new MessageSchemeStatus("ZARF ID YOK", 1141, "ZARF ID YOK");
        public static readonly MessageSchemeStatus ZARF_ID_VE_ZIP_DOSYASI_ADI_AYNI_OLMALI = new MessageSchemeStatus("ZARF ID VE ZIP DOSYASI ADI AYNI OLMALI", 1142, "ZARF ID VE ZIP DOSYASI ADI AYNI OLMALI");
        public static readonly MessageSchemeStatus GECERSIZ_VERSIYON = new MessageSchemeStatus("GECERSIZ VERSIYON", 1143, "GECERSIZ VERSIYON");
        public static readonly MessageSchemeStatus SCHEMATRON_KONTROL_SONUCU_HATALI = new MessageSchemeStatus("SCHEMATRON KONTROL SONUCU HATALI", 1150, "SCHEMATRON KONTROL SONUCU HATALI");
        public static readonly MessageSchemeStatus XML_SEMA_KONTROLUNDEN_GECEMEDI = new MessageSchemeStatus("XML SEMA KONTROLUNDEN GECEMEDI", 1160, "XML SEMA KONTROLUNDEN GECEMEDI");
        public static readonly MessageSchemeStatus IMZA_SAHIBI_TCKN_VKN_ALINAMADI = new MessageSchemeStatus("IMZA SAHIBI TCKN VKN ALINAMADI", 1161, "IMZA SAHIBI TCKN VKN ALINAMADI");
        public static readonly MessageSchemeStatus IMZA_KAYDEDILEMEDI = new MessageSchemeStatus("IMZA KAYDEDILEMEDI", 1162, "IMZA KAYDEDILEMEDI");
        public static readonly MessageSchemeStatus GONDERILEN_ZARFDA_ONCEDEN_KAYITLI_FATURA_VAR = new MessageSchemeStatus("GONDERILEN ZARFDA ONCEDEN KAYITLI FATURA VAR", 1163, "GONDERILEN ZARF SISTEMDE DAHA ONCE KAYITLI OLAN BIR FATURAYI ICERMEKTEDIR.");
        public static readonly MessageSchemeStatus GONDERILEN_ZARFDA_ONCEDEN_KAYITLI_IRSALIYE_VAR = new MessageSchemeStatus("GONDERILEN ZARFDA ONCEDEN KAYITLI IRSALIYE VAR", 1163, "GONDERILEN ZARF SISTEMDE DAHA ONCE KAYITLI OLAN BIR IRSALIYEYI ICERMEKTEDIR.");
        public static readonly MessageSchemeStatus YETKI_KONTROL_EDILEMEDI = new MessageSchemeStatus("YETKI KONTROL EDILEMEDI", 1170, "YETKI KONTROL EDILEMEDI");
        public static readonly MessageSchemeStatus GONDERICI_BIRIM_YETKISI_YOK = new MessageSchemeStatus("GONDERICI BIRIM YETKISI YOK", 1171, "GONDERICI BIRIM YETKISI YOK");
        public static readonly MessageSchemeStatus POSTA_KUTUSU_YETKISI_YOK = new MessageSchemeStatus("POSTA KUTUSU YETKISI YOK", 1172, "POSTA KUTUSU YETKISI YOK");
        public static readonly MessageSchemeStatus IMZA_YETKISI_KONTROL_EDILEMEDI = new MessageSchemeStatus("IMZA YETKISI KONTROL EDILEMEDI", 1175, "IMZA YETKISI KONTROL EDILEMEDI");
        public static readonly MessageSchemeStatus IMZA_SAHIBI_YETKISIZ = new MessageSchemeStatus("IMZA SAHIBI YETKISIZ", 1176, "IMZA SAHIBI YETKISIZ");
        public static readonly MessageSchemeStatus ADRES_KONTROL_EDILEMEDI = new MessageSchemeStatus("ADRES KONTROL EDILEMEDI", 1180, "ADRES KONTROL EDILEMEDI");
        public static readonly MessageSchemeStatus ADRES_BULUNAMADI = new MessageSchemeStatus("ADRES BULUNAMADI", 1181, "ADRES BULUNAMADI");
        public static readonly MessageSchemeStatus SISTEM_YANITI_HAZIRLANAMADI = new MessageSchemeStatus("SISTEM YANITI HAZIRLANAMADI", 1190, "SISTEM YANITI HAZIRLANAMADI");
        public static readonly MessageSchemeStatus SISTEM_HATASI = new MessageSchemeStatus("SISTEM HATASI", 1195, "SISTEM HATASI");
        public static readonly MessageSchemeStatus ZARF_BASARIYLA_ISLENDI = new MessageSchemeStatus("ZARF BASARIYLA ISLENDI", 1200, "ZARF BASARIYLA ISLENDI");
        public static readonly MessageSchemeStatus DOKUMAN_BULUNAN_ADRESE_GONDERILEMEDI = new MessageSchemeStatus("DOKUMAN BULUNAN ADRESE GONDERILEMEDI", 1210, "DOKUMAN BULUNAN ADRESE GONDERILEMEDI");
        public static readonly MessageSchemeStatus DOKUMAN_GONDERIMI_BASARISIZ = new MessageSchemeStatus("DOKUMAN GONDERIMI BASARISIZ", 1215, "DOKUMAN GONDERIMI BASARISIZ. TERKAR GONDERME SONLANDI");
        public static readonly MessageSchemeStatus HEDEFTEN_SISTEM_YANITI_GELMEDI = new MessageSchemeStatus("HEDEFTEN SISTEM YANITI GELMEDI", 1220, "HEDEFTEN SISTEM YANITI GELMEDI");
        public static readonly MessageSchemeStatus HEDEFTEN_SISTEM_YANITI_BASARISIZ_GELDI = new MessageSchemeStatus("HEDEFTEN SISTEM YANITI BASARISIZ GELDI", 1230, "HEDEFTEN SISTEM YANITI BASARISIZ GELDI");
        public static readonly MessageSchemeStatus BASARIYLA_TAMAMLANDI = new MessageSchemeStatus("BASARIYLA TAMAMLANDI", 1300, "BASARIYLA TAMAMLANDI");


        private MessageSchemeStatus(String name, Int32 code, String description)
        {
            this.name = name;
            this.code = code;
            this.description = description;
        }
        public override String ToString()
        {
            return name;
        }
        public String GetName()
        {
            return name;
        }
        public Int32 GetCode()
        {
            return code;
        }
        public String GetDescription()
        {
            return description;
        }
    }
}
