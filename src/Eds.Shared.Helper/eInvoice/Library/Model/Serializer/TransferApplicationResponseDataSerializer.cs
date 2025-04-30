using Eds.Shared.Helper.VeribanGlobal.Library.Model.Serializer;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase;

namespace Eds.Shared.Helper.eInvoice.Library.Model.Serializer
{
    public class TransferApplicationResponseDataSerializer : BaseSerializer
    {
        public string SerializeAndGetXmlContent(ApplicationResponseModel applicationResponseModel, bool includeUBLExtension, bool byPassModelControl = false, bool clearXmlDocumentHeaderTag = false)
        {
            if (applicationResponseModel == null)
                throw new ArgumentNullException("applicationResponseModel", "applicationResponseModel must have a reference");

            if (includeUBLExtension)
            {
                applicationResponseModel.UBLExtensions = new List<UblExtension>() { GetDefaultUBLExtension() };
            }

            if (!byPassModelControl)
            {
                KeyValuePair<bool, List<string>> controlData = TransferApplicationResponseDataModelControl(applicationResponseModel);
                if (!controlData.Key)
                {
                    throw new Exception("MODEL CONTROL FAIL : " + ControlledDataHandle(controlData.Value));
                }
            }

            //CLEAR XMLNS
            applicationResponseModel.xmlns = (new ApplicationResponseModel().xmlns);

            return SerializeAndGetXmlContentBase(new List<ApplicationResponseModel>() { applicationResponseModel }, !clearXmlDocumentHeaderTag);
        }

        public bool SerializeAndSaveXmlFile(string saveFileFullPath, ApplicationResponseModel applicationResponseModel, bool includeUBLExtension, bool byPassModelControl = false)
        {
            if (applicationResponseModel == null)
                throw new ArgumentNullException("applicationResponseModel", "applicationResponseModel must have a reference");

            if (includeUBLExtension)
            {
                applicationResponseModel.UBLExtensions = new List<UblExtension>() { GetDefaultUBLExtension() };
            }

            if (!byPassModelControl)
            {
                KeyValuePair<bool, List<string>> controlData = TransferApplicationResponseDataModelControl(applicationResponseModel);
                if (!controlData.Key)
                {
                    throw new Exception("MODEL CONTROL FAIL : " + ControlledDataHandle(controlData.Value));
                }
            }

            File.WriteAllText(saveFileFullPath, SerializeAndGetXmlContentBase(new List<ApplicationResponseModel>() { applicationResponseModel }, true));

            return File.Exists(saveFileFullPath);
        }

        public ApplicationResponseModel DeserializeFromXmlFile(string applicationResponseFileFullPath, bool byPassModelControl = false)
        {
            if (string.IsNullOrEmpty(applicationResponseFileFullPath))
                throw new ArgumentNullException("applicationResponseFileFullPath", "applicationResponseFileFullPath must have a reference");

            string xmlContent = string.Empty;
            try
            {
                string content = File.ReadAllText(applicationResponseFileFullPath, System.Text.Encoding.UTF8);

                xmlContent = SafeLoadFromXmlFile(content);
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message.ToString()); }

            if (string.IsNullOrEmpty(xmlContent))
                throw new ArgumentException("xmlContent can not read from path");

            return DeserializeFromXmlContent(xmlContent, byPassModelControl);
        }

        public ApplicationResponseModel DeserializeFromXmlContent(string applicationResponseXmlContent, bool byPassModelControl = false)
        {
            try { applicationResponseXmlContent = applicationResponseXmlContent.Replace("<cbc:LineID />", ""); }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }

            if (string.IsNullOrEmpty(applicationResponseXmlContent))
                throw new ArgumentNullException("applicationResponseXmlContent", "applicationResponseXmlContent must have a reference");

            ApplicationResponseModel deserializeApplicationResponseModel = DeserializeFromXmlContentBase<ApplicationResponseModel>(applicationResponseXmlContent);

            if (!byPassModelControl)
            {
                KeyValuePair<bool, List<string>> controlData = TransferApplicationResponseDataModelControl(deserializeApplicationResponseModel);
                if (!controlData.Key)
                {
                    throw new Exception("MODEL CONTROL FAIL : " + ControlledDataHandle(controlData.Value));
                }
            }

            return deserializeApplicationResponseModel;
        }

        public KeyValuePair<bool, List<string>> TransferApplicationResponseDataModelControl(ApplicationResponseModel applicationResponseModel)
        {
            List<string> errorList = new List<string>();

            string errorMessage = string.Empty;
            if (!ApplicationResponseModelControl(applicationResponseModel, out errorMessage))
                errorList.Add(errorMessage);

            if (errorList.Count > 0)
                return new KeyValuePair<bool, List<string>>(false, errorList);
            else
                return new KeyValuePair<bool, List<string>>(true, null);
        }

        #region OLD CONTROL
        private bool ApplicationResponseModelControl(ApplicationResponseModel applicationResponse, out string errorMessage)
        {

            try
            {
                if (applicationResponse.SenderParty != null && applicationResponse.SenderParty.PostalAddress != null)
                {
                    if (string.IsNullOrEmpty(applicationResponse.SenderParty.PostalAddress.StreetName))
                    {
                        applicationResponse.SenderParty.PostalAddress.StreetName = string.Empty;
                    }

                    if (string.IsNullOrEmpty(applicationResponse.SenderParty.PostalAddress.CitySubdivisionName))
                    {
                        applicationResponse.SenderParty.PostalAddress.CitySubdivisionName = string.Empty;
                    }

                    if (string.IsNullOrEmpty(applicationResponse.SenderParty.PostalAddress.CityName))
                    {
                        applicationResponse.SenderParty.PostalAddress.CityName = string.Empty;
                    }
                }

                if (applicationResponse.ReceiverParty != null && applicationResponse.ReceiverParty.PostalAddress != null)
                {
                    if (string.IsNullOrEmpty(applicationResponse.ReceiverParty.PostalAddress.StreetName))
                    {
                        applicationResponse.ReceiverParty.PostalAddress.StreetName = string.Empty;
                    }

                    if (string.IsNullOrEmpty(applicationResponse.ReceiverParty.PostalAddress.CitySubdivisionName))
                    {
                        applicationResponse.ReceiverParty.PostalAddress.CitySubdivisionName = string.Empty;
                    }

                    if (string.IsNullOrEmpty(applicationResponse.ReceiverParty.PostalAddress.CityName))
                    {
                        applicationResponse.ReceiverParty.PostalAddress.CityName = string.Empty;
                    }
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }

            errorMessage = string.Empty;
            return true;
        }

        private UblExtension GetDefaultUBLExtension()
        {
            return new UblExtension()
            {
                ExtensionContent = new ExtensionContent()
                {
                    //imza eklenecek
                }
            };
        }
        #endregion
    }
}
