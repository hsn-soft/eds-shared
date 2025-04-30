namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.ConstRepository
{
    public class MutabakatDebitCreditType
    {
        public string Name { get; set; }
        public string DebitCreditType { get; set; }


        private static List<MutabakatDebitCreditType> _list;

        private static object lockSys = new object();

        static MutabakatDebitCreditType()
        {
            lock (lockSys)
            {
                build();
            }
        }

        public static List<MutabakatDebitCreditType> GetList()
        {
            return _list;
        }

        private static void build()
        {
            _list = new List<MutabakatDebitCreditType>();
            _list.Add(new MutabakatDebitCreditType() { Name = "Borç", DebitCreditType = "1" });
            _list.Add(new MutabakatDebitCreditType() { Name = "Alacak", DebitCreditType = "2" });
        }

    }
}
