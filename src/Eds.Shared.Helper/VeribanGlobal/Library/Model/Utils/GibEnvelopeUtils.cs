using System.Globalization;
using System.Net;
using System.Text;
using System.Xml;
using Eds.Shared.Helper.VeribanGlobal.Library.Common;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes.EInvoice;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.GibEnvelope;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.Utils
{
    public static class GibEnvelopeUtils
    {

        public static GibEnvelopeHeaderInfo GetEnvelopeHeaderInfoFromZipFile(byte[] gibTransferData, bool isReceivedFromGIB, bool includeEnvelopeXmlElementList)
        {
            try
            {
                var gibTransferXmlFile = ZipPackage.GetCompressedByte(gibTransferData);
                if (gibTransferXmlFile != null && gibTransferXmlFile.Count == 1)
                {
                    string originalEnvelopeXmlContent = Encoding.UTF8.GetString(gibTransferXmlFile[0].Value);

                    return HandleGIBEnvelopeXmlDocument(originalEnvelopeXmlContent, isReceivedFromGIB, includeEnvelopeXmlElementList);
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }

            return null;
        }

        public static GibEnvelopeHeaderInfo HandleGIBEnvelopeXmlDocument(string originalEnvelopeXmlContent, bool isReceivedFromGIB, bool includeEnvelopeXmlElementList)
        {
            GibEnvelopeHeaderInfo envelopeHeaderInfo = null;

            try
            {
                envelopeHeaderInfo = new GibEnvelopeHeaderInfo();

                XmlDocument envelopeXmlDocument = new XmlManager().CreateXmlDocumentFromContent(originalEnvelopeXmlContent);
                if (envelopeXmlDocument != null && envelopeXmlDocument.ChildNodes.Count > 0)
                {
                    XmlElement sbd = null;
                    foreach (XmlNode item in envelopeXmlDocument.ChildNodes)
                    {
                        if (string.Equals(item.LocalName, "StandardBusinessDocument"))
                        {
                            sbd = (XmlElement)item;
                            break;
                        }
                    }
                    if (sbd != null && sbd.ChildNodes.Count > 0)
                    {
                        XmlElement sbdheader = null;
                        XmlElement packageTemp = null;
                        foreach (XmlNode item in sbd.ChildNodes)
                        {
                            if (string.Equals(item.LocalName, "StandardBusinessDocumentHeader"))
                                sbdheader = (XmlElement)item;
                            else if (string.Equals(item.LocalName, "Package"))
                                packageTemp = (XmlElement)item;
                        }
                        if (sbdheader != null && packageTemp != null)
                        {
                            #region HANDLE HEADER INFO(WITH PREFIX)
                            XmlElement sender = null;
                            XmlElement receiver = null;
                            XmlElement headerDocInfo = null;
                            foreach (XmlNode item in sbdheader.ChildNodes)
                            {
                                if (string.Equals(item.LocalName, "Sender"))
                                    sender = (XmlElement)item;
                                else if (string.Equals(item.LocalName, "Receiver"))
                                    receiver = (XmlElement)item;
                                else if (string.Equals(item.LocalName, "DocumentIdentification"))
                                    headerDocInfo = (XmlElement)item;
                            }
                            if (sender != null && receiver != null && headerDocInfo != null)
                            {
                                #region HANDLE HEADER SENDER INFO(WITH PREFIX)
                                XmlElement senderIdentifier = null;
                                List<XmlElement> senderContactList = new List<XmlElement>();
                                foreach (XmlNode item in sender.ChildNodes)
                                {
                                    if (string.Equals(item.LocalName, "Identifier"))
                                        senderIdentifier = (XmlElement)item;
                                    else if (string.Equals(item.LocalName, "ContactInformation"))
                                        senderContactList.Add((XmlElement)item);
                                }
                                if (senderIdentifier != null && senderContactList != null && senderContactList.Count > 0)
                                {
                                    envelopeHeaderInfo.SenderAlias = senderIdentifier.InnerText;
                                    foreach (var item in senderContactList)
                                    {
                                        XmlElement senderContactValue = null;
                                        XmlElement senderContactType = null;
                                        foreach (XmlNode subItem in item.ChildNodes)
                                        {
                                            if (string.Equals(subItem.LocalName, "Contact"))
                                                senderContactValue = (XmlElement)subItem;
                                            else if (string.Equals(subItem.LocalName, "ContactTypeIdentifier"))
                                                senderContactType = (XmlElement)subItem;
                                        }
                                        if (senderContactType != null && senderContactValue != null)
                                        {
                                            if (senderContactType.InnerXml.Equals("VKN_TCKN"))
                                            {
                                                envelopeHeaderInfo.SenderRegisterNumber = senderContactValue.InnerText;
                                            }
                                            else if (senderContactType.InnerXml.Equals("UNVAN"))
                                            {
                                                envelopeHeaderInfo.SenderTitle = senderContactValue.InnerText;
                                                //UNICODE HTML CHAR DECODE, &#125
                                                envelopeHeaderInfo.SenderTitle = WebUtility.HtmlDecode(envelopeHeaderInfo.SenderTitle);
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region HANDLE HEADER RECEIVER INFO(WITH PREFIX)

                                XmlElement receiverIdentifier = null;
                                List<XmlElement> receiverContactList = new List<XmlElement>();
                                foreach (XmlNode item in receiver.ChildNodes)
                                {
                                    if (string.Equals(item.LocalName, "Identifier"))
                                        receiverIdentifier = (XmlElement)item;
                                    else if (string.Equals(item.LocalName, "ContactInformation"))
                                        receiverContactList.Add((XmlElement)item);
                                }
                                if (receiverIdentifier != null && receiverContactList != null && receiverContactList.Count > 0)
                                {
                                    envelopeHeaderInfo.ReceiverAlias = receiverIdentifier.InnerText;
                                    foreach (var item in receiverContactList)
                                    {
                                        XmlElement receiverContactValue = null;
                                        XmlElement receiverContactType = null;
                                        foreach (XmlNode subItem in item.ChildNodes)
                                        {
                                            if (string.Equals(subItem.LocalName, "Contact"))
                                                receiverContactValue = (XmlElement)subItem;
                                            else if (string.Equals(subItem.LocalName, "ContactTypeIdentifier"))
                                                receiverContactType = (XmlElement)subItem;
                                        }
                                        if (receiverContactType != null && receiverContactValue != null)
                                        {
                                            if (receiverContactType.InnerXml.Equals("VKN_TCKN"))
                                            {
                                                envelopeHeaderInfo.ReceiverRegisterNumber = receiverContactValue.InnerText;
                                            }
                                            else if (receiverContactType.InnerXml.Equals("UNVAN"))
                                            {
                                                envelopeHeaderInfo.ReceiverTitle = receiverContactValue.InnerText;
                                                //UNICODE HTML CHAR DECODE, &#125
                                                envelopeHeaderInfo.ReceiverTitle = WebUtility.HtmlDecode(envelopeHeaderInfo.ReceiverTitle);
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region HANDLE HEADER DOCUMENT INFO(WITH PREFIX)
                                XmlElement docInstanceIdentifier = null;
                                XmlElement docType = null;
                                XmlElement docCreationTime = null;
                                foreach (XmlNode item in headerDocInfo.ChildNodes)
                                {
                                    if (string.Equals(item.LocalName, "InstanceIdentifier"))
                                        docInstanceIdentifier = (XmlElement)item;
                                    else if (string.Equals(item.LocalName, "Type"))
                                        docType = (XmlElement)item;
                                    else if (string.Equals(item.LocalName, "CreationDateAndTime"))
                                        docCreationTime = (XmlElement)item;
                                }
                                if (docInstanceIdentifier != null && docType != null && docCreationTime != null)
                                {
                                    envelopeHeaderInfo.EnvelopeIdentifier = docInstanceIdentifier.InnerText;
                                    envelopeHeaderInfo.EnvelopeDirectionType = docType.InnerText;

                                    //2018-08-07T00:01:03.8377929+03:00 => 2018-08-07T00:01:03
                                    string draftCreateTime = StringOperations.CustomSubString(docCreationTime.InnerXml, 19, true);
                                    envelopeHeaderInfo.EnvelopeCreationTime = DateTime.ParseExact(draftCreateTime, DateFormats.DateTimeGIBFormatLong, CultureInfo.InvariantCulture);
                                }
                                #endregion
                            }
                            #endregion

                            #region HANDLE PACKAGE INFO(NO PREFIX)
                            XmlElement elements = (XmlElement)packageTemp.GetElementsByTagName("Elements").Item(0);
                            if (elements != null)
                            {
                                XmlElement elementType = (XmlElement)elements.GetElementsByTagName("ElementType").Item(0);
                                if (elementType != null) envelopeHeaderInfo.EnvelopeElementType = elementType.InnerText;

                                XmlElement elementCount = (XmlElement)elements.GetElementsByTagName("ElementCount").Item(0);
                                if (elementCount != null) envelopeHeaderInfo.EnvelopeElementCount = Convert.ToInt32(elementCount.InnerXml);
                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception) { envelopeHeaderInfo = null; }

            if (envelopeHeaderInfo != null)
            {
                //ALL DATA CONTROL
                try
                {
                    if (string.IsNullOrWhiteSpace(envelopeHeaderInfo.SenderRegisterNumber)) throw new ArgumentException("Unknown SenderRegisterNumber");
                    if (string.IsNullOrWhiteSpace(envelopeHeaderInfo.SenderAlias)) throw new ArgumentException("Unknown SenderAlias");
                    if (string.IsNullOrWhiteSpace(envelopeHeaderInfo.EnvelopeIdentifier)) throw new ArgumentException("Unknown EnvelopeIdentifier");
                    if (string.IsNullOrWhiteSpace(envelopeHeaderInfo.EnvelopeDirectionType)) throw new ArgumentException("Unknown EnvelopeDirectionType");
                    if (string.IsNullOrWhiteSpace(envelopeHeaderInfo.EnvelopeElementType)) throw new ArgumentException("Unknown EnvelopeElementType");

                    envelopeHeaderInfo.EnvelopeDocumentType = (byte)EInvoiceEnums.ReceivedGibQueueEnvelopeDocumentType.None;
                    if (envelopeHeaderInfo.EnvelopeDirectionType.Equals("SENDERENVELOPE"))
                    {
                        if (string.IsNullOrWhiteSpace(envelopeHeaderInfo.ReceiverRegisterNumber)) throw new ArgumentException("Unknown ReceiverRegisterNumber");
                        if (string.IsNullOrWhiteSpace(envelopeHeaderInfo.ReceiverAlias)) throw new ArgumentException("Unknown ReceiverAlias");

                        if (envelopeHeaderInfo.EnvelopeElementType.Equals("INVOICE"))
                        {
                            if (isReceivedFromGIB)
                                envelopeHeaderInfo.EnvelopeDocumentType = (byte)EInvoiceEnums.ReceivedGibQueueEnvelopeDocumentType.PurchaseInvoiceEnvelope;
                            else
                                envelopeHeaderInfo.EnvelopeDocumentType = (byte)EInvoiceEnums.SendGibQueueEnvelopeDocumentType.SalesInvoiceEnvelope;
                        }
                        else if (envelopeHeaderInfo.EnvelopeElementType.Equals("DESPATCHADVICE"))
                        {
                            if (isReceivedFromGIB)
                                envelopeHeaderInfo.EnvelopeDocumentType = (byte)EInvoiceEnums.ReceivedGibQueueEnvelopeDocumentType.PurchaseDespatchEnvelope;
                            else
                                envelopeHeaderInfo.EnvelopeDocumentType = (byte)EInvoiceEnums.SendGibQueueEnvelopeDocumentType.SalesDespatchEnvelope;
                        }
                    }
                    else if (envelopeHeaderInfo.EnvelopeDirectionType.Equals("POSTBOXENVELOPE"))
                    {
                        if (string.IsNullOrWhiteSpace(envelopeHeaderInfo.ReceiverRegisterNumber)) throw new ArgumentException("Unknown ReceiverRegisterNumber");
                        if (string.IsNullOrWhiteSpace(envelopeHeaderInfo.ReceiverAlias)) throw new ArgumentException("Unknown ReceiverAlias");

                        if (envelopeHeaderInfo.EnvelopeElementType.Equals("APPLICATIONRESPONSE"))
                        {
                            if (isReceivedFromGIB)
                                envelopeHeaderInfo.EnvelopeDocumentType = (byte)EInvoiceEnums.ReceivedGibQueueEnvelopeDocumentType.SalesInvoiceAnswerEnvelope;
                            else
                                envelopeHeaderInfo.EnvelopeDocumentType = (byte)EInvoiceEnums.SendGibQueueEnvelopeDocumentType.PurchaseInvoiceAnswerEnvelope;
                        }
                        else if (envelopeHeaderInfo.EnvelopeElementType.Equals("RECEIPTADVICE"))
                        {
                            if (isReceivedFromGIB)
                                envelopeHeaderInfo.EnvelopeDocumentType = (byte)EInvoiceEnums.ReceivedGibQueueEnvelopeDocumentType.SalesDespatchAnswerEnvelope;
                            else
                                envelopeHeaderInfo.EnvelopeDocumentType = (byte)EInvoiceEnums.SendGibQueueEnvelopeDocumentType.PurchaseDespatchAnswerEnvelope;
                        }
                    }
                    else if (envelopeHeaderInfo.EnvelopeDirectionType.Equals("SYSTEMENVELOPE") && envelopeHeaderInfo.EnvelopeElementType.Equals("APPLICATIONRESPONSE"))
                    {
                        envelopeHeaderInfo.EnvelopeDocumentType = (byte)EInvoiceEnums.ReceivedGibQueueEnvelopeDocumentType.SystemResponse;
                    }
                    if (envelopeHeaderInfo.EnvelopeDocumentType == (byte)EInvoiceEnums.ReceivedGibQueueEnvelopeDocumentType.None)
                    {
                        throw new ArgumentException("Unknown EnvelopeDocumentType");
                    }

                    if (includeEnvelopeXmlElementList)
                    {
                        //ORJINAL ZARF IÇERIĞI TEXT DEGISTIRILMEMELI
                        int appStartIndex = originalEnvelopeXmlContent.IndexOf("<ElementList>");
                        int appEndIndex = originalEnvelopeXmlContent.IndexOf("</ElementList>", appStartIndex);
                        string originalElementListContent = originalEnvelopeXmlContent.Substring(appStartIndex + ("<ElementList>").Length, appEndIndex - appStartIndex - ("</ElementList>").Length + 1);

                        //ZARF ICERISINDEKI DOKUMANLAR AYRISTIRILIYOR
                        List<string> packageElementContentList = SplitPackageElementList(envelopeHeaderInfo.EnvelopeDocumentType, envelopeHeaderInfo.EnvelopeElementCount, originalElementListContent);
                        if (packageElementContentList != null && packageElementContentList.Count == envelopeHeaderInfo.EnvelopeElementCount)
                        {
                            envelopeHeaderInfo.ElementObjectList = new List<object>();
                            foreach (string packageElementItem in packageElementContentList)
                            {
                                if (envelopeHeaderInfo.EnvelopeDocumentType == (byte)EInvoiceEnums.ReceivedGibQueueEnvelopeDocumentType.SystemResponse)
                                {
                                    //SYSTEM RESPONSE HANDLE
                                    GibEnvelopeElementSysInfo sysInfo = CreateGibEnvelopeElementSysInfo(packageElementItem);
                                    if (sysInfo != null)
                                    {
                                        envelopeHeaderInfo.ElementObjectList.Add(sysInfo);
                                    }
                                }
                                else if (envelopeHeaderInfo.EnvelopeDocumentType == (byte)EInvoiceEnums.ReceivedGibQueueEnvelopeDocumentType.PurchaseInvoiceEnvelope
                                    || envelopeHeaderInfo.EnvelopeDocumentType == (byte)EInvoiceEnums.SendGibQueueEnvelopeDocumentType.SalesInvoiceEnvelope)
                                {
                                    //DOCUMENT HANDLE
                                    GibEnvelopeElementDocInfo docInfo = CreateGibEnvelopeElementInvoiceInfo(packageElementItem);
                                    if (docInfo != null)
                                    {
                                        envelopeHeaderInfo.ElementObjectList.Add(docInfo);
                                    }
                                }
                                else if (envelopeHeaderInfo.EnvelopeDocumentType == (byte)EInvoiceEnums.ReceivedGibQueueEnvelopeDocumentType.PurchaseDespatchEnvelope
                                    || envelopeHeaderInfo.EnvelopeDocumentType == (byte)EInvoiceEnums.SendGibQueueEnvelopeDocumentType.SalesDespatchEnvelope)
                                {
                                    //DOCUMENT HANDLE
                                    GibEnvelopeElementDocInfo docInfo = CreateGibEnvelopeElementDespatchInfo(packageElementItem);
                                    if (docInfo != null)
                                    {
                                        envelopeHeaderInfo.ElementObjectList.Add(docInfo);
                                    }
                                }
                                else if (envelopeHeaderInfo.EnvelopeDocumentType == (byte)EInvoiceEnums.ReceivedGibQueueEnvelopeDocumentType.SalesInvoiceAnswerEnvelope
                                    || envelopeHeaderInfo.EnvelopeDocumentType == (byte)EInvoiceEnums.SendGibQueueEnvelopeDocumentType.PurchaseInvoiceAnswerEnvelope)
                                {
                                    //ANSWER HANDLE
                                    GibEnvelopeElementInvoiceAnswerInfo answerInfo = CreateGibEnvelopeElementInvoiceAnswerInfo(packageElementItem);
                                    if (answerInfo != null)
                                    {
                                        envelopeHeaderInfo.ElementObjectList.Add(answerInfo);
                                    }
                                }
                                else if (envelopeHeaderInfo.EnvelopeDocumentType == (byte)EInvoiceEnums.ReceivedGibQueueEnvelopeDocumentType.SalesDespatchAnswerEnvelope
                                    || envelopeHeaderInfo.EnvelopeDocumentType == (byte)EInvoiceEnums.SendGibQueueEnvelopeDocumentType.PurchaseDespatchAnswerEnvelope)
                                {
                                    //ANSWER HANDLE
                                    GibEnvelopeElementDespatchAnswerInfo answerInfo = CreateGibEnvelopeElementDespatchAnswerInfo(packageElementItem);
                                    if (answerInfo != null)
                                    {
                                        envelopeHeaderInfo.ElementObjectList.Add(answerInfo);
                                    }
                                }
                                else throw new ArgumentException("Unknown envelope document type");
                            }//END FOR

                            if (envelopeHeaderInfo.ElementObjectList.Count != envelopeHeaderInfo.EnvelopeElementCount)
                            {
                                throw new ArgumentException("Unknown EnvelopeElementListContent");
                            }
                        }
                        else throw new ArgumentException("Unknown EnvelopeElementListContent");
                    }
                }
                catch (Exception) { envelopeHeaderInfo = null; }

                return envelopeHeaderInfo;
            }

            return null;
        }

        private static List<string> SplitPackageElementList(byte envelopeDocumentType, int packageElementCount, string packageElementLines)
        {
            List<string> invoiceXmlContents = new List<string>();

            try
            {
                string searchTag = string.Empty;
                if (envelopeDocumentType == (byte)EInvoiceEnums.ReceivedGibQueueEnvelopeDocumentType.SystemResponse
                    || envelopeDocumentType == (byte)EInvoiceEnums.ReceivedGibQueueEnvelopeDocumentType.SalesInvoiceAnswerEnvelope
                    || envelopeDocumentType == (byte)EInvoiceEnums.SendGibQueueEnvelopeDocumentType.PurchaseInvoiceAnswerEnvelope)
                {
                    searchTag = "ApplicationResponse";
                }
                else if (envelopeDocumentType == (byte)EInvoiceEnums.ReceivedGibQueueEnvelopeDocumentType.PurchaseInvoiceEnvelope
                    || envelopeDocumentType == (byte)EInvoiceEnums.SendGibQueueEnvelopeDocumentType.SalesInvoiceEnvelope)
                {
                    searchTag = "Invoice";
                }
                else if (envelopeDocumentType == (byte)EInvoiceEnums.ReceivedGibQueueEnvelopeDocumentType.PurchaseDespatchEnvelope
                    || envelopeDocumentType == (byte)EInvoiceEnums.SendGibQueueEnvelopeDocumentType.SalesDespatchEnvelope)
                {
                    searchTag = "DespatchAdvice";
                }
                else if (envelopeDocumentType == (byte)EInvoiceEnums.ReceivedGibQueueEnvelopeDocumentType.SalesDespatchAnswerEnvelope
                    || envelopeDocumentType == (byte)EInvoiceEnums.SendGibQueueEnvelopeDocumentType.PurchaseDespatchAnswerEnvelope)
                {
                    searchTag = "ReceiptAdvice";
                }
                else throw new ArgumentException("Unknown envelope document type");

                string packageElementLinesTemp = packageElementLines;
                for (int i = 0; i < packageElementCount; i++)
                {
                    int appStartIndex = packageElementLinesTemp.IndexOf("<" + searchTag);

                    //Invoice namespace kullanılmış olabilir. <ns1:Invoice> gibi
                    if (appStartIndex == -1)
                    {
                        int tagIndex = packageElementLinesTemp.IndexOf("<");
                        int invoiceIndex = packageElementLinesTemp.IndexOf(searchTag);
                        string strPrefix = packageElementLinesTemp.Substring((tagIndex + 1), invoiceIndex - (tagIndex + 1));

                        searchTag = strPrefix + searchTag;
                        appStartIndex = packageElementLinesTemp.IndexOf("<" + searchTag);
                    }

                    int appEndIndex = packageElementLinesTemp.IndexOf("</" + searchTag + ">", appStartIndex);
                    if (appEndIndex != -1)
                    {
                        invoiceXmlContents.Add(packageElementLinesTemp.Substring(appStartIndex, appEndIndex - appStartIndex) + "</" + searchTag + ">");
                    }

                    packageElementLinesTemp = packageElementLinesTemp.Substring(appEndIndex + ("</" + searchTag + ">").Length);

                }//FOR END
            }
            catch (Exception) { invoiceXmlContents = new List<string>(); }

            return invoiceXmlContents;
        }
        private static GibEnvelopeElementSysInfo CreateGibEnvelopeElementSysInfo(string sysXmlContent)
        {
            GibEnvelopeElementSysInfo sysInfo = null;

            try
            {
                sysInfo = new GibEnvelopeElementSysInfo();

                XmlDocument xdocAppResp = GetSafeXmlContent(sysXmlContent, "ApplicationResponse");

                #region ManuelDeserialize

                List<KeyValuePair<string, string>> partyIdfDataList = new List<KeyValuePair<string, string>>();

                foreach (XmlElement item in xdocAppResp.FirstChild.ChildNodes.OfType<XmlElement>())
                {
                    if (string.Equals(item.LocalName, "UUID"))
                    {
                        sysInfo.SystemResponseUUID = item.InnerText;
                    }
                    else if (string.Equals(item.LocalName, "SenderParty"))
                    {
                        partyIdfDataList = GetSenderPartyIdList(item);
                    }
                    else if (string.Equals(item.LocalName, "DocumentResponse"))
                    {
                        XmlElement docResp = null;

                        docResp = item;

                        foreach (XmlElement i in docResp.ChildNodes.OfType<XmlElement>())
                        {
                            if (string.Equals(i.LocalName, "Response"))
                            {
                                XmlElement resp = null;

                                resp = i;

                                foreach (XmlElement respItem in resp.ChildNodes.OfType<XmlElement>())
                                {
                                    if (string.Equals(respItem.LocalName, "ResponseCode"))
                                    {
                                        //S_APR (AnswerName)
                                        //sysInfo.AnswerName = respItem.InnerText
                                    }
                                    else if (string.Equals(respItem.LocalName, "Description"))
                                    {
                                        //APPLICATIONRESPONSE (AnswerDescription)
                                        //sysInfo.AnswerDesc = respItem.InnerText
                                    }
                                }
                            }
                            else if (string.Equals(i.LocalName, "DocumentReference"))
                            {
                                foreach (XmlElement docRefItem in i.ChildNodes.OfType<XmlElement>())
                                {
                                    if (string.Equals(docRefItem.LocalName, "ID"))
                                    {
                                        //SYS RESPONSE ENVELOPE IDENTIFIER
                                        sysInfo.ReferenceEnvelopeIdentifier = docRefItem.InnerText;
                                    }
                                    else if (string.Equals(docRefItem.LocalName, "IssueDate"))
                                    {
                                        //SYS RESPONSE ENVELOPE ISSUE DATE
                                        sysInfo.ReferenceEnvelopeIssueDate = docRefItem.InnerText;
                                    }
                                    else if (string.Equals(docRefItem.LocalName, "DocumentTypeCode"))
                                    {
                                        //SYS RESPONSE ENVELOPE DOCUMENT TYPE CODE
                                        sysInfo.ReferenceDocumentTypeCode = docRefItem.InnerText;
                                    }
                                    else if (string.Equals(docRefItem.LocalName, "DocumentType"))
                                    {
                                        //SYS RESPONSE ENVELOPE DOCUMENT TYPE
                                        sysInfo.ReferenceDocumentType = docRefItem.InnerText;
                                    }
                                }
                            }
                            else if (string.Equals(i.LocalName, "LineResponse"))
                            {
                                XmlElement docLineResp = null;
                                XmlElement docLineRespResp = null;

                                docLineResp = i;

                                foreach (XmlElement LineItem in docLineResp.OfType<XmlElement>())
                                {
                                    if (string.Equals(LineItem.LocalName, "Response"))
                                    {
                                        docLineRespResp = LineItem;

                                        foreach (XmlElement docLineRespRespItem in docLineRespResp.OfType<XmlElement>())
                                        {
                                            if (string.Equals(docLineRespRespItem.LocalName, "ResponseCode"))
                                            {
                                                //1200
                                                sysInfo.ResultCode = docLineRespRespItem.InnerText;
                                            }
                                            else if (string.Equals(docLineRespRespItem.LocalName, "Description"))
                                            {
                                                //BASARIYLA ISLENDI
                                                sysInfo.ResultDescription = docLineRespRespItem.InnerText;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                if (partyIdfDataList.Count > 0)
                {
                    foreach (var item in partyIdfDataList)
                    {
                        if (item.Key.Equals("GTB_REFNO"))
                        {
                            sysInfo.GTBRefNumber = item.Value;
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(sysInfo.ReferenceEnvelopeIdentifier)) throw new ArgumentException("Unknown ReferenceEnvelopeIdentifier");
                if (string.IsNullOrWhiteSpace(sysInfo.ResultCode)) throw new ArgumentException("Unknown ResultCode");
                if (string.IsNullOrWhiteSpace(sysInfo.ResultDescription)) sysInfo.ResultDescription = sysInfo.ResultCode;
            }
            catch (Exception) { sysInfo = null; }

            return sysInfo;
        }
        private static GibEnvelopeElementInvoiceAnswerInfo CreateGibEnvelopeElementInvoiceAnswerInfo(string answerXmlContent)
        {
            GibEnvelopeElementInvoiceAnswerInfo answerInfo = null;

            try
            {
                answerInfo = new GibEnvelopeElementInvoiceAnswerInfo();
                answerInfo.AnswerDocumentXmlContent = answerXmlContent;

                XmlDocument xdocAppResp = GetSafeXmlContent(answerXmlContent, "ApplicationResponse");

                #region ManuelDeserialize

                List<KeyValuePair<string, string>> partyIdfDataList = new List<KeyValuePair<string, string>>();

                string docResponseName = null;
                string docResponseDesc = null;
                string lineResponseName = null;
                string lineResponseDesc = null;

                string answerDateStr = null;
                string answerTimeStr = null;
                foreach (XmlElement item in xdocAppResp.FirstChild.ChildNodes.OfType<XmlElement>())
                {
                    if (string.Equals(item.LocalName, "ID"))
                    {
                        answerInfo.AnswerDocumentID = item.InnerText;
                    }
                    else if (string.Equals(item.LocalName, "SenderParty"))
                    {
                        partyIdfDataList = GetSenderPartyIdList(item);
                    }
                    else if (string.Equals(item.LocalName, "UUID"))
                    {
                        answerInfo.AnswerDocumentUUID = item.InnerText;
                    }
                    else if (string.Equals(item.LocalName, "IssueDate"))
                    {
                        answerDateStr = StringOperations.CustomSubString(item.InnerXml, 10, true);
                    }
                    else if (string.Equals(item.LocalName, "IssueTime"))
                    {
                        answerTimeStr = StringOperations.CustomSubString(item.InnerXml, 8, true);
                    }
                    else if (string.Equals(item.LocalName, "Note"))
                    {
                        answerInfo.AnswerDocumentNote = item.InnerText;
                    }
                    else if (string.Equals(item.LocalName, "DocumentResponse"))
                    {
                        XmlElement docResponse = null;

                        docResponse = item;

                        foreach (XmlElement r in docResponse.ChildNodes.OfType<XmlElement>())
                        {
                            if (string.Equals(r.LocalName, "Response"))
                            {
                                XmlElement response = null;

                                response = r;

                                foreach (XmlElement respItem in response.ChildNodes.OfType<XmlElement>())
                                {
                                    if (string.Equals(respItem.LocalName, "ResponseCode"))
                                    {
                                        //KABUL
                                        docResponseName = respItem.InnerText;
                                    }
                                    else if (string.Equals(respItem.LocalName, "Description"))
                                    {
                                        //Fatura kabul edildi.
                                        docResponseDesc = respItem.InnerText;
                                    }
                                }
                            }
                            else if (string.Equals(r.LocalName, "DocumentReference"))
                            {
                                foreach (XmlElement docRefItem in r.ChildNodes.OfType<XmlElement>())
                                {
                                    if (string.Equals(docRefItem.LocalName, "ID"))
                                    {
                                        //REFERENCE DOCUMENT IDENTIFIER
                                        answerInfo.ReferenceDocumentUUID = docRefItem.InnerText;
                                    }
                                    else if (string.Equals(docRefItem.LocalName, "DocumentTypeCode"))
                                    {
                                        //REFERENCE DOCUMENT TYPE CODE
                                        answerInfo.ReferenceDocumentTypeCode = docRefItem.InnerText;
                                    }
                                    else if (string.Equals(docRefItem.LocalName, "DocumentType"))
                                    {
                                        //REFERENCE DOCUMENT TYPE
                                        answerInfo.ReferenceDocumentType = docRefItem.InnerText;
                                    }
                                }
                            }
                            else if (string.Equals(r.LocalName, "LineResponse"))
                            {
                                XmlElement docLineResponse = null;
                                XmlElement docLineRespResponse = null;

                                docLineResponse = r;

                                foreach (XmlElement LineItem in docLineResponse.OfType<XmlElement>())
                                {
                                    if (string.Equals(LineItem.LocalName, "Response"))
                                    {
                                        docLineRespResponse = LineItem;

                                        foreach (XmlElement docLineRespRespItem in docLineRespResponse.OfType<XmlElement>())
                                        {
                                            if (string.Equals(docLineRespRespItem.LocalName, "ResponseCode"))
                                            {
                                                //KABUL
                                                lineResponseName = docLineRespRespItem.InnerText;
                                            }
                                            else if (string.Equals(docLineRespRespItem.LocalName, "Description"))
                                            {
                                                //Fatura kabul edildi.
                                                lineResponseDesc = docLineRespRespItem.InnerText;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                if (partyIdfDataList.Count > 0)
                {
                    foreach (var item in partyIdfDataList)
                    {
                        if (item.Key.Equals(PartySchemeIdType.GTBREFNO.ToString()))
                        {
                            answerInfo.GTB_REFNO = item.Value;
                        }
                        else if (item.Key.Equals(PartySchemeIdType.GTBGCBTESCILNO.ToString()))
                        {
                            answerInfo.GTB_GCB_TESCILNO = item.Value;
                        }
                        else if (item.Key.Equals(PartySchemeIdType.GTBFIILIIHRACATTARIHI.ToString()))
                        {
                            answerInfo.GTB_FIILI_IHRACAT_TARIHI = item.Value;
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(answerInfo.AnswerDocumentID)) throw new ArgumentException("Unknown AnswerDocumentID");
                if (string.IsNullOrWhiteSpace(answerInfo.AnswerDocumentUUID)) throw new ArgumentException("Unknown AnswerDocumentUUID");
                if (string.IsNullOrWhiteSpace(answerInfo.ReferenceDocumentUUID)) throw new ArgumentException("Unknown ReferenceDocumentUUID");

                //cevap bilgisi bazı firmalar document tarafına bazıları line tarafına yazdığı için kontrol edilmeli.
                if (!string.IsNullOrWhiteSpace(docResponseName))
                    answerInfo.AnswerName = docResponseName;
                else if (!string.IsNullOrWhiteSpace(lineResponseName))
                    answerInfo.AnswerName = lineResponseName;

                if (!string.IsNullOrWhiteSpace(docResponseDesc))
                    answerInfo.AnswerDescription = docResponseDesc;
                else if (!string.IsNullOrWhiteSpace(lineResponseDesc))
                    answerInfo.AnswerDescription = lineResponseDesc;

                if (string.IsNullOrWhiteSpace(answerInfo.AnswerName)) throw new ArgumentException("Unknown AnswerName");

                //2018-08-07T00:01:03.8377929+03:00 => 2018-08-07T00:01:03
                if (!string.IsNullOrEmpty(answerTimeStr))
                {
                    answerDateStr += "T" + answerTimeStr;
                }
                else
                {
                    answerDateStr += "T00:00:00";
                }
                string draftAnswerTime = StringOperations.CustomSubString(answerDateStr, 19, true);
                answerInfo.AnswerTime = DateTime.ParseExact(draftAnswerTime, DateFormats.DateTimeGIBFormatLong, CultureInfo.InvariantCulture);

                if (answerInfo.AnswerTime == DateTime.MinValue) throw new ArgumentException("Unknown AnswerTime");
            }
            catch (Exception) { answerInfo = null; }

            return answerInfo;
        }
        private static GibEnvelopeElementDespatchAnswerInfo CreateGibEnvelopeElementDespatchAnswerInfo(string answerXmlContent)
        {
            GibEnvelopeElementDespatchAnswerInfo answerInfo = null;

            try
            {
                answerInfo = new GibEnvelopeElementDespatchAnswerInfo();
                answerInfo.DespatchAnswerDocumentXmlContent = answerXmlContent;

                XmlDocument xdocAppResp = GetSafeXmlContent(answerXmlContent, "ReceiptAdvice");

                #region ManuelDeserialize

                List<XmlElement> receiptLineList = new List<XmlElement>();

                string answerDateStr = null;
                string answerTimeStr = null;
                foreach (XmlElement item in xdocAppResp.FirstChild.ChildNodes.OfType<XmlElement>())
                {
                    if (string.Equals(item.LocalName, "ID"))
                    {
                        answerInfo.DespatchAnswerDocumentID = item.InnerText;
                    }
                    else if (string.Equals(item.LocalName, "UUID"))
                    {
                        answerInfo.DespatchAnswerDocumentUUID = item.InnerText;
                    }
                    else if (string.Equals(item.LocalName, "IssueDate"))
                    {
                        answerDateStr = StringOperations.CustomSubString(item.InnerXml, 10, true);
                    }
                    else if (string.Equals(item.LocalName, "IssueTime"))
                    {
                        answerTimeStr = StringOperations.CustomSubString(item.InnerXml, 8, true);
                    }
                    else if (string.Equals(item.LocalName, "Note"))
                    {
                        answerInfo.DespatchAnswerDocumentNote = item.InnerText;
                    }
                    else if (string.Equals(item.LocalName, "DespatchDocumentReference"))
                    {
                        foreach (XmlElement docRefItem in item.ChildNodes.OfType<XmlElement>())
                        {
                            if (string.Equals(docRefItem.LocalName, "ID"))
                            {
                                //REFERENCE DOCUMENT IDENTIFIER
                                answerInfo.DespatchDocumentReferenceUniqueId = docRefItem.InnerText;
                            }
                        }
                    }
                    else if (string.Equals(item.LocalName, "ReceiptLine"))
                    {
                        receiptLineList.Add(item);
                    }
                }

                if (receiptLineList.Count > 0)
                {
                    answerInfo.DespatchAnswerDespatchLines = new List<GibEnvelopeElementDespatchAnswerDetailInfo>();
                    foreach (var receiptLine in receiptLineList)
                    {
                        GibEnvelopeElementDespatchAnswerDetailInfo detail = new GibEnvelopeElementDespatchAnswerDetailInfo();
                        foreach (XmlElement i in receiptLine.ChildNodes.OfType<XmlElement>())
                        {
                            decimal value = 0;
                            if (string.Equals(i.LocalName, "ID"))
                            {
                                int lineId = 0;
                                int.TryParse(i.InnerXml, out lineId);
                                detail.LineID = lineId;
                            }
                            else if (string.Equals(i.LocalName, "ReceivedQuantity"))
                            {
                                decimal.TryParse(i.InnerXml, out value);
                                detail.ReceivedQuantity = value;
                            }
                            else if (string.Equals(i.LocalName, "ShortQuantity"))
                            {
                                decimal.TryParse(i.InnerXml, out value);
                                detail.ShortQuantity = value;
                            }
                            else if (string.Equals(i.LocalName, "OversupplyQuantity"))
                            {
                                decimal.TryParse(i.InnerXml, out value);
                                detail.OversupplyQuantity = value;
                            }
                            else if (string.Equals(i.LocalName, "RejectedQuantity"))
                            {
                                decimal.TryParse(i.InnerXml, out value);
                                detail.RejectedQuantity = value;
                            }
                            else if (string.Equals(i.LocalName, "RejectReason"))
                            {
                                if (!string.IsNullOrWhiteSpace(i.InnerXml))
                                    detail.RejectReason = i.InnerText;
                            }
                            else if (string.Equals(i.LocalName, "TimingComplaint") && !string.IsNullOrWhiteSpace(i.InnerXml))
                            {
                                detail.TimingComplaint = i.InnerText;
                            }
                        }
                        answerInfo.DespatchAnswerDespatchLines.Add(detail);
                    }
                }

                #endregion

                if (string.IsNullOrWhiteSpace(answerInfo.DespatchAnswerDocumentID)) throw new ArgumentException("Unknown AnswerDocumentID");
                if (string.IsNullOrWhiteSpace(answerInfo.DespatchAnswerDocumentUUID)) throw new ArgumentException("Unknown AnswerDocumentUUID");
                if (string.IsNullOrWhiteSpace(answerInfo.DespatchDocumentReferenceUniqueId)) throw new ArgumentException("Unknown ReferenceDocumentUUID");
                if (string.IsNullOrWhiteSpace(answerDateStr)) throw new ArgumentException("Unknown AnswerDate");
                if (!(answerInfo.DespatchAnswerDespatchLines != null && answerInfo.DespatchAnswerDespatchLines.Count > 0)) throw new ArgumentException("Unknown AnswerLines");

                //2018-08-07T00:01:03.8377929+03:00 => 2018-08-07T00:01:03
                if (!string.IsNullOrEmpty(answerTimeStr))
                {
                    answerDateStr += "T" + answerTimeStr;
                }
                else
                {
                    answerDateStr += "T00:00:00";
                }
                string draftAnswerTime = StringOperations.CustomSubString(answerDateStr, 19, true);
                answerInfo.DespatchAnswerTime = DateTime.ParseExact(draftAnswerTime, DateFormats.DateTimeGIBFormatLong, CultureInfo.InvariantCulture);

                if (answerInfo.DespatchAnswerTime == DateTime.MinValue) throw new ArgumentException("Unknown AnswerTime");
            }
            catch (Exception) { answerInfo = null; }

            return answerInfo;
        }
        private static GibEnvelopeElementDocInfo CreateGibEnvelopeElementInvoiceInfo(string invoiceXmlContent)
        {
            GibEnvelopeElementDocInfo invoiceInfo = null;

            try
            {
                invoiceInfo = new GibEnvelopeElementDocInfo();
                invoiceInfo.DocumentXmlContent = invoiceXmlContent;

                XmlDocument xdocAppResp = GetSafeXmlContent(invoiceXmlContent, "Invoice");

                #region ManuelDeserialize

                string answerDateStr = null;
                string answerTimeStr = null;
                foreach (XmlElement item in xdocAppResp.FirstChild.ChildNodes.OfType<XmlElement>())
                {
                    if (string.Equals(item.LocalName, "ID"))
                    {
                        invoiceInfo.DocumentID = item.InnerText;
                    }
                    else if (string.Equals(item.LocalName, "UUID"))
                    {
                        invoiceInfo.DocumentUUID = item.InnerText;
                    }
                    else if (string.Equals(item.LocalName, "IssueDate"))
                    {
                        answerDateStr = StringOperations.CustomSubString(item.InnerXml, 10, true);
                    }
                    else if (string.Equals(item.LocalName, "IssueTime"))
                    {
                        answerTimeStr = StringOperations.CustomSubString(item.InnerXml, 8, true);
                    }
                }

                #endregion

                if (string.IsNullOrWhiteSpace(invoiceInfo.DocumentID)) throw new ArgumentException("Unknown DocumentID");
                if (string.IsNullOrWhiteSpace(invoiceInfo.DocumentUUID)) throw new ArgumentException("Unknown DocumentUUID");

                //2018-08-07T00:01:03.8377929+03:00 => 2018-08-07T00:01:03
                if (!string.IsNullOrEmpty(answerTimeStr))
                {
                    answerDateStr += "T" + answerTimeStr;
                }
                else
                {
                    answerDateStr += "T00:00:00";
                }
                string draftAnswerTime = StringOperations.CustomSubString(answerDateStr, 19, true);
                invoiceInfo.IssueTime = DateTime.ParseExact(draftAnswerTime, DateFormats.DateTimeGIBFormatLong, CultureInfo.InvariantCulture);

                if (invoiceInfo.IssueTime == DateTime.MinValue) throw new ArgumentException("Unknown IssueTime");
            }
            catch (Exception) { invoiceInfo = null; }

            return invoiceInfo;
        }
        private static GibEnvelopeElementDocInfo CreateGibEnvelopeElementDespatchInfo(string despatchXmlContent)
        {
            GibEnvelopeElementDocInfo despatchInfo = null;

            try
            {
                despatchInfo = new GibEnvelopeElementDocInfo();
                despatchInfo.DocumentXmlContent = despatchXmlContent;

                XmlDocument xdocAppResp = GetSafeXmlContent(despatchXmlContent, "DespatchAdvice");

                #region ManuelDeserialize

                string answerDateStr = null;
                string answerTimeStr = null;
                foreach (XmlElement item in xdocAppResp.FirstChild.ChildNodes.OfType<XmlElement>())
                {
                    if (string.Equals(item.LocalName, "ID"))
                    {
                        despatchInfo.DocumentID = item.InnerText;
                    }
                    else if (string.Equals(item.LocalName, "UUID"))
                    {
                        despatchInfo.DocumentUUID = item.InnerText;
                    }
                    else if (string.Equals(item.LocalName, "IssueDate"))
                    {
                        answerDateStr = StringOperations.CustomSubString(item.InnerXml, 10, true);
                    }
                    else if (string.Equals(item.LocalName, "IssueTime"))
                    {
                        answerTimeStr = StringOperations.CustomSubString(item.InnerXml, 8, true);
                    }
                }

                #endregion

                if (string.IsNullOrWhiteSpace(despatchInfo.DocumentID)) throw new ArgumentException("Unknown DocumentID");
                if (string.IsNullOrWhiteSpace(despatchInfo.DocumentUUID)) throw new ArgumentException("Unknown DocumentUUID");

                //2018-08-07T00:01:03.8377929+03:00 => 2018-08-07T00:01:03
                if (!string.IsNullOrEmpty(answerTimeStr))
                {
                    answerDateStr += "T" + answerTimeStr;
                }
                else
                {
                    answerDateStr += "T00:00:00";
                }
                string draftAnswerTime = StringOperations.CustomSubString(answerDateStr, 19, true);
                despatchInfo.IssueTime = DateTime.ParseExact(draftAnswerTime, DateFormats.DateTimeGIBFormatLong, CultureInfo.InvariantCulture);

                if (despatchInfo.IssueTime == DateTime.MinValue) throw new ArgumentException("Unknown IssueTime");
            }
            catch (Exception) { despatchInfo = null; }

            return despatchInfo;
        }

        private static XmlDocument GetSafeXmlContent(string baseXmlContent, string searchTag)
        {
            int xmlTagStartIndex = baseXmlContent.IndexOf("<" + searchTag);

            //searchTag control, <ns1:DespatchAdvice> example
            if (xmlTagStartIndex == -1)
            {
                int tagIndex = baseXmlContent.IndexOf("<");
                int documentIndex = baseXmlContent.IndexOf(searchTag);
                string strPrefix = baseXmlContent.Substring((tagIndex + 1), documentIndex - (tagIndex + 1));

                searchTag = strPrefix + searchTag;
                xmlTagStartIndex = baseXmlContent.IndexOf("<" + searchTag);
            }

            string tempXmlContent = baseXmlContent.Substring(baseXmlContent.IndexOf("<" + searchTag));

            XmlReaderSettings set = new XmlReaderSettings();
            set.IgnoreWhitespace = true;
            XmlDocument xdocAppResp = new XmlDocument();
            try
            {
                xdocAppResp.Load(XmlReader.Create(new StringReader(tempXmlContent), set));
            }
            catch
            {
                string xmlContentWithoutHeader = string.Empty;
                string xmlContentWithoutHeaderAndFooter = string.Empty;
                //Header exclude operation...
                int xmlTagEndIndex = 0;
                if (xmlTagStartIndex >= 0)
                    xmlTagEndIndex = tempXmlContent.IndexOf(">", xmlTagStartIndex);
                if (xmlTagStartIndex >= 0 && xmlTagEndIndex >= 0)
                    xmlContentWithoutHeader = tempXmlContent.Substring(xmlTagEndIndex + ("<").Length, tempXmlContent.Length - (xmlTagEndIndex + ("<").Length));

                if (!string.IsNullOrEmpty(xmlContentWithoutHeader))
                {
                    //Footer exclude operation...
                    xmlTagStartIndex = 0;
                    xmlTagEndIndex = xmlContentWithoutHeader.IndexOf("</" + searchTag + ">");
                    if (xmlTagStartIndex >= 0 && xmlTagEndIndex >= 0)
                        xmlContentWithoutHeaderAndFooter = xmlContentWithoutHeader.Substring(0, xmlTagEndIndex);
                }

                tempXmlContent = "<" + searchTag + " "
                    + "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" "
                    + "xmlns:cac=\"urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2\" "
                    + "xmlns:cbc=\"urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2\" ";

                if (searchTag.Equals("ApplicationResponse"))
                {
                    tempXmlContent += "xmlns=\"urn:oasis:names:specification:ubl:schema:xsd:ApplicationResponse-2\" "
                        + "xsi:schemaLocation=\"urn:oasis:names:specification:ubl:schema:xsd:ApplicationResponse-2 ../xsd/maindoc/UBL-ApplicationResponse-2.1.xsd\">";
                }
                else if (searchTag.Equals("Invoice") || searchTag.Equals("DespatchAdvice") || searchTag.Equals("ReceiptAdvice"))
                {
                    tempXmlContent += "xmlns:xades=\"http://uri.etsi.org/01903/v1.3.2#\" "
                        + "xmlns:udt=\"urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2\" "
                        + "xmlns:ccts=\"urn:un:unece:uncefact:documentation:2\" "
                        + "xmlns:ubltr=\"urn:oasis:names:specification:ubl:schema:xsd:TurkishCustomizationExtensionComponents\" "
                        + "xmlns:qdt=\"urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2\" "
                        + "xmlns:ext=\"urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2\" "
                        + "xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\" ";

                    if (searchTag.Equals("Invoice"))
                    {
                        tempXmlContent += "xmlns=\"urn:oasis:names:specification:ubl:schema:xsd:Invoice-2\" "
                            + "xsi:schemaLocation=\"urn:oasis:names:specification:ubl:schema:xsd:Invoice-2 UBL-Invoice-2.1.xsd\">";
                    }
                    else if (searchTag.Equals("DespatchAdvice"))
                    {
                        tempXmlContent += "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" "
                             + "xmlns=\"urn:oasis:names:specification:ubl:schema:xsd:DespatchAdvice-2\" "
                             + "xsi:schemaLocation=\"urn:oasis:names:specification:ubl:schema:xsd:DespatchAdvice-2 UBL-DespatchAdvice-2.1.xsd\">";
                    }
                    else if (searchTag.Equals("ReceiptAdvice"))
                    {
                        tempXmlContent += "xmlns: xsd = \"http://www.w3.org/2001/XMLSchema\" "
                            + "xmlns = \"urn:oasis:names:specification:ubl:schema:xsd:ReceiptAdvice-2\" "
                            + "xsi: schemaLocation = \"urn:oasis:names:specification:ubl:schema:xsd:ReceiptAdvice-2 UBL-ReceiptAdvice-2.1.xsd\">";
                    }
                    else throw new ArgumentException("Unknow xml document type");
                }
                else throw new ArgumentException("Unknow xml document type");

                tempXmlContent += xmlContentWithoutHeaderAndFooter;
                tempXmlContent += "</" + searchTag + ">";

                xdocAppResp.Load(XmlReader.Create(new StringReader(tempXmlContent), set));
            }

            return xdocAppResp;
        }
        private static List<KeyValuePair<string, string>> GetSenderPartyIdList(XmlElement item)
        {
            List<KeyValuePair<string, string>> partyIdfDataList = new List<KeyValuePair<string, string>>();

            List<XmlElement> senderPartyIdfList = new List<XmlElement>();
            foreach (XmlElement sp in item.ChildNodes.OfType<XmlElement>())
            {
                if (string.Equals(sp.LocalName, "PartyIdentification"))
                {
                    senderPartyIdfList.Add(sp);
                }
            }
            foreach (var senderPartyIdf in senderPartyIdfList)
            {
                foreach (XmlElement i in senderPartyIdf.ChildNodes.OfType<XmlElement>())
                {
                    if (string.Equals(i.LocalName, "ID"))
                    {
                        XmlElement partyIdElement = i;
                        XmlAttribute partySchemeIdElement = null;
                        foreach (XmlAttribute attr in partyIdElement.Attributes)
                        {
                            if (string.Equals(attr.LocalName, "schemeID"))
                            {
                                partySchemeIdElement = attr;
                            }
                        }

                        if (partySchemeIdElement != null)
                        {
                            partyIdfDataList.Add(new KeyValuePair<string, string>(partySchemeIdElement.InnerXml, partyIdElement.InnerXml));
                        }
                    }
                }
            }

            return partyIdfDataList;
        }

        public static GibEnvelopeDocument CreateGIBEnvelopeDocumentModel(DateTime envelopeCreationTime, EnvelopeDocumentIdentificationType envelopeDocumentIdentificationType
            , EnvelopePackageElementType envelopePackageElementType, int envelopePackageElementCount
            , string senderAlias, string senderRegisterNumber, string senderTitle
            , string receiverAlias, string receiverRegisterNumber, string receiverTitle, string envelopeNumber = null)
        {
            GibEnvelopeDocument standartBusinessDocument = new GibEnvelopeDocument();

            standartBusinessDocument.Header = CreateGIBEnvelopeDocumentHeaderModel(envelopeCreationTime, envelopeDocumentIdentificationType
                , senderAlias, senderRegisterNumber, senderTitle, receiverAlias, receiverRegisterNumber, receiverTitle, envelopeNumber);
            if (standartBusinessDocument.Header != null)
            {
                standartBusinessDocument.Package = new PackageModel()
                {
                    Elements = new List<ElementModel>()
                    {
                        new ElementModel()
                        {
                            PackageElementType = envelopePackageElementType.ToString(),
                            PackageElementCount = envelopePackageElementCount,
                            PackageElementLines = "%#SignedContent#%"
                        }
                    }
                };
                return standartBusinessDocument;
            }
            else throw new ArgumentException("StandartBusinessDocumentHeader cannot created!");
        }

        private static HeaderModel CreateGIBEnvelopeDocumentHeaderModel(DateTime envelopeCreationTime, EnvelopeDocumentIdentificationType envelopeDocumentIdentificationType
            , string senderAlias, string senderRegisterNumber, string senderTitle
            , string receiverAlias, string receiverRegisterNumber, string receiverTitle, string envelopeNumber = null)
        {
            HeaderModel standardBusinessDocumentHeader = null;

            string instanceIdentifier = Guid.NewGuid().ToString();

            if (!string.IsNullOrEmpty(envelopeNumber))
                instanceIdentifier = envelopeNumber;

            if (!string.IsNullOrEmpty(senderAlias) && !string.IsNullOrEmpty(senderRegisterNumber) && !string.IsNullOrEmpty(receiverAlias) && !string.IsNullOrEmpty(receiverRegisterNumber))
            {
                standardBusinessDocumentHeader = new HeaderModel()
                {
                    HeaderVersion = SpecialIntegratorInfo.STANDART_BUSINESS_DOCUMENT_HEADER_VERSION,
                    Senders = new List<PartnerModel>()
                    {
                        new PartnerModel()
                        {
                            Identifier = senderAlias,
                            ContactInformations = new List<ContactInformationModel>()
                            {
                                new ContactInformationModel () { Contact = senderTitle, ContactTypeIdentifier = "UNVAN" },
                                new ContactInformationModel () { Contact = senderRegisterNumber, ContactTypeIdentifier = "VKN_TCKN" }
                            }
                        }
                    },
                    Receivers = new List<PartnerModel>()
                    {
                        new PartnerModel()
                        {
                            Identifier = receiverAlias,
                            ContactInformations = new List<ContactInformationModel>()
                            {
                                new ContactInformationModel () { Contact = receiverTitle, ContactTypeIdentifier = "UNVAN" },
                                new ContactInformationModel () { Contact = receiverRegisterNumber, ContactTypeIdentifier = "VKN_TCKN" }
                            }
                        }
                    },
                    DocumentIdentification = new DocumentIdentificationModel()
                    {
                        Standard = SpecialIntegratorInfo.DOCUMENT_IDENTIFICATION_STANDART,
                        TypeVersion = SpecialIntegratorInfo.DOCUMENT_IDENTIFICATION_TYPE_VERSION,
                        InstanceIdentifier = instanceIdentifier,
                        Type = envelopeDocumentIdentificationType.ToString(),
                        MultipleType = false,
                        CreationDateAndTime = envelopeCreationTime.ToString(DateFormats.DateTimeGIBFormatLong)
                    },
                    Manifest = new ManifestModel()
                    {
                        NumberOfItems = 1,
                        ManifestItems = new List<ManifestItemModel>()
                        {
                            new  ManifestItemModel()
                            {
                                MimeTypeQualifierCode = "application/xml",
                                UniformResourceIdentifier = string.Empty,
                                Description = string.Empty,
                                LanguageCode = "TR"
                            }
                        }
                    }
                };
            }

            return standardBusinessDocumentHeader;
        }
    }
}
