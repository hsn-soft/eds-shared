namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes.EInvoice
{
    //Bu elemanda UBL-TR içerisinde yer alan sevk irsaliyesi tiplerine ait kodlar yazılacaktır.
    public sealed class DespatchAdviceTypeCode
    {
        private readonly byte _Value;
        private readonly string _Name;
        private readonly string _Description;

        public static readonly DespatchAdviceTypeCode SEVK = new DespatchAdviceTypeCode(1, "SEVK", "Sevk");
        public static readonly DespatchAdviceTypeCode MATBUDAN = new DespatchAdviceTypeCode(2, "MATBUDAN", "Matbudan");

        public static readonly List<DespatchAdviceTypeCode> TYPE_LIST = new List<DespatchAdviceTypeCode>
        {
            SEVK, MATBUDAN
        };

        private DespatchAdviceTypeCode(byte value, string name, string description)
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
