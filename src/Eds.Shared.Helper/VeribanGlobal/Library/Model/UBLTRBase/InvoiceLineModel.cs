using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Belgede geçen mal/hizmete ilişkin bilgilerin girildiği elemandır.
    [XmlType("InvoiceLine", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class InvoiceLine
    {
        // Kalem sıra numarası girilir.
        [XmlElement("ID")]
        public CombineId ID { get; set; }

        // Kalem hakkında açıklama serbest metin olarak girilir.
        private List<String> _notes;
        [XmlElement("Note", Type = typeof(String), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public virtual List<String> Notes
        {
            get { return this._notes; }
            set { if (value != null) this._notes = value; else this._notes = new List<String>(); }
        }

        // Mal/hizmet miktarı birimi ile birlikte girilir.
        [XmlElement("InvoicedQuantity")]
        public virtual BaseUnit InvoicedQuantity { get; set; }

        // Mal/hizmet miktarı ile Mal/hizmet birim fiyatının çarpımı ile bulunan tutardır.
        [XmlElement("LineExtensionAmount")]
        public virtual UblBaseCurrency LineExtensionAmount { get; set; }

        //Fatura ile ilişkili sipariş dokümanının kalemlerine referans atmak için kullanılır. Bknz. OrderLineReference
        private List<OrderLineReference> _orderLineReferences;
        [XmlElement("OrderLineReference", Type = typeof(OrderLineReference), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<OrderLineReference> OrderLineReferences
        {
            get { return this._orderLineReferences; }
            set { if (value != null) this._orderLineReferences = value; else this._orderLineReferences = new List<OrderLineReference>(); }
        }

        //Fatura ile ilişkili irsaliye dokümanının kalemlerine referans atmak için kullanılır. Bknz. LineReference
        private List<LineReference> _despatchLineReferences;
        [XmlElement("DespatchLineReference", Type = typeof(LineReference), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<LineReference> DespatchLineReferences
        {
            get { return this._despatchLineReferences; }
            set { if (value != null) this._despatchLineReferences = value; else this._despatchLineReferences = new List<LineReference>(); }
        }

        //Fatura ile ilişkili alındı dokümanının kalemlerine referans atmak için kullanılır. Bknz. LineReference
        private List<LineReference> _receiptLineReferences;
        [XmlElement("ReceiptLineReference", Type = typeof(LineReference), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<LineReference> ReceiptLineReferences
        {
            get { return this._receiptLineReferences; }
            set { if (value != null) this._receiptLineReferences = value; else this._receiptLineReferences = new List<LineReference>(); }
        }

        //Kalem bazlı teslimat olması durumunda bu eleman doldurulur. Bknz. Delivery
        private List<Delivery> _deliveries;
        [XmlElement("Delivery", Type = typeof(Delivery), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<Delivery> Deliveries
        {
            get { return this._deliveries; }
            set { if (value != null) this._deliveries = value; else this._deliveries = new List<Delivery>(); }
        }

        // Kalem bazlı ıskonto/artırım tutarıdır.
        private List<AllowanceCharge> _allowanceCharges;
        [XmlElement("AllowanceCharge", Type = typeof(AllowanceCharge), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<AllowanceCharge> AllowanceCharges
        {
            get { return this._allowanceCharges; }
            set { if (value != null) this._allowanceCharges = value; else this._allowanceCharges = new List<AllowanceCharge>(); }
        }

        // Kalem bazlı vergi bilgilerinin girildiği elemandır.
        [XmlElement("TaxTotal", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual TaxTotal TaxTotal { get; set; }

        //Kalem bazlı tevkifat uygulanması durumunda bu eleman kullanılır. Bknz. TaxTotal
        private List<TaxTotal> _withholdingTaxTotals;
        [XmlElement("WithholdingTaxTotal", Type = typeof(TaxTotal), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<TaxTotal> WithholdingTaxTotals
        {
            get { return this._withholdingTaxTotals; }
            set { if (value != null) this._withholdingTaxTotals = value; else this._withholdingTaxTotals = new List<TaxTotal>(); }
        }

        // Mal/hizmet hakkında bilgiler buraya girilir.
        [XmlElement("Item", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Item Item { get; set; }

        // Mal/hizmet birim fiyatı hakkında bilgiler buraya girilir.
        [XmlElement("Price", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Price Price { get; set; }

        //Eğer ürün için ek bir birim kodu kullanılması gerekiyorsa bu elemanın içindeki InvoicedQuantity elemanı (diğer opsyonel elemanlar boş bırakılarak) kullanılabilir.
        private List<InvoiceLine> _subInvoiceLines;
        [XmlElement("SubInvoiceLine", Type = typeof(InvoiceLine), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<InvoiceLine> SubInvoiceLines
        {
            get { return this._subInvoiceLines; }
            set { if (value != null) this._subInvoiceLines = value; else this._subInvoiceLines = new List<InvoiceLine>(); }
        }
    }
}
