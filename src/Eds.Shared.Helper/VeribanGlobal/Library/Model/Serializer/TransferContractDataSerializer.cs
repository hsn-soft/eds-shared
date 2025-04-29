using Eds.Shared.Helper.VeribanGlobal.Library.Model.VeribanContract;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.Serializer
{
    public class TransferContractDataSerializer : BaseSerializer
    {
        public string SerializeAndGetXmlContent(TransferContractDataModel transferContractModel, bool byPassModelControl = false, bool clearXmlDocumentHeaderTag = false)
        {
            if (!byPassModelControl)
            {
                KeyValuePair<bool, List<string>> controlData = TransferContractDataModelControl(transferContractModel);
                if (!controlData.Key)
                {
                    throw new ArgumentException("MODEL CONTROL FAIL : " + ControlledDataHandle(controlData.Value));
                }
            }

            return SerializeAndGetXmlContentBase(new List<TransferContractDataModel>() { transferContractModel }, !clearXmlDocumentHeaderTag);
        }

        public bool SerializeAndSaveXmlFile(string saveFileFullPath, TransferContractDataModel transferContractDataModel, bool byPassModelControl = false)
        {
            if (!byPassModelControl)
            {
                KeyValuePair<bool, List<string>> controlDataModel = TransferContractDataModelControl(transferContractDataModel);
                if (!controlDataModel.Key)
                {
                    throw new ArgumentException("MODEL CONTROL FAIL : " + ControlledDataHandle(controlDataModel.Value));
                }
            }

            File.WriteAllText(saveFileFullPath, SerializeAndGetXmlContentBase(new List<TransferContractDataModel>() { transferContractDataModel }, true));

            return File.Exists(saveFileFullPath);
        }

        public TransferContractDataModel DeserializeFromXmlFile(string transferContractFileFullPath, bool byPassModelControl = false)
        {
            if (string.IsNullOrEmpty(transferContractFileFullPath))
                throw new ArgumentNullException("transferContractFileFullPath", "transferContractFileFullPath must have a reference");

            string xmlContent;
            try
            {
                string content = File.ReadAllText(transferContractFileFullPath, System.Text.Encoding.UTF8);

                xmlContent = SafeLoadFromXmlFile(content);
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message.ToString()); }

            return DeserializeFromXmlContent(xmlContent, byPassModelControl);
        }

        public TransferContractDataModel DeserializeFromXmlContent(string transferContractXmlContent, bool byPassModelControl = false)
        {
            TransferContractDataModel deserializeContractModel = null;

            if (!string.IsNullOrEmpty(transferContractXmlContent))
                deserializeContractModel = DeserializeFromXmlContentBase<TransferContractDataModel>(transferContractXmlContent);

            if (!byPassModelControl)
            {
                KeyValuePair<bool, List<string>> controlData = TransferContractDataModelControl(deserializeContractModel);
                if (!controlData.Key)
                {
                    throw new ArgumentException("MODEL CONTROL FAIL : " + ControlledDataHandle(controlData.Value));
                }
            }

            return deserializeContractModel;
        }

        public KeyValuePair<bool, List<string>> TransferContractDataModelControl(TransferContractDataModel transferContractDataModel)
        {
            List<string> errorList = new List<string>();

            if (!ContractModelControl(transferContractDataModel, out string errorMessage))
                errorList.Add(errorMessage);

            if (errorList.Count > 0)
                return new KeyValuePair<bool, List<string>>(false, errorList);
            else
                return new KeyValuePair<bool, List<string>>(true, null);
        }

        #region OLD CONTROL
        private bool ContractModelControl(TransferContractDataModel transferContractDataModel, out String errorMessage)
        {
            errorMessage = String.Empty;

            return transferContractDataModel != null;
        }
        #endregion
    }
}
