using Eds.Shared.Helper.VeribanGlobal.Library.Common;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.GibAccount;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.Serializer
{
    public class GibProcessUserAccountDocumentSerializer : BaseSerializer
    {
        public string SerializeAndGetXmlContent(GibProcessUserAccountDocument processUserAccount)
        {
            if (processUserAccount == null)
                throw new ArgumentNullException("processUserAccount", "processUserAccount must have a reference");

            IList<GibProcessUserAccountDocument> processUserAccounts = new List<GibProcessUserAccountDocument>() { processUserAccount };

            return SerializeAndGetXmlContent(processUserAccounts);
        }

        public string SerializeAndGetXmlContent(IList<GibProcessUserAccountDocument> processUserAccounts)
        {
            if (processUserAccounts == null || processUserAccounts.Count == 0)
                throw new ArgumentNullException("processUserAccounts", "processUserAccounts must have a reference");

            foreach (GibProcessUserAccountDocument processUserAccount in processUserAccounts)
            {
                //ZORUNLU ALANLAR EKLENIYOR...
                processUserAccount.ApplicationArea = GetDefaultApplicationArea();

                processUserAccount.ApplicationArea.Receiver = new Receiver() { /*BOŞ BIRAKILMALI */ };
                processUserAccount.DataArea.Process = new UAProcess() { /*BOŞ BIRAKILMALI */ };

                string errorMessage = string.Empty;
                if (!ProcessUserAccountModelControl(processUserAccount, out errorMessage))
                    throw new ArgumentException("PROCESSUSERACCOUNT MODEL CONTROL FAIL : " + errorMessage);
            }

            return SerializeAndGetXmlContentBase(processUserAccounts, false);
        }

        public GibProcessUserAccountDocument DeserializeFromXmlFile(string processUserAccountXmlFileFullPath)
        {
            if (string.IsNullOrEmpty(processUserAccountXmlFileFullPath))
                throw new ArgumentNullException("processUserAccountXmlFileFullPath", "processUserAccountXmlFileFullPath must have a reference");

            string xmlContent;
            try
            {
                string content = File.ReadAllText(processUserAccountXmlFileFullPath, System.Text.Encoding.UTF8);

                xmlContent = SafeLoadFromXmlFile(content);
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message.ToString()); }

            return DeserializeFromXmlContent(xmlContent);
        }

        public GibProcessUserAccountDocument DeserializeFromXmlContent(string processUserAccountXmlContent)
        {
            if (string.IsNullOrEmpty(processUserAccountXmlContent))
                throw new ArgumentNullException("processUserAccountXmlContent", "processUserAccountXmlContent must have a reference");

            GibProcessUserAccountDocument deserializeProcessUserAccount = DeserializeFromXmlContentBase<GibProcessUserAccountDocument>(processUserAccountXmlContent);

            if (!ProcessUserAccountModelControl(deserializeProcessUserAccount, out string errorMessage))
                throw new ArgumentException("PROCESSUSERACCOUNT MODEL CONTROL FAIL : " + errorMessage);

            return deserializeProcessUserAccount;
        }

        private bool ProcessUserAccountModelControl(GibProcessUserAccountDocument processUserAccount, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(processUserAccount.ReleaseId)) { errorMessage = "processUserAccount.ReleaseId"; return false; }

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
                CreationDateTime = DateTime.Now.ToString(DateFormats.DateTimeGIBFormatLong),
                Signature = new UASignature()
                {
                    QualifyingAgencyId = ""
                }
            };
        }
    }
}
