namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.ConstRepository
{
    public class Month
    {
        public string Name { get; set; }
        public int Value { get; set; }


        private static List<Month> _list;

        private static object lockSys = new object();

        static Month()
        {
            lock (lockSys)
            {
                build();
            }
        }

        public static List<Month> GetMonthList()
        {
            return _list;
        }

        private static void build()
        {
            _list = new List<Month>();
            _list.Add(new Month() { Name = "Ocak", Value = 1 });
            _list.Add(new Month() { Name = "Şubat", Value = 2 });
            _list.Add(new Month() { Name = "Mart", Value = 3 });
            _list.Add(new Month() { Name = "Nisan", Value = 4 });
            _list.Add(new Month() { Name = "Mayıs", Value = 5 });
            _list.Add(new Month() { Name = "Haziran", Value = 6 });
            _list.Add(new Month() { Name = "Temmuz", Value = 7 });
            _list.Add(new Month() { Name = "Ağustos", Value = 8 });
            _list.Add(new Month() { Name = "Eylül", Value = 9 });
            _list.Add(new Month() { Name = "Ekim", Value = 10 });
            _list.Add(new Month() { Name = "Kasım", Value = 11 });
            _list.Add(new Month() { Name = "Aralık", Value = 12 });
        }

    }
}
