namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes.EInvoice
{
    public sealed class DocumentResponseCodeType
    {
        private readonly byte _Value;
        private readonly string _Name;
        private readonly string _Description;

        public static readonly DocumentResponseCodeType KABUL = new DocumentResponseCodeType(1, "KABUL", "Faturanın KABUL edildiğini belirtir.");
        public static readonly DocumentResponseCodeType RED = new DocumentResponseCodeType(2, "RED", "Faturanın RED edildiğini belirtir.");
        public static readonly DocumentResponseCodeType IADE = new DocumentResponseCodeType(3, "IADE", "Faturanın IADE edildiğini belirtir.");
        public static readonly DocumentResponseCodeType S_APR = new DocumentResponseCodeType(4, "S_APR", "Sistem alındı/alınamadı yanıtı.");
        public static readonly DocumentResponseCodeType GUMRUKONAY = new DocumentResponseCodeType(5, "GUMRUKONAY", "GUMRUKONAY");

        public static readonly List<DocumentResponseCodeType> TYPE_LIST = new List<DocumentResponseCodeType>
        {
            KABUL, RED, IADE, GUMRUKONAY
        };

        private DocumentResponseCodeType(byte value, string name, string description)
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
