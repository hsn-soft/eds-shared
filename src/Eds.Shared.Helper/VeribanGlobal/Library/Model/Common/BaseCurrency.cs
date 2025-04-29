using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.Common
{
    [XmlType("Amount")]
    public class UblBaseCurrency
    {
        [XmlAttribute("currencyID")]
        public string CurrencyID { get; set; }

        [XmlText]
        public Decimal Value { get; set; }
    }

    [XmlType("Amount")]
    public class ExcBaseCurrency
    {
        [XmlAttribute("paraBirim")]
        public string CurrencyName { get; set; }

        [XmlAttribute("kur")]
        public decimal CurrencyAmount { get; set; }

        [XmlText]
        public decimal Value { get; set; }
    }

    [XmlType("DocumentCurrencyCode")]
    public class DocumentCurrencyCode
    {
        public DocumentCurrencyCode()
        {
            this.ListID = "ISO 4217 Alpha";
            this.ListAgencyName = "United Nations Economic Commission for Europe";
            this.ListName = "Currency";
            this.ListVersionID = "2001";
        }

        [XmlText()]
        public string Name { get; set; }

        [XmlAttribute("listID")]
        public string ListID { get; set; }

        [XmlAttribute("listAgencyName")]
        public string ListAgencyName { get; set; }

        [XmlAttribute("listName")]
        public string ListName { get; set; }

        [XmlAttribute("listVersionID")]
        public string ListVersionID { get; set; }
    }
}
