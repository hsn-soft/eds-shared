namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes.EArchive
{
    public sealed class CreditNoteTypeCode
    {
        private readonly byte _Value;
        private readonly string _Name;
        private readonly string _Description;

        public static readonly CreditNoteTypeCode MUSTAHSILMAKBUZ = new CreditNoteTypeCode(1, "MUSTAHSILMAKBUZ", "Mustahsil Makbuz");

        public static readonly List<CreditNoteTypeCode> TYPE_LIST = new List<CreditNoteTypeCode>
        {
            MUSTAHSILMAKBUZ
        };

        private CreditNoteTypeCode(byte value, string name, string description)
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
