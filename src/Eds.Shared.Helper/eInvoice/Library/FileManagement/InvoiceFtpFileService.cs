using Eds.Shared.Helper.VeribanGlobal.Library.FileManagement;
using Microsoft.Extensions.Configuration;

namespace Eds.Shared.Helper.eInvoice.Library.FileManagement
{
    public class FtpFileInfo
    {
        /// <summary>
        ///     Dosya adı . Full path
        /// </summary>
        public string FileFullPath { get; set; }

        /// <summary>
        ///     Dokuman türü. XML,UBLTR,TXT
        /// </summary>
        public FtpFileTypes FtpFileType { get; set; }

        /// <summary>
        ///     Dosya uzantısı.
        /// </summary>
        public string Extension { get; set; }
    }

    public enum FtpFileTypes
    {
        UBL_TR,
        UBL_TR_SIGNED,
        XML,
        XLS,
        CSV,
        TXT,
        SAP
    }

    public class InvoiceFtpFileService
    {
        private readonly string _EInvoiceFtpFileBase;

        private readonly FMFile _fmFile = null;

        public InvoiceFtpFileService()
        {
            this._EInvoiceFtpFileBase = new ConfigurationManager()["EINVOICE_FtpFileBase"];

            _fmFile = new FMFile();
        }

        public List<FtpFileInfo> GetOutBoxRawXmlFiles(string accountParameter, FtpFileTypes ftpFileType)
        {
            List<FtpFileInfo> files = new List<FtpFileInfo>();

            try
            {
                if (string.IsNullOrEmpty(accountParameter)) throw new Exception("AccountParameter unknown");

                string sourcePath = string.Format("{0}\\OutBox\\Raw\\", accountParameter);
                sourcePath = _EInvoiceFtpFileBase + sourcePath;

                string extension = string.Empty;
                switch (ftpFileType)
                {
                    case FtpFileTypes.UBL_TR:
                        {
                            sourcePath += "UblTr";
                            extension = "xml";
                            break;
                        }
                    case FtpFileTypes.UBL_TR_SIGNED:
                        {
                            sourcePath += "UblTrWithSignature";
                            extension = "xml";
                            break;
                        }
                    case FtpFileTypes.XML:
                        {
                            sourcePath += "Xml";
                            extension = "xml";
                            break;
                        }
                    case FtpFileTypes.XLS:
                        {
                            sourcePath += "Xls";
                            extension = "xls";
                            break;
                        }
                    case FtpFileTypes.CSV:
                        {
                            sourcePath += "Csv";
                            extension = "csv";
                            break;
                        }
                    case FtpFileTypes.TXT:
                        {
                            sourcePath += "Txt";
                            extension = "txt";
                            break;
                        }
                    case FtpFileTypes.SAP:
                        {
                            sourcePath += "SAP";
                            extension = "xml";
                            break;
                        }
                }

                if (!Directory.Exists(sourcePath))
                    Directory.CreateDirectory(sourcePath);

                string extensionFilter = string.Format("*.{0}", extension);
                List<string> folderFiles = Directory.GetFiles(sourcePath, extensionFilter).Take(50).ToList();
                foreach (var file in folderFiles)
                {
                    FileInfo info = new FileInfo(file);
                    FtpFileInfo fileInfo = new FtpFileInfo();
                    fileInfo.Extension = info.Extension;
                    fileInfo.FileFullPath = info.FullName;
                    fileInfo.FtpFileType = ftpFileType;
                    files.Add(fileInfo);
                }
            }
            catch (Exception)
            {
                files = null;
            }

            return files;
        }

        public KeyValuePair<bool, string> MoveError(string sourceFilePath, string accountParameter, int Year, int Month)
        {
            if (string.IsNullOrEmpty(accountParameter)) return new KeyValuePair<bool, string>(false, "AccountParameter unknown");

            string destinationPath = string.Format("{0}\\OutBox\\Error\\{1}\\{2}\\", accountParameter, Year.ToString(), Month.ToString().PadLeft(2, '0'));
            destinationPath = _EInvoiceFtpFileBase + destinationPath;

            return _fmFile.MoveFile(sourceFilePath, destinationPath, true);
        }

        public KeyValuePair<bool, string> SaveFtpErrorContent(string errorFileNameWithoutExtension, string errorMessageLine, string accountParameter, int Year, int Month)
        {
            if (string.IsNullOrEmpty(accountParameter)) return new KeyValuePair<bool, string>(false, "AccountParameter unknown");

            string destinationPath = string.Format("{0}\\OutBox\\Error\\{1}\\{2}\\", accountParameter, Year.ToString(), Month.ToString().PadLeft(2, '0'));
            destinationPath = _EInvoiceFtpFileBase + destinationPath;

            string contentFileName = string.Format("{0}_error.txt", errorFileNameWithoutExtension);


            DirectoryInfo di = new DirectoryInfo(destinationPath);
            if (!di.Exists) di.Create();

            string fileSaveFullPath = Path.Combine(destinationPath, contentFileName);

            using (StreamWriter sw = File.Exists(fileSaveFullPath) ? File.AppendText(fileSaveFullPath) : File.CreateText(fileSaveFullPath))
            {
                sw.WriteLine(errorMessageLine);
            }

            return new KeyValuePair<bool, string>(true, fileSaveFullPath);
        }

        public KeyValuePair<bool, string> CopyToInboxFolder(string sourceFilePath, string accountParameter, string customerParameter, int Year, int Month)
        {
            if (string.IsNullOrEmpty(accountParameter)) return new KeyValuePair<bool, string>(false, "AccountParameter unknown");
            if (string.IsNullOrEmpty(customerParameter)) return new KeyValuePair<bool, string>(false, "CustomerParameter unknown");

            string destinationPath = string.Format("{0}\\Inbox\\{1}\\{2}\\{3}\\", accountParameter, customerParameter, Year.ToString(), Month.ToString().PadLeft(2, '0'));
            destinationPath = _EInvoiceFtpFileBase + destinationPath;

            return _fmFile.CopyFile(sourceFilePath, destinationPath, true);
        }
    }
}