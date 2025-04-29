namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.GlobalTypes.EArchive
{
    public sealed class InternetSalesSchemeId
    {
        private readonly String name;
        private readonly String description;

        public static readonly InternetSalesSchemeId INTWEBADRES = new InternetSalesSchemeId("INTWEBADRES", "Web Adresi");
        public static readonly InternetSalesSchemeId INTODEMESEKLI = new InternetSalesSchemeId("INTODEMESEKLI", "Ödeme Şekli");
        public static readonly InternetSalesSchemeId INTODEMETARIHI = new InternetSalesSchemeId("INTODEMETARIHI", "Ödeme Tarihi");
        public static readonly InternetSalesSchemeId INTGONDERITASIYAN = new InternetSalesSchemeId("INTGONDERITASIYAN", "Gönderi Taşıyan");
        public static readonly InternetSalesSchemeId INTTASIYICIVKN = new InternetSalesSchemeId("INTTASIYICIVKN", "Taşıyıcı VKN");
        public static readonly InternetSalesSchemeId INTGONDERIMTARIHI = new InternetSalesSchemeId("INTGONDERIMTARIHI", "Gönderim Tarihi");

        public static readonly InternetSalesSchemeId XSLTDISPATCH = new InternetSalesSchemeId("XSLTDISPATCH", "İrsaliye yerine geçer.");
        public static readonly InternetSalesSchemeId XSLTINTERNET = new InternetSalesSchemeId("XSLTINTERNET", "Bu satış internet üzerinden yapılmıştır.");
        public static readonly InternetSalesSchemeId XSLTELECTRONIC = new InternetSalesSchemeId("XSLTELECTRONIC", "e-Arşiv izni kapsamında elektronik ortamda iletilmiştir.");

        private InternetSalesSchemeId(String name, String description)
        {
            this.name = name;
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
        public String GetDescription()
        {
            return description;
        }
    }
}
