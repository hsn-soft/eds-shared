namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.ConstRepository
{
    public class TaxType
    {

        public int Id { get; set; }

        public string Code { get; set; }

        public string ShortName { get; set; }

        public string Name { get; set; }


        private static List<TaxType> _listTaxType;

        private static object lockSys = new object();

        static TaxType()
        {
            lock (lockSys)
            {
                buildTaxType();
            }
        }


        /// <summary>
        /// Vergi Türleri Listesi
        /// </summary>
        /// <returns></returns>
        public static List<TaxType> GetTaxTypeList()
        {
            return _listTaxType;
        }

        private static void buildTaxType()
        {
            _listTaxType = new List<TaxType>();
            _listTaxType.Add(new TaxType() { Code = "0003", Name = "GELİR VERGİSİ STOPAJI", ShortName = "GV STOPAJI(-)" });
            _listTaxType.Add(new TaxType() { Code = "SGK_PRIM", Name = "KURUMLAR VERGİSİ STOPAJI", ShortName = "KV STOPAJI(+)" });
            _listTaxType.Add(new TaxType() { Code = "0011", Name = "KURUMLAR VERGİSİ STOPAJI", ShortName = "KV STOPAJI(+)" });
            _listTaxType.Add(new TaxType() { Code = "0015", Name = "GERÇEK USULDE KATMA DEĞER VERGİSİ", ShortName = "KDV GERCEK " });
            _listTaxType.Add(new TaxType() { Code = "0021", Name = "BANKA MUAMELELERİ VERGİSİ", ShortName = "BMV" });
            _listTaxType.Add(new TaxType() { Code = "0061", Name = "KAYNAK KULLANIMI DESTEKLEME FONU KESİNTİSİ", ShortName = "KKDF KESİNTİ" });
            _listTaxType.Add(new TaxType() { Code = "0071", Name = "PETROL VE DOĞALGAZ ÜRÜNLERİNE İLİŞKİN ÖZEL TÜKETİM VERGİSİ", ShortName = "ÖTV 1.LİSTE" });
            _listTaxType.Add(new TaxType() { Code = "0073", Name = "KOLALI GAZOZ, ALKOLLÜ İÇEÇEKLER VE TÜTÜN MAMÜLLERİNE İLİŞKİN ÖZEL TÜKETİM VERGİSİ", ShortName = "ÖTV 3.LİSTE" });
            _listTaxType.Add(new TaxType() { Code = "0074", Name = "DAYANIKLI TÜKETİM VE DİĞER MALLARA İLİŞKİN ÖZEL TÜKETİM VERGİSİ", ShortName = "ÖTV 4.LİSTE" });
            _listTaxType.Add(new TaxType() { Code = "0075", Name = "ALKOLLÜ İÇEÇEKLERE İLİŞKİN ÖZEL TÜKETİM VERGİSİ", ShortName = "ÖTV 3A LİSTE" });
            _listTaxType.Add(new TaxType() { Code = "0076", Name = "TÜTÜN MAMÜLLERİNE İLİŞKİN ÖZEL TÜKETİM VERGİSİ", ShortName = "ÖTV 3B LİSTE" });
            _listTaxType.Add(new TaxType() { Code = "0077", Name = "KOLALI GAZOZLARA İLİŞKİN ÖZEL TÜKETİM VERGİSİ", ShortName = "ÖTV 3C LİSTE" });
            _listTaxType.Add(new TaxType() { Code = "1047", Name = "DAMGA VERGİSİ", ShortName = "DAMGA V" });
            _listTaxType.Add(new TaxType() { Code = "1048", Name = "5035 SAYILI KANUNA GÖRE DAMGA VERGİSİ", ShortName = "5035SKDAMGAV" });
            _listTaxType.Add(new TaxType() { Code = "4071", Name = "ELEKTRİK VE HAVAGAZI TÜKETİM VERGİSİ", ShortName = "ELK.HAVAGAZ.TÜK.VER." });
            _listTaxType.Add(new TaxType() { Code = "4080", Name = "ÖZEL İLETİŞİM VERGİSİ", ShortName = "Ö.İLETİŞİM V " });
            _listTaxType.Add(new TaxType() { Code = "4081", Name = "5035 SAYILI KANUNA GÖRE ÖZEL İLETİŞİM VERGİSİ", ShortName = "5035ÖZİLETV." });
            _listTaxType.Add(new TaxType() { Code = "4171", Name = "PETROL VE DOĞALGAZ ÜRÜNLERİNE İLİŞKİN ÖTV TEVKİFATI", ShortName = "PTR-DGZ ÖTV TEVKİFAT" });
            _listTaxType.Add(new TaxType() { Code = "8001", Name = "BORSA TESCİL ÜCRETİ", ShortName = "BORSA TES.ÜC." });
            _listTaxType.Add(new TaxType() { Code = "8002", Name = "ENERJİ FONU", ShortName = "ENERJİ FONU" });
            _listTaxType.Add(new TaxType() { Code = "8004", Name = "TRT PAYI", ShortName = "TRT PAYI" });
            _listTaxType.Add(new TaxType() { Code = "8005", Name = "ELEKTRİK TÜKETİM VERGİSİ", ShortName = "ELK.TÜK.VER." });
            _listTaxType.Add(new TaxType() { Code = "8006", Name = "TELSİZ KULLANIM ÜCRETİ", ShortName = "TK KULLANIM" });
            _listTaxType.Add(new TaxType() { Code = "8007", Name = "TELSİZ RUHSAT ÜCRETİ", ShortName = "TK RUHSAT" });
            _listTaxType.Add(new TaxType() { Code = "8008", Name = "ÇEVRE TEMİZLİK VERGİSİ ", ShortName = "ÇEV. TEM .VER." });
            _listTaxType.Add(new TaxType() { Code = "9015", Name = "KATMA DEĞER VERGİSİ TEVKİFATI", ShortName = "KDV TEVKİFAT" });
            _listTaxType.Add(new TaxType() { Code = "9021", Name = "4961 BANKA SİGORTA MUAMELELERİ VERGİSİ", ShortName = "4961BANKASMV" });
            _listTaxType.Add(new TaxType() { Code = "9040", Name = "MERA FONU", ShortName = "MERA FONU" });
            _listTaxType.Add(new TaxType() { Code = "9077", Name = "MOTORLU TAŞIT ARAÇLARINA İLİŞKİN ÖZEL TÜKETİM VERGİSİ (TESCİLE TABİ OLANLAR)", ShortName = "ÖTV 2.LİSTE" });
            _listTaxType.Add(new TaxType() { Code = "9944", Name = "BELEDİYELERE ÖDENEN HAL RÜSUMU", ShortName = "BEL.ÖD.HAL RÜSUM" });
        }
    }


    public class ExportTaxExemptionReasonCodeType
    {

        public int Id { get; set; }

        public string Code { get; set; }

        public string ShortName { get; set; }

        public string Name { get; set; }


        private static List<ExportTaxExemptionReasonCodeType> _listTaxType;

        private static object lockSys = new object();

        static ExportTaxExemptionReasonCodeType()
        {
            lock (lockSys)
            {
                buildTaxType();
            }
        }

        public static List<ExportTaxExemptionReasonCodeType> GetTaxTypeList()
        {
            return _listTaxType;
        }

        private static void buildTaxType()
        {
            _listTaxType = new List<ExportTaxExemptionReasonCodeType>();
            _listTaxType.Add(new ExportTaxExemptionReasonCodeType() { Code = "701", Name = "3065 s. KDV Kanununun 11/1-c md. Kapsamındaki İhraç Kayıtlı Satış", ShortName = "" });
            _listTaxType.Add(new ExportTaxExemptionReasonCodeType() { Code = "702", Name = "DİİB ve Geçici Kabul Rejimi Kapsamındaki Satışlar", ShortName = "" });
            _listTaxType.Add(new ExportTaxExemptionReasonCodeType() { Code = "703", Name = "4760 s. ÖTV Kanununun 8/2 Md. Kapsamındaki İhraç Kayıtlı Satış", ShortName = "" });
        }
    }

}
