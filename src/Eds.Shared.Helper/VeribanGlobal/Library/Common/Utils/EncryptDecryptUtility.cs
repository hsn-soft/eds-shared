using System.Security.Cryptography;
using System.Text;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils
{
    public static class EncryptDecryptUtility
    {

        private const string EncryptionKey =
         "893C3872-4061-476B-6282-D2DD8bAA8127" +
             "57367F25-1701-4099-9409-7D75667C149F" +
             "44A6D474-7C91-4F3D-C137-FA2E44367974";

        private static readonly char[] _base64Padding = { '=' };
        private static readonly byte[] _salt = new byte[48];

        public static string Encrypt(string inputText)
        {
            var rijndaelCipher = new RijndaelManaged();

            byte[] plainText = Encoding.UTF8.GetBytes(inputText);
            var secretKey = new Rfc2898DeriveBytes(EncryptionKey, _salt);

            using (var encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainText, 0, plainText.Length);
                        cryptoStream.FlushFinalBlock();
                        return ToUrlEncode(Convert.ToBase64String(memoryStream.ToArray()).TrimEnd(_base64Padding));
                    }
                }
            }
        }

        public static string Decrypt(string inputText)
        {
            try
            {
                var rijndaelCipher = new RijndaelManaged();

                byte[] encryptedData = Convert.FromBase64String(FixPadding(FromUrlEncode(inputText)));
                var secretKey = new Rfc2898DeriveBytes(EncryptionKey, _salt);

                using (var decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
                {
                    using (var memoryStream = new MemoryStream(encryptedData))
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            byte[] plainText = new byte[encryptedData.Length];
                            int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                            return Encoding.UTF8.GetString(plainText, 0, decryptedCount);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }


        #region Helper
        private static string FixPadding(string base64)
        {
            int paddingLength = (4 - base64.Length % 4) % 4;
            return base64.PadRight(base64.Length + paddingLength, '=');
        }

        private static string ToUrlEncode(string input)
        {
            return input.Replace('+', '.').Replace('/', '-').Replace('=', '_');
        }

        private static string FromUrlEncode(string input)
        {
            return input.Replace('.', '+').Replace('-', '/').Replace('_', '=');
        }
        #endregion

    }
}
