using System.Text;
using System.Xml;
using Eds.Shared.Helper.VeribanGlobal.Library.Common;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.ConstRepository;
using Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.Serializer
{
    public class BaseSerializer
    {
        public BaseSerializer()
        {

        }

        protected string SerializeAndGetXmlContentBase<T>(IList<T> modelList, bool includeHeaderTag, string headerXsltHref = null) where T : class
        {
            if (modelList == null || modelList.Count == 0)
                throw new ArgumentNullException("modelList", "modelList must have a reference");

            XmlManager xmlManager = new XmlManager();
            StringBuilder outputString = new StringBuilder(string.Empty);

            foreach (T model in modelList)
            {
                string gibUserListDocumentXmlContent = xmlManager.SerializeContent<T>(model).ToString();
                int xmlTagEndIndex = 0;
                int xmlTagStartIndex = gibUserListDocumentXmlContent.IndexOf("<?");
                if (xmlTagStartIndex >= 0)
                    xmlTagEndIndex = gibUserListDocumentXmlContent.IndexOf("?>", xmlTagStartIndex);

                string trimContent;
                if (xmlTagStartIndex >= 0 && xmlTagEndIndex >= 0)
                    trimContent = gibUserListDocumentXmlContent.Substring(xmlTagEndIndex + 2, gibUserListDocumentXmlContent.Length - (xmlTagEndIndex + 2));
                else
                    trimContent = gibUserListDocumentXmlContent;

                outputString.Append(trimContent + Environment.NewLine);
            }

            if (includeHeaderTag)
            {
                if (!string.IsNullOrWhiteSpace(headerXsltHref))
                {
                    outputString.Insert(0, $"<?xml-stylesheet type=\"text/xsl\" href =\"{headerXsltHref}\" ?>");
                }
                outputString.Insert(0, "<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            }

            return outputString.ToString();
        }

        protected T DeserializeFromXmlContentBase<T>(string content) where T : class
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException("content", "content must have a reference");

            XmlManager xmlManager = new XmlManager();

            T deserializeModel;
            try
            {
                deserializeModel = xmlManager.DeserializeContent<T>(content);
            }
            catch (Exception ex)
            {
                string exDesc = ex.Message.ToString();
                if (ex.InnerException != null)
                    exDesc += " (" + ex.InnerException.Message.ToString() + ")";
                throw new ArgumentException("MODEL CONTENT CAN'T DESERIALIZE : " + exDesc);
            }

            return deserializeModel;
        }

        public string ControlledDataHandle(List<string> controlData)
        {
            StringBuilder errorMessage = new StringBuilder(string.Empty);
            int maxMessageCount = 10;
            for (int i = 0; i < controlData.Count; i++)
            {
                errorMessage.Append(controlData[i] + ",");
                if (i + 1 >= maxMessageCount) break;
            }
            return errorMessage.ToString();
        }
        public string ControlledDataHandle(List<KeyValuePair<string, string>> controlData)
        {
            StringBuilder errorMessage = new StringBuilder(string.Empty);
            int maxMessageCount = 10;
            for (int i = 0; i < controlData.Count; i++)
            {
                errorMessage.Append(controlData[i].Value + ",");
                if (i + 1 >= maxMessageCount) break;
            }
            return errorMessage.ToString();
        }

        protected string SafeLoadFromXmlFile(string content)
        {
            int xmlTagEndIndex = 0;
            int xmlTagStartIndex = content.IndexOf("<?");
            if (xmlTagStartIndex >= 0)
                xmlTagEndIndex = content.IndexOf("?>", xmlTagStartIndex);
            string trimContent;
            if (xmlTagStartIndex >= 0 && xmlTagEndIndex >= 0)
                trimContent = content.Substring(xmlTagEndIndex + 2, content.Length - (xmlTagEndIndex + 2));
            else
                trimContent = content;

            trimContent = trimContent.Replace("&", "&amp;");

            byte[] bytes = Encoding.UTF8.GetBytes(trimContent);

            MemoryStream ms = new MemoryStream(bytes);

            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            XmlReader reader = XmlReader.Create(ms);
            doc.Load(reader);

            return doc.InnerXml;
        }

        protected bool SaveXmlContentFile(string outputFileFullPath, string outputstring)
        {
            bool isOperationSuccess = false;
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(outputstring);
                MemoryStream ms = new MemoryStream(bytes);

                XmlDocument doc = new XmlDocument();
                doc.PreserveWhitespace = true;
                XmlReader reader = XmlReader.Create(ms);
                doc.Load(reader);

                // write with CR+LF
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.NewLineChars = "\r\n";
                settings.Indent = true;
                XmlWriter writer = XmlWriter.Create(outputFileFullPath, settings);

                // write to file
                doc.WriteTo(writer);
                writer.Close();

                isOperationSuccess = true;
            }
            catch (Exception)
            {
                try { if (File.Exists(outputFileFullPath)) File.Delete(outputFileFullPath); }
                catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
            }

            return isOperationSuccess;
        }

        protected bool UblBaseCurrencyControl(UblBaseCurrency baseCurrencyModel, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (baseCurrencyModel == null) { errorMessage = "can't be null"; return false; }

            if (baseCurrencyModel.Value < 0) { errorMessage = "can't be negative"; return false; }

            if (baseCurrencyModel.CurrencyID != null && !CurrencyCodeControl(baseCurrencyModel.CurrencyID, out errorMessage))
            {
                return false;
            }

            return true;
        }

        protected bool ExcBaseCurrencyControl(ExcBaseCurrency baseCurrencyModel, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (baseCurrencyModel == null) { errorMessage = "can't be null"; return false; }

            if (baseCurrencyModel.Value < 0) { errorMessage = "can't be negative"; return false; }

            if (baseCurrencyModel.CurrencyName != null)
            {
                if (!CurrencyCodeControl(baseCurrencyModel.CurrencyName, out errorMessage)) { return false; }

                if (baseCurrencyModel.CurrencyAmount <= 0) { errorMessage = "grather than 0."; return false; }
            }

            return true;
        }

        protected bool GIBTimeControl(string GIBTimeString, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(GIBTimeString)) { errorMessage = "can't be empty"; return false; }

            if (GIBTimeString.Length != DateFormats.DateTimeGIBFormatTime.Length) { errorMessage = "must be " + DateFormats.DateTimeGIBFormatTime.Length + " character, " + DateFormats.DateTimeGIBFormatTime; return false; }

            if (!DateTime.TryParseExact("2018-01-01T" + GIBTimeString, DateFormats.DateTimeGIBFormatLong, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _))
            {
                errorMessage = "dateTime format must be " + DateFormats.DateTimeGIBFormatTime;
                return false;
            }

            return true;
        }

        protected bool ShortDateControl(string dateTimeString, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(dateTimeString)) { errorMessage = "can't be empty"; return false; }

            if (dateTimeString.Length != DateFormats.DateTimeGIBFormatShort.Length) { errorMessage = "must be " + DateFormats.DateTimeGIBFormatShort.Length + " character, " + DateFormats.DateTimeGIBFormatShort; return false; }

            if (!DateTime.TryParseExact(dateTimeString, DateFormats.DateTimeGIBFormatShort, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _))
            {
                errorMessage = "dateTime format must be " + DateFormats.DateTimeGIBFormatShort;
                return false;
            }

            return true;
        }

        protected bool LongDateControl(string dateTimeString, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(dateTimeString)) { errorMessage = "can't be empty"; return false; }

            if (dateTimeString.Length != DateFormats.DateTimeGIBFormatLong.Length) { errorMessage = "must be " + DateFormats.DateTimeGIBFormatLong.Length + " character, " + DateFormats.DateTimeGIBFormatLong; return false; }

            if (!DateTime.TryParseExact(dateTimeString, DateFormats.DateTimeGIBFormatLong, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _))
            {
                errorMessage = "dateTime format must be " + DateFormats.DateTimeGIBFormatLong;
                return false;
            }

            return true;
        }

        protected bool EnumTypeControl<T>(string enumString, out string errorMessage) where T : struct
        {

            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(enumString)) { errorMessage = "can't be empty"; return false; }

            if (!Enum.TryParse<T>(enumString, out _)) { errorMessage = "unknown : " + enumString; return false; }

            return true;
        }

        protected bool CurrencyCodeControl(string currencyCodeString, out string errorMessage)
        {
            errorMessage = string.Empty;
            ICollection<CurrencyType> currencyList = CurrencyType.GetCurrencyList();

            if (string.IsNullOrEmpty(currencyCodeString)) { errorMessage = "can't be empty"; return false; }

            if (currencyList.FirstOrDefault(x => x.Code.Equals(currencyCodeString)) == null)
            {
                errorMessage = "unknown, CurrencyName : " + currencyCodeString;
                return false;
            }

            return true;
        }

        protected bool NumericStringControl(string numericString, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(numericString)) { errorMessage = "can't be empty"; return false; }

            char[] digits = numericString.ToCharArray();

            for (int i = 0; i < digits.Length; i++)
            {
                if ((int)digits[i] < 48 || (int)digits[i] > 57)
                {
                    errorMessage = "must be numeric";
                    return false;
                }
            }

            return true;
        }

        protected bool GibCheckDecimal(UblBaseCurrency price)
        {
            var ci = System.Globalization.CultureInfo.InvariantCulture.Clone() as System.Globalization.CultureInfo;
            ci.NumberFormat.NumberGroupSeparator = ",";
            ci.NumberFormat.NumberDecimalSeparator = ".";

            string[] res = price.Value.ToString(ci).Split('.');
            if (res != null && res.Length == 2)
            {
                if (res[0].Length <= 15)
                {
                    if (res[1].Length > 2) return false;
                }
                else return false;
            }

            return true;
        }

    }
}
