using Eds.Shared.Helper.VeribanGlobal.Library.Model.Serializer;

namespace Eds.Shared.Helper.eInvoice.Library.Model.Serializer
{
    public class TransferCreditNoteDataSerializer : BaseSerializer
    {

        public string SerializeAndGetXmlContent(CreditNoteModel creditNoteModel, bool byPassModelControl = false, bool clearXmlDocumentHeaderTag = false)
        {
            if (creditNoteModel == null)
                throw new ArgumentNullException("creditNoteModel", "creditNoteModel must have a reference");

            if (!byPassModelControl)
            {
                KeyValuePair<bool, List<string>> controlData = TransferCreditNoteDataControl(creditNoteModel);
                if (!controlData.Key)
                {
                    throw new Exception("MODEL CONTROL FAIL : " + ControlledDataHandle(controlData.Value));
                }
            }

            //CLEAR XMLNS
            creditNoteModel.xmlns = (new CreditNoteModel().xmlns);

            return SerializeAndGetXmlContentBase(new List<CreditNoteModel>() { creditNoteModel }, !clearXmlDocumentHeaderTag);
        }

        public bool SerializeAndSaveXmlFile(string saveFileFullPath, CreditNoteModel creditNoteModel, bool byPassModelControl = false)
        {
            if (creditNoteModel == null)
                throw new ArgumentNullException("creditNoteModel", "creditNoteModel must have a reference");

            if (!byPassModelControl)
            {
                KeyValuePair<bool, List<string>> controlData = TransferCreditNoteDataControl(creditNoteModel);
                if (!controlData.Key)
                {
                    throw new Exception("MODEL CONTROL FAIL : " + ControlledDataHandle(controlData.Value));
                }
            }

            File.WriteAllText(saveFileFullPath, SerializeAndGetXmlContentBase(new List<CreditNoteModel>() { creditNoteModel }, true));

            return File.Exists(saveFileFullPath);
        }

        public CreditNoteModel DeserializeFromXmlFile(string creditNoteFileFullPath, bool byPassModelControl = false)
        {
            if (string.IsNullOrEmpty(creditNoteFileFullPath))
                throw new ArgumentNullException("creditNoteFileFullPath", "creditNoteFileFullPath must have a reference");

            string xmlContent = string.Empty;
            try
            {
                string content = File.ReadAllText(creditNoteFileFullPath, System.Text.Encoding.UTF8);

                xmlContent = SafeLoadFromXmlFile(content);
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message.ToString()); }

            if (string.IsNullOrEmpty(xmlContent))
                throw new ArgumentException("xmlContent can not read from path");

            return DeserializeFromXmlContent(xmlContent, byPassModelControl);
        }

        public CreditNoteModel DeserializeFromXmlContent(string creditNoteXmlContent, bool byPassModelControl = false)
        {
            if (string.IsNullOrEmpty(creditNoteXmlContent))
                throw new ArgumentNullException("creditNoteXmlContent", "creditNoteXmlContent must have a reference");

            CreditNoteModel deserializeCreditNoteModel = DeserializeFromXmlContentBase<CreditNoteModel>(creditNoteXmlContent);

            if (!byPassModelControl)
            {
                KeyValuePair<bool, List<string>> controlData = TransferCreditNoteDataControl(deserializeCreditNoteModel);
                if (!controlData.Key)
                {
                    throw new Exception("MODEL CONTROL FAIL : " + ControlledDataHandle(controlData.Value));
                }
            }

            return deserializeCreditNoteModel;
        }

        public KeyValuePair<bool, List<string>> TransferCreditNoteDataControl(CreditNoteModel creditNoteModel)
        {
            List<string> errorList = new List<string>();

            string errorMessage = string.Empty;
            if (!CreditNoteModelControl(creditNoteModel, out errorMessage))
                errorList.Add(errorMessage);

            if (errorList.Count > 0)
                return new KeyValuePair<bool, List<string>>(false, errorList);
            else
                return new KeyValuePair<bool, List<string>>(true, null);
        }

        #region FAKE CONTROL
        private bool CreditNoteModelControl(CreditNoteModel creditNoteModel, out string errorMessage)
        {
            errorMessage = string.Empty;

            return creditNoteModel != null;
        }
        #endregion
    }

}
