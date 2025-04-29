namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.ConstRepository
{
    public class CurrencyType
    {
        public int Id { get; set; }

        //Para Birimi Kodu
        public string Code { get; set; }

        //Adı
        public string Name { get; set; }

        /// <summary>
        /// Döviz türleri . en çok kullanılanlar en başa gelecek
        /// </summary>
        public static List<CurrencyType> GetCurrencyList()
        {
            return _currencyType;
        }

        private static List<CurrencyType> _currencyType;

        private static object lockSys = new object();

        static CurrencyType()
        {
            lock (lockSys)
            {
                buildCurrencyType();
            }
        }

        private static void buildCurrencyType()
        {
            _currencyType = new List<CurrencyType>();
            _currencyType.Add(new CurrencyType() { Code = "TRL", Name = "Turkish Lira" });
            _currencyType.Add(new CurrencyType() { Code = "USD", Name = "US Dollar" });
            _currencyType.Add(new CurrencyType() { Code = "EUR", Name = "Euro" });
            _currencyType.Add(new CurrencyType() { Code = "AED", Name = "Dirham" });
            _currencyType.Add(new CurrencyType() { Code = "AFN", Name = "Afghani" });
            _currencyType.Add(new CurrencyType() { Code = "ALL", Name = "Lek" });
            _currencyType.Add(new CurrencyType() { Code = "AMD", Name = "Dram" });
            _currencyType.Add(new CurrencyType() { Code = "ANG", Name = "Netherlands Antillian Guilder" });
            _currencyType.Add(new CurrencyType() { Code = "AOA", Name = "Kwanza" });
            _currencyType.Add(new CurrencyType() { Code = "ARS", Name = "Argentine Peso" });
            _currencyType.Add(new CurrencyType() { Code = "AUD", Name = "Australian Dollar" });
            _currencyType.Add(new CurrencyType() { Code = "AWG", Name = "Aruban Guilder" });
            _currencyType.Add(new CurrencyType() { Code = "AZM", Name = "Azerbaijanian Manat" });
            _currencyType.Add(new CurrencyType() { Code = "BAM", Name = "Convertible Mark" });
            _currencyType.Add(new CurrencyType() { Code = "BBD", Name = "Barbados Dollar" });
            _currencyType.Add(new CurrencyType() { Code = "BDT", Name = "Taka" });
            _currencyType.Add(new CurrencyType() { Code = "BGN", Name = "Bulgarian Lev" });
            _currencyType.Add(new CurrencyType() { Code = "BHD", Name = "Bahraini Dinar" });
            _currencyType.Add(new CurrencyType() { Code = "BIF", Name = "Burundi Franc" });
            _currencyType.Add(new CurrencyType() { Code = "BMD", Name = "Bermudian Dollar (customarily: Bermuda Dollar)" });
            _currencyType.Add(new CurrencyType() { Code = "BND", Name = "Brunei Dollar" });
            _currencyType.Add(new CurrencyType() { Code = "BOB", Name = "Boliviano" });
            _currencyType.Add(new CurrencyType() { Code = "BRL", Name = "Brazilian Real" });
            _currencyType.Add(new CurrencyType() { Code = "BSD", Name = "Bahamian Dollar" });
            _currencyType.Add(new CurrencyType() { Code = "BTN", Name = "Ngultrum" });
            _currencyType.Add(new CurrencyType() { Code = "BWP", Name = "Pula" });
            _currencyType.Add(new CurrencyType() { Code = "BYR", Name = "Belarussian Ruble" });
            _currencyType.Add(new CurrencyType() { Code = "BZD", Name = "Belize Dollar" });
            _currencyType.Add(new CurrencyType() { Code = "CAD", Name = "Canadian Dollar" });
            _currencyType.Add(new CurrencyType() { Code = "CDF", Name = "Franc Congolais" });
            _currencyType.Add(new CurrencyType() { Code = "CHF", Name = "Swiss Franc" });
            _currencyType.Add(new CurrencyType() { Code = "CLP", Name = "Chilean Peso" });
            _currencyType.Add(new CurrencyType() { Code = "CNY", Name = "Yuan Renminbi" });
            _currencyType.Add(new CurrencyType() { Code = "COP", Name = "Colombian Peso" });
            _currencyType.Add(new CurrencyType() { Code = "CRC", Name = "Costa Rican Colon" });
            _currencyType.Add(new CurrencyType() { Code = "CUP", Name = "Cuban Peso" });
            _currencyType.Add(new CurrencyType() { Code = "CVE", Name = "Cape Verde Escudo" });
            _currencyType.Add(new CurrencyType() { Code = "CYP", Name = "Cyprus Pound" });
            _currencyType.Add(new CurrencyType() { Code = "CZK", Name = "Czech Koruna" });
            _currencyType.Add(new CurrencyType() { Code = "DJF", Name = "Djibouti Franc" });
            _currencyType.Add(new CurrencyType() { Code = "DKK", Name = "Danish Krone" });
            _currencyType.Add(new CurrencyType() { Code = "DOP", Name = "Dominican Peso" });
            _currencyType.Add(new CurrencyType() { Code = "DZD", Name = "Algerian Dinar" });
            _currencyType.Add(new CurrencyType() { Code = "EEK", Name = "Kroon" });
            _currencyType.Add(new CurrencyType() { Code = "EGP", Name = "Egyptian Pound" });
            _currencyType.Add(new CurrencyType() { Code = "ERN", Name = "Nakfa" });
            _currencyType.Add(new CurrencyType() { Code = "ETB", Name = "Ethopian Birr" });
            _currencyType.Add(new CurrencyType() { Code = "FJD", Name = "Fiji Dollar" });
            _currencyType.Add(new CurrencyType() { Code = "FKP", Name = "Falkland Islands Pound" });
            _currencyType.Add(new CurrencyType() { Code = "GBP", Name = "Pound Sterling" });
            _currencyType.Add(new CurrencyType() { Code = "GEL", Name = "Lari" });
            _currencyType.Add(new CurrencyType() { Code = "GHC", Name = "Cedi" });
            _currencyType.Add(new CurrencyType() { Code = "GIP", Name = "Gibraltar Pound" });
            _currencyType.Add(new CurrencyType() { Code = "GMD", Name = "Dalasi" });
            _currencyType.Add(new CurrencyType() { Code = "GNF", Name = "Guinea Franc" });
            _currencyType.Add(new CurrencyType() { Code = "GTQ", Name = "Quetzal" });
            _currencyType.Add(new CurrencyType() { Code = "GYD", Name = "Guyana Dollar" });
            _currencyType.Add(new CurrencyType() { Code = "HKD", Name = "Honk Kong Dollar" });
            _currencyType.Add(new CurrencyType() { Code = "HNL", Name = "Lempira" });
            _currencyType.Add(new CurrencyType() { Code = "HRK", Name = "Kuna" });
            _currencyType.Add(new CurrencyType() { Code = "HTG", Name = "Gourde" });
            _currencyType.Add(new CurrencyType() { Code = "HUF", Name = "Forint" });
            _currencyType.Add(new CurrencyType() { Code = "IDR", Name = "Rupiah" });
            _currencyType.Add(new CurrencyType() { Code = "ILS", Name = "New Israeli Sheqel" });
            _currencyType.Add(new CurrencyType() { Code = "INR", Name = "Indian Rupee" });
            _currencyType.Add(new CurrencyType() { Code = "IQD", Name = "Iraqi Dinar" });
            _currencyType.Add(new CurrencyType() { Code = "IRR", Name = "Iranian Rial" });
            _currencyType.Add(new CurrencyType() { Code = "ISK", Name = "Iceland Krona" });
            _currencyType.Add(new CurrencyType() { Code = "JMD", Name = "Jamaican Dollar" });
            _currencyType.Add(new CurrencyType() { Code = "JOD", Name = "Jordanian Dinar" });
            _currencyType.Add(new CurrencyType() { Code = "JPY", Name = "Yen" });
            _currencyType.Add(new CurrencyType() { Code = "KES", Name = "Kenyan Shilling" });
            _currencyType.Add(new CurrencyType() { Code = "KGS", Name = "Som" });
            _currencyType.Add(new CurrencyType() { Code = "KHR", Name = "Riel" });
            _currencyType.Add(new CurrencyType() { Code = "KMF", Name = "Comoro Franc" });
            _currencyType.Add(new CurrencyType() { Code = "KPW", Name = "North Korean Won" });
            _currencyType.Add(new CurrencyType() { Code = "KRW", Name = "Won" });
            _currencyType.Add(new CurrencyType() { Code = "KWD", Name = "Kuwaiti Dinar" });
            _currencyType.Add(new CurrencyType() { Code = "KYD", Name = "Cayman Islands Dollar" });
            _currencyType.Add(new CurrencyType() { Code = "KZT", Name = "Tenge" });
            _currencyType.Add(new CurrencyType() { Code = "LAK", Name = "Kip" });
            _currencyType.Add(new CurrencyType() { Code = "LBP", Name = "Lebanese Pound" });
            _currencyType.Add(new CurrencyType() { Code = "LKR", Name = "Sri Lanka Rupee" });
            _currencyType.Add(new CurrencyType() { Code = "LRD", Name = "Liberian Dollar" });
            _currencyType.Add(new CurrencyType() { Code = "LSL", Name = "Loti" });
            _currencyType.Add(new CurrencyType() { Code = "LTL", Name = "Lithuanian Litas" });
            _currencyType.Add(new CurrencyType() { Code = "LVL", Name = "Latvian Lats" });
            _currencyType.Add(new CurrencyType() { Code = "LYD", Name = "Libyan Dinar" });
            _currencyType.Add(new CurrencyType() { Code = "MAD", Name = "Morrocan Dirham" });
            _currencyType.Add(new CurrencyType() { Code = "MDL", Name = "Moldovan Leu" });
            _currencyType.Add(new CurrencyType() { Code = "MGF", Name = "Malagasy Franc" });
            _currencyType.Add(new CurrencyType() { Code = "MKD", Name = "Denar" });
            _currencyType.Add(new CurrencyType() { Code = "MMK", Name = "Kyat" });
            _currencyType.Add(new CurrencyType() { Code = "MNT", Name = "Tugrik" });
            _currencyType.Add(new CurrencyType() { Code = "MOP", Name = "Pataca" });
            _currencyType.Add(new CurrencyType() { Code = "MRO", Name = "Ouguiya" });
            _currencyType.Add(new CurrencyType() { Code = "MTL", Name = "Maltese Lira" });
            _currencyType.Add(new CurrencyType() { Code = "MUR", Name = "Mauritius Rupee" });
            _currencyType.Add(new CurrencyType() { Code = "MVR", Name = "Rufiyaa" });
            _currencyType.Add(new CurrencyType() { Code = "MWK", Name = "Kwacha" });
            _currencyType.Add(new CurrencyType() { Code = "MXN", Name = "Mexican Peso" });
            _currencyType.Add(new CurrencyType() { Code = "MYR", Name = "Malaysian Ringgit" });
            _currencyType.Add(new CurrencyType() { Code = "MZM", Name = "Metical" });
            _currencyType.Add(new CurrencyType() { Code = "NAD", Name = "Namibia Dollar" });
            _currencyType.Add(new CurrencyType() { Code = "NGN", Name = "Naira" });
            _currencyType.Add(new CurrencyType() { Code = "NIO", Name = "Cordoba Oro" });
            _currencyType.Add(new CurrencyType() { Code = "NOK", Name = "Norwegian Krone" });
            _currencyType.Add(new CurrencyType() { Code = "NPR", Name = "Nepalese Rupee" });
            _currencyType.Add(new CurrencyType() { Code = "NZD", Name = "New Zealand Dollar" });
            _currencyType.Add(new CurrencyType() { Code = "OMR", Name = "Rial Omani" });
            _currencyType.Add(new CurrencyType() { Code = "PAB", Name = "Balboa" });
            _currencyType.Add(new CurrencyType() { Code = "PEN", Name = "Nuevo Sol" });
            _currencyType.Add(new CurrencyType() { Code = "PGK", Name = "Kina" });
            _currencyType.Add(new CurrencyType() { Code = "PHP", Name = "Philippine Peso" });
            _currencyType.Add(new CurrencyType() { Code = "PKR", Name = "Pakistan Rupee" });
            _currencyType.Add(new CurrencyType() { Code = "PLN", Name = "Zloty" });
            _currencyType.Add(new CurrencyType() { Code = "PYG", Name = "Guarani" });
            _currencyType.Add(new CurrencyType() { Code = "QAR", Name = "Qatari Rial" });
            _currencyType.Add(new CurrencyType() { Code = "ROL", Name = "Leu" });
            _currencyType.Add(new CurrencyType() { Code = "RUB", Name = "Russian Ruble" });
            _currencyType.Add(new CurrencyType() { Code = "RWF", Name = "Rwanda Franc" });
            _currencyType.Add(new CurrencyType() { Code = "SAR", Name = "Saudi Riyal" });
            _currencyType.Add(new CurrencyType() { Code = "SBD", Name = "Solomon Islands Dollar" });
            _currencyType.Add(new CurrencyType() { Code = "SCR", Name = "Seychelles Rupee" });
            _currencyType.Add(new CurrencyType() { Code = "SDD", Name = "Sudanese Dinar" });
            _currencyType.Add(new CurrencyType() { Code = "SEK", Name = "Swedish Krona" });
            _currencyType.Add(new CurrencyType() { Code = "SGD", Name = "Singapore Dollar" });
            _currencyType.Add(new CurrencyType() { Code = "SHP", Name = "St. Helena Pound" });
            _currencyType.Add(new CurrencyType() { Code = "SIT", Name = "Tolar" });
            _currencyType.Add(new CurrencyType() { Code = "SKK", Name = "Slovak Koruna" });
            _currencyType.Add(new CurrencyType() { Code = "SLL", Name = "Leone" });
            _currencyType.Add(new CurrencyType() { Code = "SOS", Name = "Somali Shilling" });
            _currencyType.Add(new CurrencyType() { Code = "SRG", Name = "Suriname Guilder" });
            _currencyType.Add(new CurrencyType() { Code = "STD", Name = "Dobra" });
            _currencyType.Add(new CurrencyType() { Code = "SVC", Name = "El Salvador Colon" });
            _currencyType.Add(new CurrencyType() { Code = "SYP", Name = "Syrian Pound" });
            _currencyType.Add(new CurrencyType() { Code = "SZL", Name = "Lilangeni" });
            _currencyType.Add(new CurrencyType() { Code = "THB", Name = "Baht" });
            _currencyType.Add(new CurrencyType() { Code = "TJS", Name = "Somoni" });
            _currencyType.Add(new CurrencyType() { Code = "TMM", Name = "Manat" });
            _currencyType.Add(new CurrencyType() { Code = "TND", Name = "Tunisian Dinar" });
            _currencyType.Add(new CurrencyType() { Code = "TOP", Name = "Paanga" });
            _currencyType.Add(new CurrencyType() { Code = "TTD", Name = "Trinidad and Tobago Dollar" });
            _currencyType.Add(new CurrencyType() { Code = "TWD", Name = "New Taiwan Dollar" });
            _currencyType.Add(new CurrencyType() { Code = "TZS", Name = "Tanzanian Shilling" });
            _currencyType.Add(new CurrencyType() { Code = "UAH", Name = "Hryvnia" });
            _currencyType.Add(new CurrencyType() { Code = "UGX", Name = "Uganda Shilling" });
            _currencyType.Add(new CurrencyType() { Code = "UYU", Name = "Peso Uruguayo" });
            _currencyType.Add(new CurrencyType() { Code = "UZS", Name = "Uzbekistan Sum" });
            _currencyType.Add(new CurrencyType() { Code = "VEF", Name = "Venezuela Bolivar" });
            _currencyType.Add(new CurrencyType() { Code = "VND", Name = "Dong" });
            _currencyType.Add(new CurrencyType() { Code = "VUV", Name = "Vatu" });
            _currencyType.Add(new CurrencyType() { Code = "WST", Name = "Tala" });
            _currencyType.Add(new CurrencyType() { Code = "XAF", Name = "CFA Franc" });
            _currencyType.Add(new CurrencyType() { Code = "XAG", Name = "Silver" });
            _currencyType.Add(new CurrencyType() { Code = "XAU", Name = "Gold" });
            _currencyType.Add(new CurrencyType() { Code = "XCD", Name = "East Carribean Dollar" });
            _currencyType.Add(new CurrencyType() { Code = "XDR", Name = "SDR" });
            _currencyType.Add(new CurrencyType() { Code = "XOF", Name = "CFA Franc" });
            _currencyType.Add(new CurrencyType() { Code = "XPD", Name = "Palladium" });
            _currencyType.Add(new CurrencyType() { Code = "XPF", Name = "CFP Franc" });
            _currencyType.Add(new CurrencyType() { Code = "XPT", Name = "Platinum" });
            _currencyType.Add(new CurrencyType() { Code = "YER", Name = "Yemeni Rial" });
            _currencyType.Add(new CurrencyType() { Code = "YUM", Name = "New Dinar" });
            _currencyType.Add(new CurrencyType() { Code = "ZAR", Name = "Rand" });
            _currencyType.Add(new CurrencyType() { Code = "ZMK", Name = "Kwacha" });
            _currencyType.Add(new CurrencyType() { Code = "ZWD", Name = "Zimbabwe Dollar" });
        }
    }
}
