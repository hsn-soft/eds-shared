namespace Eds.Shared.Helper.VeribanGlobal.Library.SmsManagement
{
    #region Interface
    public interface ISmsService
    {
        SmsResponse Send(SmsRequest args, string PostAddress);
    }

    #endregion

    #region Helper
    public class SmsRequest : Request { }
    public class SmsResponse : Response { }

    public class Request
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Header { get; set; }
        public string SmsText { get; set; }
        public string Gsm { get; set; }
    }

    public class Response
    {
        public string State { get; set; }
    }
    #endregion
}
