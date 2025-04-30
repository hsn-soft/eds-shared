namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.GlobalTypes.EArchive
{
    public sealed class Tax
    {
        private readonly String name;
        private readonly String code;
        private readonly String description;

        public static readonly Tax TaxType_0003 = new Tax("0003", "0003", "");
        public static readonly Tax TaxType_SGK_PRIM = new Tax("SGK_PRIM", "SGK_PRIM", "");
        public static readonly Tax TaxType_0015 = new Tax("0015", "0015", "");
        public static readonly Tax TaxType_0021 = new Tax("0021", "0021", "");
        public static readonly Tax TaxType_0061 = new Tax("0061", "0061", "");
        public static readonly Tax TaxType_0071 = new Tax("0071", "0071", "");
        public static readonly Tax TaxType_0073 = new Tax("0073", "0073", "");
        public static readonly Tax TaxType_0074 = new Tax("0074", "0074", "");
        public static readonly Tax TaxType_0075 = new Tax("0075", "0075", "");
        public static readonly Tax TaxType_0076 = new Tax("0076", "0076", "");
        public static readonly Tax TaxType_0077 = new Tax("0077", "0077", "");
        public static readonly Tax TaxType_1047 = new Tax("1047", "1047", "");
        public static readonly Tax TaxType_1048 = new Tax("1048", "1048", "");
        public static readonly Tax TaxType_4071 = new Tax("4071", "4071", "");
        public static readonly Tax TaxType_4080 = new Tax("4080", "4080", "");
        public static readonly Tax TaxType_4081 = new Tax("4081", "4081", "");
        public static readonly Tax TaxType_4171 = new Tax("4171", "4171", "");
        public static readonly Tax TaxType_8001 = new Tax("8001", "8001", "");
        public static readonly Tax TaxType_8002 = new Tax("8002", "8002", "");
        public static readonly Tax TaxType_8004 = new Tax("8004", "8004", "");
        public static readonly Tax TaxType_8005 = new Tax("8005", "8005", "");
        public static readonly Tax TaxType_8006 = new Tax("8006", "8006", "");
        public static readonly Tax TaxType_8007 = new Tax("8007", "8007", "");
        public static readonly Tax TaxType_8008 = new Tax("8008", "8008", "");
        public static readonly Tax TaxType_9015 = new Tax("9015", "9015", "");
        public static readonly Tax TaxType_9021 = new Tax("9021", "9021", "");
        public static readonly Tax TaxType_9040 = new Tax("9040", "9040", "");
        public static readonly Tax TaxType_9077 = new Tax("9077", "9077", "");

        public static readonly List<string> TaxTypeList = new List<string>
        {
             TaxType_0003.GetCode() ,
             TaxType_SGK_PRIM.GetCode() ,
             TaxType_0015.GetCode() ,
             TaxType_0021.GetCode() ,
             TaxType_0061.GetCode() ,
             TaxType_0071.GetCode() ,
             TaxType_0073.GetCode() ,
             TaxType_0074.GetCode() ,
             TaxType_0075.GetCode() ,
             TaxType_0076.GetCode() ,
             TaxType_0077.GetCode() ,
             TaxType_1047.GetCode() ,
             TaxType_1048.GetCode() ,
             TaxType_4071.GetCode() ,
             TaxType_4080.GetCode() ,
             TaxType_4081.GetCode() ,
             TaxType_4171.GetCode() ,
             TaxType_8001.GetCode() ,
             TaxType_8002.GetCode() ,
             TaxType_8004.GetCode() ,
             TaxType_8005.GetCode() ,
             TaxType_8006.GetCode() ,
             TaxType_8007.GetCode() ,
             TaxType_8008.GetCode() ,
             TaxType_9015.GetCode() ,
             TaxType_9021.GetCode() ,
             TaxType_9040.GetCode() ,
             TaxType_9077.GetCode()
        };


        private Tax(String name, String code, String description)
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
    }
}
