namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils
{
    public static class DateTimeExtension
    {
        public static string convertDMY(this DateTime date)
        {
            return date.ToString("dd.MM.yyyy");
        }

        public static string convertymd(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        public static DateTime GetLastDayOfMont(this DateTime date)
        {
            date = new DateTime(date.Year, date.Month, 1);
            return date.AddMonths(1).AddDays(-1);
        }
        public static DateTime GetFirstDayOfMont(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }
    }
}
