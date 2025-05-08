using Eds.Shared.Helper.eInvoice.Library.Model.Serializer;
using Eds.Shared.Helper.VeribanGlobal.Library.Common;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes.EInvoice;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.GibEnvelope;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Utils;

namespace Eds.Shared.Helper.eInvoice.Library.Model.Utils
{
    public static class ApplicationResponseUtils
    {
        public static string CreateGIBQueryResponseEnvelopeDocumentContent(string refEnvelopeIdentifier, string documentTypeCode
            , string receiverTaxNo, string receiverTitle, int responseCode, string description)
        {
            ApplicationResponseModel appResponse = new ApplicationResponseModel()
            {
                #region APPLICATION RESPONSE MODEL
                ProfileID = "TICARIFATURA",
                ID = Guid.NewGuid().ToString(),
                UUID = Guid.NewGuid().ToString(),
                IssueDate = DateTime.Now.ToString(DateFormats.DateTimeGIBFormatShort),
                IssueTime = DateTime.Now.ToString(DateFormats.DateTimeGIBFormatTime),
                SenderParty = new Party()
                {
                    PartyIdentification = new List<PartyIdentification>()
                    {
                        new PartyIdentification()
                        {
                            ID=new CombineId() { Id = SpecialIntegratorInfo.SEALER_TAX_NO, SchemeId = "VKN" }
                        }
                    },
                    PartyName = new PartyName() { Name = SpecialIntegratorInfo.SEALER_TITLE },
                    PostalAddress = new Address()
                    {
                        StreetName = "Atakan Sk. No:14 ",
                        CitySubdivisionName = "Mecidiyeköy/Şişli",
                        CityName = "ISTANBUL",
                        Country = new Country() { IdentificationCode = "TR", Name = "Türkiye" }
                    }
                },
                ReceiverParty = new Party()
                {
                    PartyIdentification = new List<PartyIdentification>()
                    {
                        new PartyIdentification()
                        {
                            ID = new CombineId() { Id = receiverTaxNo, SchemeId = "VKN" }
                        }
                    },
                    PartyName = new PartyName() { Name = receiverTitle },
                    PostalAddress = new Address()
                    {
                        CitySubdivisionName = string.Empty,
                        CityName = string.Empty,
                        Country = new Country() { IdentificationCode = "TR", Name = "Türkiye" }
                    }
                },
                DocumentResponse = new DocumentResponse()
                {
                    Response = new Response()
                    {
                        ReferenceID = Guid.NewGuid().ToString(),
                        ResponseCode = "S_APR",
                        Description = "APPLICATIONRESPONSE"
                    },
                    DocumentReference = new DocumentReference()
                    {
                        ID = new CombineId() { Id = refEnvelopeIdentifier },
                        IssueDate = DateTime.Now.ToString(DateFormats.DateTimeGIBFormatShort),
                        DocumentTypeCode = documentTypeCode,
                        DocumentType = documentTypeCode
                    },
                    LineResponses = new List<LineResponse>()
                    {
                        new LineResponse()
                        {
                            LineReference = new LineReference()
                            {
                                LineID = "0",
                                DocumentReference = new DocumentReference()
                                {
                                    ID = new CombineId() { Id = refEnvelopeIdentifier },
                                    IssueDate = DateTime.Now.ToString(DateFormats.DateTimeGIBFormatShort)
                                }
                            },
                            Response = new Response()
                            {
                                ReferenceID = Guid.NewGuid().ToString(),
                                ResponseCode = responseCode.ToString(),
                                Description = description
                            }
                        }
                    }
                }
                #endregion
            };

            TransferApplicationResponseDataSerializer applicationResponseSerializer = new TransferApplicationResponseDataSerializer();
            string appResponseXmlContent = applicationResponseSerializer.SerializeAndGetXmlContent(appResponse, false, true, true);

            GibEnvelopeDocument standartBusinessDocument = GibEnvelopeUtils.CreateGIBEnvelopeDocumentModel(DateTime.Now
                , EnvelopeDocumentIdentificationType.SYSTEMENVELOPE, EnvelopePackageElementType.APPLICATIONRESPONSE, 1
                , "FIB", SpecialIntegratorInfo.SEALER_TAX_NO, SpecialIntegratorInfo.SEALER_TITLE
                , "defaultgb", receiverTaxNo, receiverTitle);

            string standartBusinessDocumentXmlContent = new XmlManager().SerializeContent<GibEnvelopeDocument>(standartBusinessDocument).ToString();

            //ADD APPLICATIONRESPONSE XML DATA
            standartBusinessDocumentXmlContent = standartBusinessDocumentXmlContent.Replace("%#SignedContent#%", appResponseXmlContent);

            return standartBusinessDocumentXmlContent;
        }

        public static string CreateGIBSystemResponseEnvelopeDocumentContent(DateTime envelopeCreationTime, string envelopeNumber
            , string refEnvelopeIdentifier, string responseCode, string responseDesc
            , string senderAlias, CombineId senderRegisterNumber, string senderTitle
            , string receiverAlias, CombineId receiverRegisterNumber, string receiverTitle)
        {
            string appResponseID = DateTime.Now.ToString(DateFormats.DateTimeUniqueFormat);
            appResponseID = appResponseID.Substring(0, appResponseID.Length - 1);

            ApplicationResponseModel appResponse = new ApplicationResponseModel()
            {
                #region APPLICATION RESPONSE MODEL
                ProfileID = "UBL-TR-PROFILE-1",
                ID = "APR" + appResponseID,
                UUID = Guid.NewGuid().ToString(),
                IssueDate = DateTime.Now.ToString(DateFormats.DateTimeGIBFormatShort),
                IssueTime = DateTime.Now.ToString(DateFormats.DateTimeGIBFormatTime),
                SenderParty = new Party()
                {
                    PartyIdentification = new List<PartyIdentification>() { new PartyIdentification() { ID = senderRegisterNumber } },
                    PostalAddress = new Address()
                    {
                        StreetName = "Atakan Sk. No:14 ",
                        CitySubdivisionName = "Mecidiyeköy/Şişli",
                        CityName = "ISTANBUL",
                        Country = new Country() { IdentificationCode = "TR", Name = "Türkiye" }
                    }
                },
                ReceiverParty = new Party()
                {
                    PartyIdentification = new List<PartyIdentification>() { new PartyIdentification() { ID = receiverRegisterNumber } },
                    PostalAddress = new Address()
                    {
                        CitySubdivisionName = string.Empty,
                        CityName = string.Empty,
                        Country = new Country() { IdentificationCode = "TR", Name = "Türkiye" }
                    }
                },
                DocumentResponse = new DocumentResponse()
                {
                    Response = new Response()
                    {
                        ReferenceID = Guid.NewGuid().ToString(),
                        ResponseCode = DocumentResponseCodeType.S_APR.ToString(),
                        Description = "APPLICATIONRESPONSE"
                    },
                    DocumentReference = new DocumentReference()
                    {
                        //ZARF ID
                        ID = new CombineId() { Id = refEnvelopeIdentifier },
                        IssueDate = DateTime.Now.ToString(DateFormats.DateTimeGIBFormatShort),
                        DocumentTypeCode = EnvelopeDocumentIdentificationType.SENDERENVELOPE.ToString(),
                        DocumentType = EnvelopeDocumentIdentificationType.SENDERENVELOPE.ToString()
                    },
                    LineResponses = new List<LineResponse>()
                    {
                        new LineResponse()
                        {
                            LineReference = new LineReference()
                            {
                                LineID = "0",
                                DocumentReference = new DocumentReference()
                                {
                                    //ZARF ID
                                    ID = new CombineId() { Id= refEnvelopeIdentifier },
                                    IssueDate = DateTime.Now.ToString(DateFormats.DateTimeGIBFormatShort)
                                }
                            },
                            Response = new Response()
                            {
                                ReferenceID = Guid.NewGuid().ToString(),
                                ResponseCode = responseCode,
                                Description = responseDesc
                            }

                        }
                    }
                }
                #endregion
            };

            if (!string.IsNullOrEmpty(senderTitle))
                appResponse.SenderParty.PartyName = new PartyName() { Name = senderTitle };
            if (!string.IsNullOrEmpty(receiverTitle))
                appResponse.ReceiverParty.PartyName = new PartyName() { Name = receiverTitle };

            TransferApplicationResponseDataSerializer applicationResponseSerializer = new TransferApplicationResponseDataSerializer();
            string appResponseXmlContent = applicationResponseSerializer.SerializeAndGetXmlContent(appResponse, false, false, true);

            GibEnvelopeDocument standartBusinessDocument = GibEnvelopeUtils.CreateGIBEnvelopeDocumentModel(envelopeCreationTime
                , EnvelopeDocumentIdentificationType.SYSTEMENVELOPE, EnvelopePackageElementType.APPLICATIONRESPONSE, 1
                , senderAlias, senderRegisterNumber.Id, senderTitle
                , receiverAlias, receiverRegisterNumber.Id, receiverTitle, envelopeNumber);

            string standartBusinessDocumentXmlContent = new XmlManager().SerializeContent<GibEnvelopeDocument>(standartBusinessDocument).ToString();

            //ADD APPLICATIONRESPONSE XML DATA
            standartBusinessDocumentXmlContent = standartBusinessDocumentXmlContent.Replace("%#SignedContent#%", appResponseXmlContent);

            return standartBusinessDocumentXmlContent;
        }
    }
}
