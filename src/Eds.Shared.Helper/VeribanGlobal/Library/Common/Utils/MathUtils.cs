namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils
{
    public static class MathUtils
    {
        public static decimal RoundDown(decimal number, int decimalPlaces)
        {
            if (decimalPlaces < 1) decimalPlaces = 1;
            int dPlace = Convert.ToInt32(Math.Pow(10, decimalPlaces));

            return Math.Floor(number * dPlace) / dPlace;
        }
        public static double RoundDown(double number, int decimalPlaces)
        {
            if (decimalPlaces < 1) decimalPlaces = 1;
            int dPlace = Convert.ToInt32(Math.Pow(10, decimalPlaces));

            return Math.Floor(number * dPlace) / dPlace;
        }

        public static decimal RoundUp(decimal number, int decimalPlaces)
        {
            return Math.Round(number, decimalPlaces, MidpointRounding.AwayFromZero);
        }
        public static double RoundUp(double number, int decimalPlaces)
        {
            return Math.Round(number, decimalPlaces, MidpointRounding.AwayFromZero);
        }

    }
}
