namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.ConstRepository
{
    public class DayList
    {
        public string Name { get; set; }
        public string Value { get; set; }

        private static List<DayList> _list;


        private static object lockSys = new object();

        static DayList()
        {
            lock (lockSys)
            {
                build();
            }
        }

        public static List<DayList> GetList()
        {
            return _list;
        }

        private static void build()
        {
            _list = new List<DayList>();
            for (int i = 1; i <= 31; i++)
            {
                _list.Add(new DayList() { Name = i.ToString().PadLeft(2, '0'), Value = i.ToString().PadLeft(2, '0') });
            }
        }

    }

    public class HourList
    {
        public string Name { get; set; }
        public string Value { get; set; }

        private static List<HourList> _list;


        private static object lockSys = new object();

        static HourList()
        {
            lock (lockSys)
            {
                build();
            }
        }

        public static List<HourList> GetList()
        {
            return _list;
        }

        private static void build()
        {
            _list = new List<HourList>();
            for (int i = 0; i < 24; i++)
            {
                _list.Add(new HourList() { Name = i.ToString().PadLeft(2, '0'), Value = i.ToString().PadLeft(2, '0') });
            }
        }

    }

    public class MinuteList
    {
        public string Name { get; set; }
        public string Value { get; set; }

        private static List<MinuteList> _list;


        private static object lockSys = new object();

        static MinuteList()
        {
            lock (lockSys)
            {
                build();
            }
        }

        public static List<MinuteList> GetList()
        {
            return _list;
        }

        private static void build()
        {
            _list = new List<MinuteList>();
            for (int i = 0; i < 60; i++)
            {
                _list.Add(new MinuteList() { Name = i.ToString().PadLeft(2, '0'), Value = i.ToString().PadLeft(2, '0') });
            }
        }

    }
}
