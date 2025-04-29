namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.GlobalTypes.EInvoice
{
    public sealed class Tax
    {
        private readonly String name;
        private readonly String code;
        private readonly String description;

        #region ReadOnlyTaxType


        public static readonly Tax TaxType_8001 = new Tax("8001", "8001", "");
        public static readonly Tax TaxType_8003 = new Tax("8003", "8003", "");
        public static readonly Tax TaxType_8005 = new Tax("8005", "8005", "");
        public static readonly Tax TaxType_8007 = new Tax("8007", "8007", "");
        public static readonly Tax TaxType_8002 = new Tax("8002", "8002", "");
        public static readonly Tax TaxType_8004 = new Tax("8004", "8004", "");
        public static readonly Tax TaxType_8006 = new Tax("8006", "8006", "");
        public static readonly Tax TaxType_8008 = new Tax("8008", "8008", "");


        public static readonly Tax TaxType_0001 = new Tax("0001", "0001", "");
        public static readonly Tax TaxType_0002 = new Tax("0002", "0002", "");
        public static readonly Tax TaxType_0003 = new Tax("0003", "0003", "");
        public static readonly Tax TaxType_0004 = new Tax("0004", "0004", "");
        public static readonly Tax TaxType_0005 = new Tax("0005", "0005", "");
        public static readonly Tax TaxType_0006 = new Tax("0006", "0006", "");
        public static readonly Tax TaxType_0007 = new Tax("0007", "0007", "");
        public static readonly Tax TaxType_0010 = new Tax("0010", "0010", "");
        public static readonly Tax TaxType_SGK_PRIM = new Tax("SGK_PRIM", "SGK_PRIM", "");
        public static readonly Tax TaxType_0012 = new Tax("0012", "0012", "");
        public static readonly Tax TaxType_0014 = new Tax("0014", "0014", "");
        public static readonly Tax TaxType_0015 = new Tax("0015", "0015", "");
        public static readonly Tax TaxType_0017 = new Tax("0017", "0017", "");
        public static readonly Tax TaxType_0020 = new Tax("0020", "0020", "");
        public static readonly Tax TaxType_0021 = new Tax("0021", "0021", "");
        public static readonly Tax TaxType_0022 = new Tax("0022", "0022", "");
        public static readonly Tax TaxType_0023 = new Tax("0023", "0023", "");
        public static readonly Tax TaxType_0024 = new Tax("0024", "0024", "");
        public static readonly Tax TaxType_0027 = new Tax("0027", "0027", "");
        public static readonly Tax TaxType_0032 = new Tax("0032", "0032", "");
        public static readonly Tax TaxType_0033 = new Tax("0033", "0033", "");
        public static readonly Tax TaxType_0040 = new Tax("0040", "0040", "");
        public static readonly Tax TaxType_0046 = new Tax("0046", "0046", "");
        public static readonly Tax TaxType_0048 = new Tax("0048", "0048", "");
        public static readonly Tax TaxType_0049 = new Tax("0049", "0049", "");
        public static readonly Tax TaxType_0050 = new Tax("0050", "0050", "");
        public static readonly Tax TaxType_0051 = new Tax("0051", "0051", "");
        public static readonly Tax TaxType_0053 = new Tax("0053", "0053", "");
        public static readonly Tax TaxType_0056 = new Tax("0056", "0056", "");
        public static readonly Tax TaxType_0057 = new Tax("0057", "0057", "");
        public static readonly Tax TaxType_0060 = new Tax("0060", "0060", "");
        public static readonly Tax TaxType_0061 = new Tax("0061", "0061", "");
        public static readonly Tax TaxType_0062 = new Tax("0062", "0062", "");
        public static readonly Tax TaxType_0067 = new Tax("0067", "0067", "");
        public static readonly Tax TaxType_0071 = new Tax("0071", "0071", "");
        public static readonly Tax TaxType_0073 = new Tax("0073", "0073", "");
        public static readonly Tax TaxType_0074 = new Tax("0074", "0074", "");
        public static readonly Tax TaxType_0075 = new Tax("0075", "0075", "");
        public static readonly Tax TaxType_0076 = new Tax("0076", "0076", "");
        public static readonly Tax TaxType_0077 = new Tax("0077", "0077", "");
        public static readonly Tax TaxType_0091 = new Tax("0091", "0091", "");
        public static readonly Tax TaxType_0092 = new Tax("0092", "0092", "");
        public static readonly Tax TaxType_0093 = new Tax("0093", "0093", "");
        public static readonly Tax TaxType_0094 = new Tax("0094", "0094", "");
        public static readonly Tax TaxType_1013 = new Tax("1013", "1013", "");
        public static readonly Tax TaxType_1018 = new Tax("1018", "1018", "");
        public static readonly Tax TaxType_1020 = new Tax("1020", "1020", "");
        public static readonly Tax TaxType_1026 = new Tax("1026", "1026", "");
        public static readonly Tax TaxType_1027 = new Tax("1027", "1027", "");
        public static readonly Tax TaxType_1028 = new Tax("1028", "1028", "");
        public static readonly Tax TaxType_1030 = new Tax("1030", "1030", "");
        public static readonly Tax TaxType_1034 = new Tax("1034", "1034", "");
        public static readonly Tax TaxType_1037 = new Tax("1037", "1037", "");
        public static readonly Tax TaxType_1042 = new Tax("1042", "1042", "");
        public static readonly Tax TaxType_1043 = new Tax("1043", "1043", "");
        public static readonly Tax TaxType_1046 = new Tax("1046", "1046", "");
        public static readonly Tax TaxType_1047 = new Tax("1047", "1047", "");
        public static readonly Tax TaxType_1048 = new Tax("1048", "1048", "");
        public static readonly Tax TaxType_1050 = new Tax("1050", "1050", "");
        public static readonly Tax TaxType_1051 = new Tax("1051", "1051", "");
        public static readonly Tax TaxType_1052 = new Tax("1052", "1052", "");
        public static readonly Tax TaxType_1053 = new Tax("1053", "1053", "");
        public static readonly Tax TaxType_1055 = new Tax("1055", "1055", "");
        public static readonly Tax TaxType_1060 = new Tax("1060", "1060", "");
        public static readonly Tax TaxType_1061 = new Tax("1061", "1061", "");
        public static readonly Tax TaxType_1067 = new Tax("1067", "1067", "");
        public static readonly Tax TaxType_1084 = new Tax("1084", "1084", "");
        public static readonly Tax TaxType_1085 = new Tax("1085", "1085", "");
        public static readonly Tax TaxType_1086 = new Tax("1086", "1086", "");
        public static readonly Tax TaxType_1087 = new Tax("1087", "1087", "");
        public static readonly Tax TaxType_1088 = new Tax("1088", "1088", "");
        public static readonly Tax TaxType_1089 = new Tax("1089", "1089", "");
        public static readonly Tax TaxType_1090 = new Tax("1090", "1090", "");
        public static readonly Tax TaxType_1091 = new Tax("1091", "1091", "");
        public static readonly Tax TaxType_1092 = new Tax("1092", "1092", "");
        public static readonly Tax TaxType_1093 = new Tax("1093", "1093", "");
        public static readonly Tax TaxType_1094 = new Tax("1094", "1094", "");
        public static readonly Tax TaxType_1095 = new Tax("1095", "1095", "");
        public static readonly Tax TaxType_1096 = new Tax("1096", "1096", "");
        public static readonly Tax TaxType_1097 = new Tax("1097", "1097", "");
        public static readonly Tax TaxType_3024 = new Tax("3024", "3024", "");
        public static readonly Tax TaxType_3061 = new Tax("3061", "3061", "");
        public static readonly Tax TaxType_3073 = new Tax("3073", "3073", "");
        public static readonly Tax TaxType_3074 = new Tax("3074", "3074", "");
        public static readonly Tax TaxType_3076 = new Tax("3076", "3076", "");
        public static readonly Tax TaxType_3077 = new Tax("3077", "3077", "");
        public static readonly Tax TaxType_3078 = new Tax("3078", "3078", "");
        public static readonly Tax TaxType_3080 = new Tax("3080", "3080", "");
        public static readonly Tax TaxType_3099 = new Tax("3099", "3099", "");
        public static readonly Tax TaxType_4001 = new Tax("4001", "4001", "");
        public static readonly Tax TaxType_4003 = new Tax("4003", "4003", "");
        public static readonly Tax TaxType_4004 = new Tax("4004", "4004", "");
        public static readonly Tax TaxType_4005 = new Tax("4005", "4005", "");
        public static readonly Tax TaxType_4006 = new Tax("4006", "4006", "");
        public static readonly Tax TaxType_4007 = new Tax("4007", "4007", "");
        public static readonly Tax TaxType_4008 = new Tax("4008", "4008", "");
        public static readonly Tax TaxType_4010 = new Tax("4010", "4010", "");
        public static readonly Tax TaxType_4011 = new Tax("4011", "4011", "");
        public static readonly Tax TaxType_4012 = new Tax("4012", "4012", "");
        public static readonly Tax TaxType_4014 = new Tax("4014", "4014", "");
        public static readonly Tax TaxType_4015 = new Tax("4015", "4015", "");
        public static readonly Tax TaxType_4016 = new Tax("4016", "4016", "");
        public static readonly Tax TaxType_4021 = new Tax("4021", "4021", "");
        public static readonly Tax TaxType_4023 = new Tax("4023", "4023", "");
        public static readonly Tax TaxType_4024 = new Tax("4024", "4024", "");
        public static readonly Tax TaxType_4030 = new Tax("4030", "4030", "");
        public static readonly Tax TaxType_4034 = new Tax("4034", "4034", "");
        public static readonly Tax TaxType_4035 = new Tax("4035", "4035", "");
        public static readonly Tax TaxType_4036 = new Tax("4036", "4036", "");
        public static readonly Tax TaxType_4037 = new Tax("4037", "4037", "");
        public static readonly Tax TaxType_4040 = new Tax("4040", "4040", "");
        public static readonly Tax TaxType_4041 = new Tax("4041", "4041", "");
        public static readonly Tax TaxType_4042 = new Tax("4042", "4042", "");
        public static readonly Tax TaxType_4043 = new Tax("4043", "4043", "");
        public static readonly Tax TaxType_4044 = new Tax("4044", "4044", "");
        public static readonly Tax TaxType_4046 = new Tax("4046", "4046", "");
        public static readonly Tax TaxType_4047 = new Tax("4047", "4047", "");
        public static readonly Tax TaxType_4048 = new Tax("4048", "4048", "");
        public static readonly Tax TaxType_4049 = new Tax("4049", "4049", "");
        public static readonly Tax TaxType_4050 = new Tax("4050", "4050", "");
        public static readonly Tax TaxType_4060 = new Tax("4060", "4060", "");
        public static readonly Tax TaxType_4061 = new Tax("4061", "4061", "");
        public static readonly Tax TaxType_4063 = new Tax("4063", "4063", "");
        public static readonly Tax TaxType_4070 = new Tax("4070", "4070", "");
        public static readonly Tax TaxType_4071 = new Tax("4071", "4071", "");
        public static readonly Tax TaxType_4072 = new Tax("4072", "4072", "");
        public static readonly Tax TaxType_4073 = new Tax("4073", "4073", "");
        public static readonly Tax TaxType_4074 = new Tax("4074", "4074", "");
        public static readonly Tax TaxType_4077 = new Tax("4077", "4077", "");
        public static readonly Tax TaxType_4078 = new Tax("4078", "4078", "");
        public static readonly Tax TaxType_4079 = new Tax("4079", "4079", "");
        public static readonly Tax TaxType_4080 = new Tax("4080", "4080", "");
        public static readonly Tax TaxType_4081 = new Tax("4081", "4081", "");
        public static readonly Tax TaxType_4101 = new Tax("4101", "4101", "");
        public static readonly Tax TaxType_4103 = new Tax("4103", "4103", "");
        public static readonly Tax TaxType_4107 = new Tax("4107", "4107", "");
        public static readonly Tax TaxType_4110 = new Tax("4110", "4110", "");
        public static readonly Tax TaxType_4112 = new Tax("4112", "4112", "");
        public static readonly Tax TaxType_4114 = new Tax("4114", "4114", "");
        public static readonly Tax TaxType_4115 = new Tax("4115", "4115", "");
        public static readonly Tax TaxType_4121 = new Tax("4121", "4121", "");
        public static readonly Tax TaxType_4122 = new Tax("4122", "4122", "");
        public static readonly Tax TaxType_4123 = new Tax("4123", "4123", "");
        public static readonly Tax TaxType_4124 = new Tax("4124", "4124", "");
        public static readonly Tax TaxType_4125 = new Tax("4125", "4125", "");
        public static readonly Tax TaxType_4126 = new Tax("4126", "4126", "");
        public static readonly Tax TaxType_4127 = new Tax("4127", "4127", "");
        public static readonly Tax TaxType_4128 = new Tax("4128", "4128", "");
        public static readonly Tax TaxType_4130 = new Tax("4130", "4130", "");
        public static readonly Tax TaxType_4140 = new Tax("4140", "4140", "");
        public static readonly Tax TaxType_4150 = new Tax("4150", "4150", "");
        public static readonly Tax TaxType_4151 = new Tax("4151", "4151", "");
        public static readonly Tax TaxType_4171 = new Tax("4171", "4171", "");
        public static readonly Tax TaxType_4178 = new Tax("4178", "4178", "");
        public static readonly Tax TaxType_4201 = new Tax("4201", "4201", "");
        public static readonly Tax TaxType_4203 = new Tax("4203", "4203", "");
        public static readonly Tax TaxType_4204 = new Tax("4204", "4204", "");
        public static readonly Tax TaxType_4205 = new Tax("4205", "4205", "");
        public static readonly Tax TaxType_4206 = new Tax("4206", "4206", "");
        public static readonly Tax TaxType_4207 = new Tax("4207", "4207", "");
        public static readonly Tax TaxType_4208 = new Tax("4208", "4208", "");
        public static readonly Tax TaxType_4210 = new Tax("4210", "4210", "");
        public static readonly Tax TaxType_4211 = new Tax("4211", "4211", "");
        public static readonly Tax TaxType_4215 = new Tax("4215", "4215", "");
        public static readonly Tax TaxType_4216 = new Tax("4216", "4216", "");
        public static readonly Tax TaxType_4217 = new Tax("4217", "4217", "");
        public static readonly Tax TaxType_4218 = new Tax("4218", "4218", "");
        public static readonly Tax TaxType_4220 = new Tax("4220", "4220", "");
        public static readonly Tax TaxType_4222 = new Tax("4222", "4222", "");
        public static readonly Tax TaxType_4811 = new Tax("4811", "4811", "");
        public static readonly Tax TaxType_9000 = new Tax("9000", "9000", "");
        public static readonly Tax TaxType_9001 = new Tax("9001", "9001", "");
        public static readonly Tax TaxType_9002 = new Tax("9002", "9002", "");
        public static readonly Tax TaxType_9003 = new Tax("9003", "9003", "");
        public static readonly Tax TaxType_9004 = new Tax("9004", "9004", "");
        public static readonly Tax TaxType_9005 = new Tax("9005", "9005", "");
        public static readonly Tax TaxType_9006 = new Tax("9006", "9006", "");
        public static readonly Tax TaxType_9007 = new Tax("9007", "9007", "");
        public static readonly Tax TaxType_9008 = new Tax("9008", "9008", "");
        public static readonly Tax TaxType_9009 = new Tax("9009", "9009", "");
        public static readonly Tax TaxType_9010 = new Tax("9010", "9010", "");
        public static readonly Tax TaxType_9011 = new Tax("9011", "9011", "");
        public static readonly Tax TaxType_9012 = new Tax("9012", "9012", "");
        public static readonly Tax TaxType_9013 = new Tax("9013", "9013", "");
        public static readonly Tax TaxType_9014 = new Tax("9014", "9014", "");
        public static readonly Tax TaxType_9015 = new Tax("9015", "9015", "");
        public static readonly Tax TaxType_9016 = new Tax("9016", "9016", "");
        public static readonly Tax TaxType_9017 = new Tax("9017", "9017", "");
        public static readonly Tax TaxType_9018 = new Tax("9018", "9018", "");
        public static readonly Tax TaxType_9019 = new Tax("9019", "9019", "");
        public static readonly Tax TaxType_9020 = new Tax("9020", "9020", "");
        public static readonly Tax TaxType_9021 = new Tax("9021", "9021", "");
        public static readonly Tax TaxType_9022 = new Tax("9022", "9022", "");
        public static readonly Tax TaxType_9023 = new Tax("9023", "9023", "");
        public static readonly Tax TaxType_9024 = new Tax("9024", "9024", "");
        public static readonly Tax TaxType_9025 = new Tax("9025", "9025", "");
        public static readonly Tax TaxType_9026 = new Tax("9026", "9026", "");
        public static readonly Tax TaxType_9027 = new Tax("9027", "9027", "");
        public static readonly Tax TaxType_9028 = new Tax("9028", "9028", "");
        public static readonly Tax TaxType_9029 = new Tax("9029", "9029", "");
        public static readonly Tax TaxType_9030 = new Tax("9030", "9030", "");
        public static readonly Tax TaxType_9031 = new Tax("9031", "9031", "");
        public static readonly Tax TaxType_9032 = new Tax("9032", "9032", "");
        public static readonly Tax TaxType_9033 = new Tax("9033", "9033", "");
        public static readonly Tax TaxType_9034 = new Tax("9034", "9034", "");
        public static readonly Tax TaxType_9035 = new Tax("9035", "9035", "");
        public static readonly Tax TaxType_9036 = new Tax("9036", "9036", "");
        public static readonly Tax TaxType_9037 = new Tax("9037", "9037", "");
        public static readonly Tax TaxType_9038 = new Tax("9038", "9038", "");
        public static readonly Tax TaxType_9039 = new Tax("9039", "9039", "");
        public static readonly Tax TaxType_9040 = new Tax("9040", "9040", "");
        public static readonly Tax TaxType_9041 = new Tax("9041", "9041", "");
        public static readonly Tax TaxType_9042 = new Tax("9042", "9042", "");
        public static readonly Tax TaxType_9043 = new Tax("9043", "9043", "");
        public static readonly Tax TaxType_9044 = new Tax("9044", "9044", "");
        public static readonly Tax TaxType_9045 = new Tax("9045", "9045", "");
        public static readonly Tax TaxType_9046 = new Tax("9046", "9046", "");
        public static readonly Tax TaxType_9047 = new Tax("9047", "9047", "");
        public static readonly Tax TaxType_9048 = new Tax("9048", "9048", "");
        public static readonly Tax TaxType_9049 = new Tax("9049", "9049", "");
        public static readonly Tax TaxType_9050 = new Tax("9050", "9050", "");
        public static readonly Tax TaxType_9051 = new Tax("9051", "9051", "");
        public static readonly Tax TaxType_9052 = new Tax("9052", "9052", "");
        public static readonly Tax TaxType_9053 = new Tax("9053", "9053", "");
        public static readonly Tax TaxType_9054 = new Tax("9054", "9054", "");
        public static readonly Tax TaxType_9055 = new Tax("9055", "9055", "");
        public static readonly Tax TaxType_9056 = new Tax("9056", "9056", "");
        public static readonly Tax TaxType_9057 = new Tax("9057", "9057", "");
        public static readonly Tax TaxType_9058 = new Tax("9058", "9058", "");
        public static readonly Tax TaxType_9059 = new Tax("9059", "9059", "");
        public static readonly Tax TaxType_9060 = new Tax("9060", "9060", "");
        public static readonly Tax TaxType_9061 = new Tax("9061", "9061", "");
        public static readonly Tax TaxType_9062 = new Tax("9062", "9062", "");
        public static readonly Tax TaxType_9063 = new Tax("9063", "9063", "");
        public static readonly Tax TaxType_9064 = new Tax("9064", "9064", "");
        public static readonly Tax TaxType_9065 = new Tax("9065", "9065", "");
        public static readonly Tax TaxType_9066 = new Tax("9066", "9066", "");
        public static readonly Tax TaxType_9067 = new Tax("9067", "9067", "");
        public static readonly Tax TaxType_9068 = new Tax("9068", "9068", "");
        public static readonly Tax TaxType_9069 = new Tax("9069", "9069", "");
        public static readonly Tax TaxType_9070 = new Tax("9070", "9070", "");
        public static readonly Tax TaxType_9071 = new Tax("9071", "9071", "");
        public static readonly Tax TaxType_9072 = new Tax("9072", "9072", "");
        public static readonly Tax TaxType_9073 = new Tax("9073", "9073", "");
        public static readonly Tax TaxType_9074 = new Tax("9074", "9074", "");
        public static readonly Tax TaxType_9075 = new Tax("9075", "9075", "");
        public static readonly Tax TaxType_9076 = new Tax("9076", "9076", "");
        public static readonly Tax TaxType_9077 = new Tax("9077", "9077", "");
        public static readonly Tax TaxType_9078 = new Tax("9078", "9078", "");
        public static readonly Tax TaxType_9079 = new Tax("9079", "9079", "");
        public static readonly Tax TaxType_9080 = new Tax("9080", "9080", "");
        public static readonly Tax TaxType_9081 = new Tax("9081", "9081", "");
        public static readonly Tax TaxType_9082 = new Tax("9082", "9082", "");
        public static readonly Tax TaxType_9083 = new Tax("9083", "9083", "");
        public static readonly Tax TaxType_9084 = new Tax("9084", "9084", "");
        public static readonly Tax TaxType_9085 = new Tax("9085", "9085", "");
        public static readonly Tax TaxType_9086 = new Tax("9086", "9086", "");
        public static readonly Tax TaxType_9087 = new Tax("9087", "9087", "");
        public static readonly Tax TaxType_9088 = new Tax("9088", "9088", "");
        public static readonly Tax TaxType_9089 = new Tax("9089", "9089", "");
        public static readonly Tax TaxType_9090 = new Tax("9090", "9090", "");
        public static readonly Tax TaxType_9091 = new Tax("9091", "9091", "");
        public static readonly Tax TaxType_9092 = new Tax("9092", "9092", "");
        public static readonly Tax TaxType_9093 = new Tax("9093", "9093", "");
        public static readonly Tax TaxType_9094 = new Tax("9094", "9094", "");
        public static readonly Tax TaxType_9095 = new Tax("9095", "9095", "");
        public static readonly Tax TaxType_9096 = new Tax("9096", "9096", "");
        public static readonly Tax TaxType_9097 = new Tax("9097", "9097", "");
        public static readonly Tax TaxType_9098 = new Tax("9098", "9098", "");
        public static readonly Tax TaxType_9099 = new Tax("9099", "9099", "");
        public static readonly Tax TaxType_9101 = new Tax("9101", "9101", "");
        public static readonly Tax TaxType_9102 = new Tax("9102", "9102", "");
        public static readonly Tax TaxType_9103 = new Tax("9103", "9103", "");
        public static readonly Tax TaxType_9104 = new Tax("9104", "9104", "");
        public static readonly Tax TaxType_9105 = new Tax("9105", "9105", "");
        public static readonly Tax TaxType_9106 = new Tax("9106", "9106", "");
        public static readonly Tax TaxType_9107 = new Tax("9107", "9107", "");
        public static readonly Tax TaxType_9108 = new Tax("9108", "9108", "");
        public static readonly Tax TaxType_9109 = new Tax("9109", "9109", "");
        public static readonly Tax TaxType_9110 = new Tax("9110", "9110", "");
        public static readonly Tax TaxType_9111 = new Tax("9111", "9111", "");
        public static readonly Tax TaxType_9112 = new Tax("9112", "9112", "");
        public static readonly Tax TaxType_9113 = new Tax("9113", "9113", "");
        public static readonly Tax TaxType_9114 = new Tax("9114", "9114", "");
        public static readonly Tax TaxType_9115 = new Tax("9115", "9115", "");
        public static readonly Tax TaxType_9116 = new Tax("9116", "9116", "");
        public static readonly Tax TaxType_9117 = new Tax("9117", "9117", "");
        public static readonly Tax TaxType_9118 = new Tax("9118", "9118", "");
        public static readonly Tax TaxType_9119 = new Tax("9119", "9119", "");
        public static readonly Tax TaxType_9120 = new Tax("9120", "9120", "");
        public static readonly Tax TaxType_9121 = new Tax("9121", "9121", "");
        public static readonly Tax TaxType_9122 = new Tax("9122", "9122", "");
        public static readonly Tax TaxType_9123 = new Tax("9123", "9123", "");
        public static readonly Tax TaxType_9124 = new Tax("9124", "9124", "");
        public static readonly Tax TaxType_9125 = new Tax("9125", "9125", "");
        public static readonly Tax TaxType_9126 = new Tax("9126", "9126", "");
        public static readonly Tax TaxType_9127 = new Tax("9127", "9127", "");
        public static readonly Tax TaxType_9128 = new Tax("9128", "9128", "");
        public static readonly Tax TaxType_9129 = new Tax("9129", "9129", "");
        public static readonly Tax TaxType_9130 = new Tax("9130", "9130", "");
        public static readonly Tax TaxType_9131 = new Tax("9131", "9131", "");
        public static readonly Tax TaxType_9132 = new Tax("9132", "9132", "");
        public static readonly Tax TaxType_9133 = new Tax("9133", "9133", "");
        public static readonly Tax TaxType_9134 = new Tax("9134", "9134", "");
        public static readonly Tax TaxType_9135 = new Tax("9135", "9135", "");
        public static readonly Tax TaxType_9136 = new Tax("9136", "9136", "");
        public static readonly Tax TaxType_9137 = new Tax("9137", "9137", "");
        public static readonly Tax TaxType_9138 = new Tax("9138", "9138", "");
        public static readonly Tax TaxType_9139 = new Tax("9139", "9139", "");
        public static readonly Tax TaxType_9140 = new Tax("9140", "9140", "");
        public static readonly Tax TaxType_9141 = new Tax("9141", "9141", "");
        public static readonly Tax TaxType_9142 = new Tax("9142", "9142", "");
        public static readonly Tax TaxType_9143 = new Tax("9143", "9143", "");
        public static readonly Tax TaxType_9145 = new Tax("9145", "9145", "");
        public static readonly Tax TaxType_9146 = new Tax("9146", "9146", "");
        public static readonly Tax TaxType_9147 = new Tax("9147", "9147", "");
        public static readonly Tax TaxType_9148 = new Tax("9148", "9148", "");
        public static readonly Tax TaxType_9149 = new Tax("9149", "9149", "");
        public static readonly Tax TaxType_9150 = new Tax("9150", "9150", "");
        public static readonly Tax TaxType_9151 = new Tax("9151", "9151", "");
        public static readonly Tax TaxType_9152 = new Tax("9152", "9152", "");
        public static readonly Tax TaxType_9153 = new Tax("9153", "9153", "");
        public static readonly Tax TaxType_9154 = new Tax("9154", "9154", "");
        public static readonly Tax TaxType_9155 = new Tax("9155", "9155", "");
        public static readonly Tax TaxType_9158 = new Tax("9158", "9158", "");
        public static readonly Tax TaxType_9159 = new Tax("9159", "9159", "");
        public static readonly Tax TaxType_9160 = new Tax("9160", "9160", "");
        public static readonly Tax TaxType_9161 = new Tax("9161", "9161", "");
        public static readonly Tax TaxType_9162 = new Tax("9162", "9162", "");
        public static readonly Tax TaxType_9164 = new Tax("9164", "9164", "");
        public static readonly Tax TaxType_9165 = new Tax("9165", "9165", "");
        public static readonly Tax TaxType_9166 = new Tax("9166", "9166", "");
        public static readonly Tax TaxType_9167 = new Tax("9167", "9167", "");
        public static readonly Tax TaxType_9168 = new Tax("9168", "9168", "");
        public static readonly Tax TaxType_9169 = new Tax("9169", "9169", "");
        public static readonly Tax TaxType_9170 = new Tax("9170", "9170", "");
        public static readonly Tax TaxType_9171 = new Tax("9171", "9171", "");
        public static readonly Tax TaxType_9172 = new Tax("9172", "9172", "");
        public static readonly Tax TaxType_9173 = new Tax("9173", "9173", "");
        public static readonly Tax TaxType_9174 = new Tax("9174", "9174", "");
        public static readonly Tax TaxType_9175 = new Tax("9175", "9175", "");
        public static readonly Tax TaxType_9176 = new Tax("9176", "9176", "");
        public static readonly Tax TaxType_9179 = new Tax("9179", "9179", "");
        public static readonly Tax TaxType_9180 = new Tax("9180", "9180", "");
        public static readonly Tax TaxType_9181 = new Tax("9181", "9181", "");
        public static readonly Tax TaxType_9185 = new Tax("9185", "9185", "");
        public static readonly Tax TaxType_9190 = new Tax("9190", "9190", "");
        public static readonly Tax TaxType_9196 = new Tax("9196", "9196", "");
        public static readonly Tax TaxType_9197 = new Tax("9197", "9197", "");
        public static readonly Tax TaxType_9198 = new Tax("9198", "9198", "");
        public static readonly Tax TaxType_9200 = new Tax("9200", "9200", "");
        public static readonly Tax TaxType_9201 = new Tax("9201", "9201", "");
        public static readonly Tax TaxType_9202 = new Tax("9202", "9202", "");
        public static readonly Tax TaxType_9203 = new Tax("9203", "9203", "");
        public static readonly Tax TaxType_9213 = new Tax("9213", "9213", "");
        public static readonly Tax TaxType_9220 = new Tax("9220", "9220", "");
        public static readonly Tax TaxType_9221 = new Tax("9221", "9221", "");
        public static readonly Tax TaxType_9223 = new Tax("9223", "9223", "");
        public static readonly Tax TaxType_9224 = new Tax("9224", "9224", "");
        public static readonly Tax TaxType_9225 = new Tax("9225", "9225", "");
        public static readonly Tax TaxType_9226 = new Tax("9226", "9226", "");
        public static readonly Tax TaxType_9227 = new Tax("9227", "9227", "");
        public static readonly Tax TaxType_9228 = new Tax("9228", "9228", "");
        public static readonly Tax TaxType_9229 = new Tax("9229", "9229", "");
        public static readonly Tax TaxType_9246 = new Tax("9246", "9246", "");
        public static readonly Tax TaxType_9247 = new Tax("9247", "9247", "");
        public static readonly Tax TaxType_9301 = new Tax("9301", "9301", "");
        public static readonly Tax TaxType_9302 = new Tax("9302", "9302", "");
        public static readonly Tax TaxType_9303 = new Tax("9303", "9303", "");
        public static readonly Tax TaxType_9304 = new Tax("9304", "9304", "");
        public static readonly Tax TaxType_9305 = new Tax("9305", "9305", "");
        public static readonly Tax TaxType_9306 = new Tax("9306", "9306", "");
        public static readonly Tax TaxType_9307 = new Tax("9307", "9307", "");
        public static readonly Tax TaxType_9308 = new Tax("9308", "9308", "");
        public static readonly Tax TaxType_9309 = new Tax("9309", "9309", "");
        public static readonly Tax TaxType_9310 = new Tax("9310", "9310", "");
        public static readonly Tax TaxType_9311 = new Tax("9311", "9311", "");
        public static readonly Tax TaxType_9312 = new Tax("9312", "9312", "");
        public static readonly Tax TaxType_9315 = new Tax("9315", "9315", "");
        public static readonly Tax TaxType_9316 = new Tax("9316", "9316", "");
        public static readonly Tax TaxType_9317 = new Tax("9317", "9317", "");
        public static readonly Tax TaxType_9318 = new Tax("9318", "9318", "");
        public static readonly Tax TaxType_9319 = new Tax("9319", "9319", "");
        public static readonly Tax TaxType_9341 = new Tax("9341", "9341", "");
        public static readonly Tax TaxType_9901 = new Tax("9901", "9901", "");
        public static readonly Tax TaxType_9944 = new Tax("9944", "9944", "");
        #endregion

        public static readonly List<string> TaxTypeList = new List<string>
        {
			  #region TaxType

			  TaxType_8002.GetCode(),
              TaxType_8004.GetCode(),
              TaxType_8006.GetCode(),
              TaxType_8008.GetCode(),
              TaxType_8001.GetCode(),
              TaxType_8003.GetCode(),
              TaxType_8005.GetCode(),
              TaxType_8007.GetCode(),
              TaxType_0001.GetCode(),
              TaxType_0002.GetCode(),
              TaxType_0003.GetCode(),
              TaxType_0004.GetCode(),
              TaxType_0005.GetCode(),
              TaxType_0006.GetCode(),
              TaxType_0007.GetCode(),
              TaxType_0010.GetCode(),
              TaxType_SGK_PRIM.GetCode(),
              TaxType_0012.GetCode(),
              TaxType_0014.GetCode(),
              TaxType_0015.GetCode(),
              TaxType_0017.GetCode(),
              TaxType_0020.GetCode(),
              TaxType_0021.GetCode(),
              TaxType_0022.GetCode(),
              TaxType_0023.GetCode(),
              TaxType_0024.GetCode(),
              TaxType_0027.GetCode(),
              TaxType_0032.GetCode(),
              TaxType_0033.GetCode(),
              TaxType_0040.GetCode(),
              TaxType_0046.GetCode(),
              TaxType_0048.GetCode(),
              TaxType_0049.GetCode(),
              TaxType_0050.GetCode(),
              TaxType_0051.GetCode(),
              TaxType_0053.GetCode(),
              TaxType_0056.GetCode(),
              TaxType_0057.GetCode(),
              TaxType_0060.GetCode(),
              TaxType_0061.GetCode(),
              TaxType_0062.GetCode(),
              TaxType_0067.GetCode(),
              TaxType_0071.GetCode(),
              TaxType_0073.GetCode(),
              TaxType_0074.GetCode(),
              TaxType_0075.GetCode(),
              TaxType_0076.GetCode(),
              TaxType_0077.GetCode(),
              TaxType_0091.GetCode(),
              TaxType_0092.GetCode(),
              TaxType_0093.GetCode(),
              TaxType_0094.GetCode(),
              TaxType_1013.GetCode(),
              TaxType_1018.GetCode(),
              TaxType_1020.GetCode(),
              TaxType_1026.GetCode(),
              TaxType_1027.GetCode(),
              TaxType_1028.GetCode(),
              TaxType_1030.GetCode(),
              TaxType_1034.GetCode(),
              TaxType_1037.GetCode(),
              TaxType_1042.GetCode(),
              TaxType_1043.GetCode(),
              TaxType_1046.GetCode(),
              TaxType_1047.GetCode(),
              TaxType_1048.GetCode(),
              TaxType_1050.GetCode(),
              TaxType_1051.GetCode(),
              TaxType_1052.GetCode(),
              TaxType_1053.GetCode(),
              TaxType_1055.GetCode(),
              TaxType_1060.GetCode(),
              TaxType_1061.GetCode(),
              TaxType_1067.GetCode(),
              TaxType_1084.GetCode(),
              TaxType_1085.GetCode(),
              TaxType_1086.GetCode(),
              TaxType_1087.GetCode(),
              TaxType_1088.GetCode(),
              TaxType_1089.GetCode(),
              TaxType_1090.GetCode(),
              TaxType_1091.GetCode(),
              TaxType_1092.GetCode(),
              TaxType_1093.GetCode(),
              TaxType_1094.GetCode(),
              TaxType_1095.GetCode(),
              TaxType_1096.GetCode(),
              TaxType_1097.GetCode(),
              TaxType_3024.GetCode(),
              TaxType_3061.GetCode(),
              TaxType_3073.GetCode(),
              TaxType_3074.GetCode(),
              TaxType_3076.GetCode(),
              TaxType_3077.GetCode(),
              TaxType_3078.GetCode(),
              TaxType_3080.GetCode(),
              TaxType_3099.GetCode(),
              TaxType_4001.GetCode(),
              TaxType_4003.GetCode(),
              TaxType_4004.GetCode(),
              TaxType_4005.GetCode(),
              TaxType_4006.GetCode(),
              TaxType_4007.GetCode(),
              TaxType_4008.GetCode(),
              TaxType_4010.GetCode(),
              TaxType_4011.GetCode(),
              TaxType_4012.GetCode(),
              TaxType_4014.GetCode(),
              TaxType_4015.GetCode(),
              TaxType_4016.GetCode(),
              TaxType_4021.GetCode(),
              TaxType_4023.GetCode(),
              TaxType_4024.GetCode(),
              TaxType_4030.GetCode(),
              TaxType_4034.GetCode(),
              TaxType_4035.GetCode(),
              TaxType_4036.GetCode(),
              TaxType_4037.GetCode(),
              TaxType_4040.GetCode(),
              TaxType_4041.GetCode(),
              TaxType_4042.GetCode(),
              TaxType_4043.GetCode(),
              TaxType_4044.GetCode(),
              TaxType_4046.GetCode(),
              TaxType_4047.GetCode(),
              TaxType_4048.GetCode(),
              TaxType_4049.GetCode(),
              TaxType_4050.GetCode(),
              TaxType_4060.GetCode(),
              TaxType_4061.GetCode(),
              TaxType_4063.GetCode(),
              TaxType_4070.GetCode(),
              TaxType_4071.GetCode(),
              TaxType_4072.GetCode(),
              TaxType_4073.GetCode(),
              TaxType_4074.GetCode(),
              TaxType_4077.GetCode(),
              TaxType_4078.GetCode(),
              TaxType_4079.GetCode(),
              TaxType_4080.GetCode(),
              TaxType_4081.GetCode(),
              TaxType_4101.GetCode(),
              TaxType_4103.GetCode(),
              TaxType_4107.GetCode(),
              TaxType_4110.GetCode(),
              TaxType_4112.GetCode(),
              TaxType_4114.GetCode(),
              TaxType_4115.GetCode(),
              TaxType_4121.GetCode(),
              TaxType_4122.GetCode(),
              TaxType_4123.GetCode(),
              TaxType_4124.GetCode(),
              TaxType_4125.GetCode(),
              TaxType_4126.GetCode(),
              TaxType_4127.GetCode(),
              TaxType_4128.GetCode(),
              TaxType_4130.GetCode(),
              TaxType_4140.GetCode(),
              TaxType_4150.GetCode(),
              TaxType_4151.GetCode(),
              TaxType_4171.GetCode(),
              TaxType_4178.GetCode(),
              TaxType_4201.GetCode(),
              TaxType_4203.GetCode(),
              TaxType_4204.GetCode(),
              TaxType_4205.GetCode(),
              TaxType_4206.GetCode(),
              TaxType_4207.GetCode(),
              TaxType_4208.GetCode(),
              TaxType_4210.GetCode(),
              TaxType_4211.GetCode(),
              TaxType_4215.GetCode(),
              TaxType_4216.GetCode(),
              TaxType_4217.GetCode(),
              TaxType_4218.GetCode(),
              TaxType_4220.GetCode(),
              TaxType_4222.GetCode(),
              TaxType_4811.GetCode(),
              TaxType_9000.GetCode(),
              TaxType_9001.GetCode(),
              TaxType_9002.GetCode(),
              TaxType_9003.GetCode(),
              TaxType_9004.GetCode(),
              TaxType_9005.GetCode(),
              TaxType_9006.GetCode(),
              TaxType_9007.GetCode(),
              TaxType_9008.GetCode(),
              TaxType_9009.GetCode(),
              TaxType_9010.GetCode(),
              TaxType_9011.GetCode(),
              TaxType_9012.GetCode(),
              TaxType_9013.GetCode(),
              TaxType_9014.GetCode(),
              TaxType_9015.GetCode(),
              TaxType_9016.GetCode(),
              TaxType_9017.GetCode(),
              TaxType_9018.GetCode(),
              TaxType_9019.GetCode(),
              TaxType_9020.GetCode(),
              TaxType_9021.GetCode(),
              TaxType_9022.GetCode(),
              TaxType_9023.GetCode(),
              TaxType_9024.GetCode(),
              TaxType_9025.GetCode(),
              TaxType_9026.GetCode(),
              TaxType_9027.GetCode(),
              TaxType_9028.GetCode(),
              TaxType_9029.GetCode(),
              TaxType_9030.GetCode(),
              TaxType_9031.GetCode(),
              TaxType_9032.GetCode(),
              TaxType_9033.GetCode(),
              TaxType_9034.GetCode(),
              TaxType_9035.GetCode(),
              TaxType_9036.GetCode(),
              TaxType_9037.GetCode(),
              TaxType_9038.GetCode(),
              TaxType_9039.GetCode(),
              TaxType_9040.GetCode(),
              TaxType_9041.GetCode(),
              TaxType_9042.GetCode(),
              TaxType_9043.GetCode(),
              TaxType_9044.GetCode(),
              TaxType_9045.GetCode(),
              TaxType_9046.GetCode(),
              TaxType_9047.GetCode(),
              TaxType_9048.GetCode(),
              TaxType_9049.GetCode(),
              TaxType_9050.GetCode(),
              TaxType_9051.GetCode(),
              TaxType_9052.GetCode(),
              TaxType_9053.GetCode(),
              TaxType_9054.GetCode(),
              TaxType_9055.GetCode(),
              TaxType_9056.GetCode(),
              TaxType_9057.GetCode(),
              TaxType_9058.GetCode(),
              TaxType_9059.GetCode(),
              TaxType_9060.GetCode(),
              TaxType_9061.GetCode(),
              TaxType_9062.GetCode(),
              TaxType_9063.GetCode(),
              TaxType_9064.GetCode(),
              TaxType_9065.GetCode(),
              TaxType_9066.GetCode(),
              TaxType_9067.GetCode(),
              TaxType_9068.GetCode(),
              TaxType_9069.GetCode(),
              TaxType_9070.GetCode(),
              TaxType_9071.GetCode(),
              TaxType_9072.GetCode(),
              TaxType_9073.GetCode(),
              TaxType_9074.GetCode(),
              TaxType_9075.GetCode(),
              TaxType_9076.GetCode(),
              TaxType_9077.GetCode(),
              TaxType_9078.GetCode(),
              TaxType_9079.GetCode(),
              TaxType_9080.GetCode(),
              TaxType_9081.GetCode(),
              TaxType_9082.GetCode(),
              TaxType_9083.GetCode(),
              TaxType_9084.GetCode(),
              TaxType_9085.GetCode(),
              TaxType_9086.GetCode(),
              TaxType_9087.GetCode(),
              TaxType_9088.GetCode(),
              TaxType_9089.GetCode(),
              TaxType_9090.GetCode(),
              TaxType_9091.GetCode(),
              TaxType_9092.GetCode(),
              TaxType_9093.GetCode(),
              TaxType_9094.GetCode(),
              TaxType_9095.GetCode(),
              TaxType_9096.GetCode(),
              TaxType_9097.GetCode(),
              TaxType_9098.GetCode(),
              TaxType_9099.GetCode(),
              TaxType_9101.GetCode(),
              TaxType_9102.GetCode(),
              TaxType_9103.GetCode(),
              TaxType_9104.GetCode(),
              TaxType_9105.GetCode(),
              TaxType_9106.GetCode(),
              TaxType_9107.GetCode(),
              TaxType_9108.GetCode(),
              TaxType_9109.GetCode(),
              TaxType_9110.GetCode(),
              TaxType_9111.GetCode(),
              TaxType_9112.GetCode(),
              TaxType_9113.GetCode(),
              TaxType_9114.GetCode(),
              TaxType_9115.GetCode(),
              TaxType_9116.GetCode(),
              TaxType_9117.GetCode(),
              TaxType_9118.GetCode(),
              TaxType_9119.GetCode(),
              TaxType_9120.GetCode(),
              TaxType_9121.GetCode(),
              TaxType_9122.GetCode(),
              TaxType_9123.GetCode(),
              TaxType_9124.GetCode(),
              TaxType_9125.GetCode(),
              TaxType_9126.GetCode(),
              TaxType_9127.GetCode(),
              TaxType_9128.GetCode(),
              TaxType_9129.GetCode(),
              TaxType_9130.GetCode(),
              TaxType_9131.GetCode(),
              TaxType_9132.GetCode(),
              TaxType_9133.GetCode(),
              TaxType_9134.GetCode(),
              TaxType_9135.GetCode(),
              TaxType_9136.GetCode(),
              TaxType_9137.GetCode(),
              TaxType_9138.GetCode(),
              TaxType_9139.GetCode(),
              TaxType_9140.GetCode(),
              TaxType_9141.GetCode(),
              TaxType_9142.GetCode(),
              TaxType_9143.GetCode(),
              TaxType_9145.GetCode(),
              TaxType_9146.GetCode(),
              TaxType_9147.GetCode(),
              TaxType_9148.GetCode(),
              TaxType_9149.GetCode(),
              TaxType_9150.GetCode(),
              TaxType_9151.GetCode(),
              TaxType_9152.GetCode(),
              TaxType_9153.GetCode(),
              TaxType_9154.GetCode(),
              TaxType_9155.GetCode(),
              TaxType_9158.GetCode(),
              TaxType_9159.GetCode(),
              TaxType_9160.GetCode(),
              TaxType_9161.GetCode(),
              TaxType_9162.GetCode(),
              TaxType_9164.GetCode(),
              TaxType_9165.GetCode(),
              TaxType_9166.GetCode(),
              TaxType_9167.GetCode(),
              TaxType_9168.GetCode(),
              TaxType_9169.GetCode(),
              TaxType_9170.GetCode(),
              TaxType_9171.GetCode(),
              TaxType_9172.GetCode(),
              TaxType_9173.GetCode(),
              TaxType_9174.GetCode(),
              TaxType_9175.GetCode(),
              TaxType_9176.GetCode(),
              TaxType_9179.GetCode(),
              TaxType_9180.GetCode(),
              TaxType_9181.GetCode(),
              TaxType_9185.GetCode(),
              TaxType_9190.GetCode(),
              TaxType_9196.GetCode(),
              TaxType_9197.GetCode(),
              TaxType_9198.GetCode(),
              TaxType_9200.GetCode(),
              TaxType_9201.GetCode(),
              TaxType_9202.GetCode(),
              TaxType_9203.GetCode(),
              TaxType_9213.GetCode(),
              TaxType_9220.GetCode(),
              TaxType_9221.GetCode(),
              TaxType_9223.GetCode(),
              TaxType_9224.GetCode(),
              TaxType_9225.GetCode(),
              TaxType_9226.GetCode(),
              TaxType_9227.GetCode(),
              TaxType_9228.GetCode(),
              TaxType_9229.GetCode(),
              TaxType_9246.GetCode(),
              TaxType_9247.GetCode(),
              TaxType_9301.GetCode(),
              TaxType_9302.GetCode(),
              TaxType_9303.GetCode(),
              TaxType_9304.GetCode(),
              TaxType_9305.GetCode(),
              TaxType_9306.GetCode(),
              TaxType_9307.GetCode(),
              TaxType_9308.GetCode(),
              TaxType_9309.GetCode(),
              TaxType_9310.GetCode(),
              TaxType_9311.GetCode(),
              TaxType_9312.GetCode(),
              TaxType_9315.GetCode(),
              TaxType_9316.GetCode(),
              TaxType_9317.GetCode(),
              TaxType_9318.GetCode(),
              TaxType_9319.GetCode(),
              TaxType_9341.GetCode(),
              TaxType_9901.GetCode(),
              TaxType_9944.GetCode()
			  #endregion
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
