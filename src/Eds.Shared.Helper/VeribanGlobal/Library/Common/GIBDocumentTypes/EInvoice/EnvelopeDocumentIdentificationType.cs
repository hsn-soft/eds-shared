namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.GIBDocumentTypes.EInvoice
{
    // Gönderilen döküman için ZARF TÜRÜ
    public sealed class EnvelopeDocumentIdentificationType
    {
        private readonly byte _Value;
        private readonly string _Name;
        private readonly string _Description;

        public static readonly EnvelopeDocumentIdentificationType SENDERENVELOPE = new EnvelopeDocumentIdentificationType(1, "SENDERENVELOPE", "DOKUMAN ZARFI");
        public static readonly EnvelopeDocumentIdentificationType POSTBOXENVELOPE = new EnvelopeDocumentIdentificationType(2, "POSTBOXENVELOPE", "YANIT ZARFI");
        public static readonly EnvelopeDocumentIdentificationType SYSTEMENVELOPE = new EnvelopeDocumentIdentificationType(3, "SYSTEMENVELOPE", "SISTEM ZARFI");
        public static readonly EnvelopeDocumentIdentificationType USERENVELOPE = new EnvelopeDocumentIdentificationType(4, "USERENVELOPE", "KULLANICI ZARFI");

        public static readonly List<EnvelopeDocumentIdentificationType> TYPE_LIST = new List<EnvelopeDocumentIdentificationType>
        {
            SENDERENVELOPE, POSTBOXENVELOPE, SYSTEMENVELOPE, USERENVELOPE
        };

        private EnvelopeDocumentIdentificationType(byte value, string name, string description)
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
