using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils
{
    public class XmlManager
    {
        private static readonly Hashtable serializerCache = new Hashtable();

        public XmlDocument CreateXmlDocumentFromFile(string xmlFileFullPath)
        {
            string content = File.ReadAllText(xmlFileFullPath);

            return CreateXmlDocumentFromContent(content);
        }
        public XmlDocument CreateXmlDocumentFromContent(string content)
        {
            int xmlTagStartIndex = content.IndexOf("<?");
            int xmlTagEndIndex = -1;
            if (xmlTagStartIndex >= 0)
            {
                xmlTagEndIndex = content.IndexOf("?>", xmlTagStartIndex);
            }

            if (xmlTagStartIndex >= 0 && xmlTagEndIndex >= 0)
            {
                content = content.Substring(xmlTagEndIndex + 2, content.Length - (xmlTagEndIndex + 2));
            }

            content = content.Replace("&amp;", "&");
            content = content.Replace("&", "&amp;");

            byte[] bytes = Encoding.UTF8.GetBytes(content);

            return CreateXmlDocumentFromArray(bytes);
        }
        public XmlDocument CreateXmlDocumentFromArray(byte[] xmlDataArray)
        {
            XmlDocument xmlDocument = new XmlDocument();
            using (MemoryStream ms = new MemoryStream(xmlDataArray))
            {
                xmlDocument.PreserveWhitespace = true;
                XmlReader reader = XmlReader.Create(ms);
                xmlDocument.Load(reader);
            }

            return xmlDocument;
        }

        public StringBuilder SerializeContent<T>(T value) where T : class
        {
            if (value == null) { return null; }

            var cacheKey = new { Type = typeof(T) };
            XmlSerializer xmlSerializer = (XmlSerializer)serializerCache[cacheKey];
            if (xmlSerializer == null)
            {
                lock (serializerCache)
                {
                    // double-checked
                    xmlSerializer = (XmlSerializer)serializerCache[cacheKey];
                    if (xmlSerializer == null)
                    {
                        xmlSerializer = new XmlSerializer(typeof(T));
                        serializerCache.Add(cacheKey, xmlSerializer);
                    }
                }
            }

            StringBuilder sb = new StringBuilder();

            try
            {
                using (XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings() { OmitXmlDeclaration = true, Indent = true }))
                {
                    //// Don't include XML namespace
                    //XmlSerializerNamespaces xmlnsEmpty = new XmlSerializerNamespaces()
                    //xmlnsEmpty.Add("", "")
                    //xmlSerializer.Serialize(writer, value, xmlnsEmpty)

                    xmlSerializer.Serialize(writer, value);
                }
                string content = sb.ToString();

                //NON ASCII CHARACTER CONTROL + TURKISH CHARACTERS
                content = Regex.Replace(content, @"[^\u0020-\u007F|ÇĞİÖŞÜçğıöşü]+", string.Empty);

                StringBuilder builder = new StringBuilder(content);

                //XML NEW LINE CLEAR FOR SIGNATURE VERIFICATION
                builder = builder.Replace("&#xA;", "").Replace("&amp;#xA;", "");
                builder = builder.Replace("&#xD;", "").Replace("&amp;#xD;", "");

                //XSLT VIEW FIX : sample = "&amp ve amp; ve  &amp;amp;amp;amp ilker &amp; hasan  &amp;amp;lt;XXX&amp;amp;gt;  &lt; NNN &gt; "
                builder = builder.Replace("amp;", "");
                builder = builder.Replace("&", "&amp;");
                builder = builder.Replace("&amp;amp", "&amp;");

                builder = builder.Replace("&amp;#", "&#");
                builder = builder.Replace("&amp;lt;", "&lt;");
                builder = builder.Replace("&amp;gt;", "&gt;");

                return builder;
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message); }
        }

        public T DeserializeXmlDocumentFile<T>(string fileFullPath) where T : class
        {
            if (!File.Exists(fileFullPath)) { throw new ArgumentException("DESERIALIZE FILE NOT EXISTS"); }

            return DeserializeContent<T>(File.ReadAllText(fileFullPath));
        }

        public T DeserializeContent<T>(string content) where T : class
        {
            int xmlTagStartIndex = content.IndexOf("<?");
            int xmlTagEndIndex = -1;
            if (xmlTagStartIndex >= 0)
            {
                xmlTagEndIndex = content.IndexOf("?>", xmlTagStartIndex);
            }

            if (xmlTagStartIndex >= 0 && xmlTagEndIndex >= 0)
            {
                content = content.Substring(xmlTagEndIndex + 2, content.Length - (xmlTagEndIndex + 2));
            }

            //NON ASCII CHARACTER CONTROL + TURKISH CHARACTERS
            content = Regex.Replace(content, @"[^\u0020-\u007F|ÇĞİÖŞÜçğıöşü]+", " ");

            StringBuilder builder = new StringBuilder(content);

            //JUST MODEL DESERIALIZE THIS CONVERT FOR XSLT FOUND SCHEME CONTROL ETC.
            builder = builder.Replace("amp;", "");
            builder = builder.Replace("&", "&amp;");
            builder = builder.Replace("&amp;amp", "&amp;");

            content = builder.ToString();

            byte[] bytes = Encoding.UTF8.GetBytes(content);

            return DeserializeByte<T>(bytes);
        }

        private T DeserializeByte<T>(byte[] bytes) where T : class
        {
            var cacheKey = new { Type = typeof(T) };
            XmlSerializer xmlSerializer = (XmlSerializer)serializerCache[cacheKey];
            if (xmlSerializer == null)
            {
                lock (serializerCache)
                {
                    // double-checked
                    xmlSerializer = (XmlSerializer)serializerCache[cacheKey];
                    if (xmlSerializer == null)
                    {
                        xmlSerializer = new XmlSerializer(typeof(T));
                        serializerCache.Add(cacheKey, xmlSerializer);
                    }
                }
            }

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                return (T)xmlSerializer.Deserialize(memoryStream);
            }
        }
    }
}
