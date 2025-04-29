namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.ConstRepository
{
    public class YearList
    {
        public string Name { get; set; }
        public int Value { get; set; }


        private static List<YearList> _list;

        private static object lockSys = new object();

        static YearList()
        {
            lock (lockSys)
            {
                build();
            }
        }



        public static List<YearList> GetList()
        {
            return _list;
        }

        private static void build()
        {
            _list = new List<YearList>();
            for (int i = 2012; i <= DateTime.Now.Year; i++)
            {
                _list.Add(new YearList() { Name = i.ToString(), Value = i });
            }

        }

    }
}
