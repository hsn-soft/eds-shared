using Eds.Shared.Helper.VeribanGlobal.Library.Model.SapDocument;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.Serializer
{
    public class SapDocumentSerializer : BaseSerializer
    {
        public SapEnvelopeDocumentModel DeserializeFromXmlFile(string SAPDocumentXmlFileFullPath)
        {
            if (string.IsNullOrEmpty(SAPDocumentXmlFileFullPath))
                throw new ArgumentNullException("SAPDocumentXmlFileFullPath", "SAPDocumentXmlFileFullPath must have a reference");

            string xmlContent;
            try
            {
                string content = File.ReadAllText(SAPDocumentXmlFileFullPath, System.Text.Encoding.UTF8);

                xmlContent = SafeLoadFromXmlFile(content);
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message.ToString()); }

     
            return DeserializeFromXmlContent(xmlContent);
        }

        public SapEnvelopeDocumentModel DeserializeFromXmlContent(string SAPDocumentXmlContent)
        {
            if (string.IsNullOrEmpty(SAPDocumentXmlContent))
                throw new ArgumentNullException("SAPDocumentXmlContent", "SAPDocumentXmlContent must have a reference");

            SapEnvelopeDocumentModel deserializeSAPDocument = DeserializeFromXmlContentBase<SapEnvelopeDocumentModel>(SAPDocumentXmlContent);

            return deserializeSAPDocument;
        }

    }
}
