namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.ConstRepository
{
    public class Incoterms
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public static List<Incoterms> GetIncotermsList()
        {
            return _Incoterms;
        }

        private static List<Incoterms> _Incoterms;

        private static object lockSys = new object();

        static Incoterms()
        {
            lock (lockSys)
            {
                buildIncoterms();
            }
        }

        private static void buildIncoterms()
        {
            _Incoterms = new List<Incoterms>();
            _Incoterms.Add(new Incoterms() { Code = "CFR", Name = "Masraflar Ve Navlun / Cost And Freigh" });
            _Incoterms.Add(new Incoterms() { Code = "CIF", Name = "Masraflar,Sigorda ve Navlun / Cost,Insurance And Freight" });
            _Incoterms.Add(new Incoterms() { Code = "CIP", Name = "Taşıma ve Sigorta Ödenmiş Olarak /Carriage And Insured Paid To" });
            _Incoterms.Add(new Incoterms() { Code = "CPT", Name = "Taşıma Ödenmiş Olarak / Carriage Paid To" });
            _Incoterms.Add(new Incoterms() { Code = "DAF", Name = "Sınırda Teslim Edildi / Delivered At Frontier" });
            _Incoterms.Add(new Incoterms() { Code = "DAP", Name = "Belirlenen Yerde Teslim /Delivered At Place" });
            _Incoterms.Add(new Incoterms() { Code = "DAT", Name = "Terminalde Teslim / Delivered At Terminal" });
            _Incoterms.Add(new Incoterms() { Code = "DDP", Name = "Gümrük Vergileri Ödenmiş Olarak /Delivered Duty Paid" });
            _Incoterms.Add(new Incoterms() { Code = "DDU", Name = "Ödenmemiş Görev Teslimatı / Delivered Duty Unpaid" });
            _Incoterms.Add(new Incoterms() { Code = "DEQ", Name = "Ex Quay teslim edildi / Delivered Ex Quay" });
            _Incoterms.Add(new Incoterms() { Code = "DES", Name = "Ex Ship gönderildi / Delivered Ex Ship" });
            _Incoterms.Add(new Incoterms() { Code = "EXW", Name = "İşyerinde Teslim / EX Works" });
            _Incoterms.Add(new Incoterms() { Code = "FAS", Name = "Gemi Doğrultusunda Masrafsız /Free Alongside Ship" });
            _Incoterms.Add(new Incoterms() { Code = "FCA", Name = "Taşıyıcıya Masrafsız / Free Carrier" });
            _Incoterms.Add(new Incoterms() { Code = "FOB", Name = "Gemide MasrafSız /Free On Board" });
        }

    }
}
