namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils
{
    public static class FileSizeUtils
    {
        public static string FileSizeToString(long fileSize)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            double fileLength = fileSize;
            while (fileLength >= 1024 && order + 1 < sizes.Length)
            {
                order++;
                fileLength = fileLength / 1024;
            }
            return String.Format("{0:0.##} {1}", fileLength, sizes[order]);
        }

        public static decimal FileSizeConvertToMB(long fileSize)
        {
            return new decimal(fileSize / 1024 / 1024);
        }
    }
}
