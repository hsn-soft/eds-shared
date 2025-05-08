using Eds.Shared.Helper.VeribanGlobal.Library.Common;
using Eds.Shared.Helper.VeribanGlobal.Library.FileManagement;
using Microsoft.Extensions.Configuration;

namespace Eds.Shared.Helper.eInvoice.Library.FileManagement
{
    public class InvoiceFileService
    {
        private readonly string _EInvoiceOutboxFileDirPath;
        private readonly string _EInvoiceInboxFileDirPath;

        private readonly string _AccountXsltFileDirPath;
        private readonly string _EMailTemplateFileDirPath;

        private readonly FMFile _fmFile = null;

        public InvoiceFileService(IConfiguration configuration)
        {
            string edsVolumePath = configuration.GetValue<string>("EDS_Volume_Path") ?? throw new ArgumentNullException("EDS_Volume_Path");
            _EInvoiceOutboxFileDirPath = edsVolumePath + (configuration.GetValue<string>("EINVOICE_InvoiceOutboxFileBase") ?? throw new ArgumentNullException("EINVOICE_InvoiceOutboxFileBase"));
            _EInvoiceInboxFileDirPath = edsVolumePath + (configuration.GetValue<string>("EINVOICE_InvoiceInboxFileBase") ?? throw new ArgumentNullException("EINVOICE_InvoiceInboxFileBase"));
            _AccountXsltFileDirPath = edsVolumePath + (configuration.GetValue<string>("GLOBAL_AccountXsltFileBase") ?? throw new ArgumentNullException("GLOBAL_AccountXsltFileBase"));
            _EMailTemplateFileDirPath = edsVolumePath + (configuration.GetValue<string>("GLOBAL_EMailTemplateFileBase") ?? throw new ArgumentNullException("GLOBAL_EMailTemplateFileBase"));

            _fmFile = new FMFile();
        }

        public KeyValuePair<bool, string> SaveOutboxXml(string xmlContent, string accountParameter, int Year, byte Month, byte Day, string invoiceIdentifier)
        {
            if (string.IsNullOrEmpty(accountParameter)) return new KeyValuePair<bool, string>(false, "AccountParameter unknown");
            if (Year < 2000) return new KeyValuePair<bool, string>(false, "Year grather than 2000");
            if (Month < 1 || Month > 12) return new KeyValuePair<bool, string>(false, "Month must be 1-12");
            if (Day < 1 || Day > 31) return new KeyValuePair<bool, string>(false, "Day must be 1-31");
            if (string.IsNullOrEmpty(invoiceIdentifier)) return new KeyValuePair<bool, string>(false, "invoiceIdentifier unknown");
            if (invoiceIdentifier.Length != 36) return new KeyValuePair<bool, string>(false, "invoiceIdentifier must be 36 character");

            string destinationPath = string.Format("/{0}/{1}/{2}{3}/", accountParameter, Year.ToString(), Month.ToString().PadLeft(2, '0'), Day.ToString().PadLeft(2, '0'));
            destinationPath = _EInvoiceOutboxFileDirPath + destinationPath;

            string xmlFileNameWithoutExtension = string.Format("{0}", invoiceIdentifier);

            return _fmFile.SaveXmlFile(xmlContent, destinationPath, xmlFileNameWithoutExtension);
        }

        public KeyValuePair<bool, string> SaveInboxXml(string xmlContent, string accountParameter, int Year, byte Month, byte Day, string invoiceIdentifier)
        {
            if (string.IsNullOrEmpty(accountParameter)) return new KeyValuePair<bool, string>(false, "AccountParameter unknown");
            if (Year < 2000) return new KeyValuePair<bool, string>(false, "Year grather than 2000");
            if (Month < 1 || Month > 12) return new KeyValuePair<bool, string>(false, "Month must be 1-12");
            if (Day < 1 || Day > 31) return new KeyValuePair<bool, string>(false, "Day must be 1-31");
            if (string.IsNullOrEmpty(invoiceIdentifier)) return new KeyValuePair<bool, string>(false, "invoiceIdentifier unknown");
            if (invoiceIdentifier.Length != 36) return new KeyValuePair<bool, string>(false, "invoiceIdentifier must be 36 character");

            string destinationPath = string.Format("/{0}/{1}/{2}{3}/", accountParameter, Year.ToString(), Month.ToString().PadLeft(2, '0'), Day.ToString().PadLeft(2, '0'));
            destinationPath = _EInvoiceInboxFileDirPath + destinationPath;

            string xmlFileNameWithoutExtension = string.Format("{0}", invoiceIdentifier);

            return _fmFile.SaveXmlFile(xmlContent, destinationPath, xmlFileNameWithoutExtension);
        }

        public KeyValuePair<bool, string> GetAccountXsltFile(string accountRegisterNumber, GlobalEnums.GlobalDocumentReferenceTypes globalDocumentReferenceType)
        {
            if (string.IsNullOrEmpty(accountRegisterNumber))
            {
                throw new Exception("ACCOUNT REGISTER NUMBER IS UNKNOWN");
            }

            return GetDocumentXsltFileContentBase(globalDocumentReferenceType, accountRegisterNumber);
        }

        public KeyValuePair<bool, string> GetDefaultXsltFile(GlobalEnums.GlobalDocumentReferenceTypes globalDocumentReferenceType)
        {
            return GetDocumentXsltFileContentBase(globalDocumentReferenceType);
        }

        private KeyValuePair<bool, string> GetDocumentXsltFileContentBase(GlobalEnums.GlobalDocumentReferenceTypes globalDocumentReferenceType, string accountRegisterNumber = null)
        {
            var filePathInfo = GetDocumentXsltFilePathBase(globalDocumentReferenceType, accountRegisterNumber);

            return _fmFile.GetXmlFileContent(filePathInfo.Value);
        }

        public KeyValuePair<bool, string> GetAccountXsltFilePath(string accountRegisterNumber, GlobalEnums.GlobalDocumentReferenceTypes globalDocumentReferenceType)
        {
            if (string.IsNullOrEmpty(accountRegisterNumber))
            {
                throw new Exception("ACCOUNT REGISTER NUMBER IS UNKNOWN");
            }

            return GetDocumentXsltFilePathBase(globalDocumentReferenceType, accountRegisterNumber);
        }

        public KeyValuePair<bool, string> GetDefaultXsltFilePath(GlobalEnums.GlobalDocumentReferenceTypes globalDocumentReferenceType)
        {
            return GetDocumentXsltFilePathBase(globalDocumentReferenceType);
        }

        private KeyValuePair<bool, string> GetDocumentXsltFilePathBase(GlobalEnums.GlobalDocumentReferenceTypes globalDocumentReferenceType, string accountRegisterNumber = null)
        {
            string registerName = "Default";
            if (!string.IsNullOrEmpty(accountRegisterNumber))
            {
                registerName = accountRegisterNumber;
            }

            string processFileName = null;
            if (globalDocumentReferenceType == GlobalEnums.GlobalDocumentReferenceTypes.EINVOICE_SALES_INVOICE
                || globalDocumentReferenceType == GlobalEnums.GlobalDocumentReferenceTypes.EINVOICE_PURCHASE_INVOICE)
            {
                processFileName = string.Format("EInvoice_Invoice_{0}.XSLT", registerName);
            }
            else if (globalDocumentReferenceType == GlobalEnums.GlobalDocumentReferenceTypes.EINVOICE_SALES_INVOICE_ANSWER
                     || globalDocumentReferenceType == GlobalEnums.GlobalDocumentReferenceTypes.EINVOICE_PURCHASE_INVOICE_ANSWER)
            {
                processFileName = string.Format("EInvoice_InvoiceAnswer_{0}.XSLT", registerName);
            }

            string xsltFileFullPath = Path.Combine(_AccountXsltFileDirPath+"/", processFileName);

            if (!File.Exists(xsltFileFullPath))
            {
                return new KeyValuePair<bool, string>(false, string.Empty);
            }

            return new KeyValuePair<bool, string>(true, xsltFileFullPath);
        }

        public KeyValuePair<bool, string> GetMailHTMLTemplateAccount(string accountRegisterNumber, GlobalEnums.EmailOperationTypes emailOperationType)
        {
            if (string.IsNullOrEmpty(accountRegisterNumber))
            {
                throw new Exception("ACCOUNT REGISTER NUMBER IS UNKNOWN");
            }

            return GetMailHTMLTemplateBase(emailOperationType, accountRegisterNumber);
        }

        public KeyValuePair<bool, string> GetMailHTMLTemplateDefault(GlobalEnums.EmailOperationTypes emailOperationType)
        {
            return GetMailHTMLTemplateBase(emailOperationType, null);
        }

        private KeyValuePair<bool, string> GetMailHTMLTemplateBase(GlobalEnums.EmailOperationTypes emailOperationType, string accountRegisterNumber = null)
        {
            string registerName = "Default";
            if (!string.IsNullOrEmpty(accountRegisterNumber))
            {
                registerName = accountRegisterNumber;
            }

            string processFileName = null;

            switch (emailOperationType)
            {
                case GlobalEnums.EmailOperationTypes.EINVOICE_SALES_INVOICE_REPORT_SUCCESS:
                    {
                        processFileName = string.Format("EInvoice_InvoiceOperation_{0}.HTML", registerName);
                        break;
                    }
                case GlobalEnums.EmailOperationTypes.EINVOICE_SALES_INVOICE_REPORT_REJECTED:
                    {
                        processFileName = string.Format("EInvoice_AnswerOperation_{0}.HTML", registerName);
                        break;
                    }
                case GlobalEnums.EmailOperationTypes.EINVOICE_SALES_INVOICE_SINGLE_ACCOUNT:
                    {
                        processFileName = string.Format("EInvoice_InvoiceDocument_{0}.HTML", registerName);
                        break;
                    }

                case GlobalEnums.EmailOperationTypes.EINVOICE_PURCHASE_INVOICE_REPORT_SUCCESS:
                    {
                        processFileName = string.Format("EInvoice_InvoiceOperation_{0}.HTML", registerName);
                        break;
                    }
                case GlobalEnums.EmailOperationTypes.EINVOICE_PURCHASE_INVOICE_REPORT_REJECTED:
                    {
                        processFileName = string.Format("EInvoice_AnswerOperation_{0}.HTML", registerName);
                        break;
                    }
                case GlobalEnums.EmailOperationTypes.EINVOICE_PURCHASE_INVOICE_SINGLE_ACCOUNT:
                    {
                        processFileName = string.Format("EInvoice_InvoiceDocument_{0}.HTML", registerName);
                        break;
                    }

                case GlobalEnums.EmailOperationTypes.EINVOICE_SALES_INVOICE_SINGLE_CUSTOMER:
                    {
                        processFileName = string.Format("EInvoice_InvoiceDocument_{0}.HTML", registerName);
                        break;
                    }
            }

            string htmlFileFullPath = Path.Combine(_EMailTemplateFileDirPath+"/", processFileName);

            return _fmFile.GetXmlFileContent(htmlFileFullPath);
        }
    }
}