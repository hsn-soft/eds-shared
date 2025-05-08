using System.Collections;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils
{
    public class SafeVar
    {
        protected SafeVar()
        {

        }
        public static bool GetBool(object value)
        {
            if (value == null || value == DBNull.Value)
                return false;

            try
            {
                Boolean.TryParse(value.ToString(), out bool retValue);

                return retValue;
            }
            catch { return false; }
        }

        public static String GetString(object value)
        {
            return GetString(value, false);
        }
        public static String GetString(object value, bool isTrim)
        {
            if (value == null || value == DBNull.Value || String.IsNullOrEmpty(value.ToString()))
                return String.Empty;

            String retValue = String.Empty;

            try
            {
                retValue = value.ToString();

                if (isTrim) retValue = retValue.Trim();

                return retValue == "&nbsp;" || String.IsNullOrEmpty(retValue) ? String.Empty : retValue;
            }
            catch { return String.Empty; }
        }

        public static Int32 GetInt(object value)
        {
            return GetInt(value, false);
        }
        public static Int32 GetInt(object value, bool isRetValueNotNegative)
        {
            return GetInt(value, false, false);
        }
        public static Int32 GetInt(object value, bool isRetValueNotNegative, bool isMathCeiling)
        {
            if (value == null || value == DBNull.Value)
                return isRetValueNotNegative ? 0 : Int32.MinValue;

            Int32 retValue;
            try
            {
                if (Int32.TryParse(value.ToString() + String.Empty, out retValue))
                {
                    Int32 maxOrMin;
                    Int32.TryParse(Int32.MinValue.ToString() + String.Empty, out maxOrMin);
                    if (maxOrMin == retValue) return isRetValueNotNegative ? 0 : Int32.MinValue;

                    Int32.TryParse(Int32.MaxValue.ToString() + String.Empty, out maxOrMin);
                    if (maxOrMin == retValue) return Int32.MaxValue;
                }
                else
                {
                    //may be decimal number
                    Decimal decimalValue = GetDecimal(value);
                    if (decimalValue > 0)
                        retValue = isMathCeiling ? (Int32)Math.Ceiling(decimalValue) : (Int32)Math.Floor(decimalValue);
                    else
                        retValue = isRetValueNotNegative ? 0 : Int32.MinValue;
                }
                return retValue;
            }
            catch { return isRetValueNotNegative ? 0 : Int32.MinValue; }
        }

        public static float GetFloat(object value)
        {
            return GetFloat(value, false);
        }
        public static float GetFloat(object value, bool isRetValueNotNegative)
        {
            if (value == null || value == DBNull.Value)
                return isRetValueNotNegative ? 0 : float.MinValue;

            float retValue;

            try
            {
                if (float.TryParse(value.ToString().Replace('.', ',') + String.Empty, out retValue))
                {
                    float maxOrMin;
                    float.TryParse(float.MinValue.ToString().Replace('.', ',') + String.Empty, out maxOrMin);
                    if (maxOrMin == retValue) return isRetValueNotNegative ? 0 : float.MinValue;

                    float.TryParse(float.MaxValue.ToString().Replace('.', ',') + String.Empty, out maxOrMin);
                    if (maxOrMin == retValue) return float.MaxValue;

                    return retValue;
                }
                else return isRetValueNotNegative ? 0 : float.MinValue;
            }
            catch { return isRetValueNotNegative ? 0 : float.MinValue; }
        }

        public static Decimal GetDecimal(object value)
        {
            return GetDecimal(value, false);
        }
        public static Decimal GetDecimal(object value, bool isRetValueNotNegative)
        {
            if (value == null || value == DBNull.Value)
                return isRetValueNotNegative ? 0 : Decimal.MinValue;

            Decimal retValue;

            try
            {
                if (Decimal.TryParse(value.ToString().Replace('.', ',') + String.Empty, out retValue))
                {
                    Decimal maxOrMin;
                    Decimal.TryParse(Decimal.MinValue.ToString().Replace('.', ',') + String.Empty, out maxOrMin);
                    if (maxOrMin == retValue) return isRetValueNotNegative ? 0 : Decimal.MinValue;

                    Decimal.TryParse(Decimal.MaxValue.ToString().Replace('.', ',') + String.Empty, out maxOrMin);
                    if (maxOrMin == retValue) return Decimal.MaxValue;

                    return retValue;
                }
                else return isRetValueNotNegative ? 0 : Decimal.MinValue;
            }
            catch { return isRetValueNotNegative ? 0 : Decimal.MinValue; }
        }

        public static DateTime GetDate(object value)
        {
            if (value == null || value == DBNull.Value || String.IsNullOrEmpty(value.ToString()))
                return DateTime.MinValue;

            DateTime retValue;
            try
            {
                return DateTime.TryParse(value.ToString() + String.Empty, out retValue) ? retValue : DateTime.MinValue;
            }
            catch { return DateTime.MinValue; }
        }

        public static ArrayList GetArrayList(object value)
        {
            if (value is ArrayList)
                return value as ArrayList;

            return new ArrayList();
        }

        public static bool HasRow(DataTable value)
        {
            bool ret = false;
            if (value != null && value.Rows.Count > 0)
                ret = true;
            return ret;
        }

        public static string AntiHackingFilter(String STR)
        {
            return AntiHackingFilter(STR, false, false);
        }
        public static string AntiHackingFilter(String STR, bool isTrim)
        {
            return AntiHackingFilter(STR, false, isTrim);
        }
        public static String AntiHackingFilter(String s, bool isUpperCase, bool isTrim)
        {
            if (!String.IsNullOrEmpty(s))
            {
                //replace sql and html wildcards
                //possible reasons: XSS injection, SQL injection
                s = s.Replace(";", ":");
                s = s.Replace("'", "`");
                s = s.Replace("<", "{");
                s = s.Replace(">", "}");
                s = s.Replace("--", "_");
                s = s.Replace("%", "/");
                s = s.Replace("*", "#");
                s = s.Replace("&", "");
                if (isTrim) s = s.Trim();
            }

            return s;
        }

        public static string GetHashedPassword(string pass)
        {

            MD5 HashGenerator = MD5CryptoServiceProvider.Create();
            byte[] hashData = HashGenerator.ComputeHash(Encoding.ASCII.GetBytes(pass));
            return ToHexString(hashData);
        }

        public static string ToHexString(byte[] bytes)
        {
            char[] hexDigits = {
                                '0', '1', '2', '3', '4', '5', '6', '7',
                                '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};
            char[] chars = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                int b = bytes[i];
                chars[i * 2] = hexDigits[b >> 4];
                chars[i * 2 + 1] = hexDigits[b & 0xF];
            }
            return new string(chars);
        }

        public static bool HasItems(object o)
        {
            if (o == null) return false;

            switch (o.GetType().ToString())
            {
                case "System.Collections.ArrayList":
                    ArrayList a = (ArrayList)o;
                    return a.Count > 0;
                case "System.Data.DataTable":
                    DataTable dt = (DataTable)o;
                    return dt.Rows.Count > 0;
                case "System.Data.DataRow[]":
                    DataRow[] drCollection = (DataRow[])o;
                    return drCollection.Length > 0;
                    //can be add more types later
            }

            return false;
        }

    }
}
