using System.Text.RegularExpressions;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils
{
    public static class RegexUtilities
    {
        public const string MatchEmailPattern = @"([a-zA-Z0-9_\-+_.\.]+)@([a-zA-Z0-9_\-+_.\.]+)$";

        public static bool IsValidEmail(string email)
        {
            if (email != null) return Regex.IsMatch(email, MatchEmailPattern);
            else return false;
        }
    }
}
