using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibAccount
{
    [XmlType("ApplicationArea")]
    public class ApplicationArea
    {
        //Kullanıcı eklemek isteyen özel entegratörün bilgileri yazılmalıdır.
        private Sender _senders;
        [XmlElement("Sender", Type = typeof(Sender))]
        public virtual Sender Sender
        {
            get { return this._senders; }
            set { if (value != null) this._senders = value; else this._senders = new Sender(); }
        }

        private Receiver _receiver;
        [XmlElement("Receiver", Type = typeof(Receiver))]
        public virtual Receiver Receiver
        {
            get { return this._receiver; }
            set { if (value != null) this._receiver = value; else this._receiver = new Receiver(); }
        }

        //XML'in oluşturulma zamanı yazılmalıdır.
        [XmlElement("CreationDateTime")]
        public string CreationDateTime { get; set; }

        private UASignature _signature;
        [XmlElement("Signature", Type = typeof(UASignature), Namespace = "http://www.openapplications.org/oagis/9")]
        public virtual UASignature Signature
        {
            get { return this._signature; }
            set { if (value != null) this._signature = value; else this._signature = new UASignature(); }
        }
    }
}
