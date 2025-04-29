using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils
{
    [Serializable]
    public class XsltConvertException : Exception
    {
        public XsltConvertException(String msg) : base(msg) { }

        protected XsltConvertException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }

    [Serializable]
    public class XsltConvertTimeoutException : XsltConvertException
    {
        public XsltConvertTimeoutException() : base("XML to HTML conversion process has not finished in the given period.") { }

        protected XsltConvertTimeoutException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }

    public class XsltProcessDocument
    {
        public String XmlFileFullPath { get; set; }
        public String XsltBase64Content { get; set; }
    }

    public class XsltOutput
    {
        public String OutputFilePath { get; set; }
        public Stream OutputStream { get; set; }
    }

    public class XsltConvertEnvironment
    {
        public String TempFolderPath { get; set; }
        public String XmlToHtmlExePath { get; set; }
        public int Timeout { get; set; }
        public bool Debug { get; set; }
    }

    public static class XsltConvert
    {
        static XsltConvertEnvironment _e;

        public static XsltConvertEnvironment Environment
        {
            get
            {
                if (_e == null)
                    _e = new XsltConvertEnvironment
                    {
                        TempFolderPath = Path.GetTempPath(),
                        XmlToHtmlExePath = @"C:\VeribanLibrary\VeribanXsltConverter\HtmlBuilder.exe",
                        Timeout = 60000
                    };
                return _e;
            }
        }

        public static void ConvertXmlToHtml(XsltProcessDocument input, XsltOutput output, bool isAdvancedVersion)
        {
            ConvertXmlToHtml(input, null, output, isAdvancedVersion);
        }

        public static void ConvertXmlToHtml(XsltProcessDocument input, XsltConvertEnvironment environment, XsltOutput output, bool isAdvancedVersion)
        {
            if (input == null)
            {
                throw new XsltConvertException("input parameter is not null");
            }

            if (string.IsNullOrWhiteSpace(input.XmlFileFullPath))
            {
                throw new XsltConvertException("You must enter valid XML file full path");
            }

            if (!File.Exists(input.XmlFileFullPath))
            {
                throw new XsltConvertException(String.Format("XML file not found : {0}", input.XmlFileFullPath));
            }

            if (environment == null)
                environment = Environment;

            String outputHtmlFilePath;
            bool outputFiledelete;
            if (!string.IsNullOrEmpty(output.OutputFilePath))
            {
                outputHtmlFilePath = output.OutputFilePath;
                outputFiledelete = false;
            }
            else
            {
                outputHtmlFilePath = Path.Combine(environment.TempFolderPath, String.Format("{0}.html", Guid.NewGuid()));
                outputFiledelete = true;
            }

            if (!File.Exists(environment.XmlToHtmlExePath))
                throw new XsltConvertException(String.Format("File '{0}' not found. Check if VeribanXsltConverter.HtmlBuilder application is installed.", environment.XmlToHtmlExePath));

            try
            {
                bool debug = true;
                using (Process myProcess = new Process())
                {
                    if (isAdvancedVersion)
                    {
                        myProcess.StartInfo.FileName = @"C:\VeribanLibrary\VeribanXsltConverter\HtmlBuilder.exe";
                    }
                    else
                    {
                        myProcess.StartInfo.FileName = @"C:\VeribanLibrary\VeribanXsltConverter\HtmlBuilder_SE.exe";
                    }

                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.RedirectStandardInput = true;

                    myProcess.StartInfo.CreateNoWindow = !debug;
                    myProcess.StartInfo.RedirectStandardError = !debug;

                    StringBuilder paramsBuilder = new StringBuilder();

                    paramsBuilder.AppendFormat("{0} {1}", input.XmlFileFullPath, outputHtmlFilePath);

                    myProcess.StartInfo.Arguments = paramsBuilder.ToString();

                    Console.WriteLine("START CHILDPROCESS");
                    myProcess.Start();

                    using (var stream = new StreamWriter(myProcess.StandardInput.BaseStream, Encoding.UTF8))
                    {
                        if (!string.IsNullOrWhiteSpace(input.XsltBase64Content))
                        {
                            //CLEAR NEW LINE CHARACTER
                            string controlledInputBase64String = input.XsltBase64Content.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");

                            byte[] buffer = Encoding.UTF8.GetBytes(controlledInputBase64String);
                            stream.BaseStream.Write(buffer, 0, buffer.Length);

                            //ADD READLINE INPUT CHARACTER
                            buffer = Encoding.UTF8.GetBytes("\n");
                            stream.BaseStream.Write(buffer, 0, buffer.Length);
                            stream.Write("\r");
                        }
                        else
                        {
                            //ANOTHER EXE BYPASS READLINE WHEN STANDART INPUT DISPOSE , IF STANDART INPUT NOT DISPOSE WAIT TIME (10000)
                        }
                    }

                    // WAIT CHILD PROCESS
                    Console.WriteLine("WAIT CHILDPROCESS");
                    myProcess.WaitForExit(15000);
                    Console.WriteLine("END CHILDPROCESS");

                    if (!File.Exists(outputHtmlFilePath))
                    {
                        if (myProcess.ExitCode != 0)
                        {
                            string error = string.Empty;
                            if (myProcess.StartInfo.RedirectStandardError)
                            {
                                error = myProcess.StandardError.ReadToEnd();
                            }
                            error = error + string.Format(" Process exited with code {0}.", myProcess.ExitCode);

                            throw new XsltConvertException(string.Format("XsltCompiler Output: \r\n{0}", error));
                        }

                        throw new XsltConvertException(String.Format("XML to HTML conversion of '{0}' failed. Reason: Output file '{1}' not found.", input.XmlFileFullPath, outputHtmlFilePath));
                    }

                    if (output.OutputStream != null)
                    {
                        using (Stream fs = new FileStream(outputHtmlFilePath, FileMode.Open))
                        {
                            byte[] buffer = new byte[32 * 1024];
                            int read;

                            while ((read = fs.Read(buffer, 0, buffer.Length)) > 0)
                                output.OutputStream.Write(buffer, 0, read);
                        }
                    }
                }
            }
            finally
            {
                if (outputFiledelete && File.Exists(outputHtmlFilePath))
                    File.Delete(outputHtmlFilePath);
            }
        }
    }
}
