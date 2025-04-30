namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.ConstRepository
{
    public class MutabakatType
    {
        public string Name { get; set; }
        public int Value { get; set; }


        private static List<MutabakatType> _list;

        private static object lockSys = new object();

        static MutabakatType()
        {
            lock (lockSys)
            {
                build();
            }
        }

        public static List<MutabakatType> GetList()
        {
            return _list;
        }

        private static void build()
        {
            _list = new List<MutabakatType>();
            _list.Add(new MutabakatType() { Name = "Cari Mutabakat", Value = 1 });
        }

    }
}
