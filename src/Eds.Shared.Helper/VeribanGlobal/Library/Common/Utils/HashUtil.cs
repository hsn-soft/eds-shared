using System.Security.Cryptography;
using System.Text;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils
{
    public class HashUtil : HashGenerator
    {
        private static HashGenerator Create()
        {
            return new HashGenerator();
        }

        public static string GetHashSha256(string inputText)
        {
            byte[] data = Encoding.UTF8.GetBytes(inputText);
            return GetHashSha256(data);
        }

        public static string GetHashSha256(byte[] dataArray)
        {
            HashGenerator hashGenerator = HashUtil.Create();
            byte[] hashData = hashGenerator.DataHashSha256(dataArray);
            return hashGenerator.HexBytesToString(hashData);
        }

        public static string GetFileHashSha256(string fileFullPath)
        {
            HashGenerator hashGenerator = HashUtil.Create();
            byte[] hashData = hashGenerator.FileHashSha256(fileFullPath);
            return hashGenerator.HexBytesToString(hashData);
        }

        public static string GetHashMD5(string inputText)
        {
            byte[] data = Encoding.UTF8.GetBytes(inputText);
            return GetHashMD5(data);
        }

        public static string GetHashMD5(byte[] dataArray)
        {
            HashGenerator hashGenerator = HashUtil.Create();
            byte[] hashData = hashGenerator.DataHashMD5(dataArray);
            return hashGenerator.HexBytesToString(hashData);
        }

        public static string GetFileHashMD5(string fileFullPath)
        {
            HashGenerator hashGenerator = HashUtil.Create();
            byte[] hashData = hashGenerator.FileHashMD5(fileFullPath);
            return hashGenerator.HexBytesToString(hashData);
        }
    }

    public class HashGenerator
    {
        // The cryptographic service provider.
        private readonly MD5 Md5 = MD5.Create();

        public byte[] FileHashMD5(string fileFullPath)
        {
            using (FileStream stream = File.OpenRead(fileFullPath))
            {
                return Md5.ComputeHash(stream);
            }
        }
        public byte[] DataHashMD5(byte[] data)
        {
            return Md5.ComputeHash(data, 0, data.Length);
        }

        // The cryptographic service provider.
        private readonly SHA256 Sha256 = SHA256.Create();

        // Compute the file's hash.
        public byte[] FileHashSha256(string fileFullPath)
        {
            using (FileStream stream = File.OpenRead(fileFullPath))
            {
                return Sha256.ComputeHash(stream);
            }
        }
        public byte[] DataHashSha256(byte[] data)
        {
            return Sha256.ComputeHash(data, 0, data.Length);
        }

        // Return a byte array as a sequence of hex values.
        public string HexBytesToString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(string.Empty);
            foreach (byte b in bytes) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}
