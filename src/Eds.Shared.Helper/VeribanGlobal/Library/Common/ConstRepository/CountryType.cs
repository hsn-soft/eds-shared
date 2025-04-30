namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.ConstRepository
{
    public class CountryType
    {

        public int Id { get; set; }

        //Ülke Adı
        public string Name { get; set; }

        //Ülke Kodu
        public string CountryCode { get; set; }

        private static List<CountryType> _countries;

        private static object lockSys = new object();


        static CountryType()
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

        public static void buildCountry()
        {
            _countries = new List<CountryType>();
            _countries.Add(new CountryType() { CountryCode = "-", Name = "Diğer" });
            _countries.Add(new CountryType() { CountryCode = "TR", Name = "Türkiye" });
            _countries.Add(new CountryType() { CountryCode = "USA", Name = "ABD" });
            _countries.Add(new CountryType() { CountryCode = "ISV", Name = "ABD Virgin Adaları" });
            _countries.Add(new CountryType() { CountryCode = "AFG", Name = "Afganistan" });
            _countries.Add(new CountryType() { CountryCode = "GER", Name = "Almanya" });
            _countries.Add(new CountryType() { CountryCode = "ASA", Name = "Amerikan Samoası" });
            _countries.Add(new CountryType() { CountryCode = "AND", Name = "Andora" });
            _countries.Add(new CountryType() { CountryCode = "ANG", Name = "Angola" });
            _countries.Add(new CountryType() { CountryCode = "ANT", Name = "Antigua ve Barbuda" });
            _countries.Add(new CountryType() { CountryCode = "ARG", Name = "Arjantin" });
            _countries.Add(new CountryType() { CountryCode = "ALB", Name = "Arnavutluk" });
            _countries.Add(new CountryType() { CountryCode = "ARU", Name = "Aruba" });
            _countries.Add(new CountryType() { CountryCode = "AUS", Name = "Avustralya" });
            _countries.Add(new CountryType() { CountryCode = "AUT", Name = "Avusturya" });
            _countries.Add(new CountryType() { CountryCode = "AZE", Name = "Azerbaycan" });
            _countries.Add(new CountryType() { CountryCode = "BAH", Name = "Bahamalar" });
            _countries.Add(new CountryType() { CountryCode = "BRN", Name = "Bahreyn" });
            _countries.Add(new CountryType() { CountryCode = "BAN", Name = "Bangladeş" });
            _countries.Add(new CountryType() { CountryCode = "BAR", Name = "Barbados Barbados" });
            _countries.Add(new CountryType() { CountryCode = "BLR", Name = "Belarus" });
            _countries.Add(new CountryType() { CountryCode = "BEL", Name = "Belçika" });
            _countries.Add(new CountryType() { CountryCode = "BIZ", Name = "Belize" });
            _countries.Add(new CountryType() { CountryCode = "BEN", Name = "Benin" });
            _countries.Add(new CountryType() { CountryCode = "BER", Name = "Bermuda" });
            _countries.Add(new CountryType() { CountryCode = "BHU", Name = "Bhutan" });
            _countries.Add(new CountryType() { CountryCode = "UAE", Name = "Birleşik Arap Emirlikleri" });
            _countries.Add(new CountryType() { CountryCode = "GBR", Name = "Birleşik Krallık" });
            _countries.Add(new CountryType() { CountryCode = "BOL", Name = "Bolivya" });
            _countries.Add(new CountryType() { CountryCode = "BIH", Name = "Bosna Hersek" });
            _countries.Add(new CountryType() { CountryCode = "BOT", Name = "Botsvana" });
            _countries.Add(new CountryType() { CountryCode = "BRA", Name = "Brezilya" });
            _countries.Add(new CountryType() { CountryCode = "IVB", Name = "Britanya Virjin Adaları" });
            _countries.Add(new CountryType() { CountryCode = "BRU", Name = "Brunei" });
            _countries.Add(new CountryType() { CountryCode = "BUL", Name = "Bulgaristan" });
            _countries.Add(new CountryType() { CountryCode = "BUR", Name = "Burkina Faso" });
            _countries.Add(new CountryType() { CountryCode = "BDI", Name = "Burundi" });
            _countries.Add(new CountryType() { CountryCode = "CPV", Name = "Cape Verde" });
            _countries.Add(new CountryType() { CountryCode = "CAY", Name = "Cayman Adaları" });
            _countries.Add(new CountryType() { CountryCode = "ALG", Name = "Cezayir" });
            _countries.Add(new CountryType() { CountryCode = "TPE", Name = "Chinese Taipei" });
            _countries.Add(new CountryType() { CountryCode = "DJI", Name = "Cibuti" });
            _countries.Add(new CountryType() { CountryCode = "COK", Name = "Cook Adaları" });
            _countries.Add(new CountryType() { CountryCode = "CHA", Name = "Çad" });
            _countries.Add(new CountryType() { CountryCode = "CZE", Name = "Çek Cumhuriyeti" });
            _countries.Add(new CountryType() { CountryCode = "CHN", Name = "Çin" });
            _countries.Add(new CountryType() { CountryCode = "DEN", Name = "Danimarka" });
            _countries.Add(new CountryType() { CountryCode = "COD", Name = "Demokratik Kongo Cumhuriyeti" });
            _countries.Add(new CountryType() { CountryCode = "TLS", Name = "Doğu Timor" });
            _countries.Add(new CountryType() { CountryCode = "DOM", Name = "Dominik Cumhuriyeti" });
            _countries.Add(new CountryType() { CountryCode = "DMA", Name = "Dominika" });
            _countries.Add(new CountryType() { CountryCode = "ECU", Name = "Ekvador" });
            _countries.Add(new CountryType() { CountryCode = "GEQ", Name = "Ekvator Ginesi" });
            _countries.Add(new CountryType() { CountryCode = "ESA", Name = "El Salvador" });
            _countries.Add(new CountryType() { CountryCode = "INA", Name = "Endonezya" });
            _countries.Add(new CountryType() { CountryCode = "ERI", Name = "Eritre" });
            _countries.Add(new CountryType() { CountryCode = "ARM", Name = "Ermenistan" });
            _countries.Add(new CountryType() { CountryCode = "EST", Name = "Estonya" });
            _countries.Add(new CountryType() { CountryCode = "ETH", Name = "Etiyopya" });
            _countries.Add(new CountryType() { CountryCode = "MAR", Name = "Fas" });
            _countries.Add(new CountryType() { CountryCode = "FIJ", Name = "Fiji" });
            _countries.Add(new CountryType() { CountryCode = "CIV", Name = "Fildişi Sahili" });
            _countries.Add(new CountryType() { CountryCode = "PHI", Name = "Filipinler" });
            _countries.Add(new CountryType() { CountryCode = "PLE", Name = "Filistin" });
            _countries.Add(new CountryType() { CountryCode = "FIN", Name = "Finlandiya" });
            _countries.Add(new CountryType() { CountryCode = "FRA", Name = "Fransa" });
            _countries.Add(new CountryType() { CountryCode = "GAB", Name = "Gabon" });
            _countries.Add(new CountryType() { CountryCode = "GAM", Name = "Gambiya" });
            _countries.Add(new CountryType() { CountryCode = "GHA", Name = "Gana" });
            _countries.Add(new CountryType() { CountryCode = "GUI", Name = "Gine" });
            _countries.Add(new CountryType() { CountryCode = "GBS", Name = "Gine Bissau" });
            _countries.Add(new CountryType() { CountryCode = "GRN", Name = "Grenada" });
            _countries.Add(new CountryType() { CountryCode = "GUM", Name = "Guam" });
            _countries.Add(new CountryType() { CountryCode = "GUA", Name = "Guatemala" });
            _countries.Add(new CountryType() { CountryCode = "GUY", Name = "Guyana" });
            _countries.Add(new CountryType() { CountryCode = "RSA", Name = "Güney Afrika" });
            _countries.Add(new CountryType() { CountryCode = "KOR", Name = "Güney Kore" });
            _countries.Add(new CountryType() { CountryCode = "GEO", Name = "Gürcistan" });
            _countries.Add(new CountryType() { CountryCode = "HAI", Name = "Haiti" });
            _countries.Add(new CountryType() { CountryCode = "CRO", Name = "Hırvatistan" });
            _countries.Add(new CountryType() { CountryCode = "IND", Name = "Hindistan" });
            _countries.Add(new CountryType() { CountryCode = "NED", Name = "Hollanda" });
            _countries.Add(new CountryType() { CountryCode = "AHO", Name = "Hollanda Antilleri" });
            _countries.Add(new CountryType() { CountryCode = "HON", Name = "Honduras" });
            _countries.Add(new CountryType() { CountryCode = "HKG", Name = "Hong Kong" });
            _countries.Add(new CountryType() { CountryCode = "IRQ", Name = "Irak" });
            _countries.Add(new CountryType() { CountryCode = "IRI", Name = "İran" });
            _countries.Add(new CountryType() { CountryCode = "IRL", Name = "İrlanda" });
            _countries.Add(new CountryType() { CountryCode = "ESP", Name = "İspanya" });
            _countries.Add(new CountryType() { CountryCode = "ISR", Name = "İsrail" });
            _countries.Add(new CountryType() { CountryCode = "SWE", Name = "İsveç" });
            _countries.Add(new CountryType() { CountryCode = "SUI", Name = "İsviçre" });
            _countries.Add(new CountryType() { CountryCode = "ITA", Name = "İtalya" });
            _countries.Add(new CountryType() { CountryCode = "ISL", Name = "İzlanda" });
            _countries.Add(new CountryType() { CountryCode = "JAM", Name = "Jamaika" });
            _countries.Add(new CountryType() { CountryCode = "JPN", Name = "Japonya" });
            _countries.Add(new CountryType() { CountryCode = "CAM", Name = "Kamboçya" });
            _countries.Add(new CountryType() { CountryCode = "CMR", Name = "Kamerun" });
            _countries.Add(new CountryType() { CountryCode = "CAN", Name = "Kanada" });
            _countries.Add(new CountryType() { CountryCode = "MNE", Name = "Karadağ" });
            _countries.Add(new CountryType() { CountryCode = "QAT", Name = "Katar" });
            _countries.Add(new CountryType() { CountryCode = "KAZ", Name = "Kazakistan" });
            _countries.Add(new CountryType() { CountryCode = "KEN", Name = "Kenya" });
            _countries.Add(new CountryType() { CountryCode = "CYP", Name = "Kıbrıs" });
            _countries.Add(new CountryType() { CountryCode = "KGZ", Name = "Kırgızistan" });
            _countries.Add(new CountryType() { CountryCode = "KIR", Name = "Kiribati" });
            _countries.Add(new CountryType() { CountryCode = "COL", Name = "Kolombiya" });
            _countries.Add(new CountryType() { CountryCode = "COM", Name = "Komorlar" });
            _countries.Add(new CountryType() { CountryCode = "CGO", Name = "Kongo" });
            _countries.Add(new CountryType() { CountryCode = "CRC", Name = "Kosta Rika" });
            _countries.Add(new CountryType() { CountryCode = "KUW", Name = "Kuveyt" });
            _countries.Add(new CountryType() { CountryCode = "PRK", Name = "Kuzey Kore" });
            _countries.Add(new CountryType() { CountryCode = "CUB", Name = "Küba" });
            _countries.Add(new CountryType() { CountryCode = "LAO", Name = "Laos" });
            _countries.Add(new CountryType() { CountryCode = "LES", Name = "Lesoto" });
            _countries.Add(new CountryType() { CountryCode = "LAT", Name = "Letonya" });
            _countries.Add(new CountryType() { CountryCode = "LBR", Name = "Liberya" });
            _countries.Add(new CountryType() { CountryCode = "LBA", Name = "Libya" });
            _countries.Add(new CountryType() { CountryCode = "LIE", Name = "Liechtenstein" });
            _countries.Add(new CountryType() { CountryCode = "LTU", Name = "Litvanya" });
            _countries.Add(new CountryType() { CountryCode = "LIB", Name = "Lübnan" });
            _countries.Add(new CountryType() { CountryCode = "LUX", Name = "Lüksemburg" });
            _countries.Add(new CountryType() { CountryCode = "HUN", Name = "Macaristan" });
            _countries.Add(new CountryType() { CountryCode = "MAD", Name = "Madagaskar" });
            _countries.Add(new CountryType() { CountryCode = "MKD", Name = "Makedonya" });
            _countries.Add(new CountryType() { CountryCode = "MAW", Name = "Malavi" });
            _countries.Add(new CountryType() { CountryCode = "MDV", Name = "Maldivler" });
            _countries.Add(new CountryType() { CountryCode = "MAS", Name = "Malezya" });
            _countries.Add(new CountryType() { CountryCode = "MLI", Name = "Mali" });
            _countries.Add(new CountryType() { CountryCode = "MLT", Name = "Malta" });
            _countries.Add(new CountryType() { CountryCode = "MHL", Name = "Marşal Adaları" });
            _countries.Add(new CountryType() { CountryCode = "MRI", Name = "Mauritius" });
            _countries.Add(new CountryType() { CountryCode = "MEX", Name = "Meksika" });
            _countries.Add(new CountryType() { CountryCode = "EGY", Name = "Mısır" });
            _countries.Add(new CountryType() { CountryCode = "FSM", Name = "Mikronezya" });
            _countries.Add(new CountryType() { CountryCode = "MGL", Name = "Moğolistan" });
            _countries.Add(new CountryType() { CountryCode = "MDA", Name = "Moldova" });
            _countries.Add(new CountryType() { CountryCode = "MON", Name = "Monako" });
            _countries.Add(new CountryType() { CountryCode = "MTN", Name = "Moritanya" });
            _countries.Add(new CountryType() { CountryCode = "MOZ", Name = "Mozambik" });
            _countries.Add(new CountryType() { CountryCode = "MYA", Name = "Myanmar" });
            _countries.Add(new CountryType() { CountryCode = "NAM", Name = "Namibya" });
            _countries.Add(new CountryType() { CountryCode = "NRU", Name = "Nauru" });
            _countries.Add(new CountryType() { CountryCode = "NEP", Name = "Nepal" });
            _countries.Add(new CountryType() { CountryCode = "NIG", Name = "Nijer" });
            _countries.Add(new CountryType() { CountryCode = "NGR", Name = "Nijerya" });
            _countries.Add(new CountryType() { CountryCode = "NCA", Name = "Nikaragua" });
            _countries.Add(new CountryType() { CountryCode = "NOR", Name = "Norveç" });
            _countries.Add(new CountryType() { CountryCode = "CAF", Name = "Orta Afrika Cumhuriyeti" });
            _countries.Add(new CountryType() { CountryCode = "UZB", Name = "Özbekistan" });
            _countries.Add(new CountryType() { CountryCode = "PAK", Name = "Pakistan" });
            _countries.Add(new CountryType() { CountryCode = "PLW", Name = "Palau" });
            _countries.Add(new CountryType() { CountryCode = "PAN", Name = "Panama" });
            _countries.Add(new CountryType() { CountryCode = "PNG", Name = "Papua Yeni Gine" });
            _countries.Add(new CountryType() { CountryCode = "PAR", Name = "Paraguay" });
            _countries.Add(new CountryType() { CountryCode = "PER", Name = "Peru" });
            _countries.Add(new CountryType() { CountryCode = "POL", Name = "Polonya" });
            _countries.Add(new CountryType() { CountryCode = "POR", Name = "Portekiz" });
            _countries.Add(new CountryType() { CountryCode = "PUR", Name = "Porto Riko" });
            _countries.Add(new CountryType() { CountryCode = "ROU", Name = "Romanya" });
            _countries.Add(new CountryType() { CountryCode = "RWA", Name = "Ruanda" });
            _countries.Add(new CountryType() { CountryCode = "RUS", Name = "Rusya" });
            _countries.Add(new CountryType() { CountryCode = "SKN", Name = "Saint Kitts ve Nevis" });
            _countries.Add(new CountryType() { CountryCode = "LCA", Name = "Saint Lucia" });
            _countries.Add(new CountryType() { CountryCode = "VIN", Name = "Saint Vincent ve Grenadinler" });
            _countries.Add(new CountryType() { CountryCode = "SAM", Name = "Samoa" });
            _countries.Add(new CountryType() { CountryCode = "SMR", Name = "San Marino" });
            _countries.Add(new CountryType() { CountryCode = "STP", Name = "Sao Tome ve Principe" });
            _countries.Add(new CountryType() { CountryCode = "SEN", Name = "Senegal" });
            _countries.Add(new CountryType() { CountryCode = "SEY", Name = "Seyşeller" });
            _countries.Add(new CountryType() { CountryCode = "SRB", Name = "Sırbistan" });
            _countries.Add(new CountryType() { CountryCode = "SLE", Name = "Sierra Leone" });
            _countries.Add(new CountryType() { CountryCode = "SIN", Name = "Singapur" });
            _countries.Add(new CountryType() { CountryCode = "SVK", Name = "Slovakya" });
            _countries.Add(new CountryType() { CountryCode = "SLO", Name = "Slovenia" });
            _countries.Add(new CountryType() { CountryCode = "SOL", Name = "Solomon Adaları" });
            _countries.Add(new CountryType() { CountryCode = "SOM", Name = "Somali" });
            _countries.Add(new CountryType() { CountryCode = "SRI", Name = "Sri Lanka" });
            _countries.Add(new CountryType() { CountryCode = "SUD", Name = "Sudan" });
            _countries.Add(new CountryType() { CountryCode = "SUR", Name = "Surinam" });
            _countries.Add(new CountryType() { CountryCode = "SYR", Name = "Suriye" });
            _countries.Add(new CountryType() { CountryCode = "KSA", Name = "Suudi Arabistan" });
            _countries.Add(new CountryType() { CountryCode = "SWZ", Name = "Swaziland" });
            _countries.Add(new CountryType() { CountryCode = "CHI", Name = "Şili" });
            _countries.Add(new CountryType() { CountryCode = "TJK", Name = "Tacikistan" });
            _countries.Add(new CountryType() { CountryCode = "TAN", Name = "Tanzanya" });
            _countries.Add(new CountryType() { CountryCode = "THA", Name = "Tayland" });
            _countries.Add(new CountryType() { CountryCode = "TOG", Name = "Togo" });
            _countries.Add(new CountryType() { CountryCode = "TGA", Name = "Tonga" });
            _countries.Add(new CountryType() { CountryCode = "TRI", Name = "Trinidad ve Tobago" });
            _countries.Add(new CountryType() { CountryCode = "TUN", Name = "Tunus" });
            _countries.Add(new CountryType() { CountryCode = "TUV", Name = "Tuvalu" });
            _countries.Add(new CountryType() { CountryCode = "TKM", Name = "Türkmenistan" });
            _countries.Add(new CountryType() { CountryCode = "UGA", Name = "Uganda" });
            _countries.Add(new CountryType() { CountryCode = "UKR", Name = "Ukrayna" });
            _countries.Add(new CountryType() { CountryCode = "OMA", Name = "Umman" });
            _countries.Add(new CountryType() { CountryCode = "URU", Name = "Uruguay" });
            _countries.Add(new CountryType() { CountryCode = "JOR", Name = "Ürdün" });
            _countries.Add(new CountryType() { CountryCode = "VAN", Name = "Vanuatu" });
            _countries.Add(new CountryType() { CountryCode = "VEN", Name = "Venezuela" });
            _countries.Add(new CountryType() { CountryCode = "VIE", Name = "Vietnam" });
            _countries.Add(new CountryType() { CountryCode = "YEM", Name = "Yemen" });
            _countries.Add(new CountryType() { CountryCode = "NZL", Name = "Yeni Zelanda" });
            _countries.Add(new CountryType() { CountryCode = "GRE", Name = "Yunanistan" });
            _countries.Add(new CountryType() { CountryCode = "ZAM", Name = "Zambiya" });
            _countries.Add(new CountryType() { CountryCode = "ZIM", Name = "Zimbabve" });
        }
    }
}
