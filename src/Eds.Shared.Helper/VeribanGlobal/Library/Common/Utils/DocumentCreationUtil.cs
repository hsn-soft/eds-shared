using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils
{
    public static class DocumentCreationUtil
    {
        public static string CreateHtmlContentWithXmlDataAndXsltDesign(string xmlContent, string xsltFilePath)
        {
            if (!string.IsNullOrWhiteSpace(xsltFilePath))
            {
                try
                {
                    XslCompiledTransform proc = new XslCompiledTransform(true);
                    XsltSettings xsltSettings = new XsltSettings(true, false);

                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.DtdProcessing = DtdProcessing.Ignore;
                    settings.ValidationType = ValidationType.DTD;

                    proc.Load(xsltFilePath, xsltSettings, new XmlUrlResolver());

                    return CreateHtmlContentBase(xmlContent, settings, proc);
                }
                catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
            }

            return null;
        }
        public static string CreateHtmlContentWithXmlDataAndXsltDesignOnWeb(string documentFileFullPath, string xsltBase64Content)
        {
            if (!string.IsNullOrWhiteSpace(xsltBase64Content) && !string.IsNullOrWhiteSpace(documentFileFullPath) && File.Exists(documentFileFullPath))
            {
                return CreateHtmlContentWithXmlDataAndXsltDesignOnApi(File.ReadAllText(documentFileFullPath, Encoding.UTF8), xsltBase64Content);
            }

            return null;
        }
        public static string CreateHtmlContentWithXmlDataAndXsltDesignOnApi(string xmlContent, string xsltBase64Content)
        {
            if (!string.IsNullOrWhiteSpace(xsltBase64Content))
            {
                try
                {
                    XslCompiledTransform proc = new XslCompiledTransform(true);
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.DtdProcessing = DtdProcessing.Ignore;
                    settings.ValidationType = ValidationType.DTD;

                    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(xsltBase64Content)))
                    {
                        using (XmlReader xr = XmlReader.Create(ms, settings))
                        {
                            proc.Load(xr);
                        }
                    }

                    return CreateHtmlContentBase(xmlContent, settings, proc);
                }
                catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
            }

            return null;
        }
        private static string CreateHtmlContentBase(string xmlContent, XmlReaderSettings settings, XslCompiledTransform proc)
        {
            string xmlDataContent = xmlContent;
            try
            {
                xmlDataContent = xmlDataContent.Replace("&", "&amp;");
                xmlDataContent = xmlDataContent.Replace("&amp;amp;", "&amp;");
                xmlDataContent = xmlDataContent.Replace("&amp;lt;", "&lt;");
                xmlDataContent = xmlDataContent.Replace("&amp;gt;", "&gt;");
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xmlDataContent)))
            {
                using (XmlReader xr = XmlReader.Create(ms, settings))
                {
                    using (StringWriter sw = new StringWriter())
                    {
                        proc.Transform(xr, null, sw);
                        return sw.ToString();
                    }
                }
            }
        }

        public static string CreateHtmlContentWithXmlDataAndXsltDesignOnApp(string documentFileFullPath, string xsltBase64Content, bool isAdvancedVersion, string alternateAppFullPath = null)
        {
            if (!string.IsNullOrWhiteSpace(documentFileFullPath) && !string.IsNullOrWhiteSpace(xsltBase64Content))
            {
                FileInfo xmlFi = new FileInfo(documentFileFullPath);
                if (xmlFi.Exists)
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            var processDocument = new XsltProcessDocument()
                            {
                                XmlFileFullPath = documentFileFullPath,
                                XsltBase64Content = xsltBase64Content
                            };

                            var xsltOutput = new XsltOutput()
                            {
                                //OutputFilePath = testOutputPath,
                                OutputStream = ms
                            };

                            if (!string.IsNullOrEmpty(alternateAppFullPath))
                            {
                                var pathInfo = new XsltConvertEnvironment
                                {
                                    TempFolderPath = Path.GetTempPath(),
                                    XmlToHtmlExePath = alternateAppFullPath,
                                    Timeout = 60000
                                };
                                XsltConvert.ConvertXmlToHtml(processDocument, pathInfo, xsltOutput, true);
                            }
                            else
                            {
                                XsltConvert.ConvertXmlToHtml(processDocument, xsltOutput, isAdvancedVersion);
                            }

                            return System.Text.Encoding.UTF8.GetString(ms.ToArray());
                        }
                    }
                    catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
                }
            }

            return null;
        }
    }
}
