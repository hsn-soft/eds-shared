namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.ConstRepository
{
    public class TransportModeCode
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static List<TransportModeCode> GetTransportModeCodeList()
        {
            return _transportModeCodes;
        }

        private static List<TransportModeCode> _transportModeCodes;

        private static object lockSys = new object();

        static TransportModeCode()
        {
            lock (lockSys)
            {
                buildTransportModeCode();
            }
        }

        private static void buildTransportModeCode()
        {
            _transportModeCodes = new List<TransportModeCode>();
            _transportModeCodes.Add(new TransportModeCode() { Id = 0, Name = "Belirtilmedi" });
            _transportModeCodes.Add(new TransportModeCode() { Id = 1, Name = "Denizyolu" });
            _transportModeCodes.Add(new TransportModeCode() { Id = 2, Name = "Demiryolu" });
            _transportModeCodes.Add(new TransportModeCode() { Id = 3, Name = "Karayolu" });
            _transportModeCodes.Add(new TransportModeCode() { Id = 4, Name = "Havayolu" });
            _transportModeCodes.Add(new TransportModeCode() { Id = 5, Name = "Posta" });
            _transportModeCodes.Add(new TransportModeCode() { Id = 6, Name = "Çok araçlı" });
            _transportModeCodes.Add(new TransportModeCode() { Id = 7, Name = "Sabit taşıma tesisleri" });
            _transportModeCodes.Add(new TransportModeCode() { Id = 8, Name = "İç su taşımacılığı" });
            _transportModeCodes.Add(new TransportModeCode() { Id = 9, Name = "Taşıma modu uygun değil" });
        }
    }
}
