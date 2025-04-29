namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.ConstRepository
{
    public class CountryTypeForIhracat
    {
        public int Id { get; set; }

        //Ülke Adı
        public string Name { get; set; }

        //Ülke Kodu ISO listesi alpha-2 ye göre alınması gerekiyor ihracat için.
        public string CountryCode { get; set; }

        private static List<CountryType> _countries;

        private static object lockSys = new object();


        static CountryTypeForIhracat()
        {
            lock (lockSys)
            {
                buildCountry();
            }
        }


        /// <summary>
        /// Ülkeler Listesi :en çok kullanılanlar en başa gelecek
        /// </summary>
        /// <returns></returns>
        public static List<CountryType> GetCountryList()
        {
            return _countries;
        }

        private static void buildCountry()
        {
            _countries = new List<CountryType>();
            _countries.Add(new CountryType() { CountryCode = "-", Name = "SEÇİNİZ" });
            _countries.Add(new CountryType() { CountryCode = "AF", Name = "AFGANİSTAN" });
            _countries.Add(new CountryType() { CountryCode = "AX", Name = "ALAND ADALARI" });
            _countries.Add(new CountryType() { CountryCode = "AL", Name = "ARNAVUTLUK" });
            _countries.Add(new CountryType() { CountryCode = "DZ", Name = "CEZAYİR" });
            _countries.Add(new CountryType() { CountryCode = "AS", Name = "AMERİKAN SAMOASI" });
            _countries.Add(new CountryType() { CountryCode = "AD", Name = "ANDORRA" });
            _countries.Add(new CountryType() { CountryCode = "AO", Name = "ANGOLA" });
            _countries.Add(new CountryType() { CountryCode = "AI", Name = "ANGUİLLA" });
            _countries.Add(new CountryType() { CountryCode = "AQ", Name = "ANTARKTİKA" });
            _countries.Add(new CountryType() { CountryCode = "AG", Name = "ANTİGUA VE BARBUDA" });
            _countries.Add(new CountryType() { CountryCode = "AR", Name = "ARJANTİN" });
            _countries.Add(new CountryType() { CountryCode = "AM", Name = "ERMENİSTAN" });
            _countries.Add(new CountryType() { CountryCode = "AW", Name = "ARUBA" });
            _countries.Add(new CountryType() { CountryCode = "AU", Name = "AVUSTRALYA" });
            _countries.Add(new CountryType() { CountryCode = "AT", Name = "AVUSTURYA" });
            _countries.Add(new CountryType() { CountryCode = "AZ", Name = "AZERBEYCAN" });
            _countries.Add(new CountryType() { CountryCode = "BS", Name = "BAHAMALAR" });
            _countries.Add(new CountryType() { CountryCode = "BH", Name = "BAHREYN" });
            _countries.Add(new CountryType() { CountryCode = "BD", Name = "BANGLADEŞ" });
            _countries.Add(new CountryType() { CountryCode = "BB", Name = "BARBADOS" });
            _countries.Add(new CountryType() { CountryCode = "BY", Name = "BEYAZ RUSYA" });
            _countries.Add(new CountryType() { CountryCode = "BE", Name = "BELÇİKA" });
            _countries.Add(new CountryType() { CountryCode = "BZ", Name = "BELİZE" });
            _countries.Add(new CountryType() { CountryCode = "BJ", Name = "BENİN" });
            _countries.Add(new CountryType() { CountryCode = "BM", Name = "BERMUDA" });
            _countries.Add(new CountryType() { CountryCode = "BT", Name = "BHUTAN" });
            _countries.Add(new CountryType() { CountryCode = "BO", Name = "BOLİVYA (ÇOKULUSLU DEVLET)" });
            _countries.Add(new CountryType() { CountryCode = "BQ", Name = "BONAİRE, SİNT EUSTATİUS VE SABA" });
            _countries.Add(new CountryType() { CountryCode = "BA", Name = "BOSNA HERSEK" });
            _countries.Add(new CountryType() { CountryCode = "BW", Name = "BOTSVANA" });
            _countries.Add(new CountryType() { CountryCode = "BV", Name = "BOUVET ADASI" });
            _countries.Add(new CountryType() { CountryCode = "BR", Name = "BREZİLYA" });
            _countries.Add(new CountryType() { CountryCode = "IO", Name = "İNGİLİZ HİNT OKYANUSU BÖLGESİ (THE)" });
            _countries.Add(new CountryType() { CountryCode = "BN", Name = "BRUNEİ SULTANLIĞI" });
            _countries.Add(new CountryType() { CountryCode = "BG", Name = "BULGARİSTAN" });
            _countries.Add(new CountryType() { CountryCode = "BF", Name = "BURKİNA FASO" });
            _countries.Add(new CountryType() { CountryCode = "BI", Name = "BURUNDİ" });
            _countries.Add(new CountryType() { CountryCode = "CV", Name = "CABO VERDE" });
            _countries.Add(new CountryType() { CountryCode = "KH", Name = "KAMBOÇYA" });
            _countries.Add(new CountryType() { CountryCode = "CM", Name = "CAMEROON" });
            _countries.Add(new CountryType() { CountryCode = "CA", Name = "KANADA" });
            _countries.Add(new CountryType() { CountryCode = "KY", Name = "CAYMAN ADALARI" });
            _countries.Add(new CountryType() { CountryCode = "CF", Name = "ORTA AFRİKA CUMHURİYETİ" });
            _countries.Add(new CountryType() { CountryCode = "TD", Name = "CHAD" });
            _countries.Add(new CountryType() { CountryCode = "CL", Name = "ŞİLİ" });
            _countries.Add(new CountryType() { CountryCode = "CN", Name = "ÇİN" });
            _countries.Add(new CountryType() { CountryCode = "CX", Name = "CHRİSTMAS ADASI" });
            _countries.Add(new CountryType() { CountryCode = "CC", Name = "COCOS (KEELİNG) ADALARI (THE)" });
            _countries.Add(new CountryType() { CountryCode = "CO", Name = "KOLOMBİYA" });
            _countries.Add(new CountryType() { CountryCode = "KM", Name = "KOMORLAR" });
            _countries.Add(new CountryType() { CountryCode = "CD", Name = "KONGO (DEMOKRATİK CUMHURİYETİ)" });
            _countries.Add(new CountryType() { CountryCode = "CG", Name = "KONGO" });
            _countries.Add(new CountryType() { CountryCode = "CK", Name = "COOK ADALARI" });
            _countries.Add(new CountryType() { CountryCode = "CR", Name = "KOSTA RİKA" });
            _countries.Add(new CountryType() { CountryCode = "CI", Name = "FİLDİŞİ SAHİLİ" });
            _countries.Add(new CountryType() { CountryCode = "HR", Name = "HIRVATİSTAN" });
            _countries.Add(new CountryType() { CountryCode = "CU", Name = "KÜBA" });
            _countries.Add(new CountryType() { CountryCode = "CW", Name = "CURACAO" });
            _countries.Add(new CountryType() { CountryCode = "CY", Name = "KIBRIS" });
            _countries.Add(new CountryType() { CountryCode = "CZ", Name = "ÇEKYA" });
            _countries.Add(new CountryType() { CountryCode = "DK", Name = "DANİMARKA" });
            _countries.Add(new CountryType() { CountryCode = "DJ", Name = "CİBUTİ" });
            _countries.Add(new CountryType() { CountryCode = "DM", Name = "DOMİNİCA" });
            _countries.Add(new CountryType() { CountryCode = "DO", Name = "DOMİNİK CUMHURİYETİ" });
            _countries.Add(new CountryType() { CountryCode = "EC", Name = "EKVADOR" });
            _countries.Add(new CountryType() { CountryCode = "EG", Name = "MISIR" });
            _countries.Add(new CountryType() { CountryCode = "SV", Name = "EL SALVADOR" });
            _countries.Add(new CountryType() { CountryCode = "GQ", Name = "EKVATOR GİNESİ" });
            _countries.Add(new CountryType() { CountryCode = "ER", Name = "ERİTREA" });
            _countries.Add(new CountryType() { CountryCode = "EE", Name = "ESTONYA" });
            _countries.Add(new CountryType() { CountryCode = "SZ", Name = "ESWATİNİ" });
            _countries.Add(new CountryType() { CountryCode = "ET", Name = "ETİYOPYA" });
            _countries.Add(new CountryType() { CountryCode = "FK", Name = "FALKLAND ADALARI (THE) [MALVİNAS]" });
            _countries.Add(new CountryType() { CountryCode = "FO", Name = "FAROE ADALARI" });
            _countries.Add(new CountryType() { CountryCode = "FJ", Name = "FİJİ" });
            _countries.Add(new CountryType() { CountryCode = "FI", Name = "FİNLANDİYA" });
            _countries.Add(new CountryType() { CountryCode = "FR", Name = "FRANSA" });
            _countries.Add(new CountryType() { CountryCode = "GF", Name = "FRANSIZ GUYANASI" });
            _countries.Add(new CountryType() { CountryCode = "PF", Name = "FRANSIZ POLİNEZYASI" });
            _countries.Add(new CountryType() { CountryCode = "TF", Name = "FRANSIZ GÜNEY TOPRAKLARI" });
            _countries.Add(new CountryType() { CountryCode = "GA", Name = "GABON" });
            _countries.Add(new CountryType() { CountryCode = "GM", Name = "GAMBİYA" });
            _countries.Add(new CountryType() { CountryCode = "GE", Name = "GEORGİA" });
            _countries.Add(new CountryType() { CountryCode = "DE", Name = "ALMANYA" });
            _countries.Add(new CountryType() { CountryCode = "GH", Name = "GANA" });
            _countries.Add(new CountryType() { CountryCode = "GI", Name = "CEBELİTARIK" });
            _countries.Add(new CountryType() { CountryCode = "GR", Name = "YUNANİSTAN" });
            _countries.Add(new CountryType() { CountryCode = "GL", Name = "GRÖNLAND" });
            _countries.Add(new CountryType() { CountryCode = "GD", Name = "GRENADA" });
            _countries.Add(new CountryType() { CountryCode = "GP", Name = "GUADELOUPE" });
            _countries.Add(new CountryType() { CountryCode = "GU", Name = "GUAM" });
            _countries.Add(new CountryType() { CountryCode = "GT", Name = "GUATEMALA" });
            _countries.Add(new CountryType() { CountryCode = "GG", Name = "GUERNSEY" });
            _countries.Add(new CountryType() { CountryCode = "GN", Name = "GİNE" });
            _countries.Add(new CountryType() { CountryCode = "GW", Name = "GİNE-BİSSAU" });
            _countries.Add(new CountryType() { CountryCode = "GY", Name = "GUYANA" });
            _countries.Add(new CountryType() { CountryCode = "HT", Name = "HAİTİ" });
            _countries.Add(new CountryType() { CountryCode = "HM", Name = "HEARD VE MCDONALD ADALARI" });
            _countries.Add(new CountryType() { CountryCode = "VA", Name = "VATİKAN ŞEHİR DEVLETİ" });
            _countries.Add(new CountryType() { CountryCode = "HN", Name = "HONDURAS" });
            _countries.Add(new CountryType() { CountryCode = "HK", Name = "HONG KONG" });
            _countries.Add(new CountryType() { CountryCode = "HU", Name = "MACARİSTAN" });
            _countries.Add(new CountryType() { CountryCode = "IS", Name = "İZLANDA" });
            _countries.Add(new CountryType() { CountryCode = "IN", Name = "HİNDİSTAN" });
            _countries.Add(new CountryType() { CountryCode = "ID", Name = "ENDONEZYA" });
            _countries.Add(new CountryType() { CountryCode = "IR", Name = "İRAN (İSLAM CUMHURİYETİ)" });
            _countries.Add(new CountryType() { CountryCode = "IQ", Name = "IRAK" });
            _countries.Add(new CountryType() { CountryCode = "IE", Name = "İRLANDA" });
            _countries.Add(new CountryType() { CountryCode = "IM", Name = "MAN ADASI" });
            _countries.Add(new CountryType() { CountryCode = "IL", Name = "İSRAİL" });
            _countries.Add(new CountryType() { CountryCode = "IT", Name = "İTALYA" });
            _countries.Add(new CountryType() { CountryCode = "JM", Name = "JAMAİKA" });
            _countries.Add(new CountryType() { CountryCode = "JP", Name = "JAPONYA" });
            _countries.Add(new CountryType() { CountryCode = "JE", Name = "JERSEY" });
            _countries.Add(new CountryType() { CountryCode = "JO", Name = "ÜRDÜN" });
            _countries.Add(new CountryType() { CountryCode = "KZ", Name = "KAZAKİSTAN" });
            _countries.Add(new CountryType() { CountryCode = "KE", Name = "KENYA" });
            _countries.Add(new CountryType() { CountryCode = "KI", Name = "KİRİBATİ" });
            _countries.Add(new CountryType() { CountryCode = "KP", Name = "KORE (DEMOKRATİK HALK CUMHURİYETİ)" });
            _countries.Add(new CountryType() { CountryCode = "KR", Name = "KORE (CUMHURİYETİ)" });
            _countries.Add(new CountryType() { CountryCode = "KW", Name = "KUVEYT" });
            _countries.Add(new CountryType() { CountryCode = "KG", Name = "KIRGIZİSTAN" });
            _countries.Add(new CountryType() { CountryCode = "LA", Name = "LAO DEMOKRATİK HALK CUMHURİYETİ" });
            _countries.Add(new CountryType() { CountryCode = "LV", Name = "LETONYA" });
            _countries.Add(new CountryType() { CountryCode = "LB", Name = "LÜBNAN" });
            _countries.Add(new CountryType() { CountryCode = "LS", Name = "LESOTHO" });
            _countries.Add(new CountryType() { CountryCode = "LR", Name = "LİBERYA" });
            _countries.Add(new CountryType() { CountryCode = "LY", Name = "LİBYA" });
            _countries.Add(new CountryType() { CountryCode = "LI", Name = "LİECHTENSTEİN" });
            _countries.Add(new CountryType() { CountryCode = "LT", Name = "LİTVANYA" });
            _countries.Add(new CountryType() { CountryCode = "LU", Name = "LÜKSEMBURG" });
            _countries.Add(new CountryType() { CountryCode = "MO", Name = "MACAO" });
            _countries.Add(new CountryType() { CountryCode = "MK", Name = "MAKEDONYA" });
            _countries.Add(new CountryType() { CountryCode = "MG", Name = "MADAGASKAR" });
            _countries.Add(new CountryType() { CountryCode = "MW", Name = "MALAWİ" });
            _countries.Add(new CountryType() { CountryCode = "MY", Name = "MALEZYA" });
            _countries.Add(new CountryType() { CountryCode = "MV", Name = "MALDİVLER" });
            _countries.Add(new CountryType() { CountryCode = "ML", Name = "MALİ" });
            _countries.Add(new CountryType() { CountryCode = "MT", Name = "MALTA" });
            _countries.Add(new CountryType() { CountryCode = "MH", Name = "MARSHALL ADALARI (THE)" });
            _countries.Add(new CountryType() { CountryCode = "MQ", Name = "MARTİNİK" });
            _countries.Add(new CountryType() { CountryCode = "MR", Name = "MORİTANYA" });
            _countries.Add(new CountryType() { CountryCode = "MU", Name = "MAURİTİUS" });
            _countries.Add(new CountryType() { CountryCode = "YT", Name = "MAYOTTE" });
            _countries.Add(new CountryType() { CountryCode = "MX", Name = "MEKSİKA" });
            _countries.Add(new CountryType() { CountryCode = "FM", Name = "MİKRONEZYA (FEDERAL DEVLETLERİ)" });
            _countries.Add(new CountryType() { CountryCode = "MD", Name = "MOLDOVA CUMHURİYETİ" });
            _countries.Add(new CountryType() { CountryCode = "MC", Name = "MONAKO" });
            _countries.Add(new CountryType() { CountryCode = "MN", Name = "MOĞOLİSTAN" });
            _countries.Add(new CountryType() { CountryCode = "ME", Name = "KARADAĞ" });
            _countries.Add(new CountryType() { CountryCode = "MS", Name = "MONTSERRAT" });
            _countries.Add(new CountryType() { CountryCode = "MA", Name = "FAS" });
            _countries.Add(new CountryType() { CountryCode = "MZ", Name = "MOZAMBİK" });
            _countries.Add(new CountryType() { CountryCode = "MM", Name = "MYANMAR" });
            _countries.Add(new CountryType() { CountryCode = "NA", Name = "NAMİBYA" });
            _countries.Add(new CountryType() { CountryCode = "NR", Name = "NAURU" });
            _countries.Add(new CountryType() { CountryCode = "NP", Name = "NEPAL" });
            _countries.Add(new CountryType() { CountryCode = "NL", Name = "HOLLANDA" });
            _countries.Add(new CountryType() { CountryCode = "AN", Name = "HOLLANDA ANTİLLERİ" });
            _countries.Add(new CountryType() { CountryCode = "NC", Name = "YENİ KALEDONYA" });
            _countries.Add(new CountryType() { CountryCode = "NZ", Name = "YENİ ZELANDA" });
            _countries.Add(new CountryType() { CountryCode = "NI", Name = "NİKARAGUA" });
            _countries.Add(new CountryType() { CountryCode = "NE", Name = "NİJER" });
            _countries.Add(new CountryType() { CountryCode = "NG", Name = "NİJERYA" });
            _countries.Add(new CountryType() { CountryCode = "NU", Name = "NİUE" });
            _countries.Add(new CountryType() { CountryCode = "NF", Name = "NORFOLK ADASI" });
            _countries.Add(new CountryType() { CountryCode = "MP", Name = "KUZEY MARİANA ADALARI" });
            _countries.Add(new CountryType() { CountryCode = "NO", Name = "NORVEÇ" });
            _countries.Add(new CountryType() { CountryCode = "OM", Name = "UMMAN" });
            _countries.Add(new CountryType() { CountryCode = "PK", Name = "PAKİSTAN" });
            _countries.Add(new CountryType() { CountryCode = "PW", Name = "PALAU" });
            _countries.Add(new CountryType() { CountryCode = "PS", Name = "FİLİSTİN, DEVLET" });
            _countries.Add(new CountryType() { CountryCode = "PA", Name = "PANAMA" });
            _countries.Add(new CountryType() { CountryCode = "PG", Name = "PAPUA YENİ GİNE" });
            _countries.Add(new CountryType() { CountryCode = "PY", Name = "PARAGUAY" });
            _countries.Add(new CountryType() { CountryCode = "PE", Name = "PERU" });
            _countries.Add(new CountryType() { CountryCode = "PH", Name = "FİLİPİNLER CUMHURİYETİ" });
            _countries.Add(new CountryType() { CountryCode = "PN", Name = "PİTCAİRN" });
            _countries.Add(new CountryType() { CountryCode = "PL", Name = "POLONYA" });
            _countries.Add(new CountryType() { CountryCode = "PT", Name = "PORTEKİZ" });
            _countries.Add(new CountryType() { CountryCode = "PR", Name = "PORTO RİKO" });
            _countries.Add(new CountryType() { CountryCode = "QA", Name = "KATAR" });
            _countries.Add(new CountryType() { CountryCode = "RE", Name = "REUNİON" });
            _countries.Add(new CountryType() { CountryCode = "RO", Name = "ROMANYA" });
            _countries.Add(new CountryType() { CountryCode = "RU", Name = "RUSYA FEDERASYONU (THE)" });
            _countries.Add(new CountryType() { CountryCode = "RW", Name = "RUANDA" });
            _countries.Add(new CountryType() { CountryCode = "BL", Name = "AZİZ BARTHELEMY" });
            _countries.Add(new CountryType() { CountryCode = "SH", Name = "SAİNT HELENA, YÜKSELİŞ VE TRİSTAN DA CUNHA" });
            _countries.Add(new CountryType() { CountryCode = "KN", Name = "SAİNT KİTTS VE NEVİS" });
            _countries.Add(new CountryType() { CountryCode = "LC", Name = "SAİNT LUCİA" });
            _countries.Add(new CountryType() { CountryCode = "MF", Name = "SAİNT MARTİN (FRANSIZCA BÖLÜM)" });
            _countries.Add(new CountryType() { CountryCode = "PM", Name = "SAİNT PİERRE VE MİQUELON" });
            _countries.Add(new CountryType() { CountryCode = "VC", Name = "SAİNT VİNCENT VE GRENADİNLER" });
            _countries.Add(new CountryType() { CountryCode = "WS", Name = "SAMOA" });
            _countries.Add(new CountryType() { CountryCode = "SM", Name = "SAN MARİNO" });
            _countries.Add(new CountryType() { CountryCode = "ST", Name = "SAO TOME VE PRİNCİPE" });
            _countries.Add(new CountryType() { CountryCode = "SA", Name = "SUUDİ ARABİSTAN" });
            _countries.Add(new CountryType() { CountryCode = "SN", Name = "SENEGAL" });
            _countries.Add(new CountryType() { CountryCode = "RS", Name = "SIRBİSTAN" });
            _countries.Add(new CountryType() { CountryCode = "SC", Name = "SEYŞELLER" });
            _countries.Add(new CountryType() { CountryCode = "SL", Name = "SİERRA LEONE" });
            _countries.Add(new CountryType() { CountryCode = "SG", Name = "SİNGAPUR" });
            _countries.Add(new CountryType() { CountryCode = "SX", Name = "SİNT MAARTEN (HOLLANDACA BÖLÜM)" });
            _countries.Add(new CountryType() { CountryCode = "SK", Name = "SLOVAKYA" });
            _countries.Add(new CountryType() { CountryCode = "SI", Name = "SLOVENYA" });
            _countries.Add(new CountryType() { CountryCode = "SB", Name = "SOLOMON ADALARI" });
            _countries.Add(new CountryType() { CountryCode = "SO", Name = "SOMALİ" });
            _countries.Add(new CountryType() { CountryCode = "ZA", Name = "GÜNEY AFRİKA" });
            _countries.Add(new CountryType() { CountryCode = "GS", Name = "GÜNEY GEORGİA VE GÜNEY SANDWİCH ADALARI" });
            _countries.Add(new CountryType() { CountryCode = "SS", Name = "GÜNEY SUDAN" });
            _countries.Add(new CountryType() { CountryCode = "ES", Name = "İSPANYA" });
            _countries.Add(new CountryType() { CountryCode = "LK", Name = "SRİ LANKA" });
            _countries.Add(new CountryType() { CountryCode = "SD", Name = "SUDAN" });
            _countries.Add(new CountryType() { CountryCode = "SR", Name = "SURİNAME" });
            _countries.Add(new CountryType() { CountryCode = "SJ", Name = "SVALBARD VE JAN MAYEN" });
            _countries.Add(new CountryType() { CountryCode = "SE", Name = "İSVEÇ" });
            _countries.Add(new CountryType() { CountryCode = "CH", Name = "İSVİÇRE" });
            _countries.Add(new CountryType() { CountryCode = "SY", Name = "SURİYE ARAP CUMHURİYETİ" });
            _countries.Add(new CountryType() { CountryCode = "TW", Name = "TAYVAN (ÇİN EYALETİ)" });
            _countries.Add(new CountryType() { CountryCode = "TJ", Name = "TACİKİSTAN" });
            _countries.Add(new CountryType() { CountryCode = "TZ", Name = "TANZANYA, BİRLEŞİK CUMHURİYETİ" });
            _countries.Add(new CountryType() { CountryCode = "TH", Name = "TAYLAND" });
            _countries.Add(new CountryType() { CountryCode = "TL", Name = "DOĞU TİMOR" });
            _countries.Add(new CountryType() { CountryCode = "TG", Name = "TOGO" });
            _countries.Add(new CountryType() { CountryCode = "TK", Name = "TOKELAU" });
            _countries.Add(new CountryType() { CountryCode = "TO", Name = "TONGA" });
            _countries.Add(new CountryType() { CountryCode = "TT", Name = "TRİNİDAD VE TOBAGO" });
            _countries.Add(new CountryType() { CountryCode = "TN", Name = "TUNUS" });
            _countries.Add(new CountryType() { CountryCode = "TR", Name = "TURKİYE" });
            _countries.Add(new CountryType() { CountryCode = "TM", Name = "TURKMENİSTAN" });
            _countries.Add(new CountryType() { CountryCode = "TC", Name = "TURKS VE CAİCOS ADALARI" });
            _countries.Add(new CountryType() { CountryCode = "TV", Name = "TUVALU" });
            _countries.Add(new CountryType() { CountryCode = "UG", Name = "UGANDA" });
            _countries.Add(new CountryType() { CountryCode = "UA", Name = "UKRAYNA" });
            _countries.Add(new CountryType() { CountryCode = "AE", Name = "BİRLEŞİK ARAP EMİRLİKLERİ" });
            _countries.Add(new CountryType() { CountryCode = "GB", Name = "BİRLEŞİK KRALLIK" });
            _countries.Add(new CountryType() { CountryCode = "UM", Name = "AMERİKA BİRLEŞİK DEVLETLERİ KÜÇÜK DIŞ ADALARI" });
            _countries.Add(new CountryType() { CountryCode = "US", Name = "AMERİKA BİRLEŞİK DEVLETLERİ" });
            _countries.Add(new CountryType() { CountryCode = "UY", Name = "URUGUAY" });
            _countries.Add(new CountryType() { CountryCode = "UZ", Name = "ÖZBEKİSTAN" });
            _countries.Add(new CountryType() { CountryCode = "VU", Name = "VANUATU" });
            _countries.Add(new CountryType() { CountryCode = "VE", Name = "VENEZUELA (BOLİVARCI CUMHURİYETİ)" });
            _countries.Add(new CountryType() { CountryCode = "VN", Name = "VİETNAM" });
            _countries.Add(new CountryType() { CountryCode = "VG", Name = "VİRGİN ADALARI (İNGİLİZ)" });
            _countries.Add(new CountryType() { CountryCode = "VI", Name = "VİRJİN ADALARI (ABD)" });
            _countries.Add(new CountryType() { CountryCode = "WF", Name = "WALLİS VE FUTUNA ADALARI" });
            _countries.Add(new CountryType() { CountryCode = "EH", Name = "BATI SAHRA" });
            _countries.Add(new CountryType() { CountryCode = "YE", Name = "YEMEN" });
            _countries.Add(new CountryType() { CountryCode = "ZM", Name = "ZAMBİA" });
            _countries.Add(new CountryType() { CountryCode = "ZW", Name = "ZİMBABVE" });
            _countries.Add(new CountryType() { CountryCode = "XK", Name = "KOSOVA" });
        }
    }
}
