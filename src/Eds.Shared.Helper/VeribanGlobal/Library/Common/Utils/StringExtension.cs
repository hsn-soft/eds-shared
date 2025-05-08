using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils
{
    public static class StringExtension
    {
        public static string TrimAllString(this string str, bool byPassTurkishCharReplace = false)
        {
            if (string.IsNullOrWhiteSpace(str)) return null;

            string[] whiteSpaceCharArray = str.Trim().Split(' ');

            StringBuilder nonWhiteSpaceUserName = new StringBuilder(string.Empty);

            for (int i = 0; i < whiteSpaceCharArray.Length; i++)
            {
                if (whiteSpaceCharArray[i].Length > 0)
                    nonWhiteSpaceUserName.Append(whiteSpaceCharArray[i]);
            }
            if (byPassTurkishCharReplace)
            {
                return nonWhiteSpaceUserName.ToString();
            }
            else
            {
                return nonWhiteSpaceUserName.ToString().ToUpper().TurkishCharReplace();
            }

        }

        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string TurkishCharReplace(this string str)
        {
            str = str.Replace("Ö", "O");
            str = str.Replace("Ü", "U");
            str = str.Replace("Ş", "S");
            str = str.Replace("Ğ", "G");
            str = str.Replace("Ç", "C");
            str = str.Replace("İ", "I");
            str = str.Replace("ö", "o");
            str = str.Replace("ı", "i");
            str = str.Replace("ç", "c");
            str = str.Replace("ş", "s");
            str = str.Replace("ü", "u");
            str = str.Replace("ğ", "g");
            return str;
        }

        public static bool IsIbanValid(this string ibanStr)
        {

            if (string.IsNullOrEmpty(ibanStr))
                return false;

            int ibanLength = ibanStr.Length;

            if (ibanLength != 26)
                return false;

            if (ibanStr[0] != 'T' || ibanStr[1] != 'R')
            {
                //"İlk iki hanesi TR olmalı";
                return false;
            }

            return true;
        }
    }

    public static class StringOperations
    {
        public static string CustomSubString(string value, int length, bool trim = false)
        {
            string key = null;
            if (!string.IsNullOrEmpty(value))
            {
                if (trim)
                {
                    value = value.Trim();
                }

                key = value;
                if (value.Length > length)
                    key = value.Substring(0, length);
            }
            return key;
        }

        public static string CheckUserName(string userName)
        {
            try
            {
                userName = userName.TrimAllString(true);

                //ALPHABET CHARACTER CONTROL
                string checkName = Regex.Replace(userName ?? string.Empty, @"[^\u0030-\u0039|\u0041-\u005A]+", "_").Replace("|", "_");

                return checkName;
            }
            catch { return null; }
        }

        public static string CheckDownloadFileName(string downloadCheckName)
        {
            try
            {
                if (downloadCheckName.Length > 120)
                    downloadCheckName = CustomSubString(downloadCheckName, 120);

                //ALPHABET CHARACTER CONTROL
                string checkName = Regex.Replace(downloadCheckName ?? string.Empty, @"[^\u0041-\u005A|\u0061-\u007A|ÇĞİÖŞÜçğıöşü]+", "_").Replace("|", "_");
                //TURKISH CHARACTERS
                checkName = checkName.TurkishCharReplace().ToUpper(new CultureInfo("en-US"));

                return checkName;
            }
            catch { return null; }
        }
    }
}
