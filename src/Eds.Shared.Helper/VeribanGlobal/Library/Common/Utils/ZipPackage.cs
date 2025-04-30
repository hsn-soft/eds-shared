using System.Globalization;
using System.IO.Compression;
using System.Text;
using ZipFile = System.IO.Compression.ZipFile;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils
{
    public static class ZipPackage
    {
        public static KeyValuePair<bool, string> CompressFiles(FileInfo[] fileList, string outputZipFileFullPath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(outputZipFileFullPath))
                    throw new ArgumentException("Output path is empty.");

                if (fileList == null || fileList.Length == 0)
                    throw new ArgumentException("File list is empty.");

                foreach (var file in fileList)
                    if (!file.Exists)
                        throw new FileNotFoundException($"File not found: {file.FullName}");

                if (File.Exists(outputZipFileFullPath))
                    File.Delete(outputZipFileFullPath);

                using (var zipToOpen = new FileStream(outputZipFileFullPath, FileMode.Create))
                using (var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                {
                    foreach (var file in fileList)
                    {
                        string entryName = file.Name;
                        if (archive.Entries.Any(e => e.FullName.Equals(entryName, StringComparison.OrdinalIgnoreCase)))
                        {
                            entryName = $"{Guid.NewGuid()}_{file.Name}";
                        }

                        archive.CreateEntryFromFile(file.FullName, entryName);
                    }
                }

                return new KeyValuePair<bool, string>(true, outputZipFileFullPath);
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.Message);
            }
        }

        public static List<KeyValuePair<string, byte[]>> GetCompressedByte(byte[] zipBytes)
        {
            List<KeyValuePair<string, byte[]>> files = new List<KeyValuePair<string, byte[]>>();

            using (var memoryStream = new MemoryStream(zipBytes))
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Read))
            {
                foreach (var entry in archive.Entries)
                {
                    using (var entryStream = entry.Open())
                    using (var ms = new MemoryStream())
                    {
                        entryStream.CopyTo(ms);
                        files.Add(new KeyValuePair<string, byte[]>(entry.FullName, ms.ToArray()));
                    }
                }
            }

            return files;
        }

        public static KeyValuePair<bool, string> ExtractZipFileNew(
            string sourceZipFileFullPath,
            string extractDirectoryPath,
            bool ifExtractSuccessRemoveZipFile,
            string controlExtension = null,
            bool byPassControlFileName = false,
            Guid? zipFileNamePrefixUniqueId = null)
        {
            try
            {
                if (!File.Exists(sourceZipFileFullPath))
                    return new KeyValuePair<bool, string>(false, "ZIP DOSYASI BULUNAMADI");

                string extractedFileFullPath = string.Empty;

                using (var archive = ZipFile.OpenRead(sourceZipFileFullPath))
                {
                    if (archive.Entries.Count != 1)
                        return new KeyValuePair<bool, string>(false, "ZIP ICERISINDE BIRDEN FAZLA DOSYA OLAMAZ");

                    var entry = archive.Entries.First();

                    if (entry.FullName.Contains("/") || entry.FullName.Contains("\\"))
                        return new KeyValuePair<bool, string>(false, "ZIP ICERISINDE KLASOR OLAMAZ");

                    string extractedFileName = entry.Name;

                    if (!byPassControlFileName)
                    {
                        string expectedFileName = Path.GetFileNameWithoutExtension(sourceZipFileFullPath);
                        string actualFileName = Path.GetFileNameWithoutExtension(
                            zipFileNamePrefixUniqueId.HasValue
                                ? zipFileNamePrefixUniqueId.Value.ToString("N").ToUpper() + "_" + extractedFileName
                                : extractedFileName
                        );

                        if (!string.Equals(expectedFileName, actualFileName, StringComparison.OrdinalIgnoreCase))
                            return new KeyValuePair<bool, string>(false, "ZIP ICERISINDE OLAN DOSYA ADI ILE ZIP DOSYA ADI AYNI OLMALI");
                    }

                    if (!string.IsNullOrEmpty(controlExtension) &&
                        !entry.Name.EndsWith("." + controlExtension, true, CultureInfo.InvariantCulture))
                    {
                        return new KeyValuePair<bool, string>(false, $"ZIP ICERISINDE OLAN DOSYA [ {controlExtension} ] DOSYASI DEGIL");
                    }

                    try
                    {
                        if (Directory.Exists(extractDirectoryPath))
                        {
                            Directory.Delete(extractDirectoryPath, true);
                        }

                        Directory.CreateDirectory(extractDirectoryPath);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    extractedFileFullPath = Path.Combine(extractDirectoryPath, extractedFileName);
                    entry.ExtractToFile(extractedFileFullPath, true);
                }

                if (ifExtractSuccessRemoveZipFile)
                    File.Delete(sourceZipFileFullPath);

                return new KeyValuePair<bool, string>(true, extractedFileFullPath);
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, $"ZIP ACILAMADI: {ex.Message}");
            }
        }
    }

    public static class ZipManager
    {
        public static async Task<byte[]> ZipStringAsync(string input, CancellationToken cancellationToken)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            return await ZipBytesAsync(inputBytes, cancellationToken);
        }

        public static async Task<byte[]> ZipBytesAsync(byte[] data, CancellationToken cancellationToken)
        {
            await using var output = new MemoryStream();
            await using (var gzip = new GZipStream(output, CompressionMode.Compress, leaveOpen: true))
            {
                await gzip.WriteAsync(data, 0, data.Length, cancellationToken);
            }

            return output.ToArray();
        }

        public static async Task<byte[]> UnzipBytesAsync(byte[] compressedData, CancellationToken cancellationToken)
        {
            await using var input = new MemoryStream(compressedData);
            await using var output = new MemoryStream();
            await using (var gzip = new GZipStream(input, CompressionMode.Decompress))
            {
                await gzip.CopyToAsync(output, cancellationToken);
            }

            return output.ToArray();
        }

        public static async Task WriteFileAsync(byte[] data, string path, CancellationToken cancellationToken)
        {
            await using var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            await fs.WriteAsync(data, 0, data.Length, cancellationToken);
        }

        public static async Task<byte[]> CreateZipFileAsync(string[] filePaths, CancellationToken cancellationToken)
        {
            await using var archiveStream = new MemoryStream();
            using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, leaveOpen: true))
            {
                foreach (var path in filePaths)
                {
                    if (!File.Exists(path)) continue;

                    var entry = archive.CreateEntry(Path.GetFileName(path));
                    await using var entryStream = entry.Open();
                    await using var fileStream = File.OpenRead(path);
                    await fileStream.CopyToAsync(entryStream, cancellationToken);
                }
            }

            return archiveStream.ToArray();
        }

        public static async Task<byte[]> CreateAndSaveZipFileAsync(string[] filePaths, string zipFilePath, CancellationToken cancellationToken)
        {
            var zipBytes = await CreateZipFileAsync(filePaths, cancellationToken);
            await WriteFileAsync(zipBytes, zipFilePath, cancellationToken);
            return zipBytes;
        }

        public static async Task<ExtractType> ExtractZipFileAsync(string zipFilePath, string extractDirectory, CancellationToken cancellationToken)
        {
            try
            {
                if (!File.Exists(zipFilePath))
                    return ExtractType.FileDoesNotExist;

                using var archive = ZipFile.OpenRead(zipFilePath);
                bool hasFiles = false;

                foreach (var entry in archive.Entries)
                {
                    if (string.IsNullOrWhiteSpace(entry.Name))
                        continue; // Skip directories

                    hasFiles = true;

                    if (Path.GetExtension(entry.Name).ToLowerInvariant() != ".xml")
                        return ExtractType.XmlFileIsNot;

                    string zipName = Path.GetFileNameWithoutExtension(zipFilePath);
                    string fileName = Path.GetFileNameWithoutExtension(entry.Name);

                    if (!string.Equals(zipName, fileName, StringComparison.OrdinalIgnoreCase))
                        return ExtractType.WrongFileName;

                    string destinationPath = Path.GetFullPath(Path.Combine(extractDirectory, entry.Name));

                    // 🚫 ZIP SLIP Güvenlik Kontrolü
                    if (!destinationPath.StartsWith(Path.GetFullPath(extractDirectory)))
                        throw new UnauthorizedAccessException("Zip entry is trying to extract outside of the target directory.");

                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

                    await using var outputStream = File.Create(destinationPath);
                    await using var entryStream = entry.Open();
                    await entryStream.CopyToAsync(outputStream, cancellationToken);
                }

                return hasFiles ? ExtractType.None : ExtractType.FileDoesNotExist;
            }
            catch
            {
                return ExtractType.Error;
            }
        }
    }

    public enum ExtractType
    {
        None = 0,

        /// <summary>
        /// Zip açıldı.
        /// </summary>
        Success = 1,

        /// <summary>
        ///     Zip açılamadı.
        /// </summary>
        Error = 2,

        /// <summary>
        ///     Zip bir dosya içermemektedir.
        ///     Zip uzantısı olan dosya içermemektedir.
        /// </summary>
        FileDoesNotExist = 4,

        /// <summary>
        ///     Dosyal xml değil
        /// </summary>
        XmlFileIsNot = 8,

        /// <summary>
        ///     Zip dosya adı xml dosya adı ile aynı değil
        /// </summary>
        WrongFileName = 16
    }
}