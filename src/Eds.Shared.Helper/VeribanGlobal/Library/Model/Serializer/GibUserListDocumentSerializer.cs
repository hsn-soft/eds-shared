using Eds.Shared.Helper.VeribanGlobal.Library.Model.GibUser;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.Serializer
{
    public class GibUserListDocumentSerializer : BaseSerializer
    {
        public string SerializeAndGetXmlContent(GibUserListDocument gibUserListDocument)
        {
            if (gibUserListDocument == null)
                throw new ArgumentNullException("gibUserListDocument", "gibUserListDocument must have a reference");

            IList<GibUserListDocument> gibUserListDocuments = new List<GibUserListDocument>() { gibUserListDocument };

            return SerializeAndGetXmlContent(gibUserListDocuments);
        }

        public string SerializeAndGetXmlContent(IList<GibUserListDocument> gibUserListDocuments)
        {
            if (gibUserListDocuments == null || gibUserListDocuments.Count == 0)
                throw new ArgumentNullException("gibUserListDocuments", "gibUserListDocuments must have a reference");

            return SerializeAndGetXmlContentBase(gibUserListDocuments, false);
        }

        public GibUserListDocument DeserializeFromXmlFile(string gibUserListDocumentXmlFileFullPath)
        {
            if (string.IsNullOrEmpty(gibUserListDocumentXmlFileFullPath))
                throw new ArgumentNullException("gibUserListDocumentXmlFileFullPath", "gibUserListDocumentXmlFileFullPath must have a reference");

            string xmlContent;
            try
            {
                string content = File.ReadAllText(gibUserListDocumentXmlFileFullPath, System.Text.Encoding.UTF8);

                xmlContent = SafeLoadFromXmlFile(content);
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message.ToString()); }

            return DeserializeFromXmlContent(xmlContent);
        }

        public GibUserListDocument DeserializeFromXmlContent(string gibUserListDocumentXmlContent)
        {
            if (string.IsNullOrEmpty(gibUserListDocumentXmlContent))
                throw new ArgumentNullException("gibUserListDocumentXmlContent", "gibUserListDocumentXmlContent must have a reference");

            GibUserListDocument deserializeGibUserListDocument = DeserializeFromXmlContentBase<GibUserListDocument>(gibUserListDocumentXmlContent);


            return deserializeGibUserListDocument;
        }

    }
}
