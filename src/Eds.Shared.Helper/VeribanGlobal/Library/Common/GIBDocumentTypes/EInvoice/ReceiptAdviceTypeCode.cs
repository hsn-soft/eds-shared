namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes.EInvoice
{
    //Bu elemanda UBL-TR içerisinde yer alan İrsaliye Yanıtı tiplerine ait kodlar yazılacaktır.
    public sealed class ReceiptAdviceTypeCode
    {
        private readonly byte _Value;
        private readonly string _Name;
        private readonly string _Description;

        public static readonly ReceiptAdviceTypeCode SEVK = new ReceiptAdviceTypeCode(1, "SEVK", "sevk");
        public static readonly ReceiptAdviceTypeCode MATBUDAN = new ReceiptAdviceTypeCode(2, "MATBUDAN", "Matbudan");

        public static readonly List<ReceiptAdviceTypeCode> TYPE_LIST = new List<ReceiptAdviceTypeCode>
        {
            SEVK,
            MATBUDAN
        };

        private ReceiptAdviceTypeCode(byte value, string name, string description)
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
