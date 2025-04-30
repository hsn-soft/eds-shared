using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase;

namespace Eds.Shared.Helper.eInvoice.Library.Model
{
    [XmlRoot("CreditNote", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2")]
    public class CreditNoteModel
    {
        public CreditNoteModel()
        {

            xmlns.Add("ns1", "http://uri.etsi.org/01903/v1.3.2#");
            xmlns.Add("ns2", "urn:oasis:names:specification:ubl:schema:xsd:CommonSignatureComponents-2");
            xmlns.Add("ns3", "urn:un:unece:uncefact:data:specification:CoreComponentTypeSchemaModule:2");
            xmlns.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            xmlns.Add("xades", "http://uri.etsi.org/01903/v1.4.1#");
            xmlns.Add("ns6", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDataTypes-2");
            xmlns.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            xmlns.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            xmlns.Add("ns9", "urn:oasis:names:specification:ubl:schema:xsd:UnqualifiedDataTypes-2");
            xmlns.Add("", "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2");
            xmlns.Add("ns11", "urn:oasis:names:specification:ubl:schema:xsd:SignatureBasicComponents-2");
            xmlns.Add("ns12", "urn:oasis:names:specification:ubl:schema:xsd:SignatureAggregateComponents-2");
            xmlns.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
            xmlns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            this.UBLVersionID = "2.1";
            this.CustomizationID = "TR1.2";
            this.ProfileID = "YOLCUBERABERFATURA";
            this.CopyIndicator = false;
            this.CreditNoteTypeCode = "SATISIPTALIADE";
            this.CreditNoteLines = new List<CreditNoteLine>()
            {
                new CreditNoteLine()
                {
                    ID = ""
                },
            };
        }

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces xmlns { get; set; } = new XmlSerializerNamespaces();

        [XmlAttribute("schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string schemaLocation { get; set; } = "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2 UBL-CreditNote-2.1.xsd";

        // Bu alana XAdES formatında mali mühür/ elektronik imza bilgileri yazılacaktır.
        private List<UblExtension> _UBLExtensions;
        [XmlElement("UBLExtensions", Type = typeof(List<UblExtension>), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
        public virtual List<UblExtension> UBLExtensions
        {
            get { return this._UBLExtensions; }
            set { if (value != null) this._UBLExtensions = value; else this._UBLExtensions = new List<UblExtension>(); }
        }

        // XSD dokümanının UBL versiyonu yazılacaktır.Bu değer için “2.1” kullanılacaktır.
        [XmlElement("UBLVersionID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string UBLVersionID { get; set; }

        // UBL’ in özelleştirme numarasıdır. Bu değer için “TR1.2” kullanılacaktır.
        [XmlElement("CustomizationID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string CustomizationID { get; set; }

        // Kullanılan senaryodur. "YOLCUBERABERFATURA" olmalıdır.
        [XmlElement("ProfileID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string ProfileID { get; set; }

        /// Yeni Eklendi
        [XmlElement("ProfileExecutionID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string ProfileExecutionID { get; set; }

        // Üç haneli harf grubunu ifade eden birim kod ile 13 haneli müteselsil numaranın birleşiminden meydana gelen Fatura
        // Numarası bu elemana yazılacaktır. Müteselsil numaranın ilk dört hanesi faturanın düzenlendiği yılı kalan dokuz hane ise
        // müteselsil numarayı ifade etmektedir. Fatura düzenleyen bünyesinde aynı fatura numarası birden fazla kullanılamaz.
        // Örnek: "GIB2009000000001"
        /// İlgili YOLCUBERABERFATURA senaryosundaki faturanın numarası girilecektir.
        [XmlElement("ID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string ID { get; set; }

        // Bu elemanda düzenlenen faturanın asıl veya suret olduğu gösterilecektir. Asıl ise “false”, suret ise “true”.
        [XmlElement("CopyIndicator", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public bool CopyIndicator { get; set; }

        // Evrensel Tekil Tanımlama Numarası (ETTN), düzenlenen faturanın evrensel eşsizliğini sağlayan numaradır. Bu numara
        // fatura düzenleyen tarafından standartlara uygun olarak üretilip faturalarda kullanılacaktır.
        // Örnek: "e093a490-dd99-11dd-ad8b-0800200c9a66"
        [XmlElement("UUID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string UUID { get; set; }

        // Bu elemana faturanın düzenleme tarihi yazılacaktır. Yıl-Ay-Gün (YYYY-AA-GG)
        [XmlElement("IssueDate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string IssueDate { get; set; }

        // (Zorunlu değil!) Bu elemana faturanın düzenleme saati yazılabilecektir. Saat:Dakika:Saniye
        [XmlElement("IssueTime", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string IssueTime { get; set; }

        /// Yeni Eklendi
        [XmlElement("TaxPointDate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string TaxPointDate { get; set; }

        /// Bu elemanda UBL-TR içerisinde yer alan fatura tiplerine ait kodlar yazılacaktır. "SATISIPTALIADE" olmalıdır.
        [XmlElement("CreditNoteTypeCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string CreditNoteTypeCode { get; set; }

        // (Zorunlu değil!) Faturada yer verilmek istenen genel açıklamalar için bu eleman kullanılacaktır. Birden fazla açıklama yapılmak
        // istenmesi halinde elemanın tekrar kullanımı mümkündür.
        // Örnek : "İş bu fatura muhteviyatına 7 gün içerisinde itiraz edilmediği taktirde aynen kabul edilmiş sayılır."
        private List<String> _notes;
        [XmlElement("Note", Type = typeof(String), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public virtual List<String> Notes
        {
            get { return this._notes; }
            set { if (value != null) this._notes = value; else this._notes = new List<String>(); }
        }

        // Bu elemana faturanın düzenlendiği paranın birim kodu yazılacaktır. Örnek : "TRY"
        [XmlElement("DocumentCurrencyCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public DocumentCurrencyCode DocumentCurrencyCode { get; set; }

        // (Zorunlu değil!) Fatura üzerinde gösterilen vergilerin “Belge Para Birim Kodu” dışında başka bir para birimi ile gösterilmesi gerekiyorsa
        // bu para biriminin kodu yazılabilecektir.Örnek : "USD"
        [XmlElement("TaxCurrencyCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public DocumentCurrencyCode TaxCurrencyCode { get; set; }

        // (Zorunlu değil!) Mal veya hizmet bedellerinin “Belge Para Birim Kodu” dışında bir para birimi ile gösterilmesi gerekiyorsa sözkonusu para birimi
        // bu elemana kodlanabilecektir. Örnek : "USD"
        [XmlElement("PricingCurrencyCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public DocumentCurrencyCode PricingCurrencyCode { get; set; }

        // (Zorunlu değil!) Ödemenin yapılacağı para birimi “Belge Para Birim Kodu” dışında bir para birimi ise bu bedellerin gösterildiği para birimi
        // bu elemana kodlanabilecektir. Örnek : "USD"
        [XmlElement("PaymentCurrencyCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public DocumentCurrencyCode PaymentCurrencyCode { get; set; }

        // (Zorunlu değil!) Ödenecek tutarın belge para birimi ile ödeme para birimi dışında başka bir para birimi ile de ifade edilmesinin istenmesi
        // halinde söz konusu para biriminin kodu bu elemana yazılabilecektir. Örn: "USD"
        [XmlElement("PaymentAlternativeCurrencyCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public DocumentCurrencyCode PaymentAlternativeCurrencyCode { get; set; }

        /// Yeni Eklendi
        [XmlElement("AccountingCostCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string AccountingCostCode { get; set; }

        //Mükellefin faturanın tipi olarak ilave bir ayırıma gitmesi gerekiyorsa kendi belirleyeceği farklı bir fatura tipini bu elemana yazabilir.
        [XmlElement("AccountingCost", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string AccountingCost { get; set; }

        // Bu elemana fatura üzerinde yer alan kalem sayısı yazılacaktır. Malın adedi birden fazla olsa dahi bu mal grubu tek bir kalem olarak gösterilecektir.
        [XmlElement("LineCountNumeric", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public double LineCountNumeric { get; set; }

        /// Yeni Eklendi
        [XmlElement("BuyerReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string BuyerReference { get; set; }

        // (Zorunlu değil!) Faturada dönem bilgisine yer verilmesi halinde bu eleman kullanılacaktır.
        private Period _invoicePeriod;
        [XmlElement("InvoicePeriod", Type = typeof(Period), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Period InvoicePeriod
        {
            get { return this._invoicePeriod; }
            set { if (value != null) this._invoicePeriod = value; else this._invoicePeriod = new Period(); }
        }

        /// Yeni Eklendi
        private Response _discrepancyResponse;
        [XmlElement("DiscrepancyResponse", Type = typeof(Response), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Response DiscrepancyResponse
        {
            get { return this._discrepancyResponse; }
            set { if (value != null) this._discrepancyResponse = value; else this._discrepancyResponse = new Response(); }
        }

        // (Zorunlu değil!) Sipariş bilgilerinin gösterilmesinde ve sipariş belgesinin faturaya eklenmesinde bu eleman kullanılabilecektir.
        private OrderReference _orderReference;
        [XmlElement("OrderReference", Type = typeof(OrderReference), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual OrderReference OrderReference
        {
            get { return this._orderReference; }
            set { if (value != null) this._orderReference = value; else this._orderReference = new OrderReference(); }
        }

        //Faturanın ilişkili olduğu diğer belgelerin bilgileri bu elemana yazılacaktır.
        private List<BillingReference> _billingReferences;
        [XmlElement("BillingReference", Type = typeof(BillingReference), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<BillingReference> BillingReferences
        {
            get { return this._billingReferences; }
            set { if (value != null) this._billingReferences = value; else this._billingReferences = new List<BillingReference>(); }
        }

        // (Zorunlu değil!) İrsaliye bilgileri için bu eleman kullanılabilecektir. Birden fazla irsaliyeye ait bilgilerin girilmesi ve irsaliye belgesinin faturaya
        // eklenmesinde bu eleman kullanılabilecektir.
        private List<DocumentReference> _despatchDocumentReferences;
        [XmlElement("DespatchDocumentReference", Type = typeof(DocumentReference), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<DocumentReference> DespatchDocumentReferences
        {
            get { return this._despatchDocumentReferences; }
            set { if (value != null) this._despatchDocumentReferences = value; else this._despatchDocumentReferences = new List<DocumentReference>(); }
        }

        // (Zorunlu değil!) Alındı bilgilerinin gösterilmesinde ve alındı belgesinin faturaya eklenmesinde bu eleman kullanılabilecektir.
        private List<DocumentReference> _receiptDocumentReferences;
        [XmlElement("ReceiptDocumentReference", Type = typeof(DocumentReference), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<DocumentReference> ReceiptDocumentReferences
        {
            get { return this._receiptDocumentReferences; }
            set { if (value != null) this._receiptDocumentReferences = value; else this._receiptDocumentReferences = new List<DocumentReference>(); }
        }

        //Fatura ile ilgili kontrat dokümanın bilgilerinin gösterilmesinde bu eleman kullanılabilecektir.
        private List<DocumentReference> _contractDocumentReferences;
        [XmlElement("ContractDocumentReference", Type = typeof(DocumentReference), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<DocumentReference> ContractDocumentReferences
        {
            get { return this._contractDocumentReferences; }
            set { if (value != null) this._contractDocumentReferences = value; else this._contractDocumentReferences = new List<DocumentReference>(); }
        }

        // (Zorunlu değil!) İrsaliye, sipariş ve alındı belgeleri dışında faturaya eklenmek istenen diğer belgeler için bu eleman kullanılabilecektir.
        private List<DocumentReference> _additionalDocumentReferences;
        [XmlElement("AdditionalDocumentReference", Type = typeof(DocumentReference), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<DocumentReference> AdditionalDocumentReferences
        {
            get { return this._additionalDocumentReferences; }
            set { if (value != null) this._additionalDocumentReferences = value; else this._additionalDocumentReferences = new List<DocumentReference>(); }
        }

        /// Yeni Eklendi
        private List<DocumentReference> _statementDocumentReference;
        [XmlElement("StatementDocumentReference", Type = typeof(DocumentReference), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<DocumentReference> StatementDocumentReferences
        {
            get { return this._statementDocumentReference; }
            set { if (value != null) this._statementDocumentReference = value; else this._statementDocumentReference = new List<DocumentReference>(); }
        }

        //Faturanın düzenlenmesine referans teşkil eden ilgili belgelere ait bilgiler bu elemana yazılacaktır.
        private List<DocumentReference> _originatorDocumentReferences;
        [XmlElement("OriginatorDocumentReference", Type = typeof(DocumentReference), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<DocumentReference> OriginatorDocumentReferences
        {
            get { return this._originatorDocumentReferences; }
            set { if (value != null) this._originatorDocumentReferences = value; else this._originatorDocumentReferences = new List<DocumentReference>(); }
        }

        // Bu elemanda faturada kullanılan mali mühür ve/veya elektronik imza ile sertifikalara ilişkin bilgilere yer verilecektir.
        private List<CustomSignature> _signatures;
        [XmlElement("Signature", Type = typeof(CustomSignature), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<CustomSignature> Signatures
        {
            get { return this._signatures; }
            set { if (value != null) this._signatures = value; else this._signatures = new List<CustomSignature>(); }
        }

        // Bu elemanda faturayı düzenleyen tarafın bilgileri yer alacaktır.
        private SupplierParty _accountingSupplierParty;
        [XmlElement("AccountingSupplierParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual SupplierParty AccountingSupplierParty
        {
            get { return this._accountingSupplierParty; }
            set { if (value != null) this._accountingSupplierParty = value; else this._accountingSupplierParty = new SupplierParty(); }
        }

        // Bu elemanda faturayı alan tarafın bilgileri yer alacaktır.
        private CustomerParty _accountingCustomerParty;
        [XmlElement("AccountingCustomerParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual CustomerParty AccountingCustomerParty
        {
            get { return this._accountingCustomerParty; }
            set { if (value != null) this._accountingCustomerParty = value; else this._accountingCustomerParty = new CustomerParty(); }
        }

        /// Yeni Eklendi
        private Party _payeeParty;
        [XmlElement("PayeeParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Party PayeeParty
        {
            get { return this._payeeParty; }
            set { if (value != null) this._payeeParty = value; else this._payeeParty = new Party(); }
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

        //Eğer faturadaki vergi işlerinden sorumlu olan taraf bilgileri eklenmek istiyorsa bu eleman kullanılacaktır.
        private Party _taxRepresentativeParty;
        [XmlElement("TaxRepresentativeParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Party TaxRepresentativeParty
        {
            get { return this._taxRepresentativeParty; }
            set { if (value != null) this._taxRepresentativeParty = value; else this._taxRepresentativeParty = new Party(); }
        }

        //Bu elemana gönderim, taşıma ve sevkiyat ile ilgili bilgiler yazılabilecektir.
        private List<Delivery> _Deliverys;
        [XmlElement("Delivery", Type = typeof(Delivery), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<Delivery> Deliverys
        {
            get { return this._Deliverys; }
            set { if (value != null) this._Deliverys = value; else this._Deliverys = new List<Delivery>(); }
        }

        /// Yeni Eklendi  ????
        private List<DeliveryTerms> _deliveryTerms;
        [XmlElement("DeliveryTerms", Type = typeof(DeliveryTerms), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<DeliveryTerms> DeliveryTerms
        {
            get { return this._deliveryTerms; }
            set { if (value != null) this._deliveryTerms = value; else this._deliveryTerms = new List<DeliveryTerms>(); }
        }

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

        // (Zorunlu değil!) Doküman üzerinde gösterilen vergiler, “Belge Para Birimi” dışında başka bir para birimi ile gösterilmişse ilgili döviz kuru bu elemana yazılacaktır.
        private ExchangeRate _taxExchangeRate;
        [XmlElement("TaxExchangeRate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual ExchangeRate TaxExchangeRate
        {
            get { return this._taxExchangeRate; }
            set { if (value != null) this._taxExchangeRate = value; else this._taxExchangeRate = new ExchangeRate(); }
        }

        // (Zorunlu değil!) Bu elemana fatura üzerinde yer alan mal veya hizmet bedellerinin “Belge Para Birimi” dışında başka bir para birimi
        // ile gösterilmesi halinde ilgili döviz kuru yazılacaktır.
        private ExchangeRate _pricingExchangeRate;
        [XmlElement("PricingExchangeRate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual ExchangeRate PricingExchangeRate
        {
            get { return this._pricingExchangeRate; }
            set { if (value != null) this._pricingExchangeRate = value; else this._pricingExchangeRate = new ExchangeRate(); }
        }

        // (Zorunlu değil!) Ödemenin “Belge Para Birimi” dışında herhangi bir para birimi ile yapılması halinde ilgili para biriminin döviz kuru bilgileri bu elemana yazılacaktır.
        private ExchangeRate _paymentExchangeRate;
        [XmlElement("PaymentExchangeRate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual ExchangeRate PaymentExchangeRate
        {
            get { return this._paymentExchangeRate; }
            set { if (value != null) this._paymentExchangeRate = value; else this._paymentExchangeRate = new ExchangeRate(); }
        }

        // (Zorunlu değil!) Bu elemana belge para birimi ve ödeme para birimi haricinde faturanın ödenmesinde kullanılması istenen alternatif para birimi varsa
        // bu paranın döviz kuru bilgisi yazılacaktır.
        private ExchangeRate _paymentAlternativeExchangeRate;
        [XmlElement("PaymentAlternativeExchangeRate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual ExchangeRate PaymentAlternativeExchangeRate
        {
            get { return this._paymentAlternativeExchangeRate; }
            set { if (value != null) this._paymentAlternativeExchangeRate = value; else this._paymentAlternativeExchangeRate = new ExchangeRate(); }
        }

        // (Zorunlu değil!) Bu eleman fatura bütünü üzerinden iskonto veya artırım yapılması halinde kullanılacaktır.
        private List<AllowanceCharge> _allowanceCharges;
        [XmlElement("AllowanceCharge", Type = typeof(AllowanceCharge), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<AllowanceCharge> AllowanceCharges
        {
            get { return this._allowanceCharges; }
            set { if (value != null) this._allowanceCharges = value; else this._allowanceCharges = new List<AllowanceCharge>(); }
        }

        // Bu elemana faturada yer alan vergi ve diğer yasal yükümlülükler ile ilgili bilgiler yazılacaktır.
        private List<TaxTotal> _taxTotals;
        [XmlElement("TaxTotal", Type = typeof(TaxTotal), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<TaxTotal> TaxTotals
        {
            get { return this._taxTotals; }
            set { if (value != null) this._taxTotals = value; else this._taxTotals = new List<TaxTotal>(); }
        }

        // Bu elemanda faturadaki çeşitli tutarların toplamları yer alacaktır.
        private MonetaryTotal _legalMonetaryTotal;
        [XmlElement("LegalMonetaryTotal", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual MonetaryTotal LegalMonetaryTotal
        {
            get { return this._legalMonetaryTotal; }
            set { if (value != null) this._legalMonetaryTotal = value; else this._legalMonetaryTotal = new MonetaryTotal(); }
        }

        /// Bu elemanda faturada yer alan mal ve/veya hizmetlere ait bilgiler yer alacaktır.
        private List<CreditNoteLine> _creditNoteLines;
        [XmlElement("CreditNoteLine", Type = typeof(CreditNoteLine), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<CreditNoteLine> CreditNoteLines
        {
            get { return this._creditNoteLines; }
            set { if (value != null) this._creditNoteLines = value; else this._creditNoteLines = new List<CreditNoteLine>(); }
        }
    }

    //CreditNoteLine: İçine ID alanı açılıp kapatılarak boş geçilecektir. Zorunlu
    [XmlType("CreditNoteLine", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
    public class CreditNoteLine
    {
        [XmlElement("ID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string ID { get; set; }

    }

}
