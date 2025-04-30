using System.Globalization;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils;

namespace Eds.Shared.Helper.VeribanGlobal.Library.FileManagement
{
    public class FMFile
    {
        public KeyValuePair<bool, string> SavePDFFile(string htmlContent, string fileSavePathDir, string fileNameWithoutExtension)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(fileSavePathDir);
                if (!di.Exists) di.Create();

                string saveFileFullPath = Path.Combine(fileSavePathDir, string.Format("{0}.pdf", fileNameWithoutExtension));

                PdfConvert.ConvertHtmlToPdf
                (
                    new PdfDocument
                    {
                        Url = "-",
                        Html = htmlContent
                    },
                    new PdfOutput
                    {
                        OutputFilePath = saveFileFullPath
                    }
                );

                FileInfo fi = new FileInfo(saveFileFullPath);
                if (!fi.Exists || fi.Length < 1)
                    throw new Exception("PDF DOSYASI OLUSTURULAMADI");

                return new KeyValuePair<bool, string>(true, saveFileFullPath);
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.Message);
            }
        }

        public KeyValuePair<bool, string> SaveXmlFile(string xmlContent, string fileSavePathDir, string fileNameWithoutExtension)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(fileSavePathDir);
                if (!di.Exists) di.Create();

                string draftFileFullPath = Path.Combine(fileSavePathDir, string.Format("{0}.xml", fileNameWithoutExtension));

                File.WriteAllText(draftFileFullPath, xmlContent);

                FileInfo fi = new FileInfo(draftFileFullPath);
                if (!fi.Exists || fi.Length < 1)
                    throw new Exception("XML DOSYASI OLUSTURULAMADI");

                return new KeyValuePair<bool, string>(true, draftFileFullPath);
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.Message);
            }
        }
        public KeyValuePair<bool, string> SaveXmlFileOld(string xmlContent, string xmlDirectoryPath, string storagePath)
        {
            try
            {
                if (!Directory.Exists(storagePath + xmlDirectoryPath))
                {
                    Directory.CreateDirectory(storagePath + xmlDirectoryPath);
                }

                string _fileGuid = Guid.NewGuid().ToString();
                string _xmlFile = storagePath + xmlDirectoryPath + _fileGuid + ".xml";

                while (File.Exists(_xmlFile))
                {
                    _fileGuid = Guid.NewGuid().ToString();
                    _xmlFile = storagePath + xmlDirectoryPath + _fileGuid + ".xml";
                }

                StreamWriter OurStream;
                OurStream = File.CreateText(_xmlFile);
                OurStream.WriteLine(xmlContent);
                OurStream.Close();

                if (File.Exists(_xmlFile))
                    return new KeyValuePair<bool, string>(true, _xmlFile);
                else
                    return new KeyValuePair<bool, string>(false, "File not exist : " + _xmlFile);
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.Message);
            }
        }

        public KeyValuePair<bool, string> SaveBinaryFile(byte[] fileBinaryDataArray, string directoryPath, string fileName)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(directoryPath);
                if (!di.Exists) di.Create();

                string fileSaveFullPath = Path.Combine(directoryPath, fileName);

                File.WriteAllBytes(fileSaveFullPath, fileBinaryDataArray);

                FileInfo fi = new FileInfo(fileSaveFullPath);
                if (!fi.Exists || fi.Length < 1)
                    throw new Exception("DOSYA OLUSTURULAMADI");

                return new KeyValuePair<bool, string>(true, fileSaveFullPath);
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.Message);
            }
        }

        public KeyValuePair<bool, string> GetXmlFileContent(string xmlFileFullPath)
        {
            try
            {
                FileInfo fi = new FileInfo(xmlFileFullPath);
                if (!fi.Exists || fi.Length < 1)
                    throw new Exception("XML DOSYASI BULUNAMADI");

                using (StreamReader sr = new StreamReader(xmlFileFullPath))
                {
                    return new KeyValuePair<bool, string>(true, sr.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.Message);
            }
        }

        public KeyValuePair<bool, string> AddFile(byte[] tempFile, string directoryPath, string fileName, string extension)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string _fileGuid;

                string newFileName = directoryPath + fileName + extension;

                while (File.Exists(newFileName))
                {
                    _fileGuid = Guid.NewGuid().ToString();
                    newFileName = directoryPath + _fileGuid + extension;
                }

                System.IO.FileStream _FileStream = new System.IO.FileStream(newFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);

                _FileStream.Write(tempFile, 0, tempFile.Length);

                _FileStream.Close();

                return new KeyValuePair<bool, string>(true, newFileName);

            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.Message);
            }
        }

        public KeyValuePair<bool, string> UpdateXmlFile(string xmlContent, string fileFullPath)
        {
            try
            {
                StreamWriter OurStream;
                OurStream = File.CreateText(fileFullPath);
                OurStream.Write(xmlContent);
                OurStream.Close();
                return new KeyValuePair<bool, string>(true, fileFullPath);
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.Message);
            }
        }

        public KeyValuePair<bool, string> MoveFile(string sourceFileFullPath, string destinationDirPath, bool ifExistOverwriteDestination = false)
        {
            string newFileName = Path.GetFileName(sourceFileFullPath);

            return MoveFileWithNewFileName(sourceFileFullPath, destinationDirPath, newFileName, ifExistOverwriteDestination);
        }

        public KeyValuePair<bool, string> MoveFileWithNewFileName(string sourceFileFullPath, string destinationDirPath, string newFileName, bool ifExistOverwriteDestination = false)
        {
            try
            {
                string sourceDirPath = Path.GetDirectoryName(sourceFileFullPath) + @"\";
                string sourceFileName = Path.GetFileName(sourceFileFullPath);

                if (!Directory.Exists(destinationDirPath))
                    Directory.CreateDirectory(destinationDirPath);

                //SAME DIRECTORY MOVE KONTROL
                if (string.Equals(sourceDirPath.ToLower(new CultureInfo("en-US")), destinationDirPath.ToLower(new CultureInfo("en-US")))
                    && string.Equals(sourceFileName.ToLower(new CultureInfo("en-US")), newFileName.ToLower(new CultureInfo("en-US"))))
                {
                    if (File.Exists(sourceFileFullPath))
                    {
                        //MOVE FILE READY BEFORE MOVE OPERATION
                        return new KeyValuePair<bool, string>(true, sourceFileFullPath);
                    }
                    else
                    {
                        return new KeyValuePair<bool, string>(false, "TAŞINACAK DOSYA BULUNAMADI");
                    }
                }

                if (ifExistOverwriteDestination)
                {
                    if (File.Exists(Path.Combine(destinationDirPath, newFileName)))
                        DeleteFile(Path.Combine(destinationDirPath, newFileName));

                    if (File.Exists(Path.Combine(destinationDirPath, newFileName)))
                        return new KeyValuePair<bool, string>(false, "DOSYA TAŞINAMADI");
                }
                else
                {
                    while (File.Exists(Path.Combine(destinationDirPath, newFileName)))
                    {
                        string extension = Path.GetExtension(newFileName);

                        newFileName = string.Format("{0}_{1}{2}", newFileName, Guid.NewGuid().ToString("N").ToUpper(), extension);
                    }
                }

                string destinationFileFullPath = Path.Combine(destinationDirPath, newFileName);
                File.Move(sourceFileFullPath, destinationFileFullPath);

                if (File.Exists(destinationFileFullPath))
                    return new KeyValuePair<bool, string>(true, destinationFileFullPath);
                else
                    return new KeyValuePair<bool, string>(false, "DOSYA TAŞINAMADI");
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.Message);
            }
        }

        public KeyValuePair<bool, string> CopyFile(string sourceFile, string destinationPath, bool overwrite)
        {
            try
            {
                if (!Directory.Exists(destinationPath))
                    Directory.CreateDirectory(destinationPath);

                FileInfo info = new FileInfo(sourceFile);
                if (info.Exists)
                {
                    destinationPath = destinationPath + info.Name;
                    File.Copy(sourceFile, destinationPath, overwrite);

                    if (File.Exists(destinationPath))
                        return new KeyValuePair<bool, string>(true, destinationPath);
                    else
                        return new KeyValuePair<bool, string>(false, "DOSYA KOPYALANAMADI.");
                }
                else
                    return new KeyValuePair<bool, string>(false, "DOSYA BULUNAMADI");
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.Message);
            }
        }

        public KeyValuePair<bool, string> DeleteFile(string sourceFile)
        {
            try
            {
                FileInfo info = new FileInfo(sourceFile);
                if (info.Exists)
                {
                    File.Delete(sourceFile);

                    if (!File.Exists(sourceFile))
                        return new KeyValuePair<bool, string>(true, "DOSYA SILINDI.");
                    else
                        return new KeyValuePair<bool, string>(false, "DOSYA SILINEMEDI.");
                }
                else
                    return new KeyValuePair<bool, string>(true, "DOSYA BULUNAMADI.");
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.Message);
            }
        }

        public KeyValuePair<bool, string> MoveXmlFileToArchive(string sourceFile, string targetPath)
        {
            try
            {
                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }

                string fileName = sourceFile.Substring(sourceFile.LastIndexOf("\\") + 1);

                string newFileName = targetPath + fileName;

                while (File.Exists(targetPath + fileName))
                {
                    fileName = Guid.NewGuid().ToString();
                    newFileName = targetPath + fileName + ".xml";
                }

                File.Move(sourceFile, newFileName);

                if (File.Exists(newFileName))
                    return new KeyValuePair<bool, string>(true, newFileName);
                else
                    return new KeyValuePair<bool, string>(false, "DOSYA TAŞINAMADI");
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.Message);
            }
        }
    }
}
