using System.Net;
using System.Text;

namespace Eds.Shared.Helper.VeribanGlobal.Library.SmsManagement
{
    public class SmsService : ISmsService
    {
        public SmsService(ISmsService smsService)
        {

        }

        public SmsService()
        {

        }
        public SmsResponse Send(SmsRequest args)
        {
            return Send(args, null);
        }
        public SmsResponse Send(SmsRequest args, string PostAddress)
        {
            if (PostAddress == null) PostAddress = "http://api.netgsm.com.tr/xmlbulkhttppost.asp";
            //00 273865263
            //00 273905913
            string ss = "";
            ss += "<?xml version='1.0' encoding='UTF-8'?>";
            ss += "<mainbody>";
            ss += "<header>";
            ss += "<company>NETGSM</company>";//sabit değer
            ss += "<usercode>" + args.Username + "</usercode>";//kullanıcı adı
            ss += "<password>" + args.Password + "</password>";//şifre
            ss += "<startdate></startdate>";//boş bırakılacak boş ise hemen gönderimi yapılıyor
            ss += "<stopdate></stopdate>";//boş bırakılacak
            ss += "<type>1:n</type>";
            ss += "<msgheader>" + args.Header + "</msgheader>";//Kullanıcı başlık
            ss += "</header>";
            ss += "<body>";
            ss += "<msg><![CDATA[" + args.SmsText + "]]></msg>";//mesaj içeriği (Faturanız icin tıklayınız. )
            ss += "<no>" + args.Gsm + "</no>";
            ss += "</body>";
            ss += "</mainbody>";

            string result = SendPost(PostAddress, ss);

            if (result == "-1")
            {
                return new SmsResponse { State = "-1" };
            }
            else
            {
                string[] results = result.Split(' ');
                return new SmsResponse { State = results[0] };
            }
        }

        #region Helper

        public string SendPost(string PostAddress, string xmlData)
        {
            try
            {
                using (WebClient wUpload = new WebClient())
                {
                    HttpWebRequest request = WebRequest.Create(PostAddress) as HttpWebRequest;
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    Byte[] bPostArray = Encoding.UTF8.GetBytes(xmlData);
                    Byte[] bResponse = wUpload.UploadData(PostAddress, "POST", bPostArray);
                    Char[] sReturnChars = Encoding.UTF8.GetChars(bResponse);
                    string sWebPage = new string(sReturnChars);
                    return sWebPage;
                }
            }
            catch
            {
                return "-1";
            }
        }

        #endregion
    }
}
