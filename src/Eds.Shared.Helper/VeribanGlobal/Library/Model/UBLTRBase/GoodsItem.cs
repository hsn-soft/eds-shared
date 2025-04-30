using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    [XmlType("GoodsItem", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class GoodsItem
    {
        public GoodsItem()
        {
            ID = new CombineId();
        }

        //İlgili ürünü belge içinde tekil olarak tanımlar.
        [XmlElement("ID")]
        public CombineId ID { get; set; }

        //Serbet metin olarak açıklama girilebilir.
        private List<String> _Descriptions;
        [XmlElement("Description", Type = typeof(String))]
        public virtual List<String> Descriptions
        {
            get { return this._Descriptions; }
            set { if (value != null) this._Descriptions = value; else this._Descriptions = new List<String>(); }
        }

        //Ürün tehlikeli madde kategorisinde sayılıp sayılamayacağını gösteren bilgi.
        [XmlElement("HazardousRiskIndicator")]
        public string HazardousRiskIndicator { get; set; }

        //Gümrük değeri tutarı girilir.
        [XmlElement("DeclaredCustomsValueAmount")]
        public virtual UblBaseCurrency DeclaredCustomsValueAmount { get; set; }

        //Nakliye tutarı (navlun) girilir.
        [XmlElement("DeclaredForCarriageValueAmount")]
        public virtual UblBaseCurrency DeclaredForCarriageValueAmount { get; set; }

        //Ürünün GTIP kıymet değeri girilir.
        [XmlElement("DeclaredStatisticsValueAmount")]
        public virtual UblBaseCurrency DeclaredStatisticsValueAmount { get; set; }

        //FOB tutarı girilir.
        [XmlElement("FreeOnBoardValueAmount")]
        public virtual UblBaseCurrency FreeOnBoardValueAmount { get; set; }

        //Sigorta tutarı girilir.
        [XmlElement("InsuranceValueAmount")]
        public virtual UblBaseCurrency InsuranceValueAmount { get; set; }

        //Ürünün değeri girilir. Ana seviyedeki Shipment'ın altında kullanımında toplam tutar bilgisi girilebilir.
        [XmlElement("ValueAmount")]
        public virtual UblBaseCurrency ValueAmount { get; set; }

        //Brüt ağırlığı girilir.
        [XmlElement("GrossWeightMeasure")]
        public virtual BaseUnit GrossWeightMeasure { get; set; }

        //Net ağırlığı girilir.
        [XmlElement("NetWeightMeasure")]
        public virtual BaseUnit NetWeightMeasure { get; set; }

        //Belli bir ücretin uygulanabileceği brüt ağırlığı girilir.
        [XmlElement("ChargableWeightMeasure")]
        public virtual BaseUnit ChargableWeightMeasure { get; set; }

        //Brüt hacim girilir.
        [XmlElement("GrossVolumeMeasure")]
        public virtual BaseUnit GrossVolumeMeasure { get; set; }

        //Net hacim girilir.
        [XmlElement("NetVolumeMeasure")]
        public virtual BaseUnit NetVolumeMeasure { get; set; }

        //Miktar girilir.
        [XmlElement("Quantity")]
        public virtual BaseUnit Quantity { get; set; }

        //Ürünün veya malın GTIP numarası girilir.
        [XmlElement("RequiredCustomsID")]
        public string RequiredCustomsID { get; set; }

        //Gümrük durum kodu girilir.
        [XmlElement("CustomsStatusCode")]
        public string CustomsStatusCode { get; set; }

        //İstatistiksel, tarife veya mali amaçlı gümrük mal miktarı girilir.
        [XmlElement("CustomsTariffQuantity")]
        public virtual BaseUnit CustomsTariffQuantity { get; set; }

        //Malların gümrükte ithalat için sınıflandırılmış olup olmadığını belirtir.
        [XmlElement("CustomsImportClassifiedIndicator")]
        public string CustomsImportClassifiedIndicator { get; set; }

        //Belli bir ücretin uygulanabileceği miktar girilir.
        [XmlElement("ChargeableQuantity")]
        public virtual BaseUnit ChargeableQuantity { get; set; }

        //Malların ne kadarının geri gelebileceği girilir.
        [XmlElement("ReturnableQuantity")]
        public virtual BaseUnit ReturnableQuantity { get; set; }

        //TraceID Takip numarası girilir.
        [XmlElement("TraceID")]
        public string TraceID { get; set; }

        //Malların fatura kalemleriyle olan ilişkileri girilir. Bknz. Item
        private List<Item> _Items;
        [XmlElement("Item", Type = typeof(Item), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<Item> Items
        {
            get { return this._Items; }
            set { if (value != null) this._Items = value; else this._Items = new List<Item>(); }
        }

        //Taşıma bedelinde indirim/fiyat artırımı var ise girilir. Bknz. AllowanceCharge
        private List<AllowanceCharge> _FreightAllowanceCharges;
        [XmlElement("FreightAllowanceCharge", Type = typeof(AllowanceCharge), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<AllowanceCharge> FreightAllowanceCharges
        {
            get { return this._FreightAllowanceCharges; }
            set { if (value != null) this._FreightAllowanceCharges = value; else this._FreightAllowanceCharges = new List<AllowanceCharge>(); }
        }

        // Birim fiyat ve kalem toplam fiyat bilgileri girilir.
        private List<InvoiceLine> _invoiceLines;
        [XmlElement("InvoiceLine", Type = typeof(InvoiceLine), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<InvoiceLine> InvoiceLines
        {
            get { return this._invoiceLines; }
            set { if (value != null) this._invoiceLines = value; else this._invoiceLines = new List<InvoiceLine>(); }
        }

        //Sevkiyattaki mallar ile ilgili her türlü sıcaklık bilgisi girilebilir. Bknz. Temperature
        private List<Temperature> _Temperatures;
        [XmlElement("Temperature", Type = typeof(Temperature), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<Temperature> Temperatures
        {
            get { return this._Temperatures; }
            set { if (value != null) this._Temperatures = value; else this._Temperatures = new List<Temperature>(); }
        }

        //Ürünlerin üretildiği adres girilir. Bknz. Address
        [XmlElement("OriginAddress", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Address OriginAddress { get; set; }

        //Ürünlerin diğer ölçümleri girilir. Bknz. Dimension
        private List<Dimension> _MeasurementDimensions;
        [XmlElement("MeasurementDimension", Type = typeof(Dimension), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<Dimension> MeasurementDimensions
        {
            get { return this._MeasurementDimensions; }
            set { if (value != null) this._MeasurementDimensions = value; else this._MeasurementDimensions = new List<Dimension>(); }
        }
    }
}
