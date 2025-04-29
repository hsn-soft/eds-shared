using Eds.Shared.Helper.VeribanGlobal.Library.Common;
using Eds.Shared.Helper.VeribanGlobal.Library.FileManagement;
using Microsoft.Extensions.Configuration;

namespace Eds.Shared.Helper.eInvoice.Library.FileManagement
{
    public class DespatchFileService
    {
        private readonly string _EDespatchOutboxFileBase;
        private readonly string _EDespatchInboxFileBase;

        private readonly string _AccountXsltFileBase;
        private readonly string _EMailTemplateFileBase;

        private readonly FMFile _fmFile = null;

        public DespatchFileService()
        {
            this._EDespatchOutboxFileBase = new ConfigurationManager()["EINVOICE_DespatchOutboxFileBase"];
            this._EDespatchInboxFileBase = new ConfigurationManager()["EINVOICE_DespatchInboxFileBase"];

            this._AccountXsltFileBase = new ConfigurationManager()["GLOBAL_AccountXsltFileBase"];
            this._EMailTemplateFileBase = new ConfigurationManager()["GLOBAL_EMailTemplateFileBase"];

            _fmFile = new FMFile();
        }

        public KeyValuePair<bool, string> SaveOutboxXml(string xmlContent, string accountParameter, int Year, byte Month, byte Day, string despatchIdentifier)
        {
            if (string.IsNullOrEmpty(accountParameter)) return new KeyValuePair<bool, string>(false, "AccountParameter unknown");
            if (Year < 2000) return new KeyValuePair<bool, string>(false, "Year grather than 2000");
            if (Month < 1 || Month > 12) return new KeyValuePair<bool, string>(false, "Month must be 1-12");
            if (Day < 1 || Day > 31) return new KeyValuePair<bool, string>(false, "Day must be 1-31");
            if (string.IsNullOrEmpty(despatchIdentifier)) return new KeyValuePair<bool, string>(false, "despatchIdentifier unknown");
            if (despatchIdentifier.Length != 36) return new KeyValuePair<bool, string>(false, "despatchIdentifier must be 36 character");

            string destinationPath = string.Format("{0}\\{1}\\{2}{3}\\", accountParameter, Year.ToString(), Month.ToString().PadLeft(2, '0'), Day.ToString().PadLeft(2, '0'));
            destinationPath = _EDespatchOutboxFileBase + destinationPath;

            string xmlFileNameWithoutExtension = string.Format("{0}", despatchIdentifier);

            return _fmFile.SaveXmlFile(xmlContent, destinationPath, xmlFileNameWithoutExtension);
        }

        public KeyValuePair<bool, string> SaveInboxXml(string xmlContent, string accountParameter, int Year, byte Month, byte Day, string despatchIdentifier)
        {
            if (string.IsNullOrEmpty(accountParameter)) return new KeyValuePair<bool, string>(false, "AccountParameter unknown");
            if (Year < 2000) return new KeyValuePair<bool, string>(false, "Year grather than 2000");
            if (Month < 1 || Month > 12) return new KeyValuePair<bool, string>(false, "Month must be 1-12");
            if (Day < 1 || Day > 31) return new KeyValuePair<bool, string>(false, "Day must be 1-31");
            if (string.IsNullOrEmpty(despatchIdentifier)) return new KeyValuePair<bool, string>(false, "despatchIdentifier unknown");
            if (despatchIdentifier.Length != 36) return new KeyValuePair<bool, string>(false, "despatchIdentifier must be 36 character");

            string destinationPath = string.Format("{0}\\{1}\\{2}{3}\\", accountParameter, Year.ToString(), Month.ToString().PadLeft(2, '0'), Day.ToString().PadLeft(2, '0'));
            destinationPath = _EDespatchInboxFileBase + destinationPath;

            string xmlFileNameWithoutExtension = string.Format("{0}", despatchIdentifier);

            return _fmFile.SaveXmlFile(xmlContent, destinationPath, xmlFileNameWithoutExtension);
        }

        public KeyValuePair<bool, string> GetAccountXsltFile(string accountRegisterNumber, GlobalEnums.GlobalDocumentReferenceTypes globalDocumentReferenceType)
        {
            if (string.IsNullOrEmpty(accountRegisterNumber))
            {
                throw new Exception("ACCOUNT REGISTER NUMBER IS UNKNOWN");
            }

            return GetDespatchXsltFileContentBase(globalDocumentReferenceType, accountRegisterNumber);
        }

        public KeyValuePair<bool, string> GetDefaultXsltFile(GlobalEnums.GlobalDocumentReferenceTypes globalDocumentReferenceType)
        {
            return GetDespatchXsltFileContentBase(globalDocumentReferenceType);
        }

        private KeyValuePair<bool, string> GetDespatchXsltFileContentBase(GlobalEnums.GlobalDocumentReferenceTypes globalDocumentReferenceType, string accountRegisterNumber = null)
        {
            string registerName = "Default";
            if (!string.IsNullOrEmpty(accountRegisterNumber))
            {
                registerName = accountRegisterNumber;
            }

            string processFileName = null;
            if (globalDocumentReferenceType == GlobalEnums.GlobalDocumentReferenceTypes.EINVOICE_SALES_DESPATCHE
                || globalDocumentReferenceType == GlobalEnums.GlobalDocumentReferenceTypes.EINVOICE_PURCHASE_DESPATCHE)
            {
                processFileName = string.Format("EDespatch_DespatchAdvice_{0}.XSLT", registerName);
            }
            else if (globalDocumentReferenceType == GlobalEnums.GlobalDocumentReferenceTypes.EINVOICE_SALES_DESPATCHE_ANSWER
                     || globalDocumentReferenceType == GlobalEnums.GlobalDocumentReferenceTypes.EINVOICE_PURCHASE_DESPATCHE_ANSWER)
            {
                processFileName = string.Format("EDespatch_ReceiptAdvice_{0}.XSLT", registerName);
            }

            string xsltFileFullPath = Path.Combine(_AccountXsltFileBase, processFileName);

            return _fmFile.GetXmlFileContent(xsltFileFullPath);
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
                case GlobalEnums.EmailOperationTypes.EDESPATCH_SALES_DESPATCH_REPORT_SUCCESS:
                    {
                        processFileName = string.Format("EDespatch_DespatchOperation_{0}.HTML", registerName);
                        break;
                    }
                case GlobalEnums.EmailOperationTypes.EDESPATCH_SALES_DESPATCH_REPORT_REJECTED:
                    {
                        processFileName = string.Format("EDespatch_AnswerOperation_{0}.HTML", registerName);
                        break;
                    }
                case GlobalEnums.EmailOperationTypes.EDESPATCH_SALES_DESPATCH_SINGLE_ACCOUNT:
                    {
                        processFileName = string.Format("EDespatch_DespatchDocument_{0}.HTML", registerName);
                        break;
                    }

                case GlobalEnums.EmailOperationTypes.EDESPATCH_PURCHASE_DESPATCH_REPORT_SUCCESS:
                    {
                        processFileName = string.Format("EDespatch_DespatchOperation_{0}.HTML", registerName);
                        break;
                    }
                case GlobalEnums.EmailOperationTypes.EDESPATCH_PURCHASE_DESPATCH_REPORT_REJECTED:
                    {
                        processFileName = string.Format("EDespatch_AnswerOperation_{0}.HTML", registerName);
                        break;
                    }
                case GlobalEnums.EmailOperationTypes.EDESPATCH_PURCHASE_DESPATCH_SINGLE_ACCOUNT:
                    {
                        processFileName = string.Format("EDespatch_DespatchDocument_{0}.HTML", registerName);
                        break;
                    }

                case GlobalEnums.EmailOperationTypes.EDESPATCH_SALES_DESPATCH_SINGLE_CUSTOMER:
                    {
                        processFileName = string.Format("EDespatch_DespatchDocument_{0}.HTML", registerName);
                        break;
                    }
            }

            string htmlFileFullPath = Path.Combine(_EMailTemplateFileBase, processFileName);

            return _fmFile.GetXmlFileContent(htmlFileFullPath);
        }
    }
}