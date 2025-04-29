using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    [XmlType("BillingReferenceLine")]
    public class BillingReferenceLine
    {
        //Kalem numarası girilir.
        [XmlElement("ID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]

        public string ID { get; set; }

        //Kalemin tutarı girilir.
        [XmlElement("Amount", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string Note { get; set; }

        //Kaleme eğer indirim veya fiyat artırımı uygulanmışsa girilir.
        private List<AllowanceCharge> _allowanceCharges;
        [XmlElement("AllowanceCharge")]
        public virtual List<AllowanceCharge> References
        {
            get { return this._allowanceCharges; }
            set { if (value != null) this._allowanceCharges = value; else this._allowanceCharges = new List<AllowanceCharge>(); }
        }
    }
}
