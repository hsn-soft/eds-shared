namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.ConstRepository
{
    public class PackageCode
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public static List<PackageCode> GetPackageCodeList()
        {
            return _packageCodes;
        }

        private static List<PackageCode> _packageCodes;

        private static object lockSys = new object();

        static PackageCode()
        {
            lock (lockSys)
            {
                buildPackageCode();
            }
        }

        private static void buildPackageCode()
        {
            _packageCodes = new List<PackageCode>();
            _packageCodes.Add(new PackageCode() { Code = "1A", Name = "Çelik" });
            _packageCodes.Add(new PackageCode() { Code = "1B", Name = "Alüminyum" });
            _packageCodes.Add(new PackageCode() { Code = "1D", Name = "Kontrplak" });
            _packageCodes.Add(new PackageCode() { Code = "4A", Name = "Çelik Kutu" });
            _packageCodes.Add(new PackageCode() { Code = "4B", Name = "Alüminyum Kutu" });
            _packageCodes.Add(new PackageCode() { Code = "4C", Name = "Doğal Ahşap Kutu" });
            _packageCodes.Add(new PackageCode() { Code = "4D", Name = "Kontrplak Kutu" });
            _packageCodes.Add(new PackageCode() { Code = "4G", Name = "Lif Levha" });
            _packageCodes.Add(new PackageCode() { Code = "4H", Name = "Plastik Kutu" });
            _packageCodes.Add(new PackageCode() { Code = "AL", Name = "Top" });
            _packageCodes.Add(new PackageCode() { Code = "BA", Name = "Varil" });
            _packageCodes.Add(new PackageCode() { Code = "CH", Name = "Sandık" });
            _packageCodes.Add(new PackageCode() { Code = "HN", Name = "Askılı" });
            _packageCodes.Add(new PackageCode() { Code = "IB", Name = "Karton Koli" });
            _packageCodes.Add(new PackageCode() { Code = "NE", Name = "Paketlenmemiş" });
            _packageCodes.Add(new PackageCode() { Code = "PA", Name = "Paket Ambalaj" });
            _packageCodes.Add(new PackageCode() { Code = "PC", Name = "Koli" });
            _packageCodes.Add(new PackageCode() { Code = "PX", Name = "Pallet" });
            _packageCodes.Add(new PackageCode() { Code = "RO", Name = "Rulo" });
            _packageCodes.Add(new PackageCode() { Code = "SA", Name = "Çuval" });
            _packageCodes.Add(new PackageCode() { Code = "BE", Name = "Bohça" });
            _packageCodes.Add(new PackageCode() { Code = "BG", Name = "Torba" });
            _packageCodes.Add(new PackageCode() { Code = "BH", Name = "Demet" });
            _packageCodes.Add(new PackageCode() { Code = "BI", Name = "Çöp kutusu" });
            _packageCodes.Add(new PackageCode() { Code = "BJ", Name = "Kova" });
            _packageCodes.Add(new PackageCode() { Code = "BK", Name = "Sepet" });
            _packageCodes.Add(new PackageCode() { Code = "BX", Name = "Kutu" });
            _packageCodes.Add(new PackageCode() { Code = "CB", Name = "Bira kasası" });
            _packageCodes.Add(new PackageCode() { Code = "CI", Name = "Teneke kutu" });
            _packageCodes.Add(new PackageCode() { Code = "CK", Name = "Fıçı" });
            _packageCodes.Add(new PackageCode() { Code = "CN", Name = "Konteyner" });
            _packageCodes.Add(new PackageCode() { Code = "CR", Name = "Kasa" });
            _packageCodes.Add(new PackageCode() { Code = "DK", Name = "Karton kasa" });
            _packageCodes.Add(new PackageCode() { Code = "DR", Name = "Bidon" });
            _packageCodes.Add(new PackageCode() { Code = "EC", Name = "Plastik torba" });
            _packageCodes.Add(new PackageCode() { Code = "FC", Name = "Meyve kasası" });
            _packageCodes.Add(new PackageCode() { Code = "JR", Name = "Kavanoz" });
            _packageCodes.Add(new PackageCode() { Code = "LV", Name = "Liftvan" });
            _packageCodes.Add(new PackageCode() { Code = "NE", Name = "Ambalajsız" });
            _packageCodes.Add(new PackageCode() { Code = "SU", Name = "Bavul" });
            _packageCodes.Add(new PackageCode() { Code = "TN", Name = "Teneke" });
            _packageCodes.Add(new PackageCode() { Code = "VG", Name = "Dökme gaz" });
            _packageCodes.Add(new PackageCode() { Code = "VL", Name = "Dökme sıvı" });
            _packageCodes.Add(new PackageCode() { Code = "VO", Name = "Dökme katı" });
            _packageCodes.Add(new PackageCode() { Code = "ZZ", Name = "Bilinmeyen" });
        }
    }
}
