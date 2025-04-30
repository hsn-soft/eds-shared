namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes.EInvoice
{
    // Gönderilen döküman için BELGE TÜRÜ
    public sealed class EnvelopePackageElementType
    {
        private readonly byte _Value;
        private readonly string _Name;
        private readonly string _Description;

        public static readonly EnvelopePackageElementType INVOICE = new EnvelopePackageElementType(1, "INVOICE", "FATURA DOKUMANI");
        public static readonly EnvelopePackageElementType DESPATCHADVICE = new EnvelopePackageElementType(2, "DESPATCHADVICE", "IRSALIYE DOKUMANI");
        public static readonly EnvelopePackageElementType APPLICATIONRESPONSE = new EnvelopePackageElementType(3, "APPLICATIONRESPONSE", "UYGULAMA YANITI(FATURA YANITI,SYS_RES)");
        public static readonly EnvelopePackageElementType RECEIPTADVICE = new EnvelopePackageElementType(4, "RECEIPTADVICE", "IRSALIYE DOKUMANI YANITI");
        public static readonly EnvelopePackageElementType PROCESSUSERACCOUNT = new EnvelopePackageElementType(5, "PROCESSUSERACCOUNT", "KULLANICI HESABI ACMA");
        public static readonly EnvelopePackageElementType CANCELUSERACCOUNT = new EnvelopePackageElementType(6, "CANCELUSERACCOUNT", "KULLANICI HESABI KAPAMA");
        public static readonly EnvelopePackageElementType CREDITNOTE = new EnvelopePackageElementType(7, "CREDITNOTE", "YOLCUBERABERFATURA IPTALI");

        public static readonly List<EnvelopePackageElementType> TYPE_LIST = new List<EnvelopePackageElementType>
        {
            INVOICE, DESPATCHADVICE, APPLICATIONRESPONSE, RECEIPTADVICE, PROCESSUSERACCOUNT, CANCELUSERACCOUNT,CREDITNOTE
        };

        private EnvelopePackageElementType(byte value, string name, string description)
        {
            this._Value = value;
            this._Name = name;
            this._Description = description;
        }

        public override string ToString()
        {
            return _Name;
        }

        public byte GetValue()
        {
            return _Value;
        }

        public string GetName()
        {
            return _Name;
        }

        public string GetDescription()
        {
            return _Description;
        }
    }
}
