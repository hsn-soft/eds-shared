using Eds.Shared.Helper.VeribanGlobal.Library.Common;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.GibAccount;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.Serializer
{
    public class GibCancelUserAccountDocumentSerializer : BaseSerializer
    {
        public string SerializeAndGetXmlContent(GibCancelUserAccountDocument cancelUserAccount)
        {
            if (cancelUserAccount == null)
                throw new ArgumentNullException("cancelUserAccount", "cancelUserAccount must have a reference");

            IList<GibCancelUserAccountDocument> cancelUserAccounts = new List<GibCancelUserAccountDocument>() { cancelUserAccount };

            return SerializeAndGetXmlContent(cancelUserAccounts);
        }

        public string SerializeAndGetXmlContent(IList<GibCancelUserAccountDocument> cancelUserAccounts)
        {
            if (cancelUserAccounts == null || cancelUserAccounts.Count == 0)
                throw new ArgumentNullException("cancelUserAccounts", "cancelUserAccounts must have a reference");

            foreach (GibCancelUserAccountDocument cancelUserAccount in cancelUserAccounts)
            {
                //ZORUNLU ALANLAR EKLENIYOR...
                cancelUserAccount.ApplicationArea = GetDefaultApplicationArea();

                cancelUserAccount.ApplicationArea.Receiver = new Receiver() { /*BOŞ BIRAKILMALI */ };
                cancelUserAccount.DataArea.Cancel = new UACancel() { /*BOŞ BIRAKILMALI */ };

                string errorMessage = string.Empty;
                if (!CancelUserAccountModelControl(cancelUserAccount, out errorMessage))
                    throw new ArgumentException("CANCELUSERACCOUNT MODEL CONTROL FAIL : " + errorMessage);
            }

            return SerializeAndGetXmlContentBase(cancelUserAccounts, false);
        }

        public GibCancelUserAccountDocument DeserializeFromXmlFile(string cancelUserAccountXmlFileFullPath)
        {
            if (string.IsNullOrEmpty(cancelUserAccountXmlFileFullPath))
                throw new ArgumentNullException("cancelUserAccountXmlFileFullPath", "cancelUserAccountXmlFileFullPath must have a reference");

            string xmlContent;
            try
            {
                string content = File.ReadAllText(cancelUserAccountXmlFileFullPath, System.Text.Encoding.UTF8);

                xmlContent = SafeLoadFromXmlFile(content);
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message.ToString()); }

            return DeserializeFromXmlContent(xmlContent);
        }

        public GibCancelUserAccountDocument DeserializeFromXmlContent(string cancelUserAccountXmlContent)
        {
            if (string.IsNullOrEmpty(cancelUserAccountXmlContent))
                throw new ArgumentNullException("cancelUserAccountXmlContent", "cancelUserAccountXmlContent must have a reference");

            GibCancelUserAccountDocument deserializeCancelUserAccount = DeserializeFromXmlContentBase<GibCancelUserAccountDocument>(cancelUserAccountXmlContent);

            if (!CancelUserAccountModelControl(deserializeCancelUserAccount, out string errorMessage))
                throw new ArgumentException("CANCELUSERACCOUNT MODEL CONTROL FAIL : " + errorMessage);

            return deserializeCancelUserAccount;
        }

        private bool CancelUserAccountModelControl(GibCancelUserAccountDocument cancelUserAccount, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(cancelUserAccount.ReleaseId)) { errorMessage = "cancelUserAccount.ReleaseId"; return false; }

            return true;
        }

        private ApplicationArea GetDefaultApplicationArea()
        {
            return new ApplicationArea()
            {
                Sender = new Sender()
                {
                    LogicalId = SpecialIntegratorInfo.SEALER_TAX_NO
                },
                Receiver = new Receiver(),
                CreationDateTime = DateTime.Now.ToString(DateFormats.DateTimeGIBFormatLong),
                Signature = new UASignature()
                {
                    QualifyingAgencyId = ""
                }
            };
        }
    }
}
