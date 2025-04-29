namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.GlobalTypes.EArchive
{
    public sealed class TaxWitholding
    {
        private readonly String name;
        private readonly String code;
        private readonly String description;

        public static readonly TaxWitholding TaxWitholdingType_101 = new TaxWitholding("101", "101", "[Tam]İKAMETGÂHI, İŞYERİ, KANUNİ MERKEZİ VE İŞ MERKEZİ TÜRKİYEDE BULUNMAYANLAR TARAFINDAN YAPILAN İŞL");
        public static readonly TaxWitholding TaxWitholdingType_102 = new TaxWitholding("102", "102", "[Tam]SERBEST MESLEK FAALİYETİ ÇERÇEVESİNDE YAPILAN TESLİM VE HİZMETLER [GT 117-Bölüm (2.2)]");
        public static readonly TaxWitholding TaxWitholdingType_103 = new TaxWitholding("103", "103", "[Tam]KİRALAMA İŞLEMLERİ [GT 117-Bölüm (2.3)]");
        public static readonly TaxWitholding TaxWitholdingType_104 = new TaxWitholding("104", "104", "[Tam]REKLÂM VERME İŞLEMLERİ [GT 117-Bölüm (2.4)]");
        public static readonly TaxWitholding TaxWitholdingType_150 = new TaxWitholding("150", "150", "[Tam]DİĞERLERİ");

        public static readonly TaxWitholding TaxWitholdingType_201 = new TaxWitholding("201", "201", "[Kısmi]YAPIM İŞLERİ İLE BU İŞLERLE BİRLİKTE İFA EDİLEN MÜHENDİSLİK-MİMARLIK VE ETÜT-PROJE HİZMETLERİ");
        public static readonly TaxWitholding TaxWitholdingType_202 = new TaxWitholding("202", "202", "[Kısmi]ETÜT, PLAN-PROJE, DANIŞMANLIK, DENETİM VE BENZERİ HİZMETLER[GT 117-Bölüm (3.2.2)]");
        public static readonly TaxWitholding TaxWitholdingType_203 = new TaxWitholding("203", "203", "[Kısmi]MAKİNE, TEÇHİZAT, DEMİRBAŞ VE TAŞITLARA AİT TADİL, BAKIM VE ONARIM HİZMETLERİ [GT 117-Bölüm");
        public static readonly TaxWitholding TaxWitholdingType_204 = new TaxWitholding("204", "204", "[Kısmi]YEMEK SERVİS HİZMETİ [GT 117-Bölüm (3.2.4)]");
        public static readonly TaxWitholding TaxWitholdingType_205 = new TaxWitholding("205", "205", "[Kısmi]ORGANİZASYON HİZMETİ [GT 117-Bölüm (3.2.4)]");
        public static readonly TaxWitholding TaxWitholdingType_206 = new TaxWitholding("206", "206", "[Kısmi]İŞGÜCÜ TEMİN HİZMETLERİ  [GT 117-Bölüm (3.2.5)]");
        public static readonly TaxWitholding TaxWitholdingType_207 = new TaxWitholding("207", "207", "[Kısmi]ÖZEL GÜVENLİK HİZMETİ [GT 117-Bölüm (3.2.5)]");
        public static readonly TaxWitholding TaxWitholdingType_208 = new TaxWitholding("208", "208", "[Kısmi]YAPI DENETİM HİZMETLERİ [GT 117-Bölüm (3.2.6)]");
        public static readonly TaxWitholding TaxWitholdingType_209 = new TaxWitholding("209", "209", "[Kısmi]FASON OLARAK YAPTIRILAN TEKSTİL VE KONFEKSİYON İŞLERİ, ÇANTA VE AYAKKABI DİKİM İŞLERİ VE BU");
        public static readonly TaxWitholding TaxWitholdingType_210 = new TaxWitholding("210", "210", "[Kısmi]TURİSTİK MAĞAZALARA VERİLEN MÜŞTERİ BULMA / GÖTÜRME HİZMETLERİ [GT 117-Bölüm (3.2.8)]");
        public static readonly TaxWitholding TaxWitholdingType_211 = new TaxWitholding("211", "211", "[Kısmi]SPOR KULÜPLERİNİN YAYIN, REKLÂM VE İSİM HAKKI GELİRLERİNE KONU İŞLEMLERİ [GT 117-Bölüm (3.2.9");
        public static readonly TaxWitholding TaxWitholdingType_212 = new TaxWitholding("212", "212", "[Kısmi]TEMİZLİK HİZMETİ [GT 117-Bölüm (3.2.10)]");
        public static readonly TaxWitholding TaxWitholdingType_213 = new TaxWitholding("213", "213", "[Kısmi]ÇEVRE VE BAHÇE BAKIM HİZMETLERİ [GT 117-Bölüm (3.2.10)]");
        public static readonly TaxWitholding TaxWitholdingType_214 = new TaxWitholding("214", "214", "[Kısmi]SERVİS TAŞIMACILIĞI HİZMETİ [GT 117-Bölüm (3.2.11)]");
        public static readonly TaxWitholding TaxWitholdingType_215 = new TaxWitholding("215", "215", "[Kısmi]HER TÜRLÜ BASKI VE BASIM HİZMETLERİ [GT 117-Bölüm (3.2.12)]");
        public static readonly TaxWitholding TaxWitholdingType_216 = new TaxWitholding("216", "216", "[Kısmi]5018 SAYILI KANUNA EKLİ CETVELLERDEKİ İDARE, KURUM VE KURUŞLARA YAPILAN DİĞER HİZMETLER [GT 1");
        public static readonly TaxWitholding TaxWitholdingType_217 = new TaxWitholding("217", "217", "[Kısmi]HURDA METALDEN EDLDE EDİLEN KÜLÇE TESLİMLERİ [GT 117-Bölüm (3.3.1)]");
        public static readonly TaxWitholding TaxWitholdingType_218 = new TaxWitholding("218", "218", "[Kısmi]BAKIR, ÇİNKO VE ALÜMİNYUM KÜLÇE TESLİMLERİ [GT 117-Bölüm (3.3.1)]");
        public static readonly TaxWitholding TaxWitholdingType_219 = new TaxWitholding("219", "219", "[Kısmi]BAKIR, ÇİNKO VE ALÜMİNYUM ÜRÜNLERİNİN TESLİMİ [GT 117-Bölüm (3.3.2)]");
        public static readonly TaxWitholding TaxWitholdingType_220 = new TaxWitholding("220", "220", "[Kısmi]HURDA VE ATIK TESLİMİ [GT 117-Bölüm (3.3.3)]");
        public static readonly TaxWitholding TaxWitholdingType_221 = new TaxWitholding("221", "221", "[Kısmi]METAL, PLASTİK, LASTİK, KAUÇUK, KÂĞIT VE CAM HURDA VE ATIKLARDAN ELDE EDİLEN HAMMADDE TESLİMİ");
        public static readonly TaxWitholding TaxWitholdingType_222 = new TaxWitholding("222", "222", "[Kısmi]PAMUK, TİFTİK, YÜN VE YAPAĞI İLE HAM POST VE DERİ TESLİMLERİ [GT 117-Bölüm (3.3.5)]");
        public static readonly TaxWitholding TaxWitholdingType_223 = new TaxWitholding("223", "223", "[Kısmi]AĞAÇ VE ORMAN ÜRÜNLERİ TESLİMİ [GT 117-Bölüm (3.3.6)]");
        public static readonly TaxWitholding TaxWitholdingType_250 = new TaxWitholding("250", "250", "[Kısmi]DİĞERLERİ");

        public static readonly TaxWitholding TaxWitholdingType_601 = new TaxWitholding("601", "601", "[Kısmi]YAPIM İŞLERİ İLE BU İŞLERLE BİRLİKTE İFA EDİLEN MÜHENDİSLİK-MİMARLIK VE ETÜT-PROJE HİZMETLERİ");
        public static readonly TaxWitholding TaxWitholdingType_602 = new TaxWitholding("602", "602", "[Kısmi]ETÜT, PLAN-PROJE, DANIŞMANLIK, DENETİM VE BENZERİ HİZMETLER[GT 117-Bölüm (3.2.2)]");
        public static readonly TaxWitholding TaxWitholdingType_603 = new TaxWitholding("603", "603", "[Kısmi]MAKİNE, TEÇHİZAT, DEMİRBAŞ VE TAŞITLARA AİT TADİL, BAKIM VE ONARIM HİZMETLERİ [GT 117-Bölüm");
        public static readonly TaxWitholding TaxWitholdingType_604 = new TaxWitholding("604", "604", "[Kısmi]YEMEK SERVİS HİZMETİ [GT 117-Bölüm (3.2.4)]");
        public static readonly TaxWitholding TaxWitholdingType_605 = new TaxWitholding("605", "605", "[Kısmi]ORGANİZASYON HİZMETİ [GT 117-Bölüm (3.2.4)]");
        public static readonly TaxWitholding TaxWitholdingType_606 = new TaxWitholding("606", "606", "[Kısmi]İŞGÜCÜ TEMİN HİZMETLERİ  [GT 117-Bölüm (3.2.5)]");
        public static readonly TaxWitholding TaxWitholdingType_607 = new TaxWitholding("607", "607", "[Kısmi]ÖZEL GÜVENLİK HİZMETİ [GT 117-Bölüm (3.2.5)]");
        public static readonly TaxWitholding TaxWitholdingType_608 = new TaxWitholding("608", "608", "[Kısmi]YAPI DENETİM HİZMETLERİ [GT 117-Bölüm (3.2.6)]");
        public static readonly TaxWitholding TaxWitholdingType_609 = new TaxWitholding("609", "609", "[Kısmi]FASON OLARAK YAPTIRILAN TEKSTİL VE KONFEKSİYON İŞLERİ, ÇANTA VE AYAKKABI DİKİM İŞLERİ VE BU");
        public static readonly TaxWitholding TaxWitholdingType_610 = new TaxWitholding("610", "610", "[Kısmi]TURİSTİK MAĞAZALARA VERİLEN MÜŞTERİ BULMA / GÖTÜRME HİZMETLERİ [GT 117-Bölüm (3.2.8)]");
        public static readonly TaxWitholding TaxWitholdingType_611 = new TaxWitholding("611", "611", "[Kısmi]SPOR KULÜPLERİNİN YAYIN, REKLÂM VE İSİM HAKKI GELİRLERİNE KONU İŞLEMLERİ [GT 117-Bölüm (3.2.9");
        public static readonly TaxWitholding TaxWitholdingType_612 = new TaxWitholding("612", "612", "[Kısmi]TEMİZLİK HİZMETİ [GT 117-Bölüm (3.2.10)]");
        public static readonly TaxWitholding TaxWitholdingType_613 = new TaxWitholding("613", "613", "[Kısmi]ÇEVRE VE BAHÇE BAKIM HİZMETLERİ [GT 117-Bölüm (3.2.10)]");
        public static readonly TaxWitholding TaxWitholdingType_614 = new TaxWitholding("614", "614", "[Kısmi]SERVİS TAŞIMACILIĞI HİZMETİ [GT 117-Bölüm (3.2.11)]");
        public static readonly TaxWitholding TaxWitholdingType_615 = new TaxWitholding("615", "615", "[Kısmi]HER TÜRLÜ BASKI VE BASIM HİZMETLERİ [GT 117-Bölüm (3.2.12)]");
        public static readonly TaxWitholding TaxWitholdingType_616 = new TaxWitholding("616", "616", "[Kısmi]5018 SAYILI KANUNA EKLİ CETVELLERDEKİ İDARE, KURUM VE KURUŞLARA YAPILAN DİĞER HİZMETLER [GT 1");
        public static readonly TaxWitholding TaxWitholdingType_617 = new TaxWitholding("617", "617", "[Kısmi]HURDA METALDEN EDLDE EDİLEN KÜLÇE TESLİMLERİ [GT 117-Bölüm (3.3.1)]");
        public static readonly TaxWitholding TaxWitholdingType_618 = new TaxWitholding("618", "618", "[Kısmi]BAKIR, ÇİNKO VE ALÜMİNYUM KÜLÇE TESLİMLERİ [GT 117-Bölüm (3.3.1)]");
        public static readonly TaxWitholding TaxWitholdingType_619 = new TaxWitholding("619", "619", "[Kısmi]BAKIR, ÇİNKO VE ALÜMİNYUM ÜRÜNLERİNİN TESLİMİ [GT 117-Bölüm (3.3.2)]");
        public static readonly TaxWitholding TaxWitholdingType_620 = new TaxWitholding("620", "620", "[Kısmi]HURDA VE ATIK TESLİMİ [GT 117-Bölüm (3.3.3)]");
        public static readonly TaxWitholding TaxWitholdingType_621 = new TaxWitholding("621", "621", "[Kısmi]METAL, PLASTİK, LASTİK, KAUÇUK, KÂĞIT VE CAM HURDA VE ATIKLARDAN ELDE EDİLEN HAMMADDE TESLİMİ");
        public static readonly TaxWitholding TaxWitholdingType_622 = new TaxWitholding("622", "622", "[Kısmi]PAMUK, TİFTİK, YÜN VE YAPAĞI İLE HAM POST VE DERİ TESLİMLERİ [GT 117-Bölüm (3.3.5)]");
        public static readonly TaxWitholding TaxWitholdingType_623 = new TaxWitholding("623", "623", "[Kısmi]AĞAÇ VE ORMAN ÜRÜNLERİ TESLİMİ [GT 117-Bölüm (3.3.6)]");
        public static readonly TaxWitholding TaxWitholdingType_650 = new TaxWitholding("650", "650", "[Kısmi]DİĞERLERİ");

        public static readonly TaxWitholding TaxWitholdingType_701 = new TaxWitholding("701", "701", "[İhraç]İHRACATI YAPILACAK NİHAİ ÜRÜNLERİN KANUNUN 11/1-c MADDESİ KAPSAMINDA TESLİMİ");
        public static readonly TaxWitholding TaxWitholdingType_702 = new TaxWitholding("702", "702", "[İhraç]DAHİLDE İŞLEME VEYA GEÇİCİ KABUL REJİMLERİ KAPSAMINDA İHRACATI YAPILACAK ÜRÜNÜN İMALİNDE KULL");

        public static readonly List<string> TaxWitholdingTypeList = new List<string>
        {
            TaxWitholdingType_101 .GetCode(),TaxWitholdingType_102 .GetCode(),TaxWitholdingType_103 .GetCode(),TaxWitholdingType_104 .GetCode(),TaxWitholdingType_150 .GetCode(),

            TaxWitholdingType_201 .GetCode(),TaxWitholdingType_202 .GetCode(),TaxWitholdingType_203 .GetCode(),TaxWitholdingType_204 .GetCode(),TaxWitholdingType_205 .GetCode(),
            TaxWitholdingType_206 .GetCode(),TaxWitholdingType_207 .GetCode(),TaxWitholdingType_208 .GetCode(),TaxWitholdingType_209 .GetCode(),TaxWitholdingType_210 .GetCode(),
            TaxWitholdingType_211 .GetCode(),TaxWitholdingType_212 .GetCode(),TaxWitholdingType_213 .GetCode(),TaxWitholdingType_214 .GetCode(),TaxWitholdingType_215 .GetCode(),
            TaxWitholdingType_216 .GetCode(),TaxWitholdingType_217 .GetCode(),TaxWitholdingType_218 .GetCode(),TaxWitholdingType_219 .GetCode(),TaxWitholdingType_220 .GetCode(),
            TaxWitholdingType_221 .GetCode(),TaxWitholdingType_222 .GetCode(),TaxWitholdingType_223 .GetCode(),

            TaxWitholdingType_601 .GetCode(),TaxWitholdingType_602 .GetCode(),TaxWitholdingType_603 .GetCode(),TaxWitholdingType_604 .GetCode(),TaxWitholdingType_605 .GetCode(),
            TaxWitholdingType_606 .GetCode(),TaxWitholdingType_607 .GetCode(),TaxWitholdingType_608 .GetCode(),TaxWitholdingType_609 .GetCode(),TaxWitholdingType_610 .GetCode(),
            TaxWitholdingType_611 .GetCode(),TaxWitholdingType_612 .GetCode(),TaxWitholdingType_613 .GetCode(),TaxWitholdingType_614 .GetCode(),TaxWitholdingType_615 .GetCode(),
            TaxWitholdingType_616 .GetCode(),TaxWitholdingType_617 .GetCode(),TaxWitholdingType_618 .GetCode(),TaxWitholdingType_619 .GetCode(),TaxWitholdingType_620 .GetCode(),
            TaxWitholdingType_621 .GetCode(),TaxWitholdingType_622 .GetCode(),TaxWitholdingType_623 .GetCode(),TaxWitholdingType_650 .GetCode(),

            TaxWitholdingType_701 .GetCode(),TaxWitholdingType_702 .GetCode(),
        };


        private TaxWitholding(String name, String code, String description)
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
        public String GetCode()
        {
            return code;
        }
        public String GetDescription()
        {
            return description;
        }

        //Connector Geliştirmesi İçin İhtiyaç Duyuldu. 
        //Tüm Vergiler Sonra Kontrol Edilecek
        public static List<TaxWitholding> GetTaxTypeList()
        {
            List<TaxWitholding> result = new List<TaxWitholding>();

            result.Add(TaxWitholdingType_101);
            result.Add(TaxWitholdingType_102);
            result.Add(TaxWitholdingType_103);
            result.Add(TaxWitholdingType_104);
            result.Add(TaxWitholdingType_150);
            result.Add(TaxWitholdingType_201);
            result.Add(TaxWitholdingType_202);
            result.Add(TaxWitholdingType_203);
            result.Add(TaxWitholdingType_204);
            result.Add(TaxWitholdingType_205);
            result.Add(TaxWitholdingType_206);
            result.Add(TaxWitholdingType_207);
            result.Add(TaxWitholdingType_208);
            result.Add(TaxWitholdingType_209);
            result.Add(TaxWitholdingType_210);
            result.Add(TaxWitholdingType_211);
            result.Add(TaxWitholdingType_212);
            result.Add(TaxWitholdingType_213);
            result.Add(TaxWitholdingType_214);
            result.Add(TaxWitholdingType_215);
            result.Add(TaxWitholdingType_216);
            result.Add(TaxWitholdingType_217);
            result.Add(TaxWitholdingType_218);
            result.Add(TaxWitholdingType_219);
            result.Add(TaxWitholdingType_220);
            result.Add(TaxWitholdingType_221);
            result.Add(TaxWitholdingType_222);
            result.Add(TaxWitholdingType_223);
            result.Add(TaxWitholdingType_250);
            result.Add(TaxWitholdingType_601);
            result.Add(TaxWitholdingType_602);
            result.Add(TaxWitholdingType_603);
            result.Add(TaxWitholdingType_604);
            result.Add(TaxWitholdingType_605);
            result.Add(TaxWitholdingType_606);
            result.Add(TaxWitholdingType_607);
            result.Add(TaxWitholdingType_608);
            result.Add(TaxWitholdingType_609);
            result.Add(TaxWitholdingType_610);
            result.Add(TaxWitholdingType_611);
            result.Add(TaxWitholdingType_612);
            result.Add(TaxWitholdingType_613);
            result.Add(TaxWitholdingType_614);
            result.Add(TaxWitholdingType_615);
            result.Add(TaxWitholdingType_616);
            result.Add(TaxWitholdingType_617);
            result.Add(TaxWitholdingType_618);
            result.Add(TaxWitholdingType_619);
            result.Add(TaxWitholdingType_620);
            result.Add(TaxWitholdingType_621);
            result.Add(TaxWitholdingType_622);
            result.Add(TaxWitholdingType_623);
            result.Add(TaxWitholdingType_650);
            result.Add(TaxWitholdingType_701);
            result.Add(TaxWitholdingType_702);

            return result;
        }


    }
}
