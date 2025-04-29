namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.StatusCodes
{
    public sealed class MailSendResults
    {
        private readonly String name;
        private readonly Int32 code;
        private readonly String description;

        public static readonly MailSendResults InvoiceNotFound = new MailSendResults("INVOICE NOT FOUND", -1, "Maili sorgulanacak fatura bulunamadı!");
        public static readonly MailSendResults NoSend = new MailSendResults("NO SEND", 0, "Mail bilgisi girilmemiş!");
        public static readonly MailSendResults WaitForSend = new MailSendResults("GONDERIM KUYRUGUNDA", 1, "Mail gönderilmek üzere kuyrukta bekliyor");
        public static readonly MailSendResults WaitForCallBack = new MailSendResults("WAIT FOR CALLBACK", 2, "Wait For CallBack!");
        public static readonly MailSendResults Sending = new MailSendResults("MAIL GONDERILIYOR", 11, "Mail Gönderiliyor");
        public static readonly MailSendResults AddressError = new MailSendResults("GECERSIZ E-POSTA ADRESI", 30, "Geçersiz e-posta adresi!");
        public static readonly MailSendResults ProcessError = new MailSendResults("SISTEM HATASI", 31, "Mail gönderilemedi!");
        public static readonly MailSendResults CallBackError = new MailSendResults("CALLBACK ERROR", 32, "CallBackError!");
        public static readonly MailSendResults DebugTest = new MailSendResults("DEBUG TEST", 61, "Debug Test!");
        public static readonly MailSendResults SuccessfullySended = new MailSendResults("SUCCESSFULLY SENDED", 99, "Mail başarılı bir şekilde gönderildi");

        private MailSendResults(String name, Int32 code, String description)
        {
            this.name = name;
            this.code = code;
            this.description = description;
        }
        public override String ToString()
        {
            return name;
        }
        public String GetName()
        {
            return name;
        }
        public Int32 GetCode()
        {
            return code;
        }
        public String GetDescription()
        {
            return description;
        }
    }
}
