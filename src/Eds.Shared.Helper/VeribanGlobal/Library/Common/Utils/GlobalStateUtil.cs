using static Eds.Shared.Helper.VeribanGlobal.Library.Common.GlobalEnums;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils
{
    public static class EInvoiceGlobalStateUtil
    {
        public static GlobalStateClass GetGibDocumentProcessStatus(byte documentProcessState, byte documentEnvelopingState)
        {
            if (documentProcessState == (byte)EInvoiceEnums.NewGibDocumentProcessStatus.State_DraftCreatedWaitForApproved)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Default,
                    Description = "TASLAK VERI"
                };
            }
            else if (documentProcessState == (byte)EInvoiceEnums.NewGibDocumentProcessStatus.State_ApprovedWaitForDocumentRequest)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "GONDERIM LISTESINDE"
                };
            }
            else if (documentProcessState == (byte)EInvoiceEnums.NewGibDocumentProcessStatus.Error_CancelDocumentByUser)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Error,
                    Description = "IPTAL EDILDI"
                };
            }
            else if (documentProcessState == (byte)EInvoiceEnums.NewGibDocumentProcessStatus.State_DocumentCreatedWaitForSign)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Wait,
                    Description = "IMZA BEKLIYOR"
                };
            }
            else if (documentProcessState == (byte)EInvoiceEnums.NewGibDocumentProcessStatus.Process_DocumentSigning
                || documentProcessState == (byte)EInvoiceEnums.NewGibDocumentProcessStatus.Debug_Test)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "ISLEM YAPILIYOR"
                };
            }
            else if (documentProcessState == (byte)EInvoiceEnums.NewGibDocumentProcessStatus.Error_DocumentSign)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Error,
                    Description = "HATALI"
                };
            }
            else if (documentProcessState == (byte)EInvoiceEnums.NewGibDocumentProcessStatus.State_SuccessDocumentSigned)
            {
                #region ENVELOPING STATE

                // if (documentEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.State_WaitForSignedDocument
                //     || documentEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.State_PreparedWaitForRefDocument)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Default,
                //         Description = "Referans döküman bekleniyor"
                //     };
                // }
                // else if (documentEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.State_WaitForPrepareEnvelope)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Default,
                //         Description = "Zarf bağlantısı bekleniyor"
                //     };
                // }
                // else if (documentEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.Processing_PrepareEnvelope
                //     || documentEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.Processing_DebugTest
                //     || documentEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.Processing_ErrorAnalyzing)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Processing,
                //         Description = "Zarf bağlantısı yapılıyor"
                //     };
                // }
                // else if (documentEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.Error_PrepareEnvelope)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Error,
                //         Description = "Zarf bağlantısı yapılamadı"
                //     };
                // }
                // else if (documentEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.State_PreparedWaitForCreateFile)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Wait,
                //         Description = "Zarf dosyası bekleniyor"
                //     };
                // }
                // else if (documentEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.Error_CreateFile)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Error,
                //         Description = "Zarf dosyası hata aldı"
                //     };
                // }
                // else if (documentEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.State_CreatedFileWaitForSendToGib)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Wait,
                //         Description = "Gönderilmeyi bekliyor"
                //     };
                // }
                // else if (documentEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.Error_SendToGib)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Error,
                //         Description = "Zarf gönderilemedi"
                //     };
                // }
                // else if (documentEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.State_TransferredWaitForQueryFromGib)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Wait,
                //         Description = "Alındı yanıtı bekliyor"
                //     };
                // }
                // else if (documentEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.Fail_EnvelopeErrorOnGIB)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Error,
                //         Description = "Zarf merkezde hata aldı"
                //     };
                // }
                // else if (documentEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.Fail_EnvelopeErrorOnReceiver)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Error,
                //         Description = "Zarf alıcıda hata aldı"
                //     };
                // }
                // else if (documentEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.State_EnvelopeSuccessfullySend)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Success,
                //         Description = "Başarıyla alıcıya iletildi"
                //     };
                // }
                // else
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Default,
                    Description = "Bilinmiyor"
                };

                #endregion
            }
            else return new GlobalStateClass()
            {
                State = GlobalEnums.GlobalUIStates.Default,
                Description = "BILINMIYOR"
            };
        }
        public static GlobalStateClass GetGibDocumentAnswerProcessStatus(byte documentAnswerState, byte documentAnswerEnvelopingState)
        {
            if (documentAnswerState == (byte)EInvoiceEnums.NewGibDocumentProcessStatus.State_NoOperation)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Default,
                    Description = "Cevap işlemi yapılmaz"
                };
            }
            else if (documentAnswerState == (byte)EInvoiceEnums.NewGibDocumentProcessStatus.State_WaitForDraftCreated)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Wait,
                    Description = "Cevap bekliyor"
                };
            }
            else if (documentAnswerState == (byte)EInvoiceEnums.NewGibDocumentProcessStatus.State_DraftCreatedWaitForApproved)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Default,
                    Description = "Taslak olusturuldu"
                };
            }
            else if (documentAnswerState == (byte)EInvoiceEnums.NewGibDocumentProcessStatus.State_ApprovedWaitForDocumentRequest)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "Gönderim listesinde"
                };
            }
            else if (documentAnswerState == (byte)EInvoiceEnums.NewGibDocumentProcessStatus.Error_CancelDocumentByUser)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Error,
                    Description = "Iptal edildi"
                };
            }
            else if (documentAnswerState == (byte)EInvoiceEnums.NewGibDocumentProcessStatus.State_DocumentCreatedWaitForSign)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Wait,
                    Description = "Imza bekliyor"
                };
            }
            else if (documentAnswerState == (byte)EInvoiceEnums.NewGibDocumentProcessStatus.Process_DocumentSigning
                || documentAnswerState == (byte)EInvoiceEnums.NewGibDocumentProcessStatus.Debug_Test)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "Islem yapılıyor"
                };
            }
            else if (documentAnswerState == (byte)EInvoiceEnums.NewGibDocumentProcessStatus.Error_DocumentSign)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Error,
                    Description = "Hatalı"
                };
            }
            else if (documentAnswerState == (byte)EInvoiceEnums.NewGibDocumentProcessStatus.State_SuccessDocumentSigned)
            {
                #region ENVELOPING STATE
                //
                // if (documentAnswerEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.State_WaitForSignedDocument
                //     || documentAnswerEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.State_PreparedWaitForRefDocument)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Default,
                //         Description = "Referans döküman bekleniyor"
                //     };
                // }
                // else if (documentAnswerEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.State_WaitForPrepareEnvelope)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Default,
                //         Description = "Zarf bağlantısı bekleniyor"
                //     };
                // }
                // else if (documentAnswerEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.Processing_PrepareEnvelope
                //     || documentAnswerEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.Processing_DebugTest
                //     || documentAnswerEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.Processing_ErrorAnalyzing)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Processing,
                //         Description = "Zarf bağlantısı yapılıyor"
                //     };
                // }
                // else if (documentAnswerEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.Error_PrepareEnvelope)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Error,
                //         Description = "Zarf bağlantısı yapılamadı"
                //     };
                // }
                // else if (documentAnswerEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.State_PreparedWaitForCreateFile)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Wait,
                //         Description = "Zarf dosyası bekleniyor"
                //     };
                // }
                // else if (documentAnswerEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.Error_CreateFile)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Error,
                //         Description = "Zarf dosyası hata aldı"
                //     };
                // }
                // else if (documentAnswerEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.State_CreatedFileWaitForSendToGib)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Wait,
                //         Description = "Gönderilmeyi bekliyor"
                //     };
                // }
                // else if (documentAnswerEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.Error_SendToGib)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Error,
                //         Description = "Zarf gönderilemedi"
                //     };
                // }
                // else if (documentAnswerEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.State_TransferredWaitForQueryFromGib)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Wait,
                //         Description = "Alındı yanıtı bekliyor"
                //     };
                // }
                // else if (documentAnswerEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.Fail_EnvelopeErrorOnGIB)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Error,
                //         Description = "Zarf merkezde hata aldı"
                //     };
                // }
                // else if (documentAnswerEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.Fail_EnvelopeErrorOnReceiver)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Error,
                //         Description = "Zarf alıcıda hata aldı"
                //     };
                // }
                // else if (documentAnswerEnvelopingState == (byte)EInvoiceEnums.NewGibDocumentEnvelopingStatus.State_EnvelopeSuccessfullySend)
                // {
                //     return new GlobalStateClass()
                //     {
                //         State = GlobalEnums.GlobalUIStates.Success,
                //         Description = "Başarıyla alıcıya iletildi"
                //     };
                // }
                // else
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Default,
                    Description = "Bilinmiyor"
                };
                //
                #endregion
            }
            else return new GlobalStateClass()
            {
                State = GlobalEnums.GlobalUIStates.Default,
                Description = "Bilinmiyor"
            };
        }
        public static GlobalStateClass GetDespatchAnswerTypeInfo(byte despatchAnswerType)
        {
            if (despatchAnswerType == (byte)EInvoiceEnums.DespatchAnswer.CompletelyAccepted)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Success,
                    Description = "Tümü Kabul edildi"
                };
            }
            else if (despatchAnswerType == (byte)EInvoiceEnums.DespatchAnswer.CompletelyAcceptedWithComplaint)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "Tümü Kabul edildi(*)"
                };
            }
            else if (despatchAnswerType == (byte)EInvoiceEnums.DespatchAnswer.PartiallyAccepted)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "Parcali Kabul"
                };
            }
            else if (despatchAnswerType == (byte)EInvoiceEnums.DespatchAnswer.CompletelyRejected)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Error,
                    Description = "Tümü reddedildi"
                };
            }
            else return new GlobalStateClass()
            {
                State = GlobalEnums.GlobalUIStates.Default,
                Description = "Bilinmiyor"
            };
        }
        public static GlobalStateClass GetInvoiceAnswerTypeInfo(byte invoiceAnswerType)
        {
            if (invoiceAnswerType == (byte)EInvoiceEnums.BuyerAnswer.Accepted)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Success,
                    Description = "Kabul edildi"
                };
            }
            else if (invoiceAnswerType == (byte)EInvoiceEnums.BuyerAnswer.Restitute)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "Iade Edildi"
                };
            }
            else if (invoiceAnswerType == (byte)EInvoiceEnums.BuyerAnswer.Rejectted)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Error,
                    Description = "Reddedildi"
                };
            }
            else return new GlobalStateClass()
            {
                State = GlobalEnums.GlobalUIStates.Default,
                Description = "Bilinmiyor"
            };
        }

        // public static GlobalStateClass GetGibSendEnvelopeProcessStatus(byte envelopeProcessState)
        // {
        //     if (envelopeProcessState == (byte)EInvoiceEnums.SendGibEnvelopeProcessState.State_PreparedWaitForRefDocument)
        //     {
        //         return new GlobalStateClass()
        //         {
        //             State = GlobalEnums.GlobalUIStates.Default,
        //             Description = "Referans döküman bekleniyor"
        //         };
        //     }
        //     else if (envelopeProcessState == (byte)EInvoiceEnums.SendGibEnvelopeProcessState.State_PreparedWaitForCreateFile)
        //     {
        //         return new GlobalStateClass()
        //         {
        //             State = GlobalEnums.GlobalUIStates.Wait,
        //             Description = "Zarf dosyası bekleniyor"
        //         };
        //     }
        //     else if (envelopeProcessState == (byte)EInvoiceEnums.SendGibEnvelopeProcessState.Processing_CreateFile)
        //     {
        //         return new GlobalStateClass()
        //         {
        //             State = GlobalEnums.GlobalUIStates.Processing,
        //             Description = "Zarf dosyası hazırlanıyor"
        //         };
        //     }
        //     else if (envelopeProcessState == (byte)EInvoiceEnums.SendGibEnvelopeProcessState.Error_CreateFile)
        //     {
        //         return new GlobalStateClass()
        //         {
        //             State = GlobalEnums.GlobalUIStates.Error,
        //             Description = "Zarf dosyası hata aldı"
        //         };
        //     }
        //     else if (envelopeProcessState == (byte)EInvoiceEnums.SendGibEnvelopeProcessState.State_CreatedFileWaitForSendToGib)
        //     {
        //         return new GlobalStateClass()
        //         {
        //             State = GlobalEnums.GlobalUIStates.Wait,
        //             Description = "Gönderilmeyi bekliyor"
        //         };
        //     }
        //     else if (envelopeProcessState == (byte)EInvoiceEnums.SendGibEnvelopeProcessState.Processing_SendToGib)
        //     {
        //         return new GlobalStateClass()
        //         {
        //             State = GlobalEnums.GlobalUIStates.Processing,
        //             Description = "Zarf dosyası gönderiliyor"
        //         };
        //     }
        //     else if (envelopeProcessState == (byte)EInvoiceEnums.SendGibEnvelopeProcessState.Error_SendToGib)
        //     {
        //         return new GlobalStateClass()
        //         {
        //             State = GlobalEnums.GlobalUIStates.Error,
        //             Description = "Zarf gönderilemedi"
        //         };
        //     }
        //     else if (envelopeProcessState == (byte)EInvoiceEnums.SendGibEnvelopeProcessState.State_TransferredWaitForQueryFromGib)
        //     {
        //         return new GlobalStateClass()
        //         {
        //             State = GlobalEnums.GlobalUIStates.Wait,
        //             Description = "Alındı yanıtı bekliyor"
        //         };
        //     }
        //     else if (envelopeProcessState == (byte)EInvoiceEnums.SendGibEnvelopeProcessState.Processing_QueryFromGib)
        //     {
        //         return new GlobalStateClass()
        //         {
        //             State = GlobalEnums.GlobalUIStates.Processing,
        //             Description = "Zarf sorgulanıyor"
        //         };
        //     }
        //     else if (envelopeProcessState == (byte)EInvoiceEnums.SendGibEnvelopeProcessState.Fail_EnvelopeErrorOnGIB)
        //     {
        //         return new GlobalStateClass()
        //         {
        //             State = GlobalEnums.GlobalUIStates.Error,
        //             Description = "Zarf merkezde hata aldı"
        //         };
        //     }
        //     else if (envelopeProcessState == (byte)EInvoiceEnums.SendGibEnvelopeProcessState.Fail_EnvelopeErrorOnReceiver)
        //     {
        //         return new GlobalStateClass()
        //         {
        //             State = GlobalEnums.GlobalUIStates.Error,
        //             Description = "Zarf alıcıda hata aldı"
        //         };
        //     }
        //     else if (envelopeProcessState == (byte)EInvoiceEnums.SendGibEnvelopeProcessState.State_EnvelopeSuccessfullySend)
        //     {
        //         return new GlobalStateClass()
        //         {
        //             State = GlobalEnums.GlobalUIStates.Success,
        //             Description = "Başarıyla alıcıya iletildi"
        //         };
        //     }
        //     else if (envelopeProcessState == (byte)EInvoiceEnums.SendGibEnvelopeProcessState.Processing_DebugTest
        //         || envelopeProcessState == (byte)EInvoiceEnums.SendGibEnvelopeProcessState.Processing_ErrorAnalyzing)
        //     {
        //         return new GlobalStateClass()
        //         {
        //             State = GlobalEnums.GlobalUIStates.Processing,
        //             Description = "Zarf işlem yapılıyor"
        //         };
        //     }
        //     else return new GlobalStateClass()
        //     {
        //         State = GlobalEnums.GlobalUIStates.Default,
        //         Description = "Bilinmiyor"
        //     };
        // }
    }

    public static class VeribanContractGlobalStateUtil
    {
        public static GlobalStateClass GetFormProcessStatus(int formProcessState)
        {
            if (formProcessState == (byte)ContractEnums.ContractFormProcessStatus.State_DraftFileCreatedWaitForApproveDraft)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Default,
                    Description = "GONDERIM BEKLIYOR"
                };
            }
            else if (formProcessState == (byte)ContractEnums.ContractFormProcessStatus.State_DraftApprovedWaitForMailSendCompleted)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Default,
                    Description = "MAIL GONDERIM BEKLIYOR"
                };
            }
            else if (formProcessState == (byte)ContractEnums.ContractFormProcessStatus.State_MailSendCompletedWaitForCustomerSendToVeriban)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Wait,
                    Description = "FIRMA ONAY BEKLIYOR"
                };
            }
            else if (formProcessState == (byte)ContractEnums.ContractFormProcessStatus.State_CustomerSentFileWaitForPDFCreate)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Wait,
                    Description = "FORM BEKLIYOR"
                };
            }
            else if (formProcessState == (byte)ContractEnums.ContractFormProcessStatus.State_PDFCreatedWaitForPDFSign)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Wait,
                    Description = "IMZA BEKLIYOR"
                };
            }
            else if (formProcessState == (byte)ContractEnums.ContractFormProcessStatus.Success_PDFSigned)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Success,
                    Description = "IMZALANDI"
                };
            }
            else if (formProcessState == (byte)ContractEnums.ContractFormProcessStatus.Process_PDFCreating
                || formProcessState == (byte)ContractEnums.ContractFormProcessStatus.Process_PDFSigning)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "ISLENIYOR"
                };
            }
            else if (formProcessState == (byte)ContractEnums.ContractFormProcessStatus.Error_PDFCreate
                || formProcessState == (byte)ContractEnums.ContractFormProcessStatus.Error_PDFSign)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Error,
                    Description = "HATALI"
                };
            }

            return new GlobalStateClass()
            {
                State = GlobalEnums.GlobalUIStates.Default,
                Description = "Bilinmiyor!"
            };
        }

        public static GlobalStateClass GetEmailProcessStatus(int formMessageState)
        {
            if (formMessageState == (byte)ContractEnums.ContractEmailProcessState.State_WaitForRefDocument)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Default,
                    Description = "TASLAK BEKLIYOR"
                };
            }
            else if (formMessageState == (byte)ContractEnums.ContractEmailProcessState.State_WaitForSend)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Wait,
                    Description = "GONDERILECEK"
                };
            }
            else if (formMessageState == (byte)ContractEnums.ContractEmailProcessState.Processing_SendMail)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "GONDERILIYOR"
                };
            }
            else if (formMessageState == (byte)ContractEnums.ContractEmailProcessState.State_SuccessCallBack)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Success,
                    Description = "GONDERILDI"
                };
            }
            else if (formMessageState == (byte)ContractEnums.ContractEmailProcessState.Error_SendMail)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Error,
                    Description = "HATALI"
                };
            }

            return new GlobalStateClass()
            {
                State = GlobalEnums.GlobalUIStates.Default,
                Description = "BILINMIYOR"
            };
        }
    }

    public class EArchiveGlobalStateUtil : GlobalStateUtil
    {

    }

    public class ETicketGlobalStateUtil : GlobalStateUtil
    {

    }

    public class GlobalStateUtil
    {
        protected GlobalStateUtil()
        {

        }

        public static GlobalStateClass GetGlobalTransferQueueProcessState(int transferQueueState)
        {
            if (transferQueueState == (int)GlobalEnums.NewTransferQueueProcessState.DocumentAddedToQueue
                || transferQueueState == (int)GlobalEnums.NewTransferQueueProcessState.DocumentControlled
                || transferQueueState == (int)GlobalEnums.NewTransferQueueProcessState.DocumentPrepared)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Wait,
                    Description = "ISLENMEYI BEKLIYOR"
                };
            }
            else if (transferQueueState == (int)GlobalEnums.NewTransferQueueProcessState.DocumentSuccessfullyProcessed)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Success,
                    Description = "BASARIYLA ISLENDI"
                };
            }
            else if (transferQueueState == (int)GlobalEnums.NewTransferQueueProcessState.ErrorDocumentControl
                || transferQueueState == (int)GlobalEnums.NewTransferQueueProcessState.ErrorDocumentPrepared
                || transferQueueState == (int)GlobalEnums.NewTransferQueueProcessState.ErrorDocumentProcessFailed)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Error,
                    Description = "HATALI"
                };
            }

            return new GlobalStateClass()
            {
                State = GlobalEnums.GlobalUIStates.Default,
                Description = "Bilinmiyor!"
            };
        }

        public static GlobalStateClass GetGlobalReportPackageState(int packageState)
        {
            if (packageState == (int)GlobalEnums.GibReportPackageStatus.PreparedPackage || packageState == (int)GlobalEnums.GibReportPackageStatus.CreatedDraftPackage)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Default,
                    Description = "TASLAK HAZIRLANDI"
                };
            }
            else if (packageState == (int)GlobalEnums.GibReportPackageStatus.Signing)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "IMZALANIYOR"
                };
            }
            else if (packageState == (int)GlobalEnums.GibReportPackageStatus.SignError)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Error,
                    Description = "DOGRULANAMADI"
                };
            }
            else if (packageState == (int)GlobalEnums.GibReportPackageStatus.Signed || packageState == (int)GlobalEnums.GibReportPackageStatus.ReSend)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Wait,
                    Description = "MERKEZE GONDERILECEK"
                };
            }
            else if (packageState == (int)GlobalEnums.GibReportPackageStatus.Sending)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "GONDERILIYOR"
                };
            }
            else if (packageState == (int)GlobalEnums.GibReportPackageStatus.SendError)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Error,
                    Description = "GONDERILEMEDI"
                };
            }
            else if (packageState == (int)GlobalEnums.GibReportPackageStatus.Transferred || packageState == (int)GlobalEnums.GibReportPackageStatus.ReQuery)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Wait,
                    Description = "MERKEZE GONDERILDI"
                };
            }
            else if (packageState == (int)GlobalEnums.GibReportPackageStatus.ErrorAnalyzing || packageState == (int)GlobalEnums.GibReportPackageStatus.DebugTest)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "AYIKLANIYOR"
                };
            }
            else if (packageState == (int)GlobalEnums.GibReportPackageStatus.Querying)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "SORGULANIYOR"
                };
            }
            else if (packageState == (int)GlobalEnums.GibReportPackageStatus.QueryError)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Error,
                    Description = "SORGULANAMADI"
                };
            }
            else if (packageState == (int)GlobalEnums.GibReportPackageStatus.GIBDocumentError)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Error,
                    Description = "GIB HATALI RAPOR"
                };
            }
            else if (packageState == (int)GlobalEnums.GibReportPackageStatus.GIBDocumentCompleted)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Success,
                    Description = "MERKEZDE ONAYLANDI"
                };
            }

            return new GlobalStateClass()
            {
                State = GlobalEnums.GlobalUIStates.Default,
                Description = "BILINMIYOR"
            };
        }

        public static string FixMessageNonAsciiCharacter(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                string returnMessage;
                returnMessage = message.Replace("'", "");
                returnMessage = returnMessage.Replace("\"", "");
                returnMessage = returnMessage.Replace("\n", "");
                returnMessage = returnMessage.Replace("\r", "");

                return returnMessage;
            }

            return message;
        }

        public static GlobalStateClass GetNewFormProcessStatus(int formProcessState)
        {
            if (formProcessState == (byte)GlobalEnums.NewFormProcessStatus.State_WaitForApproved)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Default,
                    Description = "ONAY BEKLIYOR"
                };
            }
            else if (formProcessState == (byte)GlobalEnums.NewFormProcessStatus.State_WaitForDocumentCreate)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Default,
                    Description = "TASLAK VERI"
                };
            }
            else if (formProcessState == (byte)GlobalEnums.NewFormProcessStatus.State_WaitForDocumentSign)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Wait,
                    Description = "IMZA BEKLIYOR"
                };
            }
            else if (formProcessState == (byte)GlobalEnums.NewFormProcessStatus.State_SuccessDocumentSigned)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Success,
                    Description = "IMZALANDI"
                };
            }
            else if (formProcessState == (byte)GlobalEnums.NewFormProcessStatus.Process_DocumentCreating
                || formProcessState == (byte)GlobalEnums.NewFormProcessStatus.Process_DocumentSigning)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "ISLENIYOR"
                };
            }
            else if (formProcessState == (byte)GlobalEnums.NewFormProcessStatus.Error_DocumentCreate
                || formProcessState == (byte)GlobalEnums.NewFormProcessStatus.Error_DocumentSign)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Error,
                    Description = "HATALI"
                };
            }

            return new GlobalStateClass()
            {
                State = GlobalEnums.GlobalUIStates.Default,
                Description = "Bilinmiyor!"
            };
        }
        public static GlobalStateClass GetNewFormReportStatus(int formReportState)
        {
            if (formReportState == (byte)GlobalEnums.NewFormReportStatus.State_NoReportOperation)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Default,
                    Description = "RAPORLANAMAZ"
                };
            }
            else if (formReportState == (byte)GlobalEnums.NewFormReportStatus.State_WaitForSignedDocument)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Default,
                    Description = "IMZA BEKLIYOR"
                };
            }
            else if (formReportState == (byte)GlobalEnums.NewFormReportStatus.State_WaitForReport)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Wait,
                    Description = "RAPOR BEKLIYOR"
                };
            }
            else if (formReportState == (byte)GlobalEnums.NewFormReportStatus.Processing_PrepareReport)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "RAPORLANIYOR"
                };
            }
            else if (formReportState == (byte)GlobalEnums.NewFormReportStatus.State_SuccessReported)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Success,
                    Description = "RAPOR OLUŞTU"
                };
            }
            else if (formReportState == (byte)GlobalEnums.NewFormReportStatus.Error_ReportPrepare)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Error,
                    Description = "HATALI"
                };
            }

            return new GlobalStateClass()
            {
                State = GlobalEnums.GlobalUIStates.Default,
                Description = "BILINMIYOR"
            };
        }
        public static GlobalStateClass GetFormMessageStatus(int formMessageState)
        {
            if (formMessageState == (byte)GlobalEnums.NewFormMailStatus.State_NoEmailProcess)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Default,
                    Description = "MESAJ YOK"
                };
            }
            else if (formMessageState == (byte)GlobalEnums.NewFormMailStatus.State_NoSmtpConfig)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Default,
                    Description = "SERVER PASIF"
                };
            }
            else if (formMessageState == (byte)GlobalEnums.NewFormMailStatus.State_WaitForRefDocument)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Default,
                    Description = "IMZA BEKLIYOR"
                };
            }
            else if (formMessageState == (byte)GlobalEnums.NewFormMailStatus.State_WaitForCreateMailOrder)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Wait,
                    Description = "OLUSTURULACAK"
                };
            }
            else if (formMessageState == (byte)GlobalEnums.NewFormMailStatus.State_WaitForSend)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Wait,
                    Description = "GONDERILECEK"
                };
            }
            else if (formMessageState == (byte)GlobalEnums.NewFormMailStatus.State_WaitForCallBack)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Wait,
                    Description = "CEVAP BEKLIYOR"
                };
            }
            else if (formMessageState == (byte)GlobalEnums.NewFormMailStatus.Processing_CreateMailOrder)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "OLUSTURULUYOR"
                };
            }
            else if (formMessageState == (byte)GlobalEnums.NewFormMailStatus.Processing_SendMail)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "GONDERILIYOR"
                };
            }
            else if (formMessageState == (byte)GlobalEnums.NewFormMailStatus.State_SuccessCallBack)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Success,
                    Description = "GONDERILDI"
                };
            }
            else if (formMessageState == (byte)GlobalEnums.NewFormMailStatus.Error_CreateMailOrder
                || formMessageState == (byte)GlobalEnums.NewFormMailStatus.Error_SendMail
                || formMessageState == (byte)GlobalEnums.NewFormMailStatus.Error_CallBack)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Error,
                    Description = "HATALI"
                };
            }

            return new GlobalStateClass()
            {
                State = GlobalEnums.GlobalUIStates.Default,
                Description = "BILINMIYOR"
            };
        }
        public static GlobalStateClass GetFormMessageStatus(int formMailState, int formSMSState)
        {
            GlobalEnums.GlobalUIStates mailSate;
            #region MAIL STATE NORMALIZE
            if (formMailState == (byte)GlobalEnums.NewFormMailStatus.State_WaitForRefDocument
                || formMailState == (byte)GlobalEnums.NewFormMailStatus.State_WaitForCreateMailOrder
                || formMailState == (byte)GlobalEnums.NewFormMailStatus.State_WaitForSend
                || formMailState == (byte)GlobalEnums.NewFormMailStatus.State_WaitForCallBack)
            {
                mailSate = GlobalEnums.GlobalUIStates.Wait;
            }
            else if (formMailState == (byte)GlobalEnums.NewFormMailStatus.Processing_CreateMailOrder
                || formMailState == (byte)GlobalEnums.NewFormMailStatus.Processing_SendMail
                || formMailState == (byte)GlobalEnums.NewFormMailStatus.DebugTest)
            {
                mailSate = GlobalEnums.GlobalUIStates.Processing;
            }
            else if (formMailState == (byte)GlobalEnums.NewFormMailStatus.Error_CreateMailOrder
                || formMailState == (byte)GlobalEnums.NewFormMailStatus.Error_SendMail
                || formMailState == (byte)GlobalEnums.NewFormMailStatus.Error_CallBack)
            {
                mailSate = GlobalEnums.GlobalUIStates.Error;
            }
            else if (formMailState == (byte)GlobalEnums.NewFormMailStatus.State_SuccessCallBack)
            {
                mailSate = GlobalEnums.GlobalUIStates.Success;
            }
            else
            {
                //Unknown
                //State_NoEmailProcess
                //State_NoSmtpConfig
                mailSate = GlobalEnums.GlobalUIStates.Default;
            }
            #endregion

            GlobalEnums.GlobalUIStates smsSate;
            #region SMS STATE NORMALIZE
            if (formSMSState == (byte)GlobalEnums.NewFormMailStatus.State_WaitForRefDocument
                || formSMSState == (byte)GlobalEnums.NewFormMailStatus.State_WaitForCreateMailOrder
                || formSMSState == (byte)GlobalEnums.NewFormMailStatus.State_WaitForSend
                || formSMSState == (byte)GlobalEnums.NewFormMailStatus.State_WaitForCallBack)
            {
                smsSate = GlobalEnums.GlobalUIStates.Wait;
            }
            else if (formSMSState == (byte)GlobalEnums.NewFormMailStatus.Processing_CreateMailOrder
                || formSMSState == (byte)GlobalEnums.NewFormMailStatus.Processing_SendMail
                || formSMSState == (byte)GlobalEnums.NewFormMailStatus.DebugTest)
            {
                smsSate = GlobalEnums.GlobalUIStates.Processing;
            }
            else if (formSMSState == (byte)GlobalEnums.NewFormMailStatus.Error_CreateMailOrder
                || formSMSState == (byte)GlobalEnums.NewFormMailStatus.Error_SendMail
                || formSMSState == (byte)GlobalEnums.NewFormMailStatus.Error_CallBack)
            {
                smsSate = GlobalEnums.GlobalUIStates.Error;
            }
            else if (formSMSState == (byte)GlobalEnums.NewFormMailStatus.State_SuccessCallBack)
            {
                smsSate = GlobalEnums.GlobalUIStates.Success;
            }
            else
            {
                //Unknown
                //State_NoEmailProcess
                //State_NoSmtpConfig
                smsSate = GlobalEnums.GlobalUIStates.Default;
            }
            #endregion

            GlobalEnums.GlobalUIStates generalSate = GlobalEnums.GlobalUIStates.Default;
            string generalDescription;

            if (mailSate == smsSate)
            {
                generalSate = mailSate;
            }
            else if (mailSate == GlobalEnums.GlobalUIStates.Default || smsSate == GlobalEnums.GlobalUIStates.Default)
            {
                //sistemlerden biri kapalı, aktif sistemin durumu
                if (mailSate == GlobalEnums.GlobalUIStates.Default)
                {
                    generalSate = smsSate;
                }
                else
                {
                    generalSate = mailSate;
                }
            }
            else if (mailSate == GlobalEnums.GlobalUIStates.Processing || smsSate == GlobalEnums.GlobalUIStates.Processing)
            {
                generalSate = GlobalEnums.GlobalUIStates.Processing;
            }
            else if (mailSate == GlobalEnums.GlobalUIStates.Error || smsSate == GlobalEnums.GlobalUIStates.Error)
            {
                generalSate = GlobalEnums.GlobalUIStates.Error;
            }
            else if (mailSate == GlobalEnums.GlobalUIStates.Wait || smsSate == GlobalEnums.GlobalUIStates.Wait)
            {
                generalSate = GlobalEnums.GlobalUIStates.Wait;
            }

            switch (generalSate)
            {
                case GlobalEnums.GlobalUIStates.Wait:
                    {
                        generalDescription = "ISLEM BEKLIYOR";
                        break;
                    }
                case GlobalEnums.GlobalUIStates.Processing:
                    {
                        generalDescription = "ISLENIYOR";
                        break;
                    }
                case GlobalEnums.GlobalUIStates.Error:
                    {
                        generalDescription = "HATA";
                        break;
                    }
                case GlobalEnums.GlobalUIStates.Success:
                    {
                        generalDescription = "GONDERILDI";
                        break;
                    }
                default:
                    {
                        //GlobalEnums.GlobalUIStates.Default
                        generalDescription = "ISLEM YOK";
                        break;
                    }
            }

            return new GlobalStateClass()
            {
                State = generalSate,
                Description = generalDescription
            };
        }
        public static GlobalStateClass GetNewFormFaktoringStatus(int formFaktoringState)
        {
            if (formFaktoringState == (byte)GlobalEnums.NewFormFaktoringStatus.State_NoFaktoringOperation)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Default,
                    Description = "FK YAPILAMAZ"
                };
            }
            else if (formFaktoringState == (byte)GlobalEnums.NewFormFaktoringStatus.State_WaitForSignedDocument)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Wait,
                    Description = "IMZA BEKLIYOR"
                };
            }
            else if (formFaktoringState == (byte)GlobalEnums.NewFormFaktoringStatus.State_WaitForReportedDocument)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Wait,
                    Description = "FK RAPOR BEKLIYOR"
                };
            }
            else if (formFaktoringState == (byte)GlobalEnums.NewFormFaktoringStatus.State_WaitForApprovedFactoring)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Wait,
                    Description = "FK YAPILABILIR"
                };
            }
            else if (formFaktoringState == (byte)GlobalEnums.NewFormFaktoringStatus.State_WaitForTransferToPool)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "FK AKTARIM"
                };
            }
            else if (formFaktoringState == (byte)GlobalEnums.NewFormFaktoringStatus.Process_TransferringToPool)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "FK ISLENIYOR"
                };
            }
            else if (formFaktoringState == (byte)GlobalEnums.NewFormFaktoringStatus.State_SuccessTransferToPool)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Success,
                    Description = "FK HAVUZDA"
                };
            }
            else if (formFaktoringState == (byte)GlobalEnums.NewFormFaktoringStatus.Error_TransferToPool)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Error,
                    Description = "FK HATALI"
                };
            }

            return new GlobalStateClass()
            {
                State = GlobalEnums.GlobalUIStates.Default,
                Description = "BILINMIYOR"
            };
        }

        public static GlobalStateClass GetFaktoringDetailStatus(int faktoringDetailState)
        {
            if (faktoringDetailState == (byte)GlobalEnums.FaktoringDetailStatus.State_OfferCreated)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Wait,
                    Description = "TEKLIF CEVAP BEKLIYOR"
                };
            }
            else if (faktoringDetailState == (byte)GlobalEnums.FaktoringDetailStatus.State_OfferAccepted)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "FAKTORING ONAY BEKLIYOR"
                };
            }
            else if (faktoringDetailState == (byte)GlobalEnums.FaktoringDetailStatus.State_FaktoringApproved)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Processing,
                    Description = "FAKTORING SONUC BEKLIYOR"
                };
            }
            else if (faktoringDetailState == (byte)GlobalEnums.FaktoringDetailStatus.State_OfferRejected)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Error,
                    Description = "TEKLIF REDDEDILDI"
                };
            }
            else if (faktoringDetailState == (byte)GlobalEnums.FaktoringDetailStatus.State_FaktoringCancelled)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Error,
                    Description = "FAKTORING IPTAL"
                };
            }
            else if (faktoringDetailState == (byte)GlobalEnums.FaktoringDetailStatus.Error_FaktoringFailed)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Error,
                    Description = "FAKTORING BASARISIZ OLDU"
                };
            }
            else if (faktoringDetailState == (byte)GlobalEnums.FaktoringDetailStatus.State_FaktoringSuccess)
            {
                return new GlobalStateClass()
                {
                    State = GlobalEnums.GlobalUIStates.Success,
                    Description = "FAKTORING BASARIYLA SONUCLANDI"
                };
            }

            return new GlobalStateClass()
            {
                State = GlobalEnums.GlobalUIStates.Default,
                Description = "BILINMIYOR"
            };
        }
    }

    public class GlobalStateClass
    {
        public GlobalEnums.GlobalUIStates State { get; set; }

        public string Description { get; set; }
    }

    public static class HistoryDefaultTypeUtil
    {
        public static bool CheckIsRightHistoryType(byte historyTypeValue, HistoryDefaultTypes historyDefaultTypeChecker)
        {
            if ((historyTypeValue - (byte)historyDefaultTypeChecker) % 5 == 0)
                return true;
            return false;
        }
    }

}
