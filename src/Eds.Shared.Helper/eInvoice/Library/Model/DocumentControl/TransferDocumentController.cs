using System.Data;
using System.Globalization;
using Eds.Shared.Helper.VeribanGlobal.Library.Common;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.ConstRepository;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.GlobalTypes.EInvoice;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils.XlsEngine;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.GibEnvelope;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Serializer;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase;
using Eds.Shared.Helper.eInvoice.Library.Model.Serializer;
using Eds.Shared.Helper.eInvoice.Library.Model.Utils;


namespace Eds.Shared.Helper.eInvoice.Library.Model.DocumentControl
{
    public class TransferDocumentController
    {
        public DocumentControlResult TransferDocumentControl(string _TransferDocumentFileBase, string transferFilePath, byte transferDocumentDataType, byte globalDocumentReferenceType, int transferYear,
            Guid transferUniqueId, string controlExtension = null)
        {
            string tempExtractEnvelopeDirectory = $"{_TransferDocumentFileBase}/TempExtract/{transferUniqueId.ToString("N").ToUpper()}";
            string transferFileFullPath = $"{_TransferDocumentFileBase}/{transferFilePath}";
            DocumentControlResult documentControlResult;

            KeyValuePair<bool, string> extractResult = ZipPackage.ExtractZipFileNew(transferFileFullPath, tempExtractEnvelopeDirectory, false, controlExtension, false, transferUniqueId);
            if (extractResult.Key)
            {
                if (File.Exists(extractResult.Value))
                {
                    documentControlResult = CreateDocumentModelFromFile(extractResult.Value, transferDocumentDataType, globalDocumentReferenceType);

                    //DELETE EXTRACT FILE AND EXTRACT FILE TEMP GUID DIRECTORY
                    int counter = 10;
                    do
                    {
                        if (counter != 10) Thread.Sleep(200);

                        try
                        {
                            Directory.Delete(tempExtractEnvelopeDirectory, true);
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                        finally
                        {
                            counter++;
                        }
                    } while (Directory.Exists(tempExtractEnvelopeDirectory) && counter > 0);
                }
                else
                {
                    documentControlResult = new DocumentControlResult()
                    {
                        ResultStatus = false,
                        ResultErrorState = (byte)GlobalEnums.NewTransferQueueProcessState.ErrorDocumentPrepared,
                        ResultErrorStateDesc = string.Format("{0}:{1}", "ZIP EXTRACT ERROR", "MODEL DOSYASI BULUNAMADI"),
                        ResultDocumentReferenceNumber = null,
                    };
                }
            }
            else
            {
                documentControlResult = new DocumentControlResult()
                {
                    ResultStatus = false,
                    ResultErrorState = (byte)GlobalEnums.NewTransferQueueProcessState.ErrorDocumentPrepared,
                    ResultErrorStateDesc = string.Format("{0}:{1}", "ZIP EXTRACT ERROR", extractResult.Value),
                    ResultDocumentReferenceNumber = null,
                };
            }

            return documentControlResult;
        }

        private DocumentControlResult CreateDocumentModelFromFile(string modelFileFullPath, byte modelDocumentDataType, byte globalDocumentReferenceType)
        {
            bool modelCreateSuccess = false;
            string modelCreateErrorMessage = string.Empty;
            List<object> transferDocumentDataModelObjectList = null;
            object transferDocumentDataHeaderInfoObject = null;
            string documentReferenceNumber = null;

            try
            {
                switch (modelDocumentDataType)
                {
                    case (byte)GlobalEnums.TransferDocumentDataTypes.XML_UBLTR_INZIP:
                    case (byte)GlobalEnums.TransferDocumentDataTypes.XML_SAP_INZIP:
                        {
                            KeyValuePair<bool, List<string>> controlData = new KeyValuePair<bool, List<string>>(false, null);
                            List<object> tmpTransferDocumentDataModelList = null;
                            object tmpTransferDocumentDataHeaderInfoObject = null;
                            switch (globalDocumentReferenceType)
                            {
                                case (byte)GlobalEnums.GlobalDocumentReferenceTypes.EINVOICE_SALES_INVOICE:
                                    {
                                        string xmlContent = null;
                                        if (modelDocumentDataType == (byte)GlobalEnums.TransferDocumentDataTypes.XML_SAP_INZIP)
                                        {
                                            //SAP XML CONVERT TO INVOICE UBL-TR XML
                                            if (!SAPDocumentInvoiceToUBLTRXmlContent(File.ReadAllText(modelFileFullPath), out xmlContent))
                                            {
                                                throw new Exception("SAP CONVERT ERROR:" + xmlContent);
                                            }
                                        }
                                        else
                                        {
                                            xmlContent = File.ReadAllText(modelFileFullPath);
                                        }

                                        TransferInvoiceDataSerializer transferInvoiceDataSerializer = new TransferInvoiceDataSerializer();
                                        InvoiceModel dataModel = transferInvoiceDataSerializer.DeserializeFromXmlContent(xmlContent, true);

                                        //sahadan gelen eski versiyonları yeni versiyona ceviriyoruz!
                                        UblTr2HandlerEInvoice.SetHandleInvoice(ref dataModel);

                                        documentReferenceNumber = dataModel != null && !string.IsNullOrEmpty(dataModel.ID) ? dataModel.ID : null;
                                        tmpTransferDocumentDataModelList = new List<object>() { dataModel };

                                        //DESERIALIZE MANUEL KONTROL
                                        controlData = transferInvoiceDataSerializer.TransferInvoiceDataModelControl(tmpTransferDocumentDataModelList[0] as InvoiceModel);
                                        break;
                                    }
                                case (byte)GlobalEnums.GlobalDocumentReferenceTypes.EINVOICE_SALES_INVOICE_ENVELOPE:
                                    {
                                        var envelopeHeaderInfo = VeribanGlobal.Library.Model.Utils.GibEnvelopeUtils.HandleGIBEnvelopeXmlDocument(File.ReadAllText(modelFileFullPath), false, true);
                                        if (envelopeHeaderInfo != null)
                                        {
                                            if (envelopeHeaderInfo.ElementObjectList != null && envelopeHeaderInfo.ElementObjectList.Count > 0)
                                            {
                                                tmpTransferDocumentDataHeaderInfoObject = envelopeHeaderInfo;

                                                TransferInvoiceDataSerializer transferInvoiceDataSerializer = new TransferInvoiceDataSerializer();
                                                tmpTransferDocumentDataModelList = new List<object>();
                                                foreach (GibEnvelopeElementDocInfo docInfo in envelopeHeaderInfo.ElementObjectList)
                                                {
                                                    InvoiceModel dataModel = transferInvoiceDataSerializer.DeserializeFromXmlContent(docInfo.DocumentXmlContent, true);
                                                    documentReferenceNumber = dataModel != null && !string.IsNullOrEmpty(dataModel.ID) ? dataModel.ID : null;
                                                    tmpTransferDocumentDataModelList.Add(dataModel);

                                                    //DESERIALIZE MANUEL KONTROL
                                                    controlData = transferInvoiceDataSerializer.TransferInvoiceDataModelControl(dataModel);
                                                    if (!controlData.Key)
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                            else controlData = new KeyValuePair<bool, List<string>>(false, new List<string>() { "SalesInvoiceEnvelope Handle Error" });
                                        }
                                        else controlData = new KeyValuePair<bool, List<string>>(false, new List<string>() { "SalesInvoiceEnvelope Handle Error" });

                                        break;
                                    }
                                case (byte)GlobalEnums.GlobalDocumentReferenceTypes.EINVOICE_PURCHASE_INVOICE_ANSWER:
                                    {
                                        TransferApplicationResponseDataSerializer transferApplicationResponseDataSerializer = new TransferApplicationResponseDataSerializer();
                                        ApplicationResponseModel dataModel = transferApplicationResponseDataSerializer.DeserializeFromXmlFile(modelFileFullPath, true);
                                        documentReferenceNumber = dataModel != null && !string.IsNullOrEmpty(dataModel.ID) ? dataModel.ID : null;
                                        tmpTransferDocumentDataModelList = new List<object>() { dataModel };

                                        //DESERIALIZE MANUEL KONTROL
                                        controlData = transferApplicationResponseDataSerializer.TransferApplicationResponseDataModelControl(tmpTransferDocumentDataModelList[0] as ApplicationResponseModel);
                                        break;
                                    }
                                case (byte)GlobalEnums.GlobalDocumentReferenceTypes.EINVOICE_PURCHASE_INVOICE_ANSWER_ENVELOPE:
                                    {
                                        var envelopeHeaderInfo = VeribanGlobal.Library.Model.Utils.GibEnvelopeUtils.HandleGIBEnvelopeXmlDocument(File.ReadAllText(modelFileFullPath), false, true);
                                        if (envelopeHeaderInfo != null)
                                        {
                                            if (envelopeHeaderInfo.ElementObjectList != null && envelopeHeaderInfo.ElementObjectList.Count > 0)
                                            {
                                                tmpTransferDocumentDataHeaderInfoObject = envelopeHeaderInfo;

                                                TransferApplicationResponseDataSerializer transferApplicationResponseDataSerializer = new TransferApplicationResponseDataSerializer();
                                                tmpTransferDocumentDataModelList = new List<object>();
                                                foreach (GibEnvelopeElementInvoiceAnswerInfo docInfo in envelopeHeaderInfo.ElementObjectList)
                                                {
                                                    ApplicationResponseModel dataModel = transferApplicationResponseDataSerializer.DeserializeFromXmlContent(docInfo.AnswerDocumentXmlContent, true);
                                                    documentReferenceNumber = dataModel != null && !string.IsNullOrEmpty(dataModel.ID) ? dataModel.ID : null;
                                                    tmpTransferDocumentDataModelList.Add(dataModel);

                                                    //DESERIALIZE MANUEL KONTROL
                                                    controlData = transferApplicationResponseDataSerializer.TransferApplicationResponseDataModelControl(dataModel);
                                                    if (!controlData.Key)
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                            else controlData = new KeyValuePair<bool, List<string>>(false, new List<string>() { "SalesInvoiceAnswerEnvelope Handle Error" });
                                        }
                                        else controlData = new KeyValuePair<bool, List<string>>(false, new List<string>() { "SalesInvoiceAnswerEnvelope Handle Error" });

                                        break;
                                    }
                                case (byte)GlobalEnums.GlobalDocumentReferenceTypes.EINVOICE_SALES_DESPATCHE:
                                    {
                                        string xmlContent = null;
                                        if (modelDocumentDataType == (byte)GlobalEnums.TransferDocumentDataTypes.XML_SAP_INZIP)
                                        {
                                            //SAP XML CONVERT TO DESPATCH UBL-TR XML
                                            if (!SAPDocumentToDespatchUBLTRXmlContent(File.ReadAllText(modelFileFullPath), out xmlContent))
                                            {
                                                throw new Exception("SAP CONVERT ERROR:" + xmlContent);
                                            }
                                        }
                                        else
                                        {
                                            xmlContent = File.ReadAllText(modelFileFullPath);
                                        }

                                        TransferDespatchAdviceDataSerializer transferDespatchAdviceDataSerializer = new TransferDespatchAdviceDataSerializer();
                                        DespatchAdviceModel dataModel = transferDespatchAdviceDataSerializer.DeserializeFromXmlContent(xmlContent, true);
                                        documentReferenceNumber = dataModel != null && !string.IsNullOrEmpty(dataModel.ID) ? dataModel.ID : null;
                                        tmpTransferDocumentDataModelList = new List<object>() { dataModel };

                                        //DESERIALIZE MANUEL KONTROL
                                        controlData = transferDespatchAdviceDataSerializer.TransferDespatchAdviceDataModelControl(tmpTransferDocumentDataModelList[0] as DespatchAdviceModel);
                                        break;
                                    }
                                case (byte)GlobalEnums.GlobalDocumentReferenceTypes.EINVOICE_SALES_DESPATCHE_ENVELOPE:
                                    {
                                        var envelopeHeaderInfo = VeribanGlobal.Library.Model.Utils.GibEnvelopeUtils.HandleGIBEnvelopeXmlDocument(File.ReadAllText(modelFileFullPath), false, true);
                                        if (envelopeHeaderInfo != null)
                                        {
                                            if (envelopeHeaderInfo.ElementObjectList != null && envelopeHeaderInfo.ElementObjectList.Count > 0)
                                            {
                                                tmpTransferDocumentDataHeaderInfoObject = envelopeHeaderInfo;

                                                TransferDespatchAdviceDataSerializer transferDespatchAdviceDataSerializer = new TransferDespatchAdviceDataSerializer();
                                                tmpTransferDocumentDataModelList = new List<object>();
                                                foreach (GibEnvelopeElementDocInfo docInfo in envelopeHeaderInfo.ElementObjectList)
                                                {
                                                    DespatchAdviceModel dataModel = transferDespatchAdviceDataSerializer.DeserializeFromXmlContent(docInfo.DocumentXmlContent, true);
                                                    documentReferenceNumber = dataModel != null && !string.IsNullOrEmpty(dataModel.ID) ? dataModel.ID : null;
                                                    tmpTransferDocumentDataModelList.Add(dataModel);

                                                    //DESERIALIZE MANUEL KONTROL
                                                    controlData = transferDespatchAdviceDataSerializer.TransferDespatchAdviceDataModelControl(dataModel);
                                                    if (!controlData.Key)
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                            else controlData = new KeyValuePair<bool, List<string>>(false, new List<string>() { "SalesDespatchEnvelope Handle Error" });
                                        }
                                        else controlData = new KeyValuePair<bool, List<string>>(false, new List<string>() { "SalesDespatchEnvelope Handle Error" });

                                        break;
                                    }
                                case (byte)GlobalEnums.GlobalDocumentReferenceTypes.EINVOICE_PURCHASE_DESPATCHE_ANSWER:
                                    {
                                        TransferReceiptAdviceDataSerializer transferReceiptAdviceDataSerializer = new TransferReceiptAdviceDataSerializer();
                                        ReceiptAdviceModel dataModel = transferReceiptAdviceDataSerializer.DeserializeFromXmlFile(modelFileFullPath, true);
                                        documentReferenceNumber = dataModel != null && !string.IsNullOrEmpty(dataModel.ID) ? dataModel.ID : null;
                                        tmpTransferDocumentDataModelList = new List<object>() { dataModel };

                                        //DESERIALIZE MANUEL KONTROL
                                        controlData = transferReceiptAdviceDataSerializer.TransferReceiptAdviceDataModelControl(tmpTransferDocumentDataModelList[0] as ReceiptAdviceModel);
                                        break;
                                    }
                                case (byte)GlobalEnums.GlobalDocumentReferenceTypes.EINVOICE_PURCHASE_DESPATCHE_ANSWER_ENVELOPE:
                                    {
                                        var envelopeHeaderInfo = VeribanGlobal.Library.Model.Utils.GibEnvelopeUtils.HandleGIBEnvelopeXmlDocument(File.ReadAllText(modelFileFullPath), false, true);
                                        if (envelopeHeaderInfo != null)
                                        {
                                            if (envelopeHeaderInfo.ElementObjectList != null && envelopeHeaderInfo.ElementObjectList.Count > 0)
                                            {
                                                tmpTransferDocumentDataHeaderInfoObject = envelopeHeaderInfo;

                                                TransferReceiptAdviceDataSerializer transferReceiptAdviceDataSerializer = new TransferReceiptAdviceDataSerializer();
                                                tmpTransferDocumentDataModelList = new List<object>();
                                                foreach (GibEnvelopeElementDespatchAnswerInfo docInfo in envelopeHeaderInfo.ElementObjectList)
                                                {
                                                    ReceiptAdviceModel dataModel = transferReceiptAdviceDataSerializer.DeserializeFromXmlContent(docInfo.DespatchAnswerDocumentXmlContent, true);
                                                    documentReferenceNumber = dataModel != null && !string.IsNullOrEmpty(dataModel.ID) ? dataModel.ID : null;
                                                    tmpTransferDocumentDataModelList.Add(dataModel);

                                                    //DESERIALIZE MANUEL KONTROL
                                                    controlData = transferReceiptAdviceDataSerializer.TransferReceiptAdviceDataModelControl(dataModel);
                                                    if (!controlData.Key)
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                            else controlData = new KeyValuePair<bool, List<string>>(false, new List<string>() { "SalesDespatchAnswerEnvelope Handle Error" });
                                        }
                                        else controlData = new KeyValuePair<bool, List<string>>(false, new List<string>() { "SalesDespatchAnswerEnvelope Handle Error" });

                                        break;
                                    }
                                case (byte)GlobalEnums.GlobalDocumentReferenceTypes.EINVOICE_SALES_INVOICE_TAXFREE_REJECTION:
                                    {
                                        TransferCreditNoteDataSerializer transferCreditNoteDataSerializer = new TransferCreditNoteDataSerializer();
                                        CreditNoteModel dataModel = transferCreditNoteDataSerializer.DeserializeFromXmlFile(modelFileFullPath, true);
                                        documentReferenceNumber = dataModel != null && !string.IsNullOrEmpty(dataModel.ID) ? dataModel.ID : null;
                                        tmpTransferDocumentDataModelList = new List<object>() { dataModel };

                                        //DESERIALIZE MANUEL KONTROL
                                        controlData = transferCreditNoteDataSerializer.TransferCreditNoteDataControl(tmpTransferDocumentDataModelList[0] as CreditNoteModel);
                                        break;
                                    }
                                default:
                                    {
                                        throw new Exception("UNKNOWN GLOBAL REFERENCE DOCUMENT TYPE");
                                    }
                            }

                            if (!controlData.Key)
                            {
                                throw new Exception("TRANSFERDATA SCHEME CONTROL FAIL : " + new BaseSerializer().ControlledDataHandle(controlData.Value));
                            }
                            else
                            {
                                transferDocumentDataHeaderInfoObject = tmpTransferDocumentDataHeaderInfoObject;
                                transferDocumentDataModelObjectList = tmpTransferDocumentDataModelList;

                                modelCreateSuccess = true;
                            }

                            break;
                        }
                    case (byte)GlobalEnums.TransferDocumentDataTypes.XLS_INZIP:
                        {
                            KeyValuePair<bool, List<string>> controlData;
                            object tmpTransferDocumentDataModel = null;
                            switch (globalDocumentReferenceType)
                            {
                                case (byte)GlobalEnums.GlobalDocumentReferenceTypes.EINVOICE_SALES_INVOICE:
                                    {
                                        string xmlContent = null;

                                        //EXCEL DOCUMENT CONVERT TO UBL-TR XML
                                        if (!XlsDocumentToUBLTRXmlContent(modelFileFullPath, out xmlContent))
                                        {
                                            throw new Exception("XLS CONVERT ERROR:" + xmlContent);
                                        }

                                        TransferInvoiceDataSerializer transferInvoiceDataSerializer = new TransferInvoiceDataSerializer();
                                        InvoiceModel dataModel = transferInvoiceDataSerializer.DeserializeFromXmlContent(xmlContent, true);
                                        documentReferenceNumber = dataModel != null && !string.IsNullOrEmpty(dataModel.ID) ? dataModel.ID : null;
                                        tmpTransferDocumentDataModel = dataModel;

                                        //DESERIALIZE MANUEL KONTROL
                                        controlData = transferInvoiceDataSerializer.TransferInvoiceDataModelControl(tmpTransferDocumentDataModel as InvoiceModel);
                                        break;
                                    }
                                default:
                                    {
                                        throw new Exception("UNKNOWN GLOBAL REFERENCE DOCUMENT TYPE");
                                    }
                            }

                            if (!controlData.Key)
                            {
                                throw new Exception("TRANSFERDATA SCHEME CONTROL FAIL : " + new BaseSerializer().ControlledDataHandle(controlData.Value));
                            }
                            else
                            {
                                transferDocumentDataModelObjectList = new List<object>();

                                transferDocumentDataModelObjectList.Add(tmpTransferDocumentDataModel);

                                modelCreateSuccess = true;
                            }

                            break;
                        }
                    default:
                        {
                            modelCreateSuccess = false;
                            modelCreateErrorMessage = "MODEL TİPİ DESTEKLENMİYOR";
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                modelCreateSuccess = false;
                modelCreateErrorMessage = "MODEL OLUŞTURULAMADI : " + ex.Message;
            }

            if (modelCreateSuccess)
            {
                return new DocumentControlResult()
                {
                    ResultStatus = true,
                    ResultErrorState = 0,
                    ResultErrorStateDesc = "Model listesi oluşturuldu",
                    TransferDataHeaderInfo = transferDocumentDataHeaderInfoObject,
                    TransferDataModelList = transferDocumentDataModelObjectList,
                    ResultDocumentReferenceNumber = documentReferenceNumber,
                };
            }
            else
            {
                return new DocumentControlResult()
                {
                    ResultStatus = false,
                    ResultErrorState = (byte)GlobalEnums.NewTransferQueueProcessState.ErrorDocumentPrepared,
                    ResultErrorStateDesc = string.Format("{0}:{1}", "MODEL CREATE ERROR", modelCreateErrorMessage),
                    TransferDataHeaderInfo = null,
                    TransferDataModelList = null,
                    ResultDocumentReferenceNumber = documentReferenceNumber,
                };
            }
        }

        private bool XlsDocumentToUBLTRXmlContent(string fileFullPath, out string convertedXmlContent)
        {
            convertedXmlContent = null;

            try
            {
                InvoiceModel xlsInvoice = new InvoiceModel();

                XlsFile xlsFile = new XlsFile(fileFullPath);
                if (xlsFile != null && xlsFile.WorkbookData != null && xlsFile.WorkbookData.Tables != null)
                {
                    if (xlsFile.WorkbookData.Tables.Count == 1 && string.Equals(xlsFile.WorkbookData.Tables[0].TableName, "UBL-TR Invoice"))
                    {
                        #region UBL-TR Invoice

                        //Excel satır sayısı artığında. Bu değeri değiştirmek yeterli.
                        int xlsTotalLineNumberCount = 300;

                        //Mal Hizmet Toplam Tutar
                        int extensionAmountLineNumber = xlsTotalLineNumberCount + 26;

                        //Toplam İskonto
                        int allowanceAmountLineNumber = xlsTotalLineNumberCount + 27;

                        //Not
                        //var noteLineNumber = xlsTotalLineNumberCount + 26

                        //Hesaplanan Kdv Tutarı
                        int totalTaxLineNumber = xlsTotalLineNumberCount + 28;

                        //Vergiler Dahil Toplam Tutar
                        int totalAmountWithTaxLineNumber = xlsTotalLineNumberCount + 29;

                        //Ödenecek Tutar
                        int payableAmountLineNumber = xlsTotalLineNumberCount + 30;


                        DataTable dt = xlsFile.WorkbookData.Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            //DEFAULT SET CONSTRUCTOR
                            //xlsInvoice.UBLVersionID = "2.1"
                            //xlsInvoice.CopyIndicator = false
                            //xlsInvoice.CustomizationID = "TR1.2"

                            #region Senaryo

                            if (string.IsNullOrEmpty(dt.Rows[13][23].ToString()))
                            {
                                throw new Exception("Senaryo Alanı Alınamadı! TEMELFATURA veya TICARIFATURA olmalı!");
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[13][23].ToString()))
                            {
                                if (string.Equals(dt.Rows[13][23].ToString().Trim(), ProfileIdType.TEMELFATURA.ToString()))
                                    xlsInvoice.ProfileID = ProfileIdType.TEMELFATURA.ToString();
                                else if (string.Equals(dt.Rows[13][23].ToString().Trim(), ProfileIdType.TICARIFATURA.ToString()))
                                    xlsInvoice.ProfileID = ProfileIdType.TICARIFATURA.ToString();
                                else
                                    throw new Exception("Senaryo Alanı Geçersiz, TEMELFATURA veya TICARIFATURA olmalı!");
                            }

                            #endregion

                            #region Fatura Tipi

                            if (string.IsNullOrEmpty(dt.Rows[14][23].ToString()))
                            {
                                throw new Exception("Fatura Tipi Alanı Alınamadı! SATIS veya IADE olmalı!");
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[14][23].ToString()))
                            {
                                if (string.Equals(dt.Rows[14][23].ToString().Trim(), InvoiceTypeCode.SATIS.ToString()))
                                    xlsInvoice.InvoiceTypeCode = InvoiceTypeCode.SATIS.ToString();
                                else if (string.Equals(dt.Rows[14][23].ToString().Trim(), InvoiceTypeCode.IADE.ToString()))
                                    xlsInvoice.InvoiceTypeCode = InvoiceTypeCode.IADE.ToString();
                                else
                                {
                                    var TypeCode = InvoiceTypeCode.TYPE_LIST.FirstOrDefault(ITC => ITC.GetName() == dt.Rows[14][23].ToString().Trim());
                                    if (TypeCode != null)
                                    {
                                        xlsInvoice.InvoiceTypeCode = TypeCode.ToString();
                                    }
                                    else
                                    {
                                        throw new Exception("Fatura Tipi Alanı Geçersiz! SATIS veya IADE olmalı!");
                                    }
                                }
                            }

                            #endregion

                            #region Fatura No

                            if (string.IsNullOrEmpty(dt.Rows[15][23].ToString()))
                            {
                                throw new Exception("Fatura No Alınamadı!");
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[15][23].ToString()) && dt.Rows[15][23].ToString().Length == 16)
                                xlsInvoice.ID = dt.Rows[15][23].ToString();
                            else
                                throw new Exception("Fatura No Geçersiz! 16 karakter olmalı!");

                            #endregion

                            #region Fatura Tarih

                            if (string.IsNullOrEmpty(dt.Rows[16][23].ToString()))
                            {
                                throw new Exception("Fatura Tarihi Alınamadı!");
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[16][23].ToString()) && dt.Rows[16][23].ToString().Length == 10)
                            {
                                string datetimeStr = dt.Rows[16][23].ToString();
                                try
                                {
                                    DateTime issueDateInv = new DateTime(SafeVar.GetInt(datetimeStr.Substring(6, 4)), SafeVar.GetInt(datetimeStr.Substring(3, 2)), SafeVar.GetInt(datetimeStr.Substring(0, 2)));

                                    if (issueDateInv != null)
                                    {
                                        xlsInvoice.IssueDate = issueDateInv.ToString(DateFormats.DateTimeGIBFormatShort);
                                        xlsInvoice.IssueTime = DateTime.Now.ToLongTimeString();
                                    }
                                }
                                catch (Exception)
                                {
                                    throw new Exception("Fatura Tarihi Geçersiz! GG-AA-YYYY formatında olmalı!");
                                }
                            }
                            else
                            {
                                throw new Exception("Fatura Tarihi Geçersiz! GG-AA-YYYY formatında olmalı!");
                            }

                            #endregion

                            #region Irsaliye Bilgileri

                            if (!string.IsNullOrEmpty(dt.Rows[17][23].ToString()) && !string.IsNullOrEmpty(dt.Rows[18][23].ToString()) && dt.Rows[18][23].ToString().Length == 10)
                            {
                                string datetimeStr = dt.Rows[18][23].ToString();
                                DateTime issueDateDespatch = new DateTime(SafeVar.GetInt(datetimeStr.Substring(6, 4)), SafeVar.GetInt(datetimeStr.Substring(3, 2)), SafeVar.GetInt(datetimeStr.Substring(0, 2)));
                                if (issueDateDespatch != null)
                                {
                                    xlsInvoice.DespatchDocumentReferences = new List<DocumentReference>()
                                    {
                                        new DocumentReference() { ID = new CombineId() { Id = dt.Rows[17][23].ToString() }, IssueDate = issueDateDespatch.ToString(DateFormats.DateTimeGIBFormatShort) }
                                    };
                                }
                            }

                            #endregion

                            #region Sipariş Bilgileri

                            if (!string.IsNullOrEmpty(dt.Rows[19][23].ToString()) && !string.IsNullOrEmpty(dt.Rows[20][23].ToString()) && dt.Rows[20][23].ToString().Length == 10)
                            {
                                string datetimeStr = dt.Rows[20][23].ToString();
                                DateTime issueDateOrder = new DateTime(SafeVar.GetInt(datetimeStr.Substring(6, 4)), SafeVar.GetInt(datetimeStr.Substring(3, 2)), SafeVar.GetInt(datetimeStr.Substring(0, 2)));
                                if (issueDateOrder != null)
                                {
                                    xlsInvoice.OrderReference = new OrderReference() { ID = new CombineId() { Id = dt.Rows[19][23].ToString() }, IssueDate = issueDateOrder.ToString(DateFormats.DateTimeGIBFormatShort) };
                                }
                            }

                            #endregion

                            #region Kur Bilgileri

                            string excelCurrency = UblTr2HandlerEInvoice.TryCurrency;

                            if (!string.IsNullOrEmpty(dt.Rows[21][23].ToString()) && !string.IsNullOrEmpty(dt.Rows[22][23].ToString()))
                            {
                                excelCurrency = dt.Rows[21][23].ToString().Trim();

                                if (!CurrencyCode.GetCurrencyCodeList().Contains(excelCurrency))
                                {
                                    throw new Exception("Para Birimi Geçersiz!");
                                }

                                xlsInvoice.PricingExchangeRate = new ExchangeRate()
                                {
                                    SourceCurrencyCode = excelCurrency, TargetCurrencyCode = "TRY", CalculationRate = Math.Round(Convert.ToDouble(dt.Rows[22][23].ToString().Replace(".", ",")), 4)
                                };
                            }

                            xlsInvoice.DocumentCurrencyCode = new DocumentCurrencyCode() { Name = excelCurrency };

                            #endregion

                            #region ETTN

                            try
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[23][3].ToString()))
                                    xlsInvoice.UUID = Guid.Parse(dt.Rows[23][3].ToString()).ToString();
                                else
                                    xlsInvoice.UUID = Guid.NewGuid().ToString();
                            }
                            catch (Exception)
                            {
                                xlsInvoice.UUID = Guid.NewGuid().ToString();
                            }

                            #endregion

                            #region Gonderici Bilgileri

                            if (string.IsNullOrEmpty(dt.Rows[8][9].ToString()))
                            {
                                throw new Exception("Gönderen Vergi No Alınamadı!");
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[8][9].ToString()))
                            {
                                string accountRegisterNumber = dt.Rows[8][9].ToString().Trim();
                                string accountSchemaId = "VKN";

                                if (accountRegisterNumber.Length == 11)
                                    accountSchemaId = "TCKN";

                                xlsInvoice.AccountingSupplierParty = new SupplierParty() { Party = GetPartyData(false, accountRegisterNumber, accountSchemaId, dt) };
                            }

                            #endregion

                            #region Alıcı Bilgileri

                            if (string.IsNullOrEmpty(dt.Rows[19][9].ToString()))
                            {
                                throw new Exception("Alıcı Vergi No Alınamadı!");
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[19][9].ToString()))
                            {
                                string accountRegisterNumber = dt.Rows[19][9].ToString().Trim();
                                string accountSchemaId = "VKN";

                                if (accountRegisterNumber.Length == 11)
                                {
                                    accountSchemaId = "TCKN";

                                    xlsInvoice.AccountingCustomerParty = new CustomerParty() { Party = GetPartyData(true, accountRegisterNumber, accountSchemaId, dt) };
                                }

                                else
                                {
                                    xlsInvoice.AccountingCustomerParty = new CustomerParty() { Party = GetPartyData(true, accountRegisterNumber, accountSchemaId, dt) };
                                }
                            }

                            #endregion

                            xlsInvoice.LineCountNumeric = 0;

                            #region Fatura Satır Kontrol

                            for (int i = 26; i < 26 + xlsTotalLineNumberCount; i++)
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[i][2].ToString()))
                                    xlsInvoice.LineCountNumeric = xlsInvoice.LineCountNumeric + 1;
                                else
                                    break;
                            }

                            if (xlsInvoice.LineCountNumeric <= 0)
                            {
                                throw new Exception("Fatura Kalemi Girmelisiniz!");
                            }

                            #endregion


                            if (xlsInvoice.LineCountNumeric > 0)
                            {
                                #region Fatura Kalemleri

                                xlsInvoice.InvoiceLines = new List<InvoiceLine>();

                                for (int i = 26; i < 26 + xlsInvoice.LineCountNumeric; i++)
                                {
                                    if (!string.IsNullOrEmpty(dt.Rows[i][2].ToString()))
                                    {
                                        DataRow drInvoiceLine = dt.Rows[i];
                                        int rowNo = i - 25;

                                        #region Kontrol

                                        if (string.IsNullOrEmpty(drInvoiceLine[8].ToString()))
                                        {
                                            throw new Exception(string.Format("Miktar 0 dan büyük olmalıdır! , Kalem[{0}]", rowNo));
                                        }

                                        if (string.IsNullOrEmpty(drInvoiceLine[10].ToString()))
                                        {
                                            throw new Exception(string.Format("Birim boş olamaz! , Kalem[{0}]", rowNo));
                                        }

                                        if (string.IsNullOrEmpty(drInvoiceLine[12].ToString()))
                                        {
                                            throw new Exception(string.Format("Birim Fiyat boş olamaz! , Kalem[{0}]", rowNo));
                                        }

                                        #endregion

                                        string invoiceLineUnitStr = SafeVar.GetString(drInvoiceLine[10]).ToLower(new CultureInfo("tr-TR"));
                                        string invoiceLineUnitCode = string.Empty;
                                        if (invoiceLineUnitStr.Equals("adet"))
                                        {
                                            invoiceLineUnitCode = "NIU";
                                        }
                                        else
                                        {
                                            var code = VeribanGlobal.Library.Common.ConstRepository.UnitType.GetUnitTypeList().FirstOrDefault(o => o.Name.ToLower(new CultureInfo("tr-TR")) == invoiceLineUnitStr);
                                            invoiceLineUnitCode = code != null ? code.Code : null;
                                        }

                                        InvoiceLine invoiceLine = new InvoiceLine()
                                        {
                                            ID = new CombineId() { Id = drInvoiceLine[1].ToString() },
                                            Item = new Item() { Name = string.IsNullOrEmpty(drInvoiceLine[2].ToString()) ? null : drInvoiceLine[2].ToString() },
                                            InvoicedQuantity = new BaseUnit() { Value = SafeVar.GetInt(drInvoiceLine[8].ToString(), true), UnitCode = invoiceLineUnitCode },
                                            Price = new Price()
                                            {
                                                PriceAmount = new UblBaseCurrency()
                                                {
                                                    Value = Convert.ToDecimal(drInvoiceLine[12].ToString().Replace(".", ",")),
                                                    CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                                }
                                            },

                                            //Mal/hizmet miktarı ile Mal/hizmet birim fiyatının çarpımı ile bulunan tutardır
                                            LineExtensionAmount = new UblBaseCurrency()
                                            {
                                                Value = Convert.ToDecimal(drInvoiceLine[19].ToString().Replace(".", ",")),
                                                CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                            },
                                            AllowanceCharges = new List<AllowanceCharge>()
                                            {
                                                new AllowanceCharge()
                                                {
                                                    ChargeIndicator = false, //indirim
                                                    MultiplierFactorNumeric = drInvoiceLine[14].ToString().Replace(",", "."), //oran
                                                    Amount = new UblBaseCurrency()
                                                    {
                                                        Value = Convert.ToDecimal(drInvoiceLine[16].ToString().Replace(".", ",")),
                                                        CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                                    }, //iskonto miktarı
                                                    BaseAmount = new UblBaseCurrency()
                                                    {
                                                        Value = Convert.ToDecimal(drInvoiceLine[29].ToString().Replace(".", ",")),
                                                        CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                                    } //iskonto hesaplanan miktar
                                                }
                                            },
                                            TaxTotal = new TaxTotal()
                                            {
                                                //Kalem için hesaplanan tüm vergi tiplerinin toplam vergi tutarı girilir.//DEFAULT 1 VERGI TIPI VAR O YÜZDEN DIREK O VERGI TIPININ TOTALI
                                                TaxAmount = new UblBaseCurrency()
                                                {
                                                    Value = Convert.ToDecimal(drInvoiceLine[24].ToString().Replace(".", ",")),
                                                    CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                                },
                                                TaxSubtotals = new List<TaxSubtotal>()
                                                {
                                                    new TaxSubtotal()
                                                    {
                                                        //Verginin üzerinden hesaplandığı tutar (matrah-iskonto düşülmüş kalem tutarı) bilgisi girilecektir.
                                                        TaxableAmount = new UblBaseCurrency()
                                                        {
                                                            Value = Convert.ToDecimal(drInvoiceLine[19].ToString().Replace(".", ",")),
                                                            CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                                        },
                                                        //kalem için hesaplanan bu vergi türünün miktarı
                                                        TaxAmount = new UblBaseCurrency()
                                                        {
                                                            Value = Convert.ToDecimal(drInvoiceLine[24].ToString().Replace(".", ",")),
                                                            CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                                        },
                                                        //bu vergi türünün diğer vergi türleri arasındaki hesaplama sırası
                                                        CalculationSequenceNumeric = 1, //DEFAULT 1 VERGI TIPI VAR O YÜZDEN 1
                                                        //vergi yüzdesi
                                                        Percent = Convert.ToDecimal(drInvoiceLine[22].ToString().Replace(".", ",")) * 100,
                                                        //vergi tipi bilgisi
                                                        TaxCategory = new TaxCategory() { TaxScheme = new TaxScheme() { Name = "KDV", TaxTypeCode = "0015" } }
                                                    }
                                                }
                                            },
                                        };

                                        xlsInvoice.InvoiceLines.Add(invoiceLine);
                                    }
                                }

                                #endregion

                                #region TOPLAM ISKONTO

                                if (!string.IsNullOrEmpty(dt.Rows[allowanceAmountLineNumber][24].ToString()))
                                {
                                    xlsInvoice.AllowanceCharges = new List<AllowanceCharge>()
                                    {
                                        new AllowanceCharge()
                                        {
                                            ChargeIndicator = false,
                                            Amount = new UblBaseCurrency()
                                            {
                                                Value = Convert.ToDecimal(dt.Rows[allowanceAmountLineNumber][24].ToString().Replace(".", ",")),
                                                CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                            }
                                        }
                                    };
                                }

                                #endregion

                                #region TOPLAM VERGILER

                                if (!string.IsNullOrEmpty(dt.Rows[totalTaxLineNumber][24].ToString()))
                                {
                                    List<TaxSubtotal> taxSubTotals = new List<TaxSubtotal>();

                                    List<decimal> percentList = new List<decimal>();
                                    foreach (InvoiceLine line in xlsInvoice.InvoiceLines)
                                    {
                                        if (!percentList.Contains(line.TaxTotal.TaxSubtotals[0].Percent.Value))
                                            percentList.Add(line.TaxTotal.TaxSubtotals[0].Percent.Value);
                                    }

                                    for (int i = 0; i < percentList.Count; i++)
                                    {
                                        decimal currentTaxPercent = percentList[i];
                                        decimal totalTaxableAmount = 0;
                                        decimal totalTaxAmount = 0;

                                        foreach (InvoiceLine line in xlsInvoice.InvoiceLines)
                                        {
                                            if (currentTaxPercent == line.TaxTotal.TaxSubtotals[0].Percent.Value)
                                            {
                                                decimal currentTotalTaxableAmount = line.TaxTotal.TaxSubtotals[0].TaxableAmount.Value;
                                                decimal currentTotalTaxAmount = line.TaxTotal.TaxSubtotals[0].TaxAmount.Value;

                                                totalTaxableAmount = totalTaxableAmount + currentTotalTaxableAmount;
                                                totalTaxAmount = totalTaxAmount + currentTotalTaxAmount;
                                            }
                                        }

                                        TaxSubtotal taxSubTotalItem = new TaxSubtotal();
                                        taxSubTotalItem.CalculationSequenceNumeric = i + 1;
                                        taxSubTotalItem.Percent = currentTaxPercent;
                                        taxSubTotalItem.TaxableAmount = new UblBaseCurrency()
                                        {
                                            Value = totalTaxableAmount, CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                        };
                                        taxSubTotalItem.TaxAmount = new UblBaseCurrency()
                                        {
                                            Value = totalTaxAmount, CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                        };
                                        taxSubTotalItem.TaxCategory = new TaxCategory() { TaxExemptionReason = currentTaxPercent != 0 ? null : "Muaf", TaxScheme = new TaxScheme() { Name = "KDV", TaxTypeCode = "0015" } };

                                        taxSubTotals.Add(taxSubTotalItem);
                                    }

                                    xlsInvoice.TaxTotals = new List<TaxTotal>()
                                    {
                                        new TaxTotal()
                                        {
                                            TaxAmount = new UblBaseCurrency()
                                            {
                                                Value = Convert.ToDecimal(dt.Rows[totalTaxLineNumber][24].ToString().Replace(".", ",")),
                                                CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                            },
                                            TaxSubtotals = taxSubTotals
                                        }
                                    };
                                }

                                #endregion

                                #region GENEL TOPLAM

                                if (!string.IsNullOrEmpty(dt.Rows[payableAmountLineNumber][24].ToString()))
                                {
                                    xlsInvoice.LegalMonetaryTotal = new MonetaryTotal()
                                    {
                                        LineExtensionAmount =
                                            new UblBaseCurrency()
                                            {
                                                Value = Convert.ToDecimal(dt.Rows[extensionAmountLineNumber][24].ToString().Replace(".", ",")),
                                                CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                            },
                                        AllowanceTotalAmount = new UblBaseCurrency()
                                        {
                                            Value = Convert.ToDecimal(dt.Rows[allowanceAmountLineNumber][24].ToString().Replace(".", ",")),
                                            CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                        },

                                        //TaxExclusiveAmount = new BaseCurrency() { Value = Convert.ToDecimal(LineExtensionAmount - AllowanceTotalAmount), CurrencyID = xlsInvoice.DocumentCurrencyCode.Name },

                                        TaxInclusiveAmount =
                                            new UblBaseCurrency()
                                            {
                                                Value = Convert.ToDecimal(dt.Rows[totalAmountWithTaxLineNumber][24].ToString().Replace(".", ",")),
                                                CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                            },
                                        PayableAmount = new UblBaseCurrency()
                                        {
                                            Value = Convert.ToDecimal(dt.Rows[payableAmountLineNumber][24].ToString().Replace(".", ",")),
                                            CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                        },
                                    };
                                    xlsInvoice.LegalMonetaryTotal.TaxExclusiveAmount = new UblBaseCurrency()
                                    {
                                        Value = Convert.ToDecimal(xlsInvoice.LegalMonetaryTotal.LineExtensionAmount.Value - xlsInvoice.LegalMonetaryTotal.AllowanceTotalAmount.Value),
                                        CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                    };
                                }

                                #endregion
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[allowanceAmountLineNumber][2].ToString()))
                                xlsInvoice.Notes = new List<string>() { dt.Rows[allowanceAmountLineNumber][2].ToString() };
                        }

                        #endregion
                    }
                    else if (xlsFile.WorkbookData.Tables.Count == 1 && string.Equals(xlsFile.WorkbookData.Tables[0].TableName, "UBL-TR Ihracat"))
                    {
                        #region UBL-TR Ihracat

                        //Excel satır sayısı artığında. Bu değeri değiştirmek yeterli.
                        int xlsTotalLineNumberCount = 564;

                        //Mal Hizmet Toplam Tutar
                        int extensionAmountLineNumber = xlsTotalLineNumberCount + 25;

                        //Toplam İskonto
                        int allowanceAmountLineNumber = xlsTotalLineNumberCount + 26;

                        //Not
                        var noteLineNumber = xlsTotalLineNumberCount + 26;

                        //Toplam Sigorta / Navlun
                        int chargeAmountLineNumber = xlsTotalLineNumberCount + 27;

                        //Hesaplanan Kdv Tutarı
                        int totalTaxLineNumber = xlsTotalLineNumberCount + 28;

                        //Vergiler Dahil Toplam Tutar
                        int totalAmountWithTaxLineNumber = xlsTotalLineNumberCount + 29;

                        //Ödenecek Tutar
                        int payableAmountLineNumber = xlsTotalLineNumberCount + 30;

                        DataTable dt = xlsFile.WorkbookData.Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            //DEFAULT SET CONSTRUCTOR
                            //xlsInvoice.UBLVersionID = "2.1"
                            //xlsInvoice.CopyIndicator = false
                            //xlsInvoice.CustomizationID = "TR1.2"

                            if (!string.IsNullOrEmpty(dt.Rows[13][23].ToString()) && string.Equals(dt.Rows[13][23].ToString().Trim(), ProfileIdType.IHRACAT.ToString()))
                            {
                                xlsInvoice.ProfileID = ProfileIdType.IHRACAT.ToString();
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[14][23].ToString()) && string.Equals(dt.Rows[14][23].ToString().Trim(), InvoiceTypeCode.ISTISNA.ToString()))
                            {
                                xlsInvoice.InvoiceTypeCode = InvoiceTypeCode.ISTISNA.ToString();
                            }


                            if (string.IsNullOrEmpty(dt.Rows[15][23].ToString()))
                            {
                                throw new Exception("Fatura No Alınamadı!");
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[15][23].ToString()) && dt.Rows[15][23].ToString().Length == 16)
                                xlsInvoice.ID = dt.Rows[15][23].ToString();

                            if (!string.IsNullOrEmpty(dt.Rows[16][23].ToString()) && dt.Rows[16][23].ToString().Length == 10)
                            {
                                string datetimeStr = dt.Rows[16][23].ToString();
                                DateTime issueDate = new DateTime(SafeVar.GetInt(datetimeStr.Substring(6, 4)), SafeVar.GetInt(datetimeStr.Substring(3, 2)), SafeVar.GetInt(datetimeStr.Substring(0, 2)));

                                if (issueDate != null)
                                    xlsInvoice.IssueDate = issueDate.ToString(DateFormats.DateTimeGIBFormatShort);
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[17][23].ToString()) && !string.IsNullOrEmpty(dt.Rows[18][23].ToString()) && dt.Rows[18][23].ToString().Length == 10)
                            {
                                string datetimeStr = dt.Rows[18][23].ToString();
                                DateTime issueDateTime = new DateTime(SafeVar.GetInt(datetimeStr.Substring(6, 4)), SafeVar.GetInt(datetimeStr.Substring(3, 2)), SafeVar.GetInt(datetimeStr.Substring(0, 2)));
                                if (issueDateTime != null)
                                {
                                    xlsInvoice.DespatchDocumentReferences = new List<DocumentReference>()
                                    {
                                        new DocumentReference() { ID = new CombineId() { Id = dt.Rows[17][23].ToString() }, IssueDate = issueDateTime.ToString(DateFormats.DateTimeGIBFormatShort) }
                                    };
                                }
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[19][23].ToString()) && !string.IsNullOrEmpty(dt.Rows[20][23].ToString()) && dt.Rows[20][23].ToString().Length == 10)
                            {
                                string datetimeStr = dt.Rows[20][23].ToString();
                                DateTime issueDate = new DateTime(SafeVar.GetInt(datetimeStr.Substring(6, 4)), SafeVar.GetInt(datetimeStr.Substring(3, 2)), SafeVar.GetInt(datetimeStr.Substring(0, 2)));
                                if (issueDate != null)
                                {
                                    xlsInvoice.OrderReference = new OrderReference() { ID = new CombineId() { Id = dt.Rows[19][23].ToString() }, IssueDate = issueDate.ToString(DateFormats.DateTimeGIBFormatShort) };
                                }
                            }

                            string excelCurrency = UblTr2HandlerEInvoice.TryCurrency;

                            if (!string.IsNullOrEmpty(dt.Rows[21][23].ToString()) && !string.IsNullOrEmpty(dt.Rows[22][23].ToString()))
                            {
                                excelCurrency = dt.Rows[21][23].ToString().Trim();

                                xlsInvoice.PricingExchangeRate = new ExchangeRate()
                                {
                                    SourceCurrencyCode = excelCurrency, TargetCurrencyCode = "TRY", CalculationRate = Math.Round(Convert.ToDouble(dt.Rows[22][23].ToString().Replace(".", ",")), 4)
                                };
                            }

                            xlsInvoice.DocumentCurrencyCode = new DocumentCurrencyCode() { Name = excelCurrency };

                            try
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[22][3].ToString()))
                                    xlsInvoice.UUID = dt.Rows[22][3].ToString();
                                else
                                    xlsInvoice.UUID = Guid.NewGuid().ToString();
                            }
                            catch (Exception)
                            {
                                xlsInvoice.UUID = Guid.NewGuid().ToString();
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[8][9].ToString()))
                            {
                                string accountRegisterNumber = dt.Rows[8][9].ToString().Trim();
                                string accountSchemaId = "VKN";

                                if (accountRegisterNumber.Length == 11)
                                    accountSchemaId = "TCKN";

                                xlsInvoice.AccountingSupplierParty = new SupplierParty() { Party = GetPartyData(false, accountRegisterNumber, accountSchemaId, dt) };
                                if (accountSchemaId == "VKN")
                                {
                                    xlsInvoice.AccountingSupplierParty.Party.PartyName = new PartyName() { Name = string.IsNullOrEmpty(dt.Rows[2][3].ToString()) ? null : dt.Rows[2][3].ToString() };
                                }
                                else
                                {
                                    var titleFromExcel = string.IsNullOrEmpty(dt.Rows[2][3].ToString()) ? null : dt.Rows[2][3].ToString();
                                    if (titleFromExcel != null)
                                    {
                                        xlsInvoice.AccountingSupplierParty.Party.Person = new Person()
                                        {
                                            FirstName = titleFromExcel.Trim().Substring(0, titleFromExcel.Trim().LastIndexOf(' ')), FamilyName = titleFromExcel.Trim().Substring(titleFromExcel.Trim().LastIndexOf(' '))
                                        };
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[19][9].ToString()))
                            {
                                xlsInvoice.BuyerCustomerParty = new CustomerParty() { Party = GetPartyData(true, "EXPORT", "PARTYTYPE", dt) };

                                xlsInvoice.BuyerCustomerParty.Party.PartyLegalEntities = new List<PartyLegalEntity>()
                                {
                                    new PartyLegalEntity()
                                    {
                                        RegistrationName = string.IsNullOrEmpty(dt.Rows[13][3].ToString()) ? null : dt.Rows[13][3].ToString(),
                                        CompanyID = string.IsNullOrEmpty(dt.Rows[19][9].ToString()) ? null : dt.Rows[19][9].ToString()
                                    }
                                };
                            }

                            CustomerParty customer = new CustomerParty();

                            customer.Party = new Party();

                            customer.Party.PartyTaxScheme = new PartyTaxScheme();
                            customer.Party.PartyTaxScheme.TaxScheme = new TaxScheme();

                            xlsInvoice.AccountingCustomerParty = new CustomerParty()
                            {
                                Party = new Party()
                                {
                                    PartyIdentification = new List<PartyIdentification>() { new PartyIdentification() { ID = new CombineId() { Id = "1460415308", SchemeId = "VKN" } } },
                                    PartyName = new PartyName() { Name = "Gümrük ve Ticaret Bakanlığı" },
                                    PostalAddress = new Address()
                                    {
                                        StreetName = "Üniversiteler Mahallesi Dumlupınar Bulvarı",
                                        BuildingName = null,
                                        BuildingNumber = "151",
                                        Room = null,
                                        CitySubdivisionName = "Çankaya",
                                        CityName = "Ankara",
                                        PostalZone = null,
                                        Country = new Country() { IdentificationCode = "TR", Name = "Türkiye" }
                                    },
                                    WebSiteURI = null,
                                    Contact = new Contact() { Telephone = null, Telefax = null, ElectronicMail = null },
                                    PartyTaxScheme = new PartyTaxScheme() { TaxScheme = new TaxScheme() { Name = "Ulus" } },
                                }
                            };

                            xlsInvoice.LineCountNumeric = 0;
                            for (int i = 25; i < 25 + xlsTotalLineNumberCount; i++)
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[i][2].ToString()))
                                    xlsInvoice.LineCountNumeric = xlsInvoice.LineCountNumeric + 1;
                                else
                                    break;
                            }


                            if (xlsInvoice.LineCountNumeric > 0)
                            {
                                xlsInvoice.InvoiceLines = new List<InvoiceLine>();

                                for (int i = 25; i < 25 + xlsTotalLineNumberCount; i++)
                                {
                                    if (!string.IsNullOrEmpty(dt.Rows[i][2].ToString()))
                                    {
                                        DataRow drInvoiceLine = dt.Rows[i];
                                        int lineNumber = i - 24;
                                        string invoiceLineUnitStr = SafeVar.GetString(drInvoiceLine[10]).ToLower(new CultureInfo("tr-TR"));
                                        string invoiceLineUnitCode = string.Empty;
                                        if (invoiceLineUnitStr.Equals("adet"))
                                        {
                                            invoiceLineUnitCode = "C62";
                                        }
                                        else
                                        {
                                            var code = VeribanGlobal.Library.Common.ConstRepository.UnitType.GetUnitTypeList().FirstOrDefault(o => o.Name.ToLower(new CultureInfo("tr-TR")) == invoiceLineUnitStr);
                                            invoiceLineUnitCode = code != null ? code.Code : null;
                                        }

                                        if (string.IsNullOrEmpty(drInvoiceLine[19].ToString()))
                                        {
                                            throw new Exception("Mal Hizmet Tutarı Alınamadı!");
                                        }


                                        string transportModeId = string.Empty;
                                        string packageCode = string.Empty;
                                        string countryCode = string.Empty;


                                        var transportModeCode = VeribanGlobal.Library.Common.ConstRepository.TransportModeCode.GetTransportModeCodeList().AsEnumerable()
                                            .FirstOrDefault(o => o.Name.ToLower() == drInvoiceLine[34].ToString().Trim().ToLower());
                                        if (transportModeCode == null)
                                        {
                                            throw new Exception(string.Format("{0}. Satır Gönderim Şekli Alınamadı!", lineNumber));
                                        }

                                        var packageCodeModel = VeribanGlobal.Library.Common.ConstRepository.PackageCode.GetPackageCodeList().AsEnumerable()
                                            .FirstOrDefault(o => o.Name.ToLower() == drInvoiceLine[28].ToString().Trim().ToLower());
                                        if (packageCodeModel == null)
                                        {
                                            throw new Exception(string.Format("{0}. Satır Eşya Kap Cinsi Alınamadı!", lineNumber));
                                        }

                                        var countryCodeModel = VeribanGlobal.Library.Common.ConstRepository.CountryTypeForIhracat.GetCountryList().AsEnumerable()
                                            .FirstOrDefault(o => o.Name.ToLower() == drInvoiceLine[33].ToString().Trim().ToLower());
                                        if (countryCodeModel == null)
                                        {
                                            throw new Exception(string.Format("{0}. Satır Ülke Bilgisi Alınamadı", lineNumber));
                                        }

                                        transportModeId = transportModeCode.Id.ToString();
                                        packageCode = packageCodeModel.Code;
                                        countryCode = countryCodeModel.CountryCode;


                                        InvoiceLine invoiceLineData = new InvoiceLine();
                                        invoiceLineData.ID = new CombineId() { Id = drInvoiceLine[1].ToString() };
                                        invoiceLineData.Item = new Item() { Name = string.IsNullOrEmpty(drInvoiceLine[2].ToString()) ? null : drInvoiceLine[2].ToString() };
                                        invoiceLineData.InvoicedQuantity = new BaseUnit() { Value = Convert.ToDecimal(drInvoiceLine[8].ToString().Replace(".", ",")), UnitCode = invoiceLineUnitCode };
                                        invoiceLineData.Price = new Price()
                                        {
                                            PriceAmount = new UblBaseCurrency()
                                            {
                                                Value = Convert.ToDecimal(drInvoiceLine[12].ToString().Replace(".", ",")),
                                                CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                            }
                                        };
                                        invoiceLineData.LineExtensionAmount = new UblBaseCurrency()
                                        {
                                            Value = Convert.ToDecimal(drInvoiceLine[19].ToString().Replace(".", ",")),
                                            CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                        };


                                        invoiceLineData.Deliveries = new List<Delivery>();

                                        Delivery delivery = new Delivery();
                                        delivery.DeliveryTerms = new List<DeliveryTerms>() { new DeliveryTerms() { ID = new CombineId() { Id = drInvoiceLine[27].ToString().Trim(), SchemeId = "INCOTERMS" } } };
                                        delivery.Shipment = new Shipment();
                                        delivery.Shipment.ID = new CombineId() { Id = "." };
                                        if (!decimal.TryParse(drInvoiceLine[30].ToString(), out decimal no))
                                        {
                                            throw new Exception($"{lineNumber}. satırdaki Kap Adedi boş olamaz.");
                                        }

                                        delivery.Shipment.TransportHandlingUnits = new List<TransportHandlingUnit>()
                                        {
                                            new TransportHandlingUnit()
                                            {
                                                ActualPackages = new List<Package>()
                                                {
                                                    new Package()
                                                    {
                                                        PackagingTypeCode = packageCode,
                                                        ID = new CombineId() { Id = string.IsNullOrEmpty(drInvoiceLine[29].ToString()) ? "1" : drInvoiceLine[29].ToString().Trim() },
                                                        Quantity = new BaseUnit() { Value = Convert.ToDecimal(drInvoiceLine[30].ToString().Replace(".", ",")) }
                                                    }
                                                }
                                            }
                                        };
                                        delivery.Shipment.GoodsItems = new List<GoodsItem>() { new GoodsItem() { RequiredCustomsID = drInvoiceLine[31].ToString().Trim() } };
                                        delivery.Shipment.ShipmentStages = new List<ShipmentStage>() { new ShipmentStage() { TransportModeCode = transportModeId } };
                                        delivery.DeliveryAddress = new Address() { StreetName = drInvoiceLine[32].ToString().Trim(), Country = new Country() { IdentificationCode = countryCode, Name = drInvoiceLine[33].ToString().Trim() } };
                                        invoiceLineData.Deliveries.Add(delivery);


                                        var allowDiscountRate = drInvoiceLine[14].ToString();
                                        var alowDiscountAmount = drInvoiceLine[16].ToString();
                                        var alowDiscountBaseAmount = drInvoiceLine[39].ToString();

                                        if (string.IsNullOrEmpty(allowDiscountRate))
                                        {
                                            allowDiscountRate = "0";
                                        }

                                        if (string.IsNullOrEmpty(alowDiscountAmount))
                                        {
                                            alowDiscountAmount = "0";
                                        }

                                        if (string.IsNullOrEmpty(alowDiscountBaseAmount))
                                        {
                                            alowDiscountBaseAmount = "0";
                                        }

                                        var allowanceFreightRate = drInvoiceLine[35].ToString();
                                        var allowanceFreightAmount = drInvoiceLine[36].ToString();
                                        var allowanceFreightBaseAmount = drInvoiceLine[39].ToString();

                                        if (string.IsNullOrEmpty(allowanceFreightRate))
                                        {
                                            allowanceFreightRate = "0";
                                        }

                                        if (string.IsNullOrEmpty(allowanceFreightAmount))
                                        {
                                            allowanceFreightAmount = "0";
                                        }

                                        if (string.IsNullOrEmpty(allowanceFreightBaseAmount))
                                        {
                                            allowanceFreightBaseAmount = "0";
                                        }

                                        invoiceLineData.AllowanceCharges = new List<AllowanceCharge>()
                                        {
                                            new AllowanceCharge()
                                            {
                                                ChargeIndicator = false, //indirim
                                                MultiplierFactorNumeric = allowDiscountRate.ToString().Replace(",", "."), //oran
                                                Amount = new UblBaseCurrency()
                                                {
                                                    Value = Convert.ToDecimal(alowDiscountAmount.ToString().Replace(".", ",")),
                                                    CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                                }, //iskonto miktarı
                                                BaseAmount = new UblBaseCurrency()
                                                {
                                                    Value = Convert.ToDecimal(alowDiscountBaseAmount.ToString().Replace(".", ",")),
                                                    CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                                } //iskonto hesaplanan miktar
                                            },
                                            new AllowanceCharge()
                                            {
                                                ChargeIndicator = true, //sigorta/navlun
                                                MultiplierFactorNumeric = allowanceFreightRate.ToString().Replace(",", "."), //oran
                                                Amount = new UblBaseCurrency()
                                                {
                                                    Value = Convert.ToDecimal(allowanceFreightAmount.Replace(".", ",")),
                                                    CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                                }, //iskonto miktarı
                                                BaseAmount = new UblBaseCurrency()
                                                {
                                                    Value = Convert.ToDecimal(allowanceFreightBaseAmount.ToString().Replace(".", ",")),
                                                    CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                                } //iskonto hesaplanan miktar
                                            }
                                        };

                                        invoiceLineData.TaxTotal = new TaxTotal()
                                        {
                                            TaxAmount =
                                                new UblBaseCurrency()
                                                {
                                                    Value = Convert.ToDecimal(drInvoiceLine[24].ToString().Replace(".", ",")),
                                                    CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                                },
                                            TaxSubtotals = new List<TaxSubtotal>()
                                            {
                                                new TaxSubtotal()
                                                {
                                                    TaxableAmount = new UblBaseCurrency()
                                                    {
                                                        Value = Convert.ToDecimal(drInvoiceLine[19].ToString().Replace(".", ",")),
                                                        CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                                    },
                                                    TaxAmount = new UblBaseCurrency()
                                                    {
                                                        Value = Convert.ToDecimal(drInvoiceLine[24].ToString().Replace(".", ",")),
                                                        CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                                    },
                                                    CalculationSequenceNumeric = 1, //DEFAULT 1 VERGI TIPI VAR O YÜZDEN 1
                                                    Percent = Convert.ToDecimal(drInvoiceLine[22].ToString().Replace(".", ",")) * 100,
                                                    TaxCategory = new TaxCategory() { TaxScheme = new TaxScheme() { Name = "KDV", TaxTypeCode = "0015" } }
                                                }
                                            }
                                        };

                                        xlsInvoice.InvoiceLines.Add(invoiceLineData);
                                    }
                                }

                                //TOPLAM ISKONTO VEYA ARTTIRIM
                                if (!string.IsNullOrEmpty(dt.Rows[allowanceAmountLineNumber][24].ToString()) || !string.IsNullOrEmpty(dt.Rows[chargeAmountLineNumber][24].ToString()))
                                {
                                    if (!string.IsNullOrEmpty(dt.Rows[allowanceAmountLineNumber][24].ToString()) && !string.IsNullOrEmpty(dt.Rows[chargeAmountLineNumber][24].ToString()))
                                    {
                                        xlsInvoice.AllowanceCharges = new List<AllowanceCharge>()
                                        {
                                            new AllowanceCharge()
                                            {
                                                ChargeIndicator = false,
                                                Amount =
                                                    new UblBaseCurrency()
                                                    {
                                                        Value = Convert.ToDecimal(dt.Rows[allowanceAmountLineNumber][24].ToString().Replace(".", ",")),
                                                        CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                                    }
                                            },
                                            new AllowanceCharge()
                                            {
                                                ChargeIndicator = true,
                                                Amount = new UblBaseCurrency()
                                                {
                                                    Value = Convert.ToDecimal(dt.Rows[chargeAmountLineNumber][24].ToString().Replace(".", ",")),
                                                    CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                                }
                                            }
                                        };
                                    }
                                    else if (!string.IsNullOrEmpty(dt.Rows[allowanceAmountLineNumber][24].ToString()))
                                    {
                                        xlsInvoice.AllowanceCharges = new List<AllowanceCharge>()
                                        {
                                            new AllowanceCharge()
                                            {
                                                ChargeIndicator = false,
                                                Amount = new UblBaseCurrency()
                                                {
                                                    Value = Convert.ToDecimal(dt.Rows[allowanceAmountLineNumber][24].ToString().Replace(".", ",")),
                                                    CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                                }
                                            }
                                        };
                                    }
                                    else
                                    {
                                        xlsInvoice.AllowanceCharges = new List<AllowanceCharge>()
                                        {
                                            new AllowanceCharge()
                                            {
                                                ChargeIndicator = true,
                                                Amount = new UblBaseCurrency()
                                                {
                                                    Value = Convert.ToDecimal(dt.Rows[chargeAmountLineNumber][24].ToString().Replace(".", ",")),
                                                    CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                                }
                                            }
                                        };
                                    }
                                }

                                //TOPLAM VERGILER
                                if (!string.IsNullOrEmpty(dt.Rows[totalAmountWithTaxLineNumber][24].ToString()))
                                {
                                    List<TaxSubtotal> taxSubTotalList = new List<TaxSubtotal>();

                                    List<decimal> percentList = new List<decimal>();
                                    foreach (InvoiceLine invoiceLine in xlsInvoice.InvoiceLines)
                                    {
                                        if (!percentList.Contains(invoiceLine.TaxTotal.TaxSubtotals[0].Percent.Value))
                                            percentList.Add(invoiceLine.TaxTotal.TaxSubtotals[0].Percent.Value);
                                    }

                                    for (int i = 0; i < percentList.Count; i++)
                                    {
                                        decimal currentTaxPercent = percentList[i];
                                        decimal totalTaxableAmount = 0;
                                        decimal totalTaxAmount = 0;

                                        foreach (InvoiceLine invoiceLine in xlsInvoice.InvoiceLines)
                                        {
                                            if (currentTaxPercent == invoiceLine.TaxTotal.TaxSubtotals[0].Percent.Value)
                                            {
                                                decimal currentTotalTaxableAmount = invoiceLine.TaxTotal.TaxSubtotals[0].TaxableAmount.Value;
                                                decimal currentTotalTaxAmount = invoiceLine.TaxTotal.TaxSubtotals[0].TaxAmount.Value;

                                                totalTaxableAmount = totalTaxableAmount + currentTotalTaxableAmount;
                                                totalTaxAmount = totalTaxAmount + currentTotalTaxAmount;
                                            }
                                        }

                                        TaxSubtotal taxSubTotal = new TaxSubtotal();
                                        taxSubTotal.CalculationSequenceNumeric = i + 1;
                                        taxSubTotal.Percent = currentTaxPercent;
                                        taxSubTotal.TaxableAmount = new UblBaseCurrency()
                                        {
                                            Value = totalTaxableAmount, CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                        };
                                        taxSubTotal.TaxAmount = new UblBaseCurrency()
                                        {
                                            Value = totalTaxAmount, CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                        };
                                        taxSubTotal.TaxCategory = new TaxCategory()
                                        {
                                            TaxExemptionReasonCode = currentTaxPercent != 0 ? null : "301",
                                            TaxExemptionReason = currentTaxPercent != 0 ? null : "11 / 1 - a Mal ihracatı",
                                            TaxScheme = new TaxScheme() { Name = "KDV", TaxTypeCode = "0015" }
                                        };

                                        taxSubTotalList.Add(taxSubTotal);
                                    }

                                    xlsInvoice.TaxTotals = new List<TaxTotal>()
                                    {
                                        new TaxTotal()
                                        {
                                            TaxAmount = new UblBaseCurrency()
                                            {
                                                Value = Convert.ToDecimal(dt.Rows[totalTaxLineNumber][24].ToString().Replace(".", ",")),
                                                CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                            },
                                            TaxSubtotals = taxSubTotalList
                                        }
                                    };
                                }

                                //GENEL TOPLAMLAR
                                if (!string.IsNullOrEmpty(dt.Rows[payableAmountLineNumber][24].ToString()))
                                {
                                    xlsInvoice.LegalMonetaryTotal = new MonetaryTotal()
                                    {
                                        LineExtensionAmount =
                                            new UblBaseCurrency()
                                            {
                                                Value = Convert.ToDecimal(dt.Rows[extensionAmountLineNumber][24].ToString().Replace(".", ",")),
                                                CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                            },
                                        AllowanceTotalAmount =
                                            new UblBaseCurrency()
                                            {
                                                Value = Convert.ToDecimal(dt.Rows[allowanceAmountLineNumber][24].ToString().Replace(".", ",")),
                                                CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                            },
                                        ChargeTotalAmount = new UblBaseCurrency()
                                        {
                                            Value = Convert.ToDecimal(dt.Rows[chargeAmountLineNumber][24].ToString().Replace(".", ",")),
                                            CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                        },
                                        //TaxExclusiveAmount = new BaseCurrency() { Value = Convert.ToDecimal(LineExtensionAmount - AllowanceTotalAmount), CurrencyID = xlsInvoice.DocumentCurrencyCode.Name },

                                        TaxInclusiveAmount =
                                            new UblBaseCurrency()
                                            {
                                                Value = Convert.ToDecimal(dt.Rows[totalAmountWithTaxLineNumber][24].ToString().Replace(".", ",")),
                                                CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                            },
                                        PayableAmount = new UblBaseCurrency()
                                        {
                                            Value = Convert.ToDecimal(dt.Rows[payableAmountLineNumber][24].ToString().Replace(".", ",")),
                                            CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                        },
                                    };
                                    xlsInvoice.LegalMonetaryTotal.TaxExclusiveAmount = new UblBaseCurrency()
                                    {
                                        Value = Convert.ToDecimal(
                                            xlsInvoice.LegalMonetaryTotal.LineExtensionAmount.Value - xlsInvoice.LegalMonetaryTotal.AllowanceTotalAmount.Value + xlsInvoice.LegalMonetaryTotal.ChargeTotalAmount.Value),
                                        CurrencyID = xlsInvoice.DocumentCurrencyCode.Name == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TryCurrency : xlsInvoice.DocumentCurrencyCode.Name
                                    };
                                }
                            }

                            //Not
                            if (!string.IsNullOrEmpty(dt.Rows[noteLineNumber][2].ToString()))
                            {
                                string[] notes = dt.Rows[noteLineNumber][2].ToString().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                                xlsInvoice.Notes = notes.ToList();
                            }
                        }

                        #endregion
                    }
                    else throw new Exception("Excell file contains more than one Sheet or SheetName is not 'UBL-TR Invoice'");

                    TransferInvoiceDataSerializer iSerializer = new TransferInvoiceDataSerializer();
                    convertedXmlContent = iSerializer.SerializeAndGetXmlContent(xlsInvoice, true, true);

                    return !string.IsNullOrWhiteSpace(convertedXmlContent);
                }
                else throw new Exception("Excell file not contains Sheet'");
            }
            catch (Exception ex)
            {
                convertedXmlContent = ex.Message.ToString();
            }

            return false;
        }

        private Party GetPartyData(bool isCustomer, string accountRegisterNumber, string accountSchemaId, DataTable dt)
        {
            int adresIndex = 2;
            if (isCustomer) adresIndex = 13;

            string[] personNameArr = null;
            string firstName = "";
            string familyName = "";

            try
            {
                personNameArr = dt.Rows[adresIndex][3].ToString().Split(' ');
                firstName = personNameArr[0];
                familyName = personNameArr[1];
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            var party = new Party()
            {
                PartyIdentification = new List<PartyIdentification>() { new PartyIdentification() { ID = new CombineId() { Id = accountRegisterNumber, SchemeId = accountSchemaId } } },
                PartyName = new PartyName() { Name = string.IsNullOrEmpty(dt.Rows[adresIndex][3].ToString()) ? null : dt.Rows[adresIndex][3].ToString() },
                PostalAddress = new Address()
                {
                    StreetName = string.IsNullOrEmpty(dt.Rows[adresIndex + 1][3].ToString()) ? null : dt.Rows[adresIndex + 1][3].ToString(),
                    BuildingName = string.IsNullOrEmpty(dt.Rows[adresIndex + 2][3].ToString()) ? null : dt.Rows[adresIndex + 2][3].ToString(),
                    BuildingNumber = string.IsNullOrEmpty(dt.Rows[adresIndex + 2][9].ToString()) ? null : dt.Rows[adresIndex + 2][9].ToString(),
                    CitySubdivisionName = string.IsNullOrEmpty(dt.Rows[adresIndex + 3][6].ToString()) ? null : dt.Rows[adresIndex + 3][6].ToString(),
                    CityName = string.IsNullOrEmpty(dt.Rows[adresIndex + 3][9].ToString()) ? null : dt.Rows[adresIndex + 3][9].ToString(),
                    PostalZone = string.IsNullOrEmpty(dt.Rows[adresIndex + 3][3].ToString()) ? null : dt.Rows[adresIndex + 3][3].ToString(),
                    Country = new Country()
                },
                Contact = new Contact()
                {
                    Telephone = string.IsNullOrEmpty(dt.Rows[adresIndex + 4][3].ToString()) ? null : dt.Rows[adresIndex + 4][3].ToString(),
                    Telefax = string.IsNullOrEmpty(dt.Rows[adresIndex + 4][6].ToString()) ? null : dt.Rows[adresIndex + 4][6].ToString(),
                    ElectronicMail = string.IsNullOrEmpty(dt.Rows[adresIndex + 5][3].ToString()) ? null : dt.Rows[adresIndex + 5][3].ToString()
                },
                PartyTaxScheme = new PartyTaxScheme() { TaxScheme = new TaxScheme() { Name = string.IsNullOrEmpty(dt.Rows[adresIndex + 6][3].ToString()) ? null : dt.Rows[adresIndex + 6][3].ToString() } },
                Person = new Person { FirstName = firstName, FamilyName = familyName },
            };

            return party;
        }

        private bool SAPDocumentInvoiceToUBLTRXmlContent(string sapXmlContent, out string convertedXmlContent)
        {
            convertedXmlContent = null;

            try
            {
                SapDocumentSerializer serializer = new SapDocumentSerializer();
                VeribanGlobal.Library.Model.SapDocument.SapEnvelopeDocumentModel sapDocument = serializer.DeserializeFromXmlContent(sapXmlContent);
                var sapInvoice = sapDocument.Values.STR_DATA.INVOICE;

                //SAP Gümrük Bakanlığı bilgilerini ayrıca veremiyor, biz sabit olarak yazıyoruz.
                if (sapInvoice.PROFILE_ID.Equals("IHRACAT"))
                {
                    sapInvoice.ACP = new VeribanGlobal.Library.Model.SapDocument.Acp()
                    {
                        PARTY = new VeribanGlobal.Library.Model.SapDocument.Party()
                        {
                            PARTY_IDFICATION = new VeribanGlobal.Library.Model.SapDocument.PartyIdfication() { SCHMID = "VKN", ID = "1460415308", },
                            PARTY_NAME = new VeribanGlobal.Library.Model.SapDocument.PartyName() { NAME = "Gümrük ve Ticaret Bakanlığı" },
                            POST_ADDR = new VeribanGlobal.Library.Model.SapDocument.PostAddr()
                            {
                                STREET_NAME = "Üniversiteler Mahallesi Dumlupınar Bulvarı",
                                BUILDG_NO = "151",
                                CITY_SUBD_NAME = "Çankaya",
                                CITY_NAME = "Ankara",
                                COUNTRY = new VeribanGlobal.Library.Model.SapDocument.Country() { COUNTRY_NAME = "Türkiye" }
                            },
                            PARTY_TXSCHM = new VeribanGlobal.Library.Model.SapDocument.PartyTxschm() { TXSCHM_NAME = new VeribanGlobal.Library.Model.SapDocument.TxschmName() { NAME = "Ulus" } },
                        }
                    };

                    if (sapInvoice.BCP != null && sapInvoice.BCP.PARTY != null)
                    {
                        sapInvoice.BCP.PARTY.PARTY_IDFICATION = new VeribanGlobal.Library.Model.SapDocument.PartyIdfication() { SCHMID = "PARTYTYPE", ID = "EXPORT" };
                    }
                }

                InvoiceModel tempInvoice = new InvoiceModel();
                if (sapInvoice.CUSTOM_ID != UblTr2HandlerEInvoice.CustomizationVersion)
                {
                    tempInvoice.CustomizationID = UblTr2HandlerEInvoice.CustomizationVersion;
                }
                else
                {
                    tempInvoice.CustomizationID = sapInvoice.CUSTOM_ID;
                }

                tempInvoice.UBLVersionID = sapInvoice.UBLVERS_ID;
                tempInvoice.ProfileID = sapInvoice.PROFILE_ID;
                tempInvoice.ID = sapInvoice.ID;
                tempInvoice.CopyIndicator = sapInvoice.COPY_INDICATOR;
                tempInvoice.UUID = sapInvoice.UUID;
                tempInvoice.IssueDate = sapInvoice.ISSUE_DATE;
                tempInvoice.InvoiceTypeCode = sapInvoice.INV_TYP_CODE;

                if (sapInvoice.NOTE != null)
                {
                    tempInvoice.Notes = new List<string>();

                    foreach (var item in sapInvoice.NOTE.ITEM)
                    {
                        tempInvoice.Notes.Add(item);
                    }
                }

                if (sapInvoice.DISPATCH != null)
                {
                    tempInvoice.DespatchDocumentReferences = new List<DocumentReference>();

                    var indexDate = sapInvoice.DISPATCH.DATE.IndexOf(" / ");
                    var indexId = sapInvoice.DISPATCH.ID.IndexOf(" / ");

                    if (indexDate > 0 && indexId > 0)
                    {
                        var splitDate = sapInvoice.DISPATCH.DATE.Split('/').ToList();
                        var splitId = sapInvoice.DISPATCH.ID.Split('/').ToList();
                        foreach (var item in splitDate)
                        {
                            tempInvoice.DespatchDocumentReferences.Add(new DocumentReference() { ID = new CombineId() { Id = splitId[splitDate.IndexOf(item)] }, IssueDate = item });
                        }
                    }
                    else
                    {
                        tempInvoice.DespatchDocumentReferences.Add(new DocumentReference() { ID = new CombineId() { Id = sapInvoice.DISPATCH.ID }, IssueDate = sapInvoice.DISPATCH.DATE });
                    }
                }

                tempInvoice.DocumentCurrencyCode = new DocumentCurrencyCode()
                {
                    Name = sapInvoice.DOCU_CURR_CODE,
                    ListAgencyName = "United Nations Economic Commission for Europe",
                    ListID = "ISO 4217 Alpha",
                    ListName = "Currency",
                    ListVersionID = "2001"
                };

                tempInvoice.LineCountNumeric = sapInvoice.LINE_CNT_NUMC;

                #region OrderReference

                if (sapInvoice.ORDER_REFERENCE != null)
                {
                    tempInvoice.OrderReference = new OrderReference();

                    if (!string.IsNullOrEmpty(sapInvoice.ORDER_REFERENCE.SALES_ORDER_ID))
                    {
                        tempInvoice.OrderReference.SalesOrderID = sapInvoice.ORDER_REFERENCE.SALES_ORDER_ID;
                    }

                    if (!string.IsNullOrEmpty(sapInvoice.ORDER_REFERENCE.ISSUE_DATE))
                    {
                        tempInvoice.OrderReference.IssueDate = sapInvoice.ORDER_REFERENCE.ISSUE_DATE;
                    }

                    if (!string.IsNullOrEmpty(sapInvoice.ORDER_REFERENCE.ORDER_TYPE_CODE))
                    {
                        tempInvoice.OrderReference.OrderTypeCode = sapInvoice.ORDER_REFERENCE.ORDER_TYPE_CODE;
                    }

                    if (!string.IsNullOrEmpty(sapInvoice.ORDER_REFERENCE.ORDER_ID))
                    {
                        tempInvoice.OrderReference.ID = new CombineId() { Id = sapInvoice.ORDER_REFERENCE.ORDER_ID };
                    }
                }

                #endregion

                #region AccountingSupplierParty

                if (sapInvoice.ASP != null)
                {
                    tempInvoice.AccountingSupplierParty = new SupplierParty();
                    if (sapInvoice.ASP.PARTY != null)
                    {
                        tempInvoice.AccountingSupplierParty.Party = new Party();

                        if (!string.IsNullOrEmpty(sapInvoice.ASP.PARTY.WEB_URL))
                        {
                            tempInvoice.AccountingSupplierParty.Party.WebSiteURI = sapInvoice.ASP.PARTY.WEB_URL;
                        }

                        if (sapInvoice.ASP.PARTY.PARTY_IDFICATION != null)
                        {
                            tempInvoice.AccountingSupplierParty.Party.PartyIdentification = new List<PartyIdentification>();
                            tempInvoice.AccountingSupplierParty.Party.PartyIdentification.Add(new PartyIdentification()
                            {
                                ID = new CombineId() { Id = sapInvoice.ASP.PARTY.PARTY_IDFICATION.ID, SchemeId = sapInvoice.ASP.PARTY.PARTY_IDFICATION.SCHMID }
                            });


                            if (sapInvoice.ASP.PARTY.PARTY_NAME != null)
                            {
                                if (sapInvoice.ASP.PARTY.PARTY_IDFICATION.SCHMID == "TCKN")
                                {
                                    //PERSON ALANI XML UZERINDE OLMADIĞI İÇİN TITLE VERISININ AYNISI BASILIYOR.
                                    tempInvoice.AccountingSupplierParty.Party.Person = new Person() { FamilyName = ".", FirstName = sapInvoice.ASP.PARTY.PARTY_NAME.NAME, };
                                }
                                else
                                {
                                    tempInvoice.AccountingSupplierParty.Party.PartyName = new PartyName() { Name = sapInvoice.ASP.PARTY.PARTY_NAME.NAME };
                                }
                            }
                        }

                        #region PostalAddress

                        if (sapInvoice.ASP.PARTY.POST_ADDR != null)
                        {
                            tempInvoice.AccountingSupplierParty.Party.PostalAddress = new Address();

                            if (!string.IsNullOrEmpty(sapInvoice.ASP.PARTY.POST_ADDR.STREET_NAME))
                            {
                                tempInvoice.AccountingSupplierParty.Party.PostalAddress.StreetName = sapInvoice.ASP.PARTY.POST_ADDR.STREET_NAME;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.ASP.PARTY.POST_ADDR.BUILDG_NAME))
                            {
                                tempInvoice.AccountingSupplierParty.Party.PostalAddress.BuildingName = sapInvoice.ASP.PARTY.POST_ADDR.BUILDG_NAME;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.ASP.PARTY.POST_ADDR.BUILDG_NO))
                            {
                                tempInvoice.AccountingSupplierParty.Party.PostalAddress.BuildingNumber = sapInvoice.ASP.PARTY.POST_ADDR.BUILDG_NO;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.ASP.PARTY.POST_ADDR.CITY_SUBD_NAME))
                            {
                                tempInvoice.AccountingSupplierParty.Party.PostalAddress.CitySubdivisionName = sapInvoice.ASP.PARTY.POST_ADDR.CITY_SUBD_NAME;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.ASP.PARTY.POST_ADDR.CITY_NAME))
                            {
                                tempInvoice.AccountingSupplierParty.Party.PostalAddress.CityName = sapInvoice.ASP.PARTY.POST_ADDR.CITY_NAME;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.ASP.PARTY.POST_ADDR.POST_ZONE))
                            {
                                tempInvoice.AccountingSupplierParty.Party.PostalAddress.PostalZone = sapInvoice.ASP.PARTY.POST_ADDR.POST_ZONE;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.ASP.PARTY.POST_ADDR.REGION))
                            {
                                tempInvoice.AccountingSupplierParty.Party.PostalAddress.Region = sapInvoice.ASP.PARTY.POST_ADDR.REGION;
                            }

                            if (sapInvoice.ASP.PARTY.POST_ADDR.COUNTRY != null)
                            {
                                tempInvoice.AccountingSupplierParty.Party.PostalAddress.Country = new Country();

                                if (!string.IsNullOrEmpty(sapInvoice.ASP.PARTY.POST_ADDR.COUNTRY.COUNTRY_NAME))
                                {
                                    tempInvoice.AccountingSupplierParty.Party.PostalAddress.Country.Name = sapInvoice.ASP.PARTY.POST_ADDR.COUNTRY.COUNTRY_NAME;
                                }
                            }
                        }

                        #endregion

                        #region PartyTaxScheme

                        if (sapInvoice.ASP.PARTY.PARTY_TXSCHM != null)
                        {
                            tempInvoice.AccountingSupplierParty.Party.PartyTaxScheme = new PartyTaxScheme();
                            if (sapInvoice.ASP.PARTY.PARTY_TXSCHM.TXSCHM_NAME != null)
                            {
                                tempInvoice.AccountingSupplierParty.Party.PartyTaxScheme.TaxScheme = new TaxScheme();
                                if (!string.IsNullOrEmpty(sapInvoice.ASP.PARTY.PARTY_TXSCHM.TXSCHM_NAME.NAME))
                                {
                                    tempInvoice.AccountingSupplierParty.Party.PartyTaxScheme.TaxScheme.Name = sapInvoice.ASP.PARTY.PARTY_TXSCHM.TXSCHM_NAME.NAME;
                                }
                            }
                        }

                        #endregion

                        #region Contact

                        if (sapInvoice.ASP.PARTY.CONTACT != null)
                        {
                            tempInvoice.AccountingSupplierParty.Party.Contact = new Contact();
                            if (!string.IsNullOrEmpty(sapInvoice.ASP.PARTY.CONTACT.NOTE))
                            {
                                tempInvoice.AccountingSupplierParty.Party.Contact.Note = sapInvoice.ASP.PARTY.CONTACT.NOTE;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.ASP.PARTY.CONTACT.EMAIL))
                            {
                                tempInvoice.AccountingSupplierParty.Party.Contact.ElectronicMail = sapInvoice.ASP.PARTY.CONTACT.EMAIL;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.ASP.PARTY.CONTACT.TFAX))
                            {
                                tempInvoice.AccountingSupplierParty.Party.Contact.Telefax = sapInvoice.ASP.PARTY.CONTACT.TFAX;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.ASP.PARTY.CONTACT.TPHONE))
                            {
                                tempInvoice.AccountingSupplierParty.Party.Contact.Telephone = sapInvoice.ASP.PARTY.CONTACT.TPHONE;
                            }
                        }

                        #endregion
                    }
                }

                #endregion

                #region AccountingCustomerParty

                if (sapInvoice.ACP != null)
                {
                    tempInvoice.AccountingCustomerParty = new CustomerParty();
                    if (sapInvoice.ACP.PARTY != null)
                    {
                        tempInvoice.AccountingCustomerParty.Party = new Party();

                        if (!string.IsNullOrEmpty(sapInvoice.ACP.PARTY.WEB_URL))
                        {
                            tempInvoice.AccountingCustomerParty.Party.WebSiteURI = sapInvoice.ACP.PARTY.WEB_URL;
                        }

                        if (sapInvoice.ACP.PARTY.PARTY_IDFICATION != null)
                        {
                            tempInvoice.AccountingCustomerParty.Party.PartyIdentification = new List<PartyIdentification>();
                            tempInvoice.AccountingCustomerParty.Party.PartyIdentification.Add(new PartyIdentification()
                            {
                                ID = new CombineId() { Id = sapInvoice.ACP.PARTY.PARTY_IDFICATION.ID, SchemeId = sapInvoice.ACP.PARTY.PARTY_IDFICATION.SCHMID }
                            });
                        }

                        if (sapInvoice.ACP.PARTY.PARTY_NAME != null)
                        {
                            if (sapInvoice.ACP.PARTY.PARTY_IDFICATION.SCHMID == "TCKN")
                            {
                                //PERSON ALANI XML UZERINDE OLMADIĞI İÇİN TITLE VERISININ AYNISI BASILIYOR.
                                tempInvoice.AccountingCustomerParty.Party.Person = new Person() { FamilyName = ".", FirstName = sapInvoice.ACP.PARTY.PARTY_NAME.NAME, };
                            }
                            else
                            {
                                tempInvoice.AccountingCustomerParty.Party.PartyName = new PartyName() { Name = sapInvoice.ACP.PARTY.PARTY_NAME.NAME };
                            }
                        }

                        #region PostalAddress

                        if (sapInvoice.ACP.PARTY.POST_ADDR != null)
                        {
                            tempInvoice.AccountingCustomerParty.Party.PostalAddress = new Address();

                            if (!string.IsNullOrEmpty(sapInvoice.ACP.PARTY.POST_ADDR.STREET_NAME))
                            {
                                tempInvoice.AccountingCustomerParty.Party.PostalAddress.StreetName = sapInvoice.ACP.PARTY.POST_ADDR.STREET_NAME;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.ACP.PARTY.POST_ADDR.BUILDG_NAME))
                            {
                                tempInvoice.AccountingCustomerParty.Party.PostalAddress.BuildingName = sapInvoice.ACP.PARTY.POST_ADDR.BUILDG_NAME;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.ACP.PARTY.POST_ADDR.BUILDG_NO))
                            {
                                tempInvoice.AccountingCustomerParty.Party.PostalAddress.BuildingNumber = sapInvoice.ACP.PARTY.POST_ADDR.BUILDG_NO;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.ACP.PARTY.POST_ADDR.CITY_SUBD_NAME))
                            {
                                tempInvoice.AccountingCustomerParty.Party.PostalAddress.CitySubdivisionName = sapInvoice.ACP.PARTY.POST_ADDR.CITY_SUBD_NAME;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.ACP.PARTY.POST_ADDR.CITY_NAME))
                            {
                                tempInvoice.AccountingCustomerParty.Party.PostalAddress.CityName = sapInvoice.ACP.PARTY.POST_ADDR.CITY_NAME;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.ACP.PARTY.POST_ADDR.POST_ZONE))
                            {
                                tempInvoice.AccountingCustomerParty.Party.PostalAddress.PostalZone = sapInvoice.ACP.PARTY.POST_ADDR.POST_ZONE;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.ACP.PARTY.POST_ADDR.REGION))
                            {
                                tempInvoice.AccountingCustomerParty.Party.PostalAddress.Region = sapInvoice.ACP.PARTY.POST_ADDR.REGION;
                            }

                            if (sapInvoice.ACP.PARTY.POST_ADDR.COUNTRY != null)
                            {
                                tempInvoice.AccountingCustomerParty.Party.PostalAddress.Country = new Country();

                                if (!string.IsNullOrEmpty(sapInvoice.ACP.PARTY.POST_ADDR.COUNTRY.COUNTRY_NAME))
                                {
                                    tempInvoice.AccountingCustomerParty.Party.PostalAddress.Country.Name = sapInvoice.ACP.PARTY.POST_ADDR.COUNTRY.COUNTRY_NAME;
                                }
                            }
                        }

                        #endregion

                        #region PartyTaxScheme

                        if (sapInvoice.ACP.PARTY.PARTY_TXSCHM != null)
                        {
                            tempInvoice.AccountingCustomerParty.Party.PartyTaxScheme = new PartyTaxScheme();
                            if (sapInvoice.ACP.PARTY.PARTY_TXSCHM.TXSCHM_NAME != null)
                            {
                                tempInvoice.AccountingCustomerParty.Party.PartyTaxScheme.TaxScheme = new TaxScheme();
                                if (!string.IsNullOrEmpty(sapInvoice.ACP.PARTY.PARTY_TXSCHM.TXSCHM_NAME.NAME))
                                {
                                    tempInvoice.AccountingCustomerParty.Party.PartyTaxScheme.TaxScheme.Name = sapInvoice.ACP.PARTY.PARTY_TXSCHM.TXSCHM_NAME.NAME;
                                }
                            }
                        }

                        #endregion

                        #region Contact

                        if (sapInvoice.ACP.PARTY.CONTACT != null)
                        {
                            tempInvoice.AccountingCustomerParty.Party.Contact = new Contact();
                            if (!string.IsNullOrEmpty(sapInvoice.ACP.PARTY.CONTACT.NOTE))
                            {
                                tempInvoice.AccountingCustomerParty.Party.Contact.Note = sapInvoice.ACP.PARTY.CONTACT.NOTE;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.ACP.PARTY.CONTACT.EMAIL))
                            {
                                tempInvoice.AccountingCustomerParty.Party.Contact.ElectronicMail = sapInvoice.ACP.PARTY.CONTACT.EMAIL;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.ACP.PARTY.CONTACT.TFAX))
                            {
                                tempInvoice.AccountingCustomerParty.Party.Contact.Telefax = sapInvoice.ACP.PARTY.CONTACT.TFAX;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.ACP.PARTY.CONTACT.TPHONE))
                            {
                                tempInvoice.AccountingCustomerParty.Party.Contact.Telephone = sapInvoice.ACP.PARTY.CONTACT.TPHONE;
                            }
                        }

                        #endregion
                    }
                }

                #endregion

                #region BuyerCustomerParty

                if (sapInvoice.BCP != null && sapInvoice.PROFILE_ID.Equals("IHRACAT"))
                {
                    tempInvoice.BuyerCustomerParty = new CustomerParty();
                    if (sapInvoice.BCP.PARTY != null)
                    {
                        tempInvoice.BuyerCustomerParty.Party = new Party();

                        if (!string.IsNullOrEmpty(sapInvoice.BCP.PARTY.WEB_URL))
                        {
                            tempInvoice.BuyerCustomerParty.Party.WebSiteURI = sapInvoice.BCP.PARTY.WEB_URL;
                        }

                        if (sapInvoice.BCP.PARTY.PARTY_IDFICATION != null)
                        {
                            tempInvoice.BuyerCustomerParty.Party.PartyIdentification = new List<PartyIdentification>();
                            tempInvoice.BuyerCustomerParty.Party.PartyIdentification.Add(new PartyIdentification()
                            {
                                ID = new CombineId() { Id = sapInvoice.BCP.PARTY.PARTY_IDFICATION.ID, SchemeId = sapInvoice.BCP.PARTY.PARTY_IDFICATION.SCHMID }
                            });
                        }

                        if (sapInvoice.BCP.PARTY.PARTY_NAME != null)
                        {
                            if (sapInvoice.BCP.PARTY.PARTY_IDFICATION.SCHMID == "TCKN")
                            {
                                //PERSON ALANI XML UZERINDE OLMADIĞI İÇİN TITLE VERISININ AYNISI BASILIYOR.
                                tempInvoice.BuyerCustomerParty.Party.Person = new Person() { FamilyName = ".", FirstName = sapInvoice.BCP.PARTY.PARTY_NAME.NAME, };
                            }
                            else
                            {
                                tempInvoice.BuyerCustomerParty.Party.PartyName = new PartyName() { Name = sapInvoice.BCP.PARTY.PARTY_NAME.NAME };
                            }
                        }

                        #region PostalAddress

                        if (sapInvoice.BCP.PARTY.POST_ADDR != null)
                        {
                            tempInvoice.BuyerCustomerParty.Party.PostalAddress = new Address();

                            if (!string.IsNullOrEmpty(sapInvoice.BCP.PARTY.POST_ADDR.STREET_NAME))
                            {
                                tempInvoice.BuyerCustomerParty.Party.PostalAddress.StreetName = sapInvoice.BCP.PARTY.POST_ADDR.STREET_NAME;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.BCP.PARTY.POST_ADDR.BUILDG_NAME))
                            {
                                tempInvoice.BuyerCustomerParty.Party.PostalAddress.BuildingName = sapInvoice.BCP.PARTY.POST_ADDR.BUILDG_NAME;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.BCP.PARTY.POST_ADDR.BUILDG_NO))
                            {
                                tempInvoice.BuyerCustomerParty.Party.PostalAddress.BuildingNumber = sapInvoice.BCP.PARTY.POST_ADDR.BUILDG_NO;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.BCP.PARTY.POST_ADDR.CITY_SUBD_NAME))
                            {
                                tempInvoice.BuyerCustomerParty.Party.PostalAddress.CitySubdivisionName = sapInvoice.BCP.PARTY.POST_ADDR.CITY_SUBD_NAME;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.BCP.PARTY.POST_ADDR.CITY_NAME))
                            {
                                tempInvoice.BuyerCustomerParty.Party.PostalAddress.CityName = sapInvoice.BCP.PARTY.POST_ADDR.CITY_NAME;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.BCP.PARTY.POST_ADDR.POST_ZONE))
                            {
                                tempInvoice.BuyerCustomerParty.Party.PostalAddress.PostalZone = sapInvoice.BCP.PARTY.POST_ADDR.POST_ZONE;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.BCP.PARTY.POST_ADDR.REGION))
                            {
                                tempInvoice.BuyerCustomerParty.Party.PostalAddress.Region = sapInvoice.BCP.PARTY.POST_ADDR.REGION;
                            }

                            if (sapInvoice.BCP.PARTY.POST_ADDR.COUNTRY != null)
                            {
                                tempInvoice.BuyerCustomerParty.Party.PostalAddress.Country = new Country();

                                if (!string.IsNullOrEmpty(sapInvoice.BCP.PARTY.POST_ADDR.COUNTRY.COUNTRY_NAME))
                                {
                                    tempInvoice.BuyerCustomerParty.Party.PostalAddress.Country.Name = sapInvoice.BCP.PARTY.POST_ADDR.COUNTRY.COUNTRY_NAME;
                                }
                            }
                        }

                        #endregion

                        #region PartyTaxScheme

                        if (sapInvoice.BCP.PARTY.PARTY_TXSCHM != null)
                        {
                            tempInvoice.BuyerCustomerParty.Party.PartyTaxScheme = new PartyTaxScheme();
                            if (sapInvoice.BCP.PARTY.PARTY_TXSCHM.TXSCHM_NAME != null)
                            {
                                tempInvoice.BuyerCustomerParty.Party.PartyTaxScheme.TaxScheme = new TaxScheme();
                                if (!string.IsNullOrEmpty(sapInvoice.BCP.PARTY.PARTY_TXSCHM.TXSCHM_NAME.NAME))
                                {
                                    tempInvoice.BuyerCustomerParty.Party.PartyTaxScheme.TaxScheme.Name = sapInvoice.BCP.PARTY.PARTY_TXSCHM.TXSCHM_NAME.NAME;
                                }
                            }
                        }

                        #endregion

                        #region Contact

                        if (sapInvoice.BCP.PARTY.CONTACT != null)
                        {
                            tempInvoice.BuyerCustomerParty.Party.Contact = new Contact();
                            if (!string.IsNullOrEmpty(sapInvoice.BCP.PARTY.CONTACT.NOTE))
                            {
                                tempInvoice.BuyerCustomerParty.Party.Contact.Note = sapInvoice.BCP.PARTY.CONTACT.NOTE;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.BCP.PARTY.CONTACT.EMAIL))
                            {
                                tempInvoice.BuyerCustomerParty.Party.Contact.ElectronicMail = sapInvoice.BCP.PARTY.CONTACT.EMAIL;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.BCP.PARTY.CONTACT.TFAX))
                            {
                                tempInvoice.BuyerCustomerParty.Party.Contact.Telefax = sapInvoice.BCP.PARTY.CONTACT.TFAX;
                            }

                            if (!string.IsNullOrEmpty(sapInvoice.BCP.PARTY.CONTACT.TPHONE))
                            {
                                tempInvoice.BuyerCustomerParty.Party.Contact.Telephone = sapInvoice.BCP.PARTY.CONTACT.TPHONE;
                            }
                        }

                        #endregion
                    }

                    if (sapInvoice.BCP.PARTYLEGALENTITY != null)
                    {
                        if (tempInvoice.BuyerCustomerParty.Party == null) tempInvoice.BuyerCustomerParty.Party = new Party();

                        tempInvoice.BuyerCustomerParty.Party.PartyLegalEntities = new List<PartyLegalEntity>()
                        {
                            new PartyLegalEntity() { CompanyID = sapInvoice.BCP.PARTYLEGALENTITY.COMPANYID, RegistrationName = sapInvoice.BCP.PARTYLEGALENTITY.REGISTRATIONNAME, }
                        };
                    }
                }

                #endregion

                #region AllowanceCharge

                if (sapInvoice.ALWNC_CHRG != null)
                {
                    if (sapInvoice.ALWNC_CHRG.AMNT != null && sapInvoice.ALWNC_CHRG.AMNT.AMNT != 0)
                    {
                        if (tempInvoice.AllowanceCharges == null)
                        {
                            tempInvoice.AllowanceCharges = new List<AllowanceCharge>();
                            tempInvoice.AllowanceCharges.Add(new AllowanceCharge());
                        }

                        tempInvoice.AllowanceCharges[0].Amount = new UblBaseCurrency();
                        if (!string.IsNullOrEmpty(sapInvoice.ALWNC_CHRG.AMNT.CURR_ID))
                        {
                            tempInvoice.AllowanceCharges[0].Amount.CurrencyID = sapInvoice.ALWNC_CHRG.AMNT.CURR_ID == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TrlCurrency : sapInvoice.ALWNC_CHRG.AMNT.CURR_ID;
                        }

                        if (sapInvoice.ALWNC_CHRG.AMNT.AMNT != 0)
                        {
                            tempInvoice.AllowanceCharges[0].Amount.Value = sapInvoice.ALWNC_CHRG.AMNT.AMNT;
                        }
                    }

                    if (!string.IsNullOrEmpty(sapInvoice.ALWNC_CHRG.CHRG_INDICATOR) && tempInvoice.AllowanceCharges[0] != null)
                    {
                        tempInvoice.AllowanceCharges[0].ChargeIndicator = bool.Parse(sapInvoice.ALWNC_CHRG.CHRG_INDICATOR);
                    }
                }

                #endregion

                #region PricingExchangeRate

                if (sapInvoice.PRIC_EXCHNGRATE != null && sapInvoice.DOCU_CURR_CODE != UblTr2HandlerEInvoice.TryCurrency)
                {
                    tempInvoice.PricingExchangeRate = new ExchangeRate();

                    if (!string.IsNullOrEmpty(sapInvoice.PRIC_EXCHNGRATE.SRCE_CURRCODE))
                    {
                        tempInvoice.PricingExchangeRate.SourceCurrencyCode = sapInvoice.PRIC_EXCHNGRATE.SRCE_CURRCODE;
                    }

                    if (!string.IsNullOrEmpty(sapInvoice.PRIC_EXCHNGRATE.TRGT_CURRCODE))
                    {
                        tempInvoice.PricingExchangeRate.TargetCurrencyCode = sapInvoice.PRIC_EXCHNGRATE.TRGT_CURRCODE;
                    }


                    tempInvoice.PricingExchangeRate.CalculationRate = Convert.ToDouble(sapInvoice.PRIC_EXCHNGRATE.EXCHANGE_RATE);
                }

                #endregion

                #region TaxTotals

                if (sapInvoice.TAX_TOTAL != null)
                {
                    tempInvoice.TaxTotals = new List<TaxTotal>();
                    foreach (var item in sapInvoice.TAX_TOTAL)
                    {
                        if (item.TAX_AMNT != null)
                        {
                            var taxTotal = new TaxTotal()
                            {
                                TaxAmount = new UblBaseCurrency() { Value = item.TAX_AMNT.AMNT, CurrencyID = item.TAX_AMNT.CURR_ID == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TrlCurrency : item.TAX_AMNT.CURR_ID }
                            };

                            taxTotal.TaxSubtotals = new List<TaxSubtotal>();
                            foreach (var tSubTotalItem in item.TAX_SUBTOT)
                            {
                                var tsTotal = new TaxSubtotal();
                                if (tSubTotalItem.EINV_TXSUBTOT_TR != null)
                                {
                                    tsTotal.Percent = tSubTotalItem.EINV_TXSUBTOT_TR.PERCENT;


                                    if (tSubTotalItem.EINV_TXSUBTOT_TR.TAX_AMNT != null)
                                    {
                                        tsTotal.TaxAmount = new UblBaseCurrency()
                                        {
                                            CurrencyID = tSubTotalItem.EINV_TXSUBTOT_TR.TAX_AMNT.CURR_ID == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TrlCurrency : tSubTotalItem.EINV_TXSUBTOT_TR.TAX_AMNT.CURR_ID,
                                            Value = tSubTotalItem.EINV_TXSUBTOT_TR.TAX_AMNT.AMNT
                                        };
                                    }

                                    if (tSubTotalItem.EINV_TXSUBTOT_TR.TAX_CATEGRY != null && tSubTotalItem.EINV_TXSUBTOT_TR.TAX_CATEGRY.TAX_SCHM != null)
                                    {
                                        tsTotal.TaxCategory = new TaxCategory();

                                        tsTotal.TaxCategory.TaxScheme = new TaxScheme();
                                        if (!string.IsNullOrEmpty(tSubTotalItem.EINV_TXSUBTOT_TR.TAX_CATEGRY.TAX_SCHM.TXSCHM_NAME))
                                        {
                                            tsTotal.TaxCategory.TaxScheme.Name = tSubTotalItem.EINV_TXSUBTOT_TR.TAX_CATEGRY.TAX_SCHM.TXSCHM_NAME;
                                        }

                                        if (!string.IsNullOrEmpty(tSubTotalItem.EINV_TXSUBTOT_TR.TAX_CATEGRY.TAX_SCHM.TXTYP_CODE))
                                        {
                                            tsTotal.TaxCategory.TaxScheme.TaxTypeCode = tSubTotalItem.EINV_TXSUBTOT_TR.TAX_CATEGRY.TAX_SCHM.TXTYP_CODE;

                                            //KDV MUAFIYETI ICIN
                                            if (tsTotal.TaxAmount.Value == 0 && tsTotal.TaxCategory.TaxScheme.TaxTypeCode == Tax.TaxType_0015.GetCode())
                                            {
                                                //MUAFIYET SEBEBI GIRILMESI
                                                tsTotal.TaxCategory.TaxExemptionReasonCode = tSubTotalItem.EINV_TXSUBTOT_TR.TAX_CATEGRY.TAX_EX_REASON_CODE;
                                                tsTotal.TaxCategory.TaxExemptionReason = tSubTotalItem.EINV_TXSUBTOT_TR.TAX_CATEGRY.TAX_EX_REASON;
                                            }
                                        }
                                    }

                                    if (tSubTotalItem.EINV_TXSUBTOT_TR.TAXABLE_AMNT != null)
                                    {
                                        tsTotal.TaxableAmount = new UblBaseCurrency();
                                        if (!string.IsNullOrEmpty(tSubTotalItem.EINV_TXSUBTOT_TR.TAXABLE_AMNT.CURR_ID))
                                        {
                                            tsTotal.TaxableAmount.CurrencyID = tSubTotalItem.EINV_TXSUBTOT_TR.TAXABLE_AMNT.CURR_ID == UblTr2HandlerEInvoice.TrlCurrency
                                                ? UblTr2HandlerEInvoice.TrlCurrency
                                                : tSubTotalItem.EINV_TXSUBTOT_TR.TAXABLE_AMNT.CURR_ID;
                                        }

                                        if (tSubTotalItem.EINV_TXSUBTOT_TR.TAXABLE_AMNT.AMNT != 0)
                                        {
                                            tsTotal.TaxableAmount.Value = tSubTotalItem.EINV_TXSUBTOT_TR.TAXABLE_AMNT.AMNT;
                                        }
                                    }
                                }

                                taxTotal.TaxSubtotals.Add(tsTotal);
                            }

                            tempInvoice.TaxTotals.Add(taxTotal);
                        }
                    }
                }

                #endregion

                #region LegalMonatoryTotal

                if (sapInvoice.LMT != null)
                {
                    tempInvoice.LegalMonetaryTotal = new MonetaryTotal();
                    if (sapInvoice.LMT.ALWNC_TOT_AMNT != null)
                    {
                        tempInvoice.LegalMonetaryTotal.AllowanceTotalAmount = new UblBaseCurrency()
                        {
                            CurrencyID = sapInvoice.LMT.ALWNC_TOT_AMNT.CURR_ID == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TrlCurrency : sapInvoice.LMT.ALWNC_TOT_AMNT.CURR_ID, Value = sapInvoice.LMT.ALWNC_TOT_AMNT.AMNT
                        };
                    }

                    if (sapInvoice.LMT.CHRG_TOT_AMNT != null)
                    {
                        tempInvoice.LegalMonetaryTotal.ChargeTotalAmount = new UblBaseCurrency()
                        {
                            CurrencyID = sapInvoice.LMT.CHRG_TOT_AMNT.CURR_ID == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TrlCurrency : sapInvoice.LMT.CHRG_TOT_AMNT.CURR_ID, Value = sapInvoice.LMT.CHRG_TOT_AMNT.AMNT
                        };
                    }

                    if (sapInvoice.LMT.LINE_EXTN_AMNT != null)
                    {
                        tempInvoice.LegalMonetaryTotal.LineExtensionAmount = new UblBaseCurrency()
                        {
                            CurrencyID = sapInvoice.LMT.LINE_EXTN_AMNT.CURR_ID == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TrlCurrency : sapInvoice.LMT.LINE_EXTN_AMNT.CURR_ID, Value = sapInvoice.LMT.LINE_EXTN_AMNT.AMNT
                        };
                    }

                    if (sapInvoice.LMT.PAYABLE_AMNT != null)
                    {
                        tempInvoice.LegalMonetaryTotal.PayableAmount = new UblBaseCurrency()
                        {
                            CurrencyID = sapInvoice.LMT.PAYABLE_AMNT.CURR_ID == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TrlCurrency : sapInvoice.LMT.PAYABLE_AMNT.CURR_ID, Value = sapInvoice.LMT.PAYABLE_AMNT.AMNT
                        };
                    }

                    if (sapInvoice.LMT.TX_EXCL_AMNT != null)
                    {
                        tempInvoice.LegalMonetaryTotal.TaxExclusiveAmount = new UblBaseCurrency()
                        {
                            CurrencyID = sapInvoice.LMT.TX_EXCL_AMNT.CURR_ID == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TrlCurrency : sapInvoice.LMT.TX_EXCL_AMNT.CURR_ID, Value = sapInvoice.LMT.TX_EXCL_AMNT.AMNT
                        };
                    }

                    if (sapInvoice.LMT.TX_INCL_AMNT != null)
                    {
                        tempInvoice.LegalMonetaryTotal.TaxInclusiveAmount = new UblBaseCurrency()
                        {
                            CurrencyID = sapInvoice.LMT.TX_INCL_AMNT.CURR_ID == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TrlCurrency : sapInvoice.LMT.TX_INCL_AMNT.CURR_ID, Value = sapInvoice.LMT.TX_INCL_AMNT.AMNT
                        };
                    }
                }

                #endregion

                #region InvoiceLine

                if (sapInvoice.INVOICE_LINE != null && sapInvoice.INVOICE_LINE.EINV_INV_LINE_TR != null && sapInvoice.INVOICE_LINE.EINV_INV_LINE_TR.Count > 0)
                {
                    tempInvoice.InvoiceLines = new List<InvoiceLine>();
                    foreach (var line in sapInvoice.INVOICE_LINE.EINV_INV_LINE_TR)
                    {
                        var invoiceLine = new InvoiceLine();

                        invoiceLine.ID = new CombineId() { Id = line.ID };

                        if (!string.IsNullOrEmpty(line.NOTE))
                        {
                            invoiceLine.Notes = new List<string>() { line.NOTE };
                        }

                        invoiceLine.InvoicedQuantity = new BaseUnit() { UnitCode = line.INVOICED_QTY.QTY_UNIT, Value = line.INVOICED_QTY.INV_QTY };

                        if (line.LINE_EXTN_AMNT != null)
                        {
                            invoiceLine.LineExtensionAmount = new UblBaseCurrency()
                            {
                                CurrencyID = line.LINE_EXTN_AMNT.CURR_ID == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TrlCurrency : line.LINE_EXTN_AMNT.CURR_ID, Value = line.LINE_EXTN_AMNT.AMNT
                            };
                        }

                        #region DeliveryLine

                        if (line.DELIVERY != null && sapInvoice.PROFILE_ID.Equals("IHRACAT"))
                        {
                            invoiceLine.Deliveries = new List<Delivery>() { new Delivery() };

                            if (line.DELIVERY.DELIVERYADDRESS != null && line.DELIVERY.DELIVERYADDRESS.PARTY != null && line.DELIVERY.DELIVERYADDRESS.PARTY.POST_ADDR != null)
                            {
                                invoiceLine.Deliveries[0].DeliveryAddress = new Address();

                                #region addressInfo

                                if (!string.IsNullOrEmpty(line.DELIVERY.DELIVERYADDRESS.PARTY.POST_ADDR.STREET_NAME))
                                {
                                    invoiceLine.Deliveries[0].DeliveryAddress.StreetName = line.DELIVERY.DELIVERYADDRESS.PARTY.POST_ADDR.STREET_NAME;
                                }

                                if (!string.IsNullOrEmpty(line.DELIVERY.DELIVERYADDRESS.PARTY.POST_ADDR.BUILDG_NAME))
                                {
                                    invoiceLine.Deliveries[0].DeliveryAddress.BuildingName = line.DELIVERY.DELIVERYADDRESS.PARTY.POST_ADDR.BUILDG_NAME;
                                }

                                if (!string.IsNullOrEmpty(line.DELIVERY.DELIVERYADDRESS.PARTY.POST_ADDR.BUILDG_NO))
                                {
                                    invoiceLine.Deliveries[0].DeliveryAddress.BuildingNumber = line.DELIVERY.DELIVERYADDRESS.PARTY.POST_ADDR.BUILDG_NO;
                                }

                                if (!string.IsNullOrEmpty(line.DELIVERY.DELIVERYADDRESS.PARTY.POST_ADDR.CITY_SUBD_NAME))
                                {
                                    invoiceLine.Deliveries[0].DeliveryAddress.CitySubdivisionName = line.DELIVERY.DELIVERYADDRESS.PARTY.POST_ADDR.CITY_SUBD_NAME;
                                }

                                if (!string.IsNullOrEmpty(line.DELIVERY.DELIVERYADDRESS.PARTY.POST_ADDR.CITY_NAME))
                                {
                                    invoiceLine.Deliveries[0].DeliveryAddress.CityName = line.DELIVERY.DELIVERYADDRESS.PARTY.POST_ADDR.CITY_NAME;
                                }

                                if (!string.IsNullOrEmpty(line.DELIVERY.DELIVERYADDRESS.PARTY.POST_ADDR.POST_ZONE))
                                {
                                    invoiceLine.Deliveries[0].DeliveryAddress.PostalZone = line.DELIVERY.DELIVERYADDRESS.PARTY.POST_ADDR.POST_ZONE;
                                }

                                if (!string.IsNullOrEmpty(line.DELIVERY.DELIVERYADDRESS.PARTY.POST_ADDR.REGION))
                                {
                                    invoiceLine.Deliveries[0].DeliveryAddress.Region = line.DELIVERY.DELIVERYADDRESS.PARTY.POST_ADDR.REGION;
                                }

                                if (line.DELIVERY.DELIVERYADDRESS.PARTY.POST_ADDR.COUNTRY != null)
                                {
                                    invoiceLine.Deliveries[0].DeliveryAddress.Country = new Country();

                                    if (!string.IsNullOrEmpty(line.DELIVERY.DELIVERYADDRESS.PARTY.POST_ADDR.COUNTRY.COUNTRY_NAME))
                                    {
                                        invoiceLine.Deliveries[0].DeliveryAddress.Country.Name = line.DELIVERY.DELIVERYADDRESS.PARTY.POST_ADDR.COUNTRY.COUNTRY_NAME;
                                    }
                                }

                                #endregion
                            }

                            if (line.DELIVERY.DELIVERYTERMS != null)
                            {
                                invoiceLine.Deliveries[0].DeliveryTerms = new List<DeliveryTerms>() { new DeliveryTerms() { ID = new CombineId() { Id = line.DELIVERY.DELIVERYTERMS.INCOTERMS, SchemeId = "INCOTERMS" } } };
                            }

                            if (line.DELIVERY.SHIPMENT != null)
                            {
                                invoiceLine.Deliveries[0].Shipment = new Shipment();

                                if (line.DELIVERY.SHIPMENT.GOODSITEM != null)
                                {
                                    invoiceLine.Deliveries[0].Shipment.GoodsItems = new List<GoodsItem>() { new GoodsItem() };

                                    invoiceLine.Deliveries[0].Shipment.GoodsItems[0].RequiredCustomsID = line.DELIVERY.SHIPMENT.GOODSITEM.REQUIREDCUSTOMSID.PadLeft(12, '0');

                                    if (!string.IsNullOrEmpty(line.DELIVERY.SHIPMENT.GOODSITEM.WEIGHTMEASURE_UNIT))
                                    {
                                        invoiceLine.Deliveries[0].Shipment.GoodsItems[0].GrossWeightMeasure = new BaseUnit()
                                        {
                                            Value = line.DELIVERY.SHIPMENT.GOODSITEM.GROSSWEIGHTMEASURE, UnitCode = line.DELIVERY.SHIPMENT.GOODSITEM.WEIGHTMEASURE_UNIT
                                        };

                                        invoiceLine.Deliveries[0].Shipment.GoodsItems[0].NetWeightMeasure = new BaseUnit()
                                        {
                                            Value = line.DELIVERY.SHIPMENT.GOODSITEM.NETWEIGHTMEASURE, UnitCode = line.DELIVERY.SHIPMENT.GOODSITEM.WEIGHTMEASURE_UNIT
                                        };
                                    }

                                    if (!string.IsNullOrEmpty(line.DELIVERY.SHIPMENT.GOODSITEM.AMOUNT_CURRENCY))
                                    {
                                        invoiceLine.Deliveries[0].Shipment.GoodsItems[0].InsuranceValueAmount = new UblBaseCurrency()
                                        {
                                            Value = line.DELIVERY.SHIPMENT.GOODSITEM.INSURANCEVALUEAMOUNT, CurrencyID = line.DELIVERY.SHIPMENT.GOODSITEM.AMOUNT_CURRENCY
                                        };

                                        invoiceLine.Deliveries[0].Shipment.GoodsItems[0].FreeOnBoardValueAmount = new UblBaseCurrency()
                                        {
                                            Value = line.DELIVERY.SHIPMENT.GOODSITEM.FREEONBOARDVALUEAMOUNT, CurrencyID = line.DELIVERY.SHIPMENT.GOODSITEM.AMOUNT_CURRENCY
                                        };
                                    }
                                }

                                if (line.DELIVERY.SHIPMENT.SHIPMENTSTAGE != null)
                                {
                                    invoiceLine.Deliveries[0].Shipment.ShipmentStages = new List<ShipmentStage>() { new ShipmentStage() { TransportModeCode = line.DELIVERY.SHIPMENT.SHIPMENTSTAGE.TRANSPORTMODECODE } };
                                }

                                if (line.DELIVERY.SHIPMENT.TRANSPORTHANDLINGUNIT != null)
                                {
                                    invoiceLine.Deliveries[0].Shipment.TransportHandlingUnits = new List<TransportHandlingUnit>() { new TransportHandlingUnit() };
                                    if (line.DELIVERY.SHIPMENT.TRANSPORTHANDLINGUNIT.ACTUALPACKAGE != null)
                                    {
                                        invoiceLine.Deliveries[0].Shipment.TransportHandlingUnits[0].ActualPackages = new List<Package>() { new Package() };

                                        if (!string.IsNullOrEmpty(line.DELIVERY.SHIPMENT.TRANSPORTHANDLINGUNIT.ACTUALPACKAGE.ID))
                                        {
                                            invoiceLine.Deliveries[0].Shipment.TransportHandlingUnits[0].ActualPackages[0].ID = new CombineId() { Id = line.DELIVERY.SHIPMENT.TRANSPORTHANDLINGUNIT.ACTUALPACKAGE.ID };
                                        }

                                        invoiceLine.Deliveries[0].Shipment.TransportHandlingUnits[0].ActualPackages[0].Quantity = new BaseUnit() { Value = line.DELIVERY.SHIPMENT.TRANSPORTHANDLINGUNIT.ACTUALPACKAGE.QUANTITY };

                                        if (!string.IsNullOrEmpty(line.DELIVERY.SHIPMENT.TRANSPORTHANDLINGUNIT.ACTUALPACKAGE.PACKAGINGTYPECODE))
                                        {
                                            invoiceLine.Deliveries[0].Shipment.TransportHandlingUnits[0].ActualPackages[0].PackagingTypeCode = line.DELIVERY.SHIPMENT.TRANSPORTHANDLINGUNIT.ACTUALPACKAGE.PACKAGINGTYPECODE;
                                        }
                                        else
                                        {
                                            invoiceLine.Deliveries[0].Shipment.TransportHandlingUnits[0].ActualPackages[0].PackagingTypeCode = line.DELIVERY.SHIPMENT.TRANSPORTHANDLINGUNIT.ACTUALPACKAGE.QUANTITY_UNIT;
                                        }
                                    }
                                }
                            }
                        }

                        #endregion

                        #region LineAllowanceCharge

                        if (line.ALWNC_CHRG != null)
                        {
                            if (line.ALWNC_CHRG.AMNT != null && line.ALWNC_CHRG.AMNT.AMNT != 0)
                            {
                                if (invoiceLine.AllowanceCharges == null)
                                {
                                    invoiceLine.AllowanceCharges = new List<AllowanceCharge>();
                                    invoiceLine.AllowanceCharges.Add(new AllowanceCharge());
                                }

                                invoiceLine.AllowanceCharges[0].Amount = new UblBaseCurrency();
                                if (!string.IsNullOrEmpty(line.ALWNC_CHRG.AMNT.CURR_ID))
                                {
                                    invoiceLine.AllowanceCharges[0].Amount.CurrencyID = line.ALWNC_CHRG.AMNT.CURR_ID == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TrlCurrency : line.ALWNC_CHRG.AMNT.CURR_ID;
                                }

                                if (line.ALWNC_CHRG.AMNT.AMNT != 0)
                                {
                                    invoiceLine.AllowanceCharges[0].Amount.Value = line.ALWNC_CHRG.AMNT.AMNT;
                                }
                            }

                            if (!string.IsNullOrEmpty(line.ALWNC_CHRG.CHRG_INDICATOR) && invoiceLine.AllowanceCharges[0] != null)
                            {
                                invoiceLine.AllowanceCharges[0].ChargeIndicator = bool.Parse(line.ALWNC_CHRG.CHRG_INDICATOR);
                            }
                        }

                        #endregion

                        #region TaxTotal

                        if (line.TAX_TOTAL != null && line.TAX_TOTAL.TAX_AMNT != null)
                        {
                            var taxTotal = new TaxTotal()
                            {
                                TaxAmount = new UblBaseCurrency()
                                {
                                    Value = line.TAX_TOTAL.TAX_AMNT.AMNT, CurrencyID = line.TAX_TOTAL.TAX_AMNT.CURR_ID == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TrlCurrency : line.TAX_TOTAL.TAX_AMNT.CURR_ID
                                }
                            };

                            taxTotal.TaxSubtotals = new List<TaxSubtotal>();

                            foreach (var tSubTot in line.TAX_TOTAL.TAX_SUBTOT)
                            {
                                var tsTotal = new TaxSubtotal();
                                if (tSubTot.EINV_TXSUBTOT_TR != null)
                                {
                                    tsTotal.Percent = tSubTot.EINV_TXSUBTOT_TR.PERCENT;


                                    if (tSubTot.EINV_TXSUBTOT_TR.TAX_AMNT != null)
                                    {
                                        tsTotal.TaxAmount = new UblBaseCurrency()
                                        {
                                            CurrencyID = tSubTot.EINV_TXSUBTOT_TR.TAX_AMNT.CURR_ID == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TrlCurrency : tSubTot.EINV_TXSUBTOT_TR.TAX_AMNT.CURR_ID,
                                            Value = tSubTot.EINV_TXSUBTOT_TR.TAX_AMNT.AMNT
                                        };
                                    }

                                    if (tSubTot.EINV_TXSUBTOT_TR.TAX_CATEGRY != null && tSubTot.EINV_TXSUBTOT_TR.TAX_CATEGRY.TAX_SCHM != null)
                                    {
                                        tsTotal.TaxCategory = new TaxCategory();
                                        tsTotal.TaxCategory.TaxScheme = new TaxScheme();
                                        if (!string.IsNullOrEmpty(tSubTot.EINV_TXSUBTOT_TR.TAX_CATEGRY.TAX_SCHM.TXSCHM_NAME))
                                        {
                                            tsTotal.TaxCategory.TaxScheme.Name = tSubTot.EINV_TXSUBTOT_TR.TAX_CATEGRY.TAX_SCHM.TXSCHM_NAME;
                                        }

                                        if (!string.IsNullOrEmpty(tSubTot.EINV_TXSUBTOT_TR.TAX_CATEGRY.TAX_SCHM.TXTYP_CODE))
                                        {
                                            tsTotal.TaxCategory.TaxScheme.TaxTypeCode = tSubTot.EINV_TXSUBTOT_TR.TAX_CATEGRY.TAX_SCHM.TXTYP_CODE;

                                            //KDV MUAFIYETI ICIN
                                            if (tsTotal.TaxAmount.Value == 0 && tsTotal.TaxCategory.TaxScheme.TaxTypeCode == Tax.TaxType_0015.GetCode())
                                            {
                                                //MUAFIYET SEBEBI GIRILMESI
                                                tsTotal.TaxCategory.TaxExemptionReasonCode = tSubTot.EINV_TXSUBTOT_TR.TAX_CATEGRY.TAX_EX_REASON_CODE;
                                                tsTotal.TaxCategory.TaxExemptionReason = tSubTot.EINV_TXSUBTOT_TR.TAX_CATEGRY.TAX_EX_REASON;
                                            }
                                        }
                                    }

                                    if (tSubTot.EINV_TXSUBTOT_TR.TAXABLE_AMNT != null)
                                    {
                                        tsTotal.TaxableAmount = new UblBaseCurrency();
                                        if (!string.IsNullOrEmpty(tSubTot.EINV_TXSUBTOT_TR.TAXABLE_AMNT.CURR_ID))
                                        {
                                            tsTotal.TaxableAmount.CurrencyID = tSubTot.EINV_TXSUBTOT_TR.TAXABLE_AMNT.CURR_ID == UblTr2HandlerEInvoice.TrlCurrency
                                                ? UblTr2HandlerEInvoice.TrlCurrency
                                                : tSubTot.EINV_TXSUBTOT_TR.TAXABLE_AMNT.CURR_ID;
                                        }

                                        if (tSubTot.EINV_TXSUBTOT_TR.TAXABLE_AMNT.AMNT != 0)
                                        {
                                            tsTotal.TaxableAmount.Value = tSubTot.EINV_TXSUBTOT_TR.TAXABLE_AMNT.AMNT;
                                        }
                                    }
                                }

                                taxTotal.TaxSubtotals.Add(tsTotal);
                            }

                            invoiceLine.TaxTotal = taxTotal;
                        }

                        #endregion

                        invoiceLine.Item = new Item()
                        {
                            Name = line.ITEM.NAME, Description = line.ITEM.DESCRIPTION, BrandName = line.ITEM.BRAND_NAME, ModelName = line.ITEM.MODELL_NAME,
                        };

                        if (line.PRICE != null && line.PRICE.PRICE_AMNT != null)
                        {
                            invoiceLine.Price = new Price();
                            invoiceLine.Price.PriceAmount = new UblBaseCurrency();
                            invoiceLine.Price.PriceAmount.CurrencyID = line.PRICE.PRICE_AMNT.CURR_ID == UblTr2HandlerEInvoice.TrlCurrency ? UblTr2HandlerEInvoice.TrlCurrency : line.PRICE.PRICE_AMNT.CURR_ID;
                            invoiceLine.Price.PriceAmount.Value = line.PRICE.PRICE_AMNT.AMNT;
                        }

                        tempInvoice.InvoiceLines.Add(invoiceLine);
                    }
                }

                #endregion

                TransferInvoiceDataSerializer iSerializer = new TransferInvoiceDataSerializer();
                convertedXmlContent = iSerializer.SerializeAndGetXmlContent(tempInvoice, true, true);

                return !string.IsNullOrWhiteSpace(convertedXmlContent);
            }
            catch (Exception ex)
            {
                convertedXmlContent = ex.Message;
            }

            return false;
        }

        private bool SAPDocumentToDespatchUBLTRXmlContent(string sapXmlContent, out string convertedXmlContent)
        {
            convertedXmlContent = null;

            try
            {
                SapDocumentSerializer serializer = new SapDocumentSerializer();
                VeribanGlobal.Library.Model.SapDocument.SapEnvelopeDocumentModel sapDocument = serializer.DeserializeFromXmlContent(sapXmlContent);
                var sapDespatch = sapDocument.Values.STR_DATA.DESPATCHADVICE;

                DespatchAdviceModel tempDespatchAdviceModel = new DespatchAdviceModel();

                #region Header

                if (sapDespatch.CUSTOM_ID != UblTr2HandlerEInvoice.CustomizationVersion)
                {
                    tempDespatchAdviceModel.CustomizationID = UblTr2HandlerEInvoice.CustomizationVersion;
                }
                else
                {
                    tempDespatchAdviceModel.CustomizationID = sapDespatch.CUSTOM_ID;
                }

                tempDespatchAdviceModel.UBLVersionID = sapDespatch.UBLVERS_ID;
                tempDespatchAdviceModel.ProfileID = sapDespatch.PROFILE_ID;
                tempDespatchAdviceModel.ID = sapDespatch.ID;
                tempDespatchAdviceModel.CopyIndicator = sapDespatch.COPY_INDICATOR;
                tempDespatchAdviceModel.UUID = sapDespatch.UUID;
                tempDespatchAdviceModel.IssueDate = sapDespatch.ISSUE_DATE;
                tempDespatchAdviceModel.IssueTime = sapDespatch.ISSUE_TIME;
                tempDespatchAdviceModel.DespatchAdviceTypeCode = sapDespatch.DEL_TYP_CODE;

                if (sapDespatch.NOTE != null)
                {
                    tempDespatchAdviceModel.Notes = new List<string>();

                    foreach (var item in sapDespatch.NOTE.ITEM)
                    {
                        tempDespatchAdviceModel.Notes.Add(item);
                    }
                }

                #endregion

                #region OrderReference

                if (sapDespatch.ORDER_REFERENCE != null)
                {
                    tempDespatchAdviceModel.OrderReference = new OrderReference();

                    if (!string.IsNullOrEmpty(sapDespatch.ORDER_REFERENCE.ID))
                    {
                        tempDespatchAdviceModel.OrderReference.ID = new CombineId { Id = sapDespatch.ORDER_REFERENCE.ID };
                    }

                    if (!string.IsNullOrEmpty(sapDespatch.ORDER_REFERENCE.ISSUEDATE))
                    {
                        tempDespatchAdviceModel.OrderReference.IssueDate = sapDespatch.ORDER_REFERENCE.ISSUEDATE;
                    }
                }

                #endregion

                #region AccountingSupplierParty

                if (sapDespatch.ASP != null)
                {
                    tempDespatchAdviceModel.DespatchSupplierParty = new SupplierParty();
                    if (sapDespatch.ASP.PARTY != null)
                    {
                        tempDespatchAdviceModel.DespatchSupplierParty.Party = new Party();

                        if (!string.IsNullOrEmpty(sapDespatch.ASP.PARTY.WEB_URL))
                        {
                            tempDespatchAdviceModel.DespatchSupplierParty.Party.WebSiteURI = sapDespatch.ASP.PARTY.WEB_URL;
                        }

                        if (sapDespatch.ASP.PARTY.PARTY_IDFICATION != null)
                        {
                            tempDespatchAdviceModel.DespatchSupplierParty.Party.PartyIdentification = new List<PartyIdentification>();
                            tempDespatchAdviceModel.DespatchSupplierParty.Party.PartyIdentification.Add(new PartyIdentification()
                            {
                                ID = new CombineId() { Id = sapDespatch.ASP.PARTY.PARTY_IDFICATION.ID, SchemeId = sapDespatch.ASP.PARTY.PARTY_IDFICATION.SCHMID }
                            });


                            if (sapDespatch.ASP.PARTY.PARTY_NAME != null)
                            {
                                if (sapDespatch.ASP.PARTY.PARTY_IDFICATION.SCHMID == "TCKN")
                                {
                                    //PERSON ALANI XML UZERINDE OLMADIĞI İÇİN TITLE VERISININ AYNISI BASILIYOR.
                                    tempDespatchAdviceModel.DespatchSupplierParty.Party.Person = new Person() { FamilyName = ".", FirstName = sapDespatch.ASP.PARTY.PARTY_NAME.NAME, };
                                }
                                else
                                {
                                    tempDespatchAdviceModel.DespatchSupplierParty.Party.PartyName = new PartyName() { Name = sapDespatch.ASP.PARTY.PARTY_NAME.NAME };
                                }
                            }
                        }

                        #region PostalAddress

                        if (sapDespatch.ASP.PARTY.POST_ADDR != null)
                        {
                            tempDespatchAdviceModel.DespatchSupplierParty.Party.PostalAddress = new Address();

                            if (!string.IsNullOrEmpty(sapDespatch.ASP.PARTY.POST_ADDR.STREET_NAME))
                            {
                                tempDespatchAdviceModel.DespatchSupplierParty.Party.PostalAddress.StreetName = sapDespatch.ASP.PARTY.POST_ADDR.STREET_NAME;
                            }

                            if (!string.IsNullOrEmpty(sapDespatch.ASP.PARTY.POST_ADDR.BUILDG_NAME))
                            {
                                tempDespatchAdviceModel.DespatchSupplierParty.Party.PostalAddress.BuildingName = sapDespatch.ASP.PARTY.POST_ADDR.BUILDG_NAME;
                            }

                            if (!string.IsNullOrEmpty(sapDespatch.ASP.PARTY.POST_ADDR.BUILDG_NO))
                            {
                                tempDespatchAdviceModel.DespatchSupplierParty.Party.PostalAddress.BuildingNumber = sapDespatch.ASP.PARTY.POST_ADDR.BUILDG_NO;
                            }

                            if (!string.IsNullOrEmpty(sapDespatch.ASP.PARTY.POST_ADDR.CITY_SUBD_NAME))
                            {
                                tempDespatchAdviceModel.DespatchSupplierParty.Party.PostalAddress.CitySubdivisionName = sapDespatch.ASP.PARTY.POST_ADDR.CITY_SUBD_NAME;
                            }

                            if (!string.IsNullOrEmpty(sapDespatch.ASP.PARTY.POST_ADDR.CITY_NAME))
                            {
                                tempDespatchAdviceModel.DespatchSupplierParty.Party.PostalAddress.CityName = sapDespatch.ASP.PARTY.POST_ADDR.CITY_NAME;
                            }

                            if (!string.IsNullOrEmpty(sapDespatch.ASP.PARTY.POST_ADDR.POST_ZONE))
                            {
                                tempDespatchAdviceModel.DespatchSupplierParty.Party.PostalAddress.PostalZone = sapDespatch.ASP.PARTY.POST_ADDR.POST_ZONE;
                            }

                            if (!string.IsNullOrEmpty(sapDespatch.ASP.PARTY.POST_ADDR.REGION))
                            {
                                tempDespatchAdviceModel.DespatchSupplierParty.Party.PostalAddress.Region = sapDespatch.ASP.PARTY.POST_ADDR.REGION;
                            }

                            if (sapDespatch.ASP.PARTY.POST_ADDR.COUNTRY != null)
                            {
                                tempDespatchAdviceModel.DespatchSupplierParty.Party.PostalAddress.Country = new Country();

                                if (!string.IsNullOrEmpty(sapDespatch.ASP.PARTY.POST_ADDR.COUNTRY.COUNTRY_NAME))
                                {
                                    tempDespatchAdviceModel.DespatchSupplierParty.Party.PostalAddress.Country.Name = sapDespatch.ASP.PARTY.POST_ADDR.COUNTRY.COUNTRY_NAME;
                                }
                            }
                        }

                        #endregion

                        #region PartyTaxScheme

                        if (sapDespatch.ASP.PARTY.PARTY_TXSCHM != null)
                        {
                            tempDespatchAdviceModel.DespatchSupplierParty.Party.PartyTaxScheme = new PartyTaxScheme();
                            if (sapDespatch.ASP.PARTY.PARTY_TXSCHM.TXSCHM_NAME != null)
                            {
                                tempDespatchAdviceModel.DespatchSupplierParty.Party.PartyTaxScheme.TaxScheme = new TaxScheme();
                                if (!string.IsNullOrEmpty(sapDespatch.ASP.PARTY.PARTY_TXSCHM.TXSCHM_NAME.NAME))
                                {
                                    tempDespatchAdviceModel.DespatchSupplierParty.Party.PartyTaxScheme.TaxScheme.Name = sapDespatch.ASP.PARTY.PARTY_TXSCHM.TXSCHM_NAME.NAME;
                                }
                            }
                        }

                        #endregion

                        #region Contact

                        if (sapDespatch.ASP.PARTY.CONTACT != null)
                        {
                            tempDespatchAdviceModel.DespatchSupplierParty.Party.Contact = new Contact();
                            if (!string.IsNullOrEmpty(sapDespatch.ASP.PARTY.CONTACT.NOTE))
                            {
                                tempDespatchAdviceModel.DespatchSupplierParty.Party.Contact.Note = sapDespatch.ASP.PARTY.CONTACT.NOTE;
                            }

                            if (!string.IsNullOrEmpty(sapDespatch.ASP.PARTY.CONTACT.EMAIL))
                            {
                                tempDespatchAdviceModel.DespatchSupplierParty.Party.Contact.ElectronicMail = sapDespatch.ASP.PARTY.CONTACT.EMAIL;
                            }

                            if (!string.IsNullOrEmpty(sapDespatch.ASP.PARTY.CONTACT.TFAX))
                            {
                                tempDespatchAdviceModel.DespatchSupplierParty.Party.Contact.Telefax = sapDespatch.ASP.PARTY.CONTACT.TFAX;
                            }

                            if (!string.IsNullOrEmpty(sapDespatch.ASP.PARTY.CONTACT.TPHONE))
                            {
                                tempDespatchAdviceModel.DespatchSupplierParty.Party.Contact.Telephone = sapDespatch.ASP.PARTY.CONTACT.TPHONE;
                            }
                        }

                        #endregion
                    }
                }

                #endregion

                #region AccountingCustomerParty

                if (sapDespatch.ACP != null)
                {
                    tempDespatchAdviceModel.DeliveryCustomerParty = new CustomerParty();
                    if (sapDespatch.ACP.PARTY != null)
                    {
                        tempDespatchAdviceModel.DeliveryCustomerParty.Party = new Party();

                        if (!string.IsNullOrEmpty(sapDespatch.ACP.PARTY.WEB_URL))
                        {
                            tempDespatchAdviceModel.DeliveryCustomerParty.Party.WebSiteURI = sapDespatch.ACP.PARTY.WEB_URL;
                        }

                        if (sapDespatch.ACP.PARTY.PARTY_IDFICATION != null)
                        {
                            tempDespatchAdviceModel.DeliveryCustomerParty.Party.PartyIdentification = new List<PartyIdentification>();
                            tempDespatchAdviceModel.DeliveryCustomerParty.Party.PartyIdentification.Add(new PartyIdentification()
                            {
                                ID = new CombineId() { Id = sapDespatch.ACP.PARTY.PARTY_IDFICATION.ID, SchemeId = sapDespatch.ACP.PARTY.PARTY_IDFICATION.SCHMID }
                            });
                        }

                        if (sapDespatch.ACP.PARTY.PARTY_NAME != null)
                        {
                            if (sapDespatch.ACP.PARTY.PARTY_IDFICATION.SCHMID == "TCKN")
                            {
                                //PERSON ALANI XML UZERINDE OLMADIĞI İÇİN TITLE VERISININ AYNISI BASILIYOR.
                                tempDespatchAdviceModel.DeliveryCustomerParty.Party.Person = new Person() { FamilyName = ".", FirstName = sapDespatch.ACP.PARTY.PARTY_NAME.NAME, };
                            }
                            else
                            {
                                tempDespatchAdviceModel.DeliveryCustomerParty.Party.PartyName = new PartyName() { Name = sapDespatch.ACP.PARTY.PARTY_NAME.NAME };
                            }
                        }

                        #region PostalAddress

                        if (sapDespatch.ACP.PARTY.POST_ADDR != null)
                        {
                            tempDespatchAdviceModel.DeliveryCustomerParty.Party.PostalAddress = new Address();

                            if (!string.IsNullOrEmpty(sapDespatch.ACP.PARTY.POST_ADDR.STREET_NAME))
                            {
                                tempDespatchAdviceModel.DeliveryCustomerParty.Party.PostalAddress.StreetName = sapDespatch.ACP.PARTY.POST_ADDR.STREET_NAME;
                            }

                            if (!string.IsNullOrEmpty(sapDespatch.ACP.PARTY.POST_ADDR.BUILDG_NAME))
                            {
                                tempDespatchAdviceModel.DeliveryCustomerParty.Party.PostalAddress.BuildingName = sapDespatch.ACP.PARTY.POST_ADDR.BUILDG_NAME;
                            }

                            if (!string.IsNullOrEmpty(sapDespatch.ACP.PARTY.POST_ADDR.BUILDG_NO))
                            {
                                tempDespatchAdviceModel.DeliveryCustomerParty.Party.PostalAddress.BuildingNumber = sapDespatch.ACP.PARTY.POST_ADDR.BUILDG_NO;
                            }

                            if (!string.IsNullOrEmpty(sapDespatch.ACP.PARTY.POST_ADDR.CITY_SUBD_NAME))
                            {
                                tempDespatchAdviceModel.DeliveryCustomerParty.Party.PostalAddress.CitySubdivisionName = sapDespatch.ACP.PARTY.POST_ADDR.CITY_SUBD_NAME;
                            }

                            if (!string.IsNullOrEmpty(sapDespatch.ACP.PARTY.POST_ADDR.CITY_NAME))
                            {
                                tempDespatchAdviceModel.DeliveryCustomerParty.Party.PostalAddress.CityName = sapDespatch.ACP.PARTY.POST_ADDR.CITY_NAME;
                            }

                            if (!string.IsNullOrEmpty(sapDespatch.ACP.PARTY.POST_ADDR.POST_ZONE))
                            {
                                tempDespatchAdviceModel.DeliveryCustomerParty.Party.PostalAddress.PostalZone = sapDespatch.ACP.PARTY.POST_ADDR.POST_ZONE;
                            }

                            if (!string.IsNullOrEmpty(sapDespatch.ACP.PARTY.POST_ADDR.REGION))
                            {
                                tempDespatchAdviceModel.DeliveryCustomerParty.Party.PostalAddress.Region = sapDespatch.ACP.PARTY.POST_ADDR.REGION;
                            }

                            if (sapDespatch.ACP.PARTY.POST_ADDR.COUNTRY != null)
                            {
                                tempDespatchAdviceModel.DeliveryCustomerParty.Party.PostalAddress.Country = new Country();

                                if (!string.IsNullOrEmpty(sapDespatch.ACP.PARTY.POST_ADDR.COUNTRY.COUNTRY_NAME))
                                {
                                    tempDespatchAdviceModel.DeliveryCustomerParty.Party.PostalAddress.Country.Name = sapDespatch.ACP.PARTY.POST_ADDR.COUNTRY.COUNTRY_NAME;
                                }
                            }
                        }

                        #endregion

                        #region PartyTaxScheme

                        if (sapDespatch.ACP.PARTY.PARTY_TXSCHM != null)
                        {
                            tempDespatchAdviceModel.DeliveryCustomerParty.Party.PartyTaxScheme = new PartyTaxScheme();
                            if (sapDespatch.ACP.PARTY.PARTY_TXSCHM.TXSCHM_NAME != null)
                            {
                                tempDespatchAdviceModel.DeliveryCustomerParty.Party.PartyTaxScheme.TaxScheme = new TaxScheme();
                                if (!string.IsNullOrEmpty(sapDespatch.ACP.PARTY.PARTY_TXSCHM.TXSCHM_NAME.NAME))
                                {
                                    tempDespatchAdviceModel.DeliveryCustomerParty.Party.PartyTaxScheme.TaxScheme.Name = sapDespatch.ACP.PARTY.PARTY_TXSCHM.TXSCHM_NAME.NAME;
                                }
                            }
                        }

                        #endregion

                        #region Contact

                        if (sapDespatch.ACP.PARTY.CONTACT != null)
                        {
                            tempDespatchAdviceModel.DeliveryCustomerParty.Party.Contact = new Contact();
                            if (!string.IsNullOrEmpty(sapDespatch.ACP.PARTY.CONTACT.NOTE))
                            {
                                tempDespatchAdviceModel.DeliveryCustomerParty.Party.Contact.Note = sapDespatch.ACP.PARTY.CONTACT.NOTE;
                            }

                            if (!string.IsNullOrEmpty(sapDespatch.ACP.PARTY.CONTACT.EMAIL))
                            {
                                tempDespatchAdviceModel.DeliveryCustomerParty.Party.Contact.ElectronicMail = sapDespatch.ACP.PARTY.CONTACT.EMAIL;
                            }

                            if (!string.IsNullOrEmpty(sapDespatch.ACP.PARTY.CONTACT.TFAX))
                            {
                                tempDespatchAdviceModel.DeliveryCustomerParty.Party.Contact.Telefax = sapDespatch.ACP.PARTY.CONTACT.TFAX;
                            }

                            if (!string.IsNullOrEmpty(sapDespatch.ACP.PARTY.CONTACT.TPHONE))
                            {
                                tempDespatchAdviceModel.DeliveryCustomerParty.Party.Contact.Telephone = sapDespatch.ACP.PARTY.CONTACT.TPHONE;
                            }
                        }

                        #endregion
                    }
                }

                #endregion

                #region Shipment

                //SAP üzerinden göndermiyorlar bu alan zorunlu olduğu için eklendi.
                tempDespatchAdviceModel.Shipment = new Shipment();
                tempDespatchAdviceModel.Shipment.ID = null;

                tempDespatchAdviceModel.Shipment.GoodsItems = new List<GoodsItem>();
                tempDespatchAdviceModel.Shipment.ShipmentStages = new List<ShipmentStage>();
                tempDespatchAdviceModel.Shipment.Delivery = new Delivery();

                tempDespatchAdviceModel.Shipment.Delivery.CarrierParty = new Party();

                GoodsItem headerGoodsItem = new GoodsItem();
                headerGoodsItem.ID = new CombineId();
                headerGoodsItem.ValueAmount = new UblBaseCurrency();
                headerGoodsItem.ValueAmount.CurrencyID = "TRY";
                headerGoodsItem.ValueAmount.Value = 0;

                tempDespatchAdviceModel.Shipment.GoodsItems.Add(headerGoodsItem);

                ShipmentStage headerShipmentStage = new ShipmentStage();
                headerShipmentStage.TransportMeans = new TransportMeans();
                headerShipmentStage.TransportMeans.RoadTransport = new RoadTransport();
                headerShipmentStage.TransportMeans.RoadTransport.LicensePlateID = new CombineId { SchemeId = "PLAKA" };

                tempDespatchAdviceModel.Shipment.ShipmentStages.Add(headerShipmentStage);

                tempDespatchAdviceModel.Shipment.Delivery.CarrierParty.PartyIdentification = new List<PartyIdentification>();
                PartyIdentification partyIdentification = new PartyIdentification();
                partyIdentification.ID = new CombineId { SchemeId = "VKN" };

                tempDespatchAdviceModel.Shipment.Delivery.CarrierParty.PartyIdentification.Add(partyIdentification);

                tempDespatchAdviceModel.Shipment.Delivery.CarrierParty.PartyName = new PartyName();
                tempDespatchAdviceModel.Shipment.Delivery.CarrierParty.PartyName.Name = "";

                tempDespatchAdviceModel.Shipment.Delivery.CarrierParty.PostalAddress = new Address();
                tempDespatchAdviceModel.Shipment.Delivery.CarrierParty.PostalAddress.CitySubdivisionName = "";
                tempDespatchAdviceModel.Shipment.Delivery.CarrierParty.PostalAddress.CityName = "";
                tempDespatchAdviceModel.Shipment.Delivery.CarrierParty.PostalAddress.Country = new Country();
                tempDespatchAdviceModel.Shipment.Delivery.CarrierParty.PostalAddress.Country.Name = "Türkiye";
                tempDespatchAdviceModel.Shipment.Delivery.CarrierParty.PostalAddress.Country.IdentificationCode = "TR";

                tempDespatchAdviceModel.Shipment.Delivery.Despatch = new Despatch();
                tempDespatchAdviceModel.Shipment.Delivery.Despatch.ActualDespatchDate = DateTime.Now.ToString("yyyy-MM-dd");
                tempDespatchAdviceModel.Shipment.Delivery.Despatch.ActualDespatchTime = DateTime.Now.ToString("HH:MM:ss");

                #endregion

                #region InvoiceLine

                if (sapDespatch.DELIVERY_LINE != null && sapDespatch.DELIVERY_LINE.ZPK_DESPATCHLINE != null && sapDespatch.DELIVERY_LINE.ZPK_DESPATCHLINE.Count > 0)
                {
                    tempDespatchAdviceModel.DespatchLines = new List<DespatchLine>();
                    foreach (var line in sapDespatch.DELIVERY_LINE.ZPK_DESPATCHLINE)
                    {
                        var despatchLine = new DespatchLine();

                        despatchLine.ID = new CombineId() { Id = line.ID };

                        despatchLine.DeliveredQuantity = new BaseUnit() { UnitCode = line.DELIVEREDQUANTITY.QTY_UNIT, Value = line.DELIVEREDQUANTITY.INV_QTY };

                        despatchLine.OrderLineReferences = new List<OrderLineReference> { new OrderLineReference { LineID = line.ORDERLINEREFERENCE.LINEID } };

                        despatchLine.Item = new Item();

                        despatchLine.Item.Name = line.ITEM.NAME;
                        despatchLine.Item.SellersItemIdentification = new ItemIdentification() { ID = new CombineId { Id = line.ITEM.SELLERS_ITEM_IDENTIFICATION.ID } };

                        despatchLine.Item.ItemInstance = new List<ItemInstance>();

                        foreach (var item in line.ITEM.ITEMINSTANCE)
                        {
                            despatchLine.Item.ItemInstance.Add(new ItemInstance { LotIdentification = new LotIdentification { LotNumberID = new CombineId { Id = item.ZPK_ITEMINSTANCE.LOTIDENTIFICATION } } });
                        }

                        tempDespatchAdviceModel.DespatchLines.Add(despatchLine);
                    }
                }

                #endregion

                TransferDespatchAdviceDataSerializer iSerializer = new TransferDespatchAdviceDataSerializer();
                convertedXmlContent = iSerializer.SerializeAndGetXmlContent(tempDespatchAdviceModel, true, true);

                return !string.IsNullOrWhiteSpace(convertedXmlContent);
            }
            catch (Exception ex)
            {
                convertedXmlContent = ex.Message;
            }

            return false;
        }
    }
}