using System.Xml.Serialization;
using Eds.Shared.Helper.eInvoice.Library.Model.Utils;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase;

namespace Eds.Shared.Helper.eInvoice.Library.Model
{
    [XmlRoot("DespatchAdvice", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:DespatchAdvice-2")]
    public class DespatchAdviceModel
    {
        public DespatchAdviceModel()
        {
            xmlns.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            xmlns.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            xmlns.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
            xmlns.Add("ccts", "urn:un:unece:uncefact:documentation:2");
            xmlns.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            xmlns.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
            xmlns.Add("ubltr", "urn:oasis:names:specification:ubl:schema:xsd:TurkishCustomizationExtensionComponents");
            xmlns.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
            xmlns.Add("xades", "http://uri.etsi.org/01903/v1.3.2#");

            xmlns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            xmlns.Add("", "urn:oasis:names:specification:ubl:schema:xsd:DespatchAdvice-2");

            this.UBLVersionID = UblTr2HandlerEInvoice.UblVersion;
            this.CustomizationID = UblTr2HandlerEInvoice.CustomizationVersion;
            this.CopyIndicator = false;
        }

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces xmlns { get; set; } = new XmlSerializerNamespaces();

        [XmlAttribute("schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string schemaLocation { get; set; } = "urn:oasis:names:specification:ubl:schema:xsd:DespatchAdvice-2 UBL" + (UblTr2HandlerEInvoice.UblVersion == "2.0" ? "TR" : "") + "-DespatchAdvice-" + UblTr2HandlerEInvoice.UblVersion + ".xsd";

        private List<UblExtension> _UBLExtensions;
        [XmlElement("UBLExtensions", Type = typeof(List<UblExtension>), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
        public virtual List<UblExtension> UBLExtensions
        {
            get { return this._UBLExtensions; }
            set { if (value != null) this._UBLExtensions = value; else this._UBLExtensions = new List<UblExtension>(); }
        }

        [XmlElement("UBLVersionID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string UBLVersionID { get; set; }

        [XmlElement("CustomizationID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string CustomizationID { get; set; }

        [XmlElement("ProfileID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string ProfileID { get; set; }

        [XmlElement("ID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string ID { get; set; }

        [XmlElement("CopyIndicator", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public bool CopyIndicator { get; set; }

        [XmlElement("UUID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string UUID { get; set; }

        [XmlElement("IssueDate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string IssueDate { get; set; }

        [XmlElement("IssueTime", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string IssueTime { get; set; }

        [XmlElement("DespatchAdviceTypeCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string DespatchAdviceTypeCode { get; set; }

        [XmlElement("DocumentCurrencyCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public DocumentCurrencyCode DocumentCurrencyCode { get; set; }

        private List<String> _notes;
        [XmlElement("Note", Type = typeof(String), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public virtual List<String> Notes
        {
            get { return this._notes; }
            set { if (value != null) this._notes = value; else this._notes = new List<String>(); }
        }

        [XmlElement("LineCountNumeric", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public double LineCountNumeric { get; set; }

        private OrderReference _orderReference;
        [XmlElement("OrderReference", Type = typeof(OrderReference), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual OrderReference OrderReference
        {
            get { return this._orderReference; }
            set { if (value != null) this._orderReference = value; else this._orderReference = new OrderReference(); }
        }

        private List<DocumentReference> _additionalDocumentReferences;
        [XmlElement("AdditionalDocumentReference", Type = typeof(DocumentReference), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<DocumentReference> AdditionalDocumentReferences
        {
            get { return this._additionalDocumentReferences; }
            set { if (value != null) this._additionalDocumentReferences = value; else this._additionalDocumentReferences = new List<DocumentReference>(); }
        }

        private List<CustomSignature> _signatures;
        [XmlElement("Signature", Type = typeof(CustomSignature), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<CustomSignature> Signatures
        {
            get { return this._signatures; }
            set { if (value != null) this._signatures = value; else this._signatures = new List<CustomSignature>(); }
        }

        private SupplierParty _despatchSupplierParty;
        [XmlElement("DespatchSupplierParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual SupplierParty DespatchSupplierParty
        {
            get { return this._despatchSupplierParty; }
            set { if (value != null) this._despatchSupplierParty = value; else this._despatchSupplierParty = new SupplierParty(); }
        }

        private CustomerParty _deliveryCustomerParty;
        [XmlElement("DeliveryCustomerParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual CustomerParty DeliveryCustomerParty
        {
            get { return this._deliveryCustomerParty; }
            set { if (value != null) this._deliveryCustomerParty = value; else this._deliveryCustomerParty = new CustomerParty(); }
        }

        private CustomerParty _buyerCustomerParty;
        [XmlElement("BuyerCustomerParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual CustomerParty BuyerCustomerParty
        {
            get { return this._buyerCustomerParty; }
            set { if (value != null) this._buyerCustomerParty = value; else this._buyerCustomerParty = new CustomerParty(); }
        }

        private SupplierParty _sellerSupplierParty;
        [XmlElement("SellerSupplierParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual SupplierParty SellerSupplierParty
        {
            get { return this._sellerSupplierParty; }
            set { if (value != null) this._sellerSupplierParty = value; else this._sellerSupplierParty = new SupplierParty(); }
        }

        private CustomerParty _originatorCustomerParty;
        [XmlElement("OriginatorCustomerParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual CustomerParty OriginatorCustomerParty
        {
            get { return this._originatorCustomerParty; }
            set { if (value != null) this._originatorCustomerParty = value; else this._originatorCustomerParty = new CustomerParty(); }
        }

        [XmlElement("Shipment", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public Shipment Shipment { get; set; }


        // (Zorunlu değil!) Bu elemana ödeme şekli ile ilgili bilgiler yazılabilecektir.
        private List<PaymentMeans> _paymentMeans;
        [XmlElement("PaymentMeans", Type = typeof(PaymentMeans), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<PaymentMeans> PaymentMeans
        {
            get { return this._paymentMeans; }
            set { if (value != null) this._paymentMeans = value; else this._paymentMeans = new List<PaymentMeans>(); }
        }

        // (Zorunlu değil!) Bu elemana ödeme koşulları ve ödemenin yapılmaması halinde uygulanacak müeyyideler yazılabilecektir.
        private PaymentTerms _paymentTerms;
        [XmlElement("PaymentTerms", Type = typeof(PaymentTerms), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual PaymentTerms PaymentTerms
        {
            get { return this._paymentTerms; }
            set { if (value != null) this._paymentTerms = value; else this._paymentTerms = new PaymentTerms(); }
        }

        private List<DespatchLine> _despatchLines;
        [XmlElement("DespatchLine", Type = typeof(DespatchLine), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<DespatchLine> DespatchLines
        {
            get { return this._despatchLines; }
            set { if (value != null) this._despatchLines = value; else this._despatchLines = new List<DespatchLine>(); }
        }
    }
}
