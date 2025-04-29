using Ionic.Zip;
using System.Globalization;
using System.IO.Compression;
using System.Text;
using ZipFile = Ionic.Zip.ZipFile;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils
{
    public static class ZipPackage
    {
        public static KeyValuePair<bool, string> CompressFiles(FileInfo[] fileList, string outputZipFileFullPath)
        {
            KeyValuePair<bool, string> operationResult;

            try
            {
                if (!string.IsNullOrEmpty(outputZipFileFullPath))
                {
                    if (File.Exists(outputZipFileFullPath))
                    {
                        File.Delete(outputZipFileFullPath);
                    }
                    if (File.Exists(outputZipFileFullPath))
                        throw new ArgumentException("outputZipFileFullPath is already path");

                    if (fileList != null && fileList.Length > 0)
                    {
                        bool existControl = true;
                        foreach (var item in fileList)
                        {
                            if (!item.Exists)
                            {
                                existControl = false;
                                break;
                            }
                        }

                        if (existControl)
                        {
                            using (ZipFile zipFile = new ZipFile())
                            {
                                foreach (var file in fileList)
                                {
                                    if (zipFile.Entries.Any(x => x.FileName.ToUpper() == (file.Name).ToUpper()))
                                    {
                                        zipFile.AddEntry(Guid.NewGuid().ToString() + "_" + file.Name, File.ReadAllBytes(file.FullName));
                                    }
                                    else
                                    {
                                        zipFile.AddEntry(file.Name, File.ReadAllBytes(file.FullName));
                                    }
                                }

                                zipFile.Save(outputZipFileFullPath);
                            }

                            if (File.Exists(outputZipFileFullPath))
                            {
                                return new KeyValuePair<bool, string>(true, outputZipFileFullPath);
                            }
                            else throw new ArgumentException("ZipFile create error");
                        }
                        else throw new ArgumentException("File not exist in fileList");
                    }
                    else throw new ArgumentException("Can't be empty fileList");
                }
                else throw new ArgumentException("Can't be empty outputZipFileFullPath");
            }
            catch (Exception ex) { operationResult = new KeyValuePair<bool, string>(false, ex.Message); }

            return operationResult;
        }

        public static List<KeyValuePair<string, byte[]>> GetCompressedByte(byte[] fileData)
        {
            List<KeyValuePair<string, byte[]>> list = new List<KeyValuePair<string, byte[]>>();

            using (ZipFile z = ZipFile.Read(new MemoryStream(fileData)))
            {
                foreach (var item in z.Entries.ToList())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        item.Extract(ms);
                        byte[] file = ms.ToArray();
                        list.Add(new KeyValuePair<string, byte[]>(item.FileName, file));
                    }
                }
            }

            return list;
        }

        public static KeyValuePair<bool, string> ExtractZipFileNew(string sourceZipFileFullPath, string extractDirectoryPath, bool ifExtractSuccessRemoveZipFile, string controlExtension = null, bool byPassControlFileName = false, Guid? zipFileNamePrefixUniqueId = null)
        {
            bool extractSuccess = false;
            string extractErrorMessage = string.Empty;
            string extractFileFullPath = string.Empty;

            if (File.Exists(sourceZipFileFullPath))
            {
                ZipFile zip = null;
                try
                {
                    zip = ZipFile.Read(sourceZipFileFullPath);

                    if (zip.Entries != null && zip.Entries.Count == 1)
                    {
                        ZipEntry entry = zip.Entries.First();

                        string[] filePathHierarchy = entry.FileName.Split('/');
                        if (filePathHierarchy != null && filePathHierarchy.Length == 1)
                        {
                            string controlEntryFileName = zipFileNamePrefixUniqueId.HasValue ? zipFileNamePrefixUniqueId.Value.ToString() + "_" + entry.FileName : entry.FileName;
                            if (byPassControlFileName || (string.Equals(Path.GetFileNameWithoutExtension(controlEntryFileName).ToLower(new CultureInfo("en-US")), Path.GetFileNameWithoutExtension(sourceZipFileFullPath).ToLower(new CultureInfo("en-US")))))
                            {
                                bool extensionControl = true;
                                if (!string.IsNullOrEmpty(controlExtension))
                                {
                                    extensionControl = (string.Equals(Path.GetExtension(entry.FileName).ToLower(new CultureInfo("en-US")), string.Format(".{0}", controlExtension.ToLower(new CultureInfo("en-US")))));
                                    if (!extensionControl) extractErrorMessage = "ZIP ICERISINDE OLAN DOSYA [ " + controlExtension + " ] DOSYASI DEGIL";
                                }

                                if (extensionControl)
                                {
                                    if (!System.IO.Directory.Exists(extractDirectoryPath))
                                        System.IO.Directory.CreateDirectory(extractDirectoryPath);

                                    //EXTRACT FILE FROM ZIP FILE
                                    entry.Extract(extractDirectoryPath, ExtractExistingFileAction.OverwriteSilently);

                                    //CONTROL EXTRACT FILE
                                    extractFileFullPath = Path.Combine(extractDirectoryPath, entry.FileName);
                                    if (File.Exists(extractFileFullPath))
                                    {
                                        extractSuccess = true;
                                    }
                                    else extractErrorMessage = "ZIP ICERISINDEN DOSYA CIKARILAMADI";
                                }
                            }
                            else extractErrorMessage = "ZIP ICERISINDE OLAN DOSYA ADI ILE ZIP DOSYA ADI AYNI OLMALI";
                        }
                        else extractErrorMessage = "ZIP ICERISINDE KLASOR OLAMAZ (HİYERARŞİK KLASÖR YAPISI OLMAMALI)";
                    }
                    else extractErrorMessage = "ZIP ICERISINDE BIRDEN FAZLA DOSYA OLAMAZ";
                }
                catch (Exception) { extractSuccess = false; extractErrorMessage = "ZIP ACILAMADI "; }
                finally
                {
                    if (zip != null)
                    {
                        zip.Dispose();
                    }

                    if (extractSuccess && ifExtractSuccessRemoveZipFile)
                    {
                        File.Delete(sourceZipFileFullPath);
                    }
                }
            }
            else extractErrorMessage = "ZIP DOSYASI BULUNAMADI";

            if (extractSuccess)
                return new KeyValuePair<bool, string>(true, extractFileFullPath);
            else
                return new KeyValuePair<bool, string>(false, extractErrorMessage);
        }
    }

    public static class ZipManager
    {
        public static byte[] ZipString(string documentString)
        {
            byte[] documentStringBytes = Encoding.UTF8.GetBytes(documentString);

            return ZipString(documentStringBytes);
        }
        public static byte[] ZipString(byte[] documentStringBytes)
        {
            using (MemoryStream memoryStreamInput = new MemoryStream(documentStringBytes))
            using (MemoryStream memoryStreamOutput = new MemoryStream())
            {
                using (GZipStream GZipStream = new GZipStream(memoryStreamOutput, CompressionMode.Compress))
                {
                    //memoryStreamInput.CopyTo(GZipStream)
                    CopyTo(memoryStreamInput, GZipStream);
                }

                return memoryStreamOutput.ToArray();
            }
        }

        public static byte[] UnzipString(byte[] zippedDocumentStringBytes)
        {
            using (MemoryStream memoryStreamInput = new MemoryStream(zippedDocumentStringBytes))
            using (MemoryStream memoryStreamOutput = new MemoryStream())
            {
                using (GZipStream GZipStream = new GZipStream(memoryStreamInput, CompressionMode.Decompress))
                {
                    //GZipStream.CopyTo(memoryStreamOutput)
                    CopyTo(GZipStream, memoryStreamOutput);
                }

                return memoryStreamOutput.ToArray();
            }
        }

        public static void WriteFile(byte[] zippedDocumentStringBytes, string documentPath)
        {
            using (FileStream file = new FileStream(documentPath, FileMode.Create))
            {
                file.Write(zippedDocumentStringBytes, 0, zippedDocumentStringBytes.Length);
            }
        }

        public static void CopyTo(Stream sourceStream, Stream targetStream)
        {
            byte[] transferBuffer = new byte[4096];

            int sourceReadBytesCount;

            while ((sourceReadBytesCount = sourceStream.Read(transferBuffer, 0, transferBuffer.Length)) != 0)
            {
                targetStream.Write(transferBuffer, 0, sourceReadBytesCount);
            }
        }

        public static byte[] CreateZipFile(string[] fileNames)
        {
            return CreateZipFile(fileNames, string.Empty);
        }
        public static byte[] CreateZipFile(string[] fileNames, string zipPassword)
        {
            using (MemoryStream memoryStreamOutput = new MemoryStream())
            {
                using (ZipFile zip = new ZipFile())
                {
                    if (!String.IsNullOrEmpty(zipPassword))
                        zip.Password = zipPassword;

                    foreach (string fileName in fileNames)
                        zip.AddFile(fileName, string.Empty);

                    zip.Save(memoryStreamOutput);  // SAVE MEMORY STREAM
                }

                return memoryStreamOutput.ToArray();
            }
        }

        public static byte[] CreateAndSaveZipFile(string[] fileNames, string zipFileToCreate)
        {
            return CreateAndSaveZipFile(fileNames, zipFileToCreate, string.Empty);
        }
        public static byte[] CreateAndSaveZipFile(string[] fileNames, string zipFileToCreate, string zipPassword)
        {
            using (MemoryStream memoryStreamOutput = new MemoryStream())
            {
                using (ZipFile zip = new ZipFile())
                {
                    if (!String.IsNullOrEmpty(zipPassword))
                        zip.Password = zipPassword;

                    foreach (string fileName in fileNames)
                        zip.AddFile(fileName, string.Empty);

                    zip.Save(zipFileToCreate); // SAVE THE HARD DISK
                }

                return memoryStreamOutput.ToArray();
            }
        }

        public static ExtractType ExtractZipFile(string zipFilePath, string extractDirectory)
        {
            ZipFile zip = null;
            ExtractType type = ExtractType.None;
            try
            {
                bool existFile = false;

                zip = ZipFile.Read(zipFilePath);

                foreach (ZipEntry e in zip)
                {
                    existFile = true;

                    FileInfo xmlFile = new FileInfo(e.FileName);

                    if (xmlFile.Extension.ToLower() != ".xml")
                    {
                        type = ExtractType.XmlFileIsNot;
                        break;
                    }
                    else
                    {
                        FileInfo info = new FileInfo(zipFilePath);

                        string zipFileName = info.Name.Replace(info.Extension, string.Empty);

                        string xmlFileName = xmlFile.Name.Replace(xmlFile.Extension, string.Empty);

                        if (xmlFileName != zipFileName)
                        {
                            type = ExtractType.WrongFileName;
                            break;
                        }
                        else
                            e.Extract(extractDirectory);
                    }
                }

                if (!existFile)
                    type = ExtractType.FileDoesNotExist;
            }
            catch (Exception)
            { type = ExtractType.Error; }
            finally
            {
                zip.Dispose();
            }

            return type;
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
