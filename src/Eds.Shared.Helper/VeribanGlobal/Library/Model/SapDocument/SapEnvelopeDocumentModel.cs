using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.SapDocument
{
    [XmlRoot(ElementName = "abap", Namespace = "http://www.sap.com/abapxml")]
    public class SapEnvelopeDocumentModel
    {
        public SapEnvelopeDocumentModel()
        {
            xmlns.Add("abap", "http://www.sap.com/abapxml");
        }

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces xmlns { get; set; } = new XmlSerializerNamespaces();

        private Values _values;
        [XmlElement("values", Namespace = "http://www.sap.com/abapxml")]
        public virtual Values Values
        {
            get { return this._values; }
            set { if (value != null) this._values = value; else this._values = new Values(); }
        }
    }

    [XmlType("values")]
    [XmlRoot(ElementName = "values", Namespace = "http://www.sap.com/abapxml")]
    public class Values
    {
        private StrData _str_data;
        [XmlElement("STR_DATA", Namespace = "")]
        public virtual StrData STR_DATA
        {
            get { return this._str_data; }
            set { if (value != null) this._str_data = value; else this._str_data = new StrData(); }
        }
    }

    [XmlType("STR_DATA")]
    [XmlRoot(ElementName = "STR_DATA")]
    public class StrData
    {
        private Sbdh _SBDH;
        [XmlElement("SBDH")]
        public virtual Sbdh SBDH
        {
            get { return this._SBDH; }
            set { if (value != null) this._SBDH = value; else this._SBDH = new Sbdh(); }
        }

        private Invoice _INVOICE;
        [XmlElement("INVOICE")]
        public virtual Invoice INVOICE
        {
            get { return this._INVOICE; }
            set { if (value != null) this._INVOICE = value; else this._INVOICE = new Invoice(); }
        }

        private DespatchAdvice _DESPATCHADVICE;
        [XmlElement("DESPATCHADVICE")]
        public virtual DespatchAdvice DESPATCHADVICE
        {
            get { return this._DESPATCHADVICE; }
            set { if (value != null) this._DESPATCHADVICE = value; else this._DESPATCHADVICE = new DespatchAdvice(); }
        }
    }

    [XmlType("SBDH")]
    [XmlRoot(ElementName = "SBDH")]
    public class Sbdh
    {
        private string _HEADER_VERSION;
        [XmlElement("HEADER_VERSION")]
        public virtual string HEADER_VERSION
        {
            get { return this._HEADER_VERSION; }
            set { if (value != null) this._HEADER_VERSION = value; }
        }

        private Sender _SENDER;
        [XmlElement("SENDER")]
        public virtual Sender SENDER
        {
            get { return this._SENDER; }
            set { if (value != null) this._SENDER = value; else this._SENDER = new Sender(); }
        }

        private Receiver _RECEIVER;
        [XmlElement("RECEIVER")]
        public virtual Receiver RECEIVER
        {
            get { return this._RECEIVER; }
            set { if (value != null) this._RECEIVER = value; else this._RECEIVER = new Receiver(); }
        }

        private DocIdfication _DOC_IDFICATION;
        [XmlElement("DOC_IDFICATION")]
        public virtual DocIdfication DOC_IDFICATION
        {
            get { return this._DOC_IDFICATION; }
            set { if (value != null) this._DOC_IDFICATION = value; else this._DOC_IDFICATION = new DocIdfication(); }
        }
    }

    [XmlType("INVOICE")]
    [XmlRoot(ElementName = "INVOICE")]
    public class Invoice
    {
        private string _UBLVERS_ID;
        [XmlElement("UBLVERS_ID")]
        public virtual string UBLVERS_ID
        {
            get { return this._UBLVERS_ID; }
            set { if (value != null) this._UBLVERS_ID = value; }
        }

        private string _CUSTOM_ID;
        [XmlElement("CUSTOM_ID")]
        public virtual string CUSTOM_ID
        {
            get { return this._CUSTOM_ID; }
            set { if (value != null) this._CUSTOM_ID = value; }
        }

        private string _PROFILE_ID;
        [XmlElement("PROFILE_ID")]
        public virtual string PROFILE_ID
        {
            get { return this._PROFILE_ID; }
            set { if (value != null) this._PROFILE_ID = value; }
        }

        private string _ID;
        [XmlElement("ID")]
        public virtual string ID
        {
            get { return this._ID; }
            set { if (value != null) this._ID = value; }
        }

        [XmlElement("COPY_INDICATOR")]
        public virtual bool COPY_INDICATOR { get; set; }

        private string _UUID;
        [XmlElement("UUID")]
        public virtual string UUID
        {
            get { return this._UUID; }
            set { if (value != null) this._UUID = Guid.Parse(value).ToString(); }
        }

        private string _ISSUE_DATE;
        [XmlElement("ISSUE_DATE")]
        public virtual string ISSUE_DATE
        {
            get { return this._ISSUE_DATE; }
            set { if (value != null) this._ISSUE_DATE = value; }
        }

        private string _INV_TYP_CODE;
        [XmlElement("INV_TYP_CODE")]
        public virtual string INV_TYP_CODE
        {
            get { return this._INV_TYP_CODE; }
            set { if (value != null) this._INV_TYP_CODE = value; }
        }

        private Note _NOTE;
        [XmlElement("NOTE")]
        public virtual Note NOTE
        {
            get { return this._NOTE; }
            set { if (value != null) this._NOTE = value; }
        }

        private string _DOCU_CURR_CODE;
        [XmlElement("DOCU_CURR_CODE")]
        public virtual string DOCU_CURR_CODE
        {
            get { return this._DOCU_CURR_CODE; }
            set { if (value != null) this._DOCU_CURR_CODE = value; }
        }

        [XmlElement("LINE_CNT_NUMC")]
        public virtual int LINE_CNT_NUMC { get; set; }

        private OrderReference _ORDER_REFERENCE;
        [XmlElement("ORDER_REFERENCE")]
        public virtual OrderReference ORDER_REFERENCE
        {
            get { return this._ORDER_REFERENCE; }
            set { if (value != null) this._ORDER_REFERENCE = value; }
        }

        private Dispatch _DISPATCH;
        [XmlElement("DISPATCH")]
        public virtual Dispatch DISPATCH
        {
            get { return this._DISPATCH; }
            set { if (value != null) this._DISPATCH = value; }
        }

        private Asp _ASP;
        [XmlElement("ASP")]
        public virtual Asp ASP
        {
            get { return this._ASP; }
            set { if (value != null) this._ASP = value; }
        }

        private Acp _ACP;
        [XmlElement("ACP")]
        public virtual Acp ACP
        {
            get { return this._ACP; }
            set { if (value != null) this._ACP = value; }
        }

        private Bcp _BCP;
        [XmlElement("BCP")]
        public virtual Bcp BCP
        {
            get { return this._BCP; }
            set { if (value != null) this._BCP = value; }
        }

        private AlwncChrg _ALWNC_CHRG;
        [XmlElement("ALWNC_CHRG")]
        public virtual AlwncChrg ALWNC_CHRG
        {
            get { return this._ALWNC_CHRG; }
            set { if (value != null) this._ALWNC_CHRG = value; }
        }

        private PricExchngrate _PRIC_EXCHNGRATE;
        [XmlElement("PRIC_EXCHNGRATE")]
        public virtual PricExchngrate PRIC_EXCHNGRATE
        {
            get { return this._PRIC_EXCHNGRATE; }
            set { if (value != null) this._PRIC_EXCHNGRATE = value; }
        }

        private List<TaxTotal> _TAX_TOTAL;
        [XmlElement("TAX_TOTAL")]
        public virtual List<TaxTotal> TAX_TOTAL
        {
            get { return this._TAX_TOTAL; }
            set { if (value != null) this._TAX_TOTAL = value; }
        }


        private Lmt _LMT;
        [XmlElement("LMT")]
        public virtual Lmt LMT
        {
            get { return this._LMT; }
            set { if (value != null) this._LMT = value; }
        }

        private InvoiceLine _INVOICELINE;
        [XmlElement("INVOICE_LINE")]
        public virtual InvoiceLine INVOICE_LINE
        {
            get { return this._INVOICELINE; }
            set { if (value != null) this._INVOICELINE = value; }
        }


    }

    #region Despatch

    [XmlType("DESPATCHADVICE")]
    [XmlRoot(ElementName = "DESPATCHADVICE")]
    public class DespatchAdvice
    {
        private string _UBLVERS_ID;
        [XmlElement("UBLVERS_ID")]
        public virtual string UBLVERS_ID
        {
            get { return this._UBLVERS_ID; }
            set { if (value != null) this._UBLVERS_ID = value; }
        }

        private string _CUSTOM_ID;
        [XmlElement("CUSTOM_ID")]
        public virtual string CUSTOM_ID
        {
            get { return this._CUSTOM_ID; }
            set { if (value != null) this._CUSTOM_ID = value; }
        }

        private string _PROFILE_ID;
        [XmlElement("PROFILE_ID")]
        public virtual string PROFILE_ID
        {
            get { return this._PROFILE_ID; }
            set { if (value != null) this._PROFILE_ID = value; }
        }

        private string _ID;
        [XmlElement("ID")]
        public virtual string ID
        {
            get { return this._ID; }
            set { if (value != null) this._ID = value; }
        }

        [XmlElement("COPY_INDICATOR")]
        public virtual bool COPY_INDICATOR { get; set; }

        private string _UUID;
        [XmlElement("UUID")]
        public virtual string UUID
        {
            get { return this._UUID; }
            set { if (value != null) this._UUID = Guid.Parse(value).ToString(); }
        }

        private string _ISSUE_DATE;
        [XmlElement("ISSUE_DATE")]
        public virtual string ISSUE_DATE
        {
            get { return this._ISSUE_DATE; }
            set { if (value != null) this._ISSUE_DATE = value; }
        }

        private string _ISSUE_TIME;
        [XmlElement("ISSUE_TIME")]
        public virtual string ISSUE_TIME
        {
            get { return this._ISSUE_TIME; }
            set { if (value != null) this._ISSUE_TIME = value; }
        }

        private string _DEL_TYP_CODE;
        [XmlElement("DEL_TYP_CODE")]
        public virtual string DEL_TYP_CODE
        {
            get { return this._DEL_TYP_CODE; }
            set { if (value != null) this._DEL_TYP_CODE = value; }
        }

        private Note _NOTE;
        [XmlElement("NOTE")]
        public virtual Note NOTE
        {
            get { return this._NOTE; }
            set { if (value != null) this._NOTE = value; }
        }

        private OrderReference _ORDER_REFERENCE;
        [XmlElement("ORDER_REFERENCE")]
        public virtual OrderReference ORDER_REFERENCE
        {
            get { return this._ORDER_REFERENCE; }
            set { if (value != null) this._ORDER_REFERENCE = value; }
        }

        private Asp _ASP;
        [XmlElement("ASP")]
        public virtual Asp ASP
        {
            get { return this._ASP; }
            set { if (value != null) this._ASP = value; }
        }

        private Acp _ACP;
        [XmlElement("ACP")]
        public virtual Acp ACP
        {
            get { return this._ACP; }
            set { if (value != null) this._ACP = value; }
        }


        private DeliveryLine _DELIVERY_LINE;
        [XmlElement("DELIVERY_LINE")]
        public virtual DeliveryLine DELIVERY_LINE
        {
            get { return this._DELIVERY_LINE; }
            set { if (value != null) this._DELIVERY_LINE = value; }
        }
    }

    [XmlType("DELIVERY_LINE")]
    [XmlRoot(ElementName = "DELIVERY_LINE")]
    public class DeliveryLine
    {
        private List<ZpkDespatchLine> _ZPK_DESPATCHLINE;
        [XmlElement("ZPK_DESPATCHLINE")]
        public virtual List<ZpkDespatchLine> ZPK_DESPATCHLINE
        {
            get { return this._ZPK_DESPATCHLINE; }
            set { if (value != null) this._ZPK_DESPATCHLINE = value; }
        }
    }

    [XmlType("ZPK_DESPATCHLINE")]
    [XmlRoot(ElementName = "ZPK_DESPATCHLINE")]
    public class ZpkDespatchLine
    {
        private string _ID;
        [XmlElement("ID")]
        public virtual string ID
        {
            get { return this._ID; }
            set { if (value != null) this._ID = value; }
        }

        private DeliveredQuantity _DELIVEREDQUANTITY;
        [XmlElement("DELIVEREDQUANTITY")]
        public virtual DeliveredQuantity DELIVEREDQUANTITY
        {
            get { return this._DELIVEREDQUANTITY; }
            set { if (value != null) this._DELIVEREDQUANTITY = value; }
        }

        private OrderLineReference _ORDERLINEREFERENCE;
        [XmlElement("ORDERLINEREFERENCE")]
        public virtual OrderLineReference ORDERLINEREFERENCE
        {
            get { return this._ORDERLINEREFERENCE; }
            set { if (value != null) this._ORDERLINEREFERENCE = value; }
        }

        private Item _ITEM;
        [XmlElement("ITEM")]
        public virtual Item ITEM
        {
            get { return this._ITEM; }
            set { if (value != null) this._ITEM = value; }
        }

        private Shipment _SHIPMENT;
        [XmlElement("SHIPMENT")]
        public virtual Shipment SHIPMENT
        {
            get { return this._SHIPMENT; }
            set { if (value != null) this._SHIPMENT = value; }
        }
    }

    [XmlType("SELLERS_ITEM_IDENTIFICATION")]
    [XmlRoot(ElementName = "SELLERS_ITEM_IDENTIFICATION")]
    public class SellersItemIdentification
    {
        [XmlElement("ID")]
        public virtual string ID { get; set; }
    }

    [XmlType("ORDERLINEREFERENCE")]
    [XmlRoot(ElementName = "ORDERLINEREFERENCE")]
    public class OrderLineReference
    {
        [XmlElement("LINEID")]
        public virtual string LINEID { get; set; }
    }

    [XmlType("DELIVEREDQUANTITY")]
    [XmlRoot(ElementName = "DELIVEREDQUANTITY")]
    public class DeliveredQuantity
    {
        [XmlElement("INV_QTY")]
        public virtual decimal INV_QTY { get; set; }

        private string _QTY_UNIT;
        [XmlElement("QTY_UNIT")]
        public virtual string QTY_UNIT
        {
            get { return this._QTY_UNIT; }
            set { if (value != null) this._QTY_UNIT = value; }
        }
    }

    [XmlType("ITEMINSTANCE")]
    [XmlRoot(ElementName = "ITEMINSTANCE")]
    public class ItemInstance
    {
        [XmlElement("ZPK_ITEMINSTANCE")]
        public virtual ZpkItemInstance ZPK_ITEMINSTANCE { get; set; }
    }

    [XmlType("ZPK_ITEMINSTANCE")]
    [XmlRoot(ElementName = "ZPK_ITEMINSTANCE")]
    public class ZpkItemInstance
    {
        private string _LOTIDENTIFICATION;
        [XmlElement("LOTIDENTIFICATION")]
        public virtual string LOTIDENTIFICATION
        {
            get { return this._LOTIDENTIFICATION; }
            set { if (value != null) this._LOTIDENTIFICATION = value; }
        }
    }

    #endregion

    [XmlType("NOTE")]
    [XmlRoot(ElementName = "NOTE")]
    public class Note
    {
        private List<string> _ITEM;
        [XmlElement("item")]
        public virtual List<string> ITEM
        {
            get { return this._ITEM; }
            set { if (value != null) this._ITEM = value; }
        }
    }

    [XmlType("DISPATCH")]
    [XmlRoot(ElementName = "DISPATCH")]
    public class Dispatch
    {
        private string _ID;
        [XmlElement("ID")]
        public virtual string ID
        {
            get { return this._ID; }
            set { if (value != null) this._ID = value; }
        }

        private string _DATE;
        [XmlElement("DATE")]
        public virtual string DATE
        {
            get { return this._DATE; }
            set { if (value != null) this._DATE = value; }
        }
    }

    [XmlType("ALWNC_CHRG")]
    [XmlRoot(ElementName = "ALWNC_CHRG")]
    public class AlwncChrg
    {
        private string _CHRG_INDICATOR;
        [XmlElement("CHRG_INDICATOR")]
        public virtual string CHRG_INDICATOR
        {
            get { return this._CHRG_INDICATOR; }
            set { if (value != null) this._CHRG_INDICATOR = value; }
        }

        private string _ALWNCHRG_REASON;
        [XmlElement("ALWNCHRG_REASON")]
        public virtual string ALWNCHRG_REASON
        {
            get { return this._ALWNCHRG_REASON; }
            set { if (value != null) this._ALWNCHRG_REASON = value; }
        }

        [XmlElement("MULTIPL_FACT_NUM")]
        public virtual decimal MULTIPL_FACT_NUM { get; set; }

        private BaseAmnt _AMNT;
        [XmlElement("AMNT")]
        public virtual BaseAmnt AMNT
        {
            get { return this._AMNT; }
            set { if (value != null) this._AMNT = value; }
        }

        private BaseAmnt _BASE_AMNT;
        [XmlElement("BASE_AMNT")]
        public virtual BaseAmnt BASE_AMNT
        {
            get { return this._BASE_AMNT; }
            set { if (value != null) this._BASE_AMNT = value; }
        }
    }

    [XmlType("PRIC_EXCHNGRATE")]
    [XmlRoot(ElementName = "PRIC_EXCHNGRATE")]
    public class PricExchngrate
    {
        private string _SRCE_CURRCODE;
        [XmlElement("SRCE_CURRCODE")]
        public virtual string SRCE_CURRCODE
        {
            get { return this._SRCE_CURRCODE; }
            set { if (value != null) this._SRCE_CURRCODE = value; }
        }

        private string _TRGT_CURRCODE;
        [XmlElement("TRGT_CURRCODE")]
        public virtual string TRGT_CURRCODE
        {
            get { return this._TRGT_CURRCODE; }
            set { if (value != null) this._TRGT_CURRCODE = value; }
        }

        [XmlElement("EXCHANGE_RATE")]
        public virtual decimal EXCHANGE_RATE { get; set; }
    }

    [XmlType("TAX_TOTAL")]
    [XmlRoot(ElementName = "TAX_TOTAL")]
    public class TaxTotal
    {
        private BaseAmnt _TAX_AMNT;
        [XmlElement("TAX_AMNT")]
        public virtual BaseAmnt TAX_AMNT
        {
            get { return this._TAX_AMNT; }
            set { if (value != null) this._TAX_AMNT = value; }
        }

        private List<TaxSubtot> _TAX_SUBTOT;
        [XmlElement("TAX_SUBTOT")]
        public virtual List<TaxSubtot> TAX_SUBTOT
        {
            get { return this._TAX_SUBTOT; }
            set { if (value != null) this._TAX_SUBTOT = value; }
        }
    }

    [XmlType("TAX_SUBTOT")]
    [XmlRoot(ElementName = "TAX_SUBTOT")]
    public class TaxSubtot
    {
        private EinvTxsubtotTr _EINV_TXSUBTOT_TR;
        [XmlElement("EINV_TXSUBTOT_TR")]
        public virtual EinvTxsubtotTr EINV_TXSUBTOT_TR
        {
            get { return this._EINV_TXSUBTOT_TR; }
            set { if (value != null) this._EINV_TXSUBTOT_TR = value; }
        }
    }

    [XmlType("EINV_TXSUBTOT_TR")]
    [XmlRoot(ElementName = "EINV_TXSUBTOT_TR")]
    public class EinvTxsubtotTr
    {
        private BaseAmnt _TAXABLE_AMNT;
        [XmlElement("TAXABLE_AMNT")]
        public virtual BaseAmnt TAXABLE_AMNT
        {
            get { return this._TAXABLE_AMNT; }
            set { if (value != null) this._TAXABLE_AMNT = value; }
        }

        private BaseAmnt _TAX_AMNT;
        [XmlElement("TAX_AMNT")]
        public virtual BaseAmnt TAX_AMNT
        {
            get { return this._TAX_AMNT; }
            set { if (value != null) this._TAX_AMNT = value; }
        }

        [XmlElement("PERCENT")]
        public virtual decimal PERCENT { get; set; }

        private TaxCategry _TAX_CATEGRY;
        [XmlElement("TAX_CATEGRY")]
        public virtual TaxCategry TAX_CATEGRY
        {
            get { return this._TAX_CATEGRY; }
            set { if (value != null) this._TAX_CATEGRY = value; }
        }


    }

    [XmlType("TAX_CATEGRY")]
    [XmlRoot(ElementName = "TAX_CATEGRY")]
    public class TaxCategry
    {
        private string _TAX_EX_REASON_CODE;
        [XmlElement("TAX_EX_REASON_CODE")]
        public virtual string TAX_EX_REASON_CODE
        {
            get { return this._TAX_EX_REASON_CODE; }
            set { if (value != null) this._TAX_EX_REASON_CODE = value; }
        }

        private string _TAX_EX_REASON;
        [XmlElement("TAX_EX_REASON")]
        public virtual string TAX_EX_REASON
        {
            get { return this._TAX_EX_REASON; }
            set { if (value != null) this._TAX_EX_REASON = value; }
        }

        private TaxSchm _TAX_SCHM;
        [XmlElement("TAX_SCHM")]
        public virtual TaxSchm TAX_SCHM
        {
            get { return this._TAX_SCHM; }
            set { if (value != null) this._TAX_SCHM = value; }
        }
    }

    [XmlType("TAX_SCHM")]
    [XmlRoot(ElementName = "TAX_SCHM")]
    public class TaxSchm
    {
        private string _TXSCHM_NAME;
        [XmlElement("TXSCHM_NAME")]
        public virtual string TXSCHM_NAME
        {
            get { return this._TXSCHM_NAME; }
            set { if (value != null) this._TXSCHM_NAME = value; }
        }

        private string _TXTYP_CODE;
        [XmlElement("TXTYP_CODE")]
        public virtual string TXTYP_CODE
        {
            get { return this._TXTYP_CODE; }
            set { if (value != null) this._TXTYP_CODE = value; }
        }
    }

    [XmlType("ORDER_REFERENCE")]
    [XmlRoot(ElementName = "ORDER_REFERENCE")]
    public class OrderReference
    {
        private string _ORDER_ID;
        [XmlElement("ORDER_ID")]
        public virtual string ORDER_ID
        {
            get { return this._ORDER_ID; }
            set { if (value != null) this._ORDER_ID = value; }
        }

        private string _SALES_ORDER_ID;
        [XmlElement("SALES_ORDER_ID")]
        public virtual string SALES_ORDER_ID
        {
            get { return this._SALES_ORDER_ID; }
            set { if (value != null) this._SALES_ORDER_ID = value; }
        }

        private string _ISSUE_DATE;
        [XmlElement("ISSUE_DATE")]
        public virtual string ISSUE_DATE
        {
            get { return this._ISSUE_DATE; }
            set { if (value != null) this._ISSUE_DATE = value; }
        }

        private string _ISSUEDATE;
        [XmlElement("ISSUEDATE")]
        public virtual string ISSUEDATE
        {
            get { return this._ISSUEDATE; }
            set { if (value != null) this._ISSUEDATE = value; }
        }

        private string _ID;
        [XmlElement("ID")]
        public virtual string ID
        {
            get { return this._ID; }
            set { if (value != null) this._ID = value; }
        }

        private string _ORDER_TYPE_CODE;
        [XmlElement("ORDER_TYPE_CODE")]
        public virtual string ORDER_TYPE_CODE
        {
            get { return this._ORDER_TYPE_CODE; }
            set { if (value != null) this._ORDER_TYPE_CODE = value; }
        }
    }

    [XmlType("INVOICE_LINE")]
    [XmlRoot(ElementName = "INVOICE_LINE")]
    public class InvoiceLine
    {
        private List<EinvInvLineTr> _EINV_INV_LINE_TR;
        [XmlElement("EINV_INV_LINE_TR")]
        public virtual List<EinvInvLineTr> EINV_INV_LINE_TR
        {
            get { return this._EINV_INV_LINE_TR; }
            set { if (value != null) this._EINV_INV_LINE_TR = value; }
        }
    }

    [XmlType("EINV_INV_LINE_TR")]
    [XmlRoot(ElementName = "EINV_INV_LINE_TR")]
    public class EinvInvLineTr
    {
        private string _ID;
        [XmlElement("ID")]
        public virtual string ID
        {
            get { return this._ID; }
            set { if (value != null) this._ID = value; }
        }

        private string _NOTE;
        [XmlElement("NOTE")]
        public virtual string NOTE
        {
            get { return this._NOTE; }
            set { if (value != null) this._NOTE = value; }
        }

        private InvoicedQty _INVOICED_QTY;
        [XmlElement("INVOICED_QTY")]
        public virtual InvoicedQty INVOICED_QTY
        {
            get { return this._INVOICED_QTY; }
            set { if (value != null) this._INVOICED_QTY = value; }
        }

        private BaseAmnt _LINE_EXTN_AMNT;
        [XmlElement("LINE_EXTN_AMNT")]
        public virtual BaseAmnt LINE_EXTN_AMNT
        {
            get { return this._LINE_EXTN_AMNT; }
            set { if (value != null) this._LINE_EXTN_AMNT = value; }
        }

        private AlwncChrg _ALWNC_CHRG;
        [XmlElement("ALWNC_CHRG")]
        public virtual AlwncChrg ALWNC_CHRG
        {
            get { return this._ALWNC_CHRG; }
            set { if (value != null) this._ALWNC_CHRG = value; }
        }

        private TaxTotal _TAX_TOTAL;
        [XmlElement("TAX_TOTAL")]
        public virtual TaxTotal TAX_TOTAL
        {
            get { return this._TAX_TOTAL; }
            set { if (value != null) this._TAX_TOTAL = value; }
        }

        private Item _ITEM;
        [XmlElement("ITEM")]
        public virtual Item ITEM
        {
            get { return this._ITEM; }
            set { if (value != null) this._ITEM = value; }
        }

        private Price _PRICE;
        [XmlElement("PRICE")]
        public virtual Price PRICE
        {
            get { return this._PRICE; }
            set { if (value != null) this._PRICE = value; }
        }

        private Delivery _DELIVERY;
        [XmlElement("DELIVERY")]
        public virtual Delivery DELIVERY
        {
            get { return this._DELIVERY; }
            set { if (value != null) this._DELIVERY = value; }
        }
    }

    [XmlType("PRICE")]
    [XmlRoot(ElementName = "PRICE")]
    public class Price
    {
        private BaseAmnt _PRICE_AMNT;
        [XmlElement("PRICE_AMNT")]
        public virtual BaseAmnt PRICE_AMNT
        {
            get { return this._PRICE_AMNT; }
            set { if (value != null) this._PRICE_AMNT = value; }
        }
    }

    [XmlType("ITEM")]
    [XmlRoot(ElementName = "ITEM")]
    public class Item
    {
        private string _DESCRIPTION;
        [XmlElement("DESCRIPTION")]
        public virtual string DESCRIPTION
        {
            get { return this._DESCRIPTION; }
            set { if (value != null) this._DESCRIPTION = value; }
        }

        private string _NAME;
        [XmlElement("NAME")]
        public virtual string NAME
        {
            get { return this._NAME; }
            set { if (value != null) this._NAME = value; }
        }

        private string _BRAND_NAME;
        [XmlElement("BRAND_NAME")]
        public virtual string BRAND_NAME
        {
            get { return this._BRAND_NAME; }
            set { if (value != null) this._BRAND_NAME = value; }
        }

        private string _MODELL_NAME;
        [XmlElement("MODELL_NAME")]
        public virtual string MODELL_NAME
        {
            get { return this._MODELL_NAME; }
            set { if (value != null) this._MODELL_NAME = value; }
        }

        private SellersItemIdentification _SELLERS_ITEM_IDENTIFICATION;
        [XmlElement("SELLERS_ITEM_IDENTIFICATION")]
        public virtual SellersItemIdentification SELLERS_ITEM_IDENTIFICATION
        {
            get { return this._SELLERS_ITEM_IDENTIFICATION; }
            set { if (value != null) this._SELLERS_ITEM_IDENTIFICATION = value; }
        }

        private List<ItemInstance> _ITEMINSTANCE;
        [XmlElement("ITEMINSTANCE")]
        public virtual List<ItemInstance> ITEMINSTANCE
        {
            get { return this._ITEMINSTANCE; }
            set { if (value != null) this._ITEMINSTANCE = value; }
        }
    }

    [XmlType("INVOICED_QTY")]
    [XmlRoot(ElementName = "INVOICED_QTY")]
    public class InvoicedQty
    {
        [XmlElement("INV_QTY")]
        public virtual decimal INV_QTY { get; set; }

        private string _QTY_UNIT;
        [XmlElement("QTY_UNIT")]
        public virtual string QTY_UNIT
        {
            get { return this._QTY_UNIT; }
            set { if (value != null) this._QTY_UNIT = value; }
        }
    }

    [XmlType("LMT")]
    [XmlRoot(ElementName = "LMT")]
    public class Lmt
    {
        private BaseAmnt _LINE_EXTN_AMNT;
        [XmlElement("LINE_EXTN_AMNT")]
        public virtual BaseAmnt LINE_EXTN_AMNT
        {
            get { return this._LINE_EXTN_AMNT; }
            set { if (value != null) this._LINE_EXTN_AMNT = value; }
        }

        private BaseAmnt _TX_EXCL_AMNT;
        [XmlElement("TX_EXCL_AMNT")]
        public virtual BaseAmnt TX_EXCL_AMNT
        {
            get { return this._TX_EXCL_AMNT; }
            set { if (value != null) this._TX_EXCL_AMNT = value; }
        }

        private BaseAmnt _TX_INCL_AMNT;
        [XmlElement("TX_INCL_AMNT")]
        public virtual BaseAmnt TX_INCL_AMNT
        {
            get { return this._TX_INCL_AMNT; }
            set { if (value != null) this._TX_INCL_AMNT = value; }
        }

        private BaseAmnt _ALWNC_TOT_AMNT;
        [XmlElement("ALWNC_TOT_AMNT")]
        public virtual BaseAmnt ALWNC_TOT_AMNT
        {
            get { return this._ALWNC_TOT_AMNT; }
            set { if (value != null) this._ALWNC_TOT_AMNT = value; }
        }

        private BaseAmnt _CHRG_TOT_AMNT;
        [XmlElement("CHRG_TOT_AMNT")]
        public virtual BaseAmnt CHRG_TOT_AMNT
        {
            get { return this._CHRG_TOT_AMNT; }
            set { if (value != null) this._CHRG_TOT_AMNT = value; }
        }

        private BaseAmnt _PAYABLE_ROUND_AMNT;
        [XmlElement("PAYABLE_ROUND_AMNT")]
        public virtual BaseAmnt PAYABLE_ROUND_AMNT
        {
            get { return this._PAYABLE_ROUND_AMNT; }
            set { if (value != null) this._PAYABLE_ROUND_AMNT = value; }
        }

        private BaseAmnt _PAYABLE_AMNT;
        [XmlElement("PAYABLE_AMNT")]
        public virtual BaseAmnt PAYABLE_AMNT
        {
            get { return this._PAYABLE_AMNT; }
            set { if (value != null) this._PAYABLE_AMNT = value; }
        }
    }

    [XmlType("BASE_AMNT")]
    [XmlRoot(ElementName = "BASE_AMNT")]
    public class BaseAmnt
    {
        [XmlElement("AMNT")]
        public virtual decimal AMNT { get; set; }

        private string _CURR_ID;
        [XmlElement("CURR_ID")]
        public virtual string CURR_ID
        {
            get { return this._CURR_ID; }
            set { if (value != null) this._CURR_ID = value; }
        }
    }

    [XmlType("ASP")]
    [XmlRoot(ElementName = "ASP")]
    public class Asp
    {
        private Party _PARTY;
        [XmlElement("PARTY")]
        public virtual Party PARTY
        {
            get { return this._PARTY; }
            set { if (value != null) this._PARTY = value; }
        }
    }

    [XmlType("ACP")]
    [XmlRoot(ElementName = "ACP")]
    public class Acp
    {
        private Party _PARTY;
        [XmlElement("PARTY")]
        public virtual Party PARTY
        {
            get { return this._PARTY; }
            set { if (value != null) this._PARTY = value; }
        }
    }

    [XmlType("BCP")]
    [XmlRoot(ElementName = "BCP")]
    public class Bcp
    {
        private Party _PARTY;
        [XmlElement("PARTY")]
        public virtual Party PARTY
        {
            get { return this._PARTY; }
            set { if (value != null) this._PARTY = value; }
        }

        private PartyLegalEntity _PARTYLEGALENTITY;
        [XmlElement("PARTYLEGALENTITY")]
        public virtual PartyLegalEntity PARTYLEGALENTITY
        {
            get { return this._PARTYLEGALENTITY; }
            set { if (value != null) this._PARTYLEGALENTITY = value; }
        }
    }

    [XmlType("PARTY")]
    [XmlRoot(ElementName = "PARTY")]
    public class Party
    {
        private string _WEB_URL;
        [XmlElement("WEB_URL")]
        public virtual string WEB_URL
        {
            get { return this._WEB_URL; }
            set { if (value != null) this._WEB_URL = value; }
        }

        private PartyIdfication _PARTY_IDFICATION;
        [XmlElement("PARTY_IDFICATION")]
        public virtual PartyIdfication PARTY_IDFICATION
        {
            get { return this._PARTY_IDFICATION; }
            set { if (value != null) this._PARTY_IDFICATION = value; }
        }

        private PartyName _PARTY_NAME;
        [XmlElement("PARTY_NAME")]
        public virtual PartyName PARTY_NAME
        {
            get { return this._PARTY_NAME; }
            set { if (value != null) this._PARTY_NAME = value; }
        }

        private PostAddr _POST_ADDR;
        [XmlElement("POST_ADDR")]
        public virtual PostAddr POST_ADDR
        {
            get { return this._POST_ADDR; }
            set { if (value != null) this._POST_ADDR = value; }
        }

        private PartyTxschm _PARTY_TXSCHM;
        [XmlElement("PARTY_TXSCHM")]
        public virtual PartyTxschm PARTY_TXSCHM
        {
            get { return this._PARTY_TXSCHM; }
            set { if (value != null) this._PARTY_TXSCHM = value; }
        }

        private Contact _CONTACT;
        [XmlElement("CONTACT")]
        public virtual Contact CONTACT
        {
            get { return this._CONTACT; }
            set { if (value != null) this._CONTACT = value; }
        }
    }

    [XmlType("POST_ADDR")]
    [XmlRoot(ElementName = "POST_ADDR")]
    public class PostAddr
    {
        private string _STREET_NAME;
        [XmlElement("STREET_NAME")]
        public virtual string STREET_NAME
        {
            get { return this._STREET_NAME; }
            set { if (value != null) this._STREET_NAME = value; }
        }

        private string _BUILDG_NAME;
        [XmlElement("BUILDG_NAME")]
        public virtual string BUILDG_NAME
        {
            get { return this._BUILDG_NAME; }
            set { if (value != null) this._BUILDG_NAME = value; }
        }

        private string _BUILDG_NO;
        [XmlElement("BUILDG_NO")]
        public virtual string BUILDG_NO
        {
            get { return this._BUILDG_NO; }
            set { if (value != null) this._BUILDG_NO = value; }
        }

        private string _CITY_SUBD_NAME;
        [XmlElement("CITY_SUBD_NAME")]
        public virtual string CITY_SUBD_NAME
        {
            get { return this._CITY_SUBD_NAME; }
            set { if (value != null) this._CITY_SUBD_NAME = value; }
        }

        private string _CITY_NAME;
        [XmlElement("CITY_NAME")]
        public virtual string CITY_NAME
        {
            get { return this._CITY_NAME; }
            set { if (value != null) this._CITY_NAME = value; }
        }

        private string _POST_ZONE;
        [XmlElement("POST_ZONE")]
        public virtual string POST_ZONE
        {
            get { return this._POST_ZONE; }
            set { if (value != null) this._POST_ZONE = value; }
        }

        private string _REGION;
        [XmlElement("REGION")]
        public virtual string REGION
        {
            get { return this._REGION; }
            set { if (value != null) this._REGION = value; }
        }

        private Country _COUNTRY;
        [XmlElement("COUNTRY")]
        public virtual Country COUNTRY
        {
            get { return this._COUNTRY; }
            set { if (value != null) this._COUNTRY = value; }
        }

    }

    [XmlType("PARTY_TXSCHM")]
    [XmlRoot(ElementName = "PARTY_TXSCHM")]
    public class PartyTxschm
    {
        private TxschmName _TXSCHM_NAME;
        [XmlElement("TXSCHM_NAME")]
        public virtual TxschmName TXSCHM_NAME
        {
            get { return this._TXSCHM_NAME; }
            set { if (value != null) this._TXSCHM_NAME = value; }
        }
    }

    [XmlType("TXSCHM_NAME")]
    [XmlRoot(ElementName = "TXSCHM_NAME")]
    public class TxschmName
    {
        private string _NAME;
        [XmlElement("NAME")]
        public virtual string NAME
        {
            get { return this._NAME; }
            set { if (value != null) this._NAME = value; }
        }
    }

    [XmlType("CONTACT")]
    [XmlRoot(ElementName = "CONTACT")]
    public class Contact
    {
        private string _TPHONE;
        [XmlElement("TPHONE")]
        public virtual string TPHONE
        {
            get { return this._TPHONE; }
            set { if (value != null) this._TPHONE = value; }
        }

        private string _TFAX;
        [XmlElement("TFAX")]
        public virtual string TFAX
        {
            get { return this._TFAX; }
            set { if (value != null) this._TFAX = value; }
        }

        private string _EMAIL;
        [XmlElement("EMAIL")]
        public virtual string EMAIL
        {
            get { return this._EMAIL; }
            set { if (value != null) this._EMAIL = value; }
        }
        private string _NOTE;
        [XmlElement("NOTE")]
        public virtual string NOTE
        {
            get { return this._NOTE; }
            set { if (value != null) this._NOTE = value; }
        }
    }

    [XmlType("COUNTRY")]
    [XmlRoot(ElementName = "COUNTRY")]
    public class Country
    {
        private string _COUNTRY_NAME;
        [XmlElement("COUNTRY_NAME")]
        public virtual string COUNTRY_NAME
        {
            get { return this._COUNTRY_NAME; }
            set { if (value != null) this._COUNTRY_NAME = value; }
        }
    }

    [XmlType("PARTY_IDFICATION")]
    [XmlRoot(ElementName = "PARTY_IDFICATION")]
    public class PartyIdfication
    {
        private string _ID;
        [XmlElement("ID")]
        public virtual string ID
        {
            get { return this._ID; }
            set { if (value != null) this._ID = value; }
        }

        private string _SCHMID;
        [XmlElement("SCHMID")]
        public virtual string SCHMID
        {
            get { return this._SCHMID; }
            set { if (value != null) this._SCHMID = value; }
        }

    }

    [XmlType("PARTY_NAME")]
    [XmlRoot(ElementName = "PARTY_NAME")]
    public class PartyName
    {
        private string _NAME;
        [XmlElement("NAME")]
        public virtual string NAME
        {
            get { return this._NAME; }
            set { if (value != null) this._NAME = value; }
        }
    }

    [XmlType("PARTYLEGALENTITY")]
    [XmlRoot(ElementName = "PARTYLEGALENTITY")]
    public class PartyLegalEntity
    {
        private string _REGISTRATIONNAME;
        [XmlElement("REGISTRATIONNAME")]
        public virtual string REGISTRATIONNAME
        {
            get { return this._REGISTRATIONNAME; }
            set { if (value != null) this._REGISTRATIONNAME = value; }
        }

        private string _COMPANYID;
        [XmlElement("COMPANYID")]
        public virtual string COMPANYID
        {
            get { return this._COMPANYID; }
            set { if (value != null) this._COMPANYID = value; }
        }
    }

    [XmlType("SENDER")]
    [XmlRoot(ElementName = "SENDER")]
    public class Sender
    {
        private string _IDENTIFIER;
        [XmlElement("IDENTIFIER")]
        public virtual string IDENTIFIER
        {
            get { return this._IDENTIFIER; }
            set { if (value != null) this._IDENTIFIER = value; }
        }

        private ContactInfo _CONTACT_INFO;
        [XmlElement("CONTACT_INFO")]
        public virtual ContactInfo CONTACT_INFO
        {
            get { return this._CONTACT_INFO; }
            set { if (value != null) this._CONTACT_INFO = value; }
        }
    }

    [XmlType("RECEIVER")]
    [XmlRoot(ElementName = "RECEIVER")]
    public class Receiver
    {
        private string _IDENTIFIER;
        [XmlElement("IDENTIFIER")]
        public virtual string IDENTIFIER
        {
            get { return this._IDENTIFIER; }
            set { if (value != null) this._IDENTIFIER = value; }
        }

        private ContactInfo _CONTACT_INFO;
        [XmlElement("CONTACT_INFO")]
        public virtual ContactInfo CONTACT_INFO
        {
            get { return this._CONTACT_INFO; }
            set { if (value != null) this._CONTACT_INFO = value; }
        }
    }

    [XmlType("DOC_IDFICATION")]
    [XmlRoot(ElementName = "DOC_IDFICATION")]
    public class DocIdfication
    {
        private string _STANDARD;
        [XmlElement("STANDARD")]
        public virtual string STANDARD
        {
            get { return this._STANDARD; }
            set { if (value != null) this._STANDARD = value; }
        }

        private string _TYP_VERSION;
        [XmlElement("TYP_VERSION")]
        public virtual string TYP_VERSION
        {
            get { return this._TYP_VERSION; }
            set { if (value != null) this._TYP_VERSION = value; }
        }

        private string _INSTANCE_IDFIER;
        [XmlElement("INSTANCE_IDFIER")]
        public virtual string INSTANCE_IDFIER
        {
            get { return this._INSTANCE_IDFIER; }
            set { if (value != null) this._INSTANCE_IDFIER = value; }
        }

        private string _ENV_TYPE;
        [XmlElement("ENV_TYPE")]
        public virtual string ENV_TYPE
        {
            get { return this._ENV_TYPE; }
            set { if (value != null) this._ENV_TYPE = value; }
        }

        private string _ENV_DATE;
        [XmlElement("ENV_DATE")]
        public virtual string ENV_DATE
        {
            get { return this._ENV_DATE; }
            set { if (value != null) this._ENV_DATE = value; }
        }

        private string _ENV_TIME;
        [XmlElement("ENV_TIME")]
        public virtual string ENV_TIME
        {
            get { return this._ENV_TIME; }
            set { if (value != null) this._ENV_TIME = value; }
        }
    }

    [XmlType("CONTACT_INFO")]
    [XmlRoot(ElementName = "CONTACT_INFO")]
    public class ContactInfo
    {

        private EinvContInfoTr _EINV_CONTINFO_TR;
        [XmlElement("EINV_CONTINFO_TR")]
        public virtual EinvContInfoTr EINV_CONTINFO_TR
        {
            get { return this._EINV_CONTINFO_TR; }
            set { if (value != null) this._EINV_CONTINFO_TR = value; }
        }
    }

    [XmlType("EINV_CONTINFO_TR")]
    [XmlRoot(ElementName = "EINV_CONTINFO_TR")]
    public class EinvContInfoTr
    {
        private string _CONTACT;
        [XmlElement("CONTACT")]
        public virtual string CONTACT
        {
            get { return this._CONTACT; }
            set { if (value != null) this._CONTACT = value; }
        }


        private string _CONTACT_TYP_ID;
        [XmlElement("CONTACT_TYP_ID")]
        public virtual string CONTACT_TYP_ID
        {
            get { return this._CONTACT_TYP_ID; }
            set { if (value != null) this._CONTACT_TYP_ID = value; }
        }
    }

    [XmlType("DELIVERY")]
    [XmlRoot(ElementName = "DELIVERY")]
    public class Delivery
    {
        private DeliveryAddress _DELIVERYADDRESS;
        [XmlElement("DELIVERYADDRESS")]
        public virtual DeliveryAddress DELIVERYADDRESS
        {
            get { return this._DELIVERYADDRESS; }
            set { if (value != null) this._DELIVERYADDRESS = value; }
        }

        private DeliveryTerms _DELIVERYTERMS;
        [XmlElement("DELIVERYTERMS")]
        public virtual DeliveryTerms DELIVERYTERMS
        {
            get { return this._DELIVERYTERMS; }
            set { if (value != null) this._DELIVERYTERMS = value; }
        }

        private Shipment _SHIPMENT;
        [XmlElement("SHIPMENT")]
        public virtual Shipment SHIPMENT
        {
            get { return this._SHIPMENT; }
            set { if (value != null) this._SHIPMENT = value; }
        }
    }

    [XmlType("DELIVERYADDRESS")]
    [XmlRoot(ElementName = "DELIVERYADDRESS")]
    public class DeliveryAddress
    {
        private Party _PARTY;
        [XmlElement("PARTY")]
        public virtual Party PARTY
        {
            get { return this._PARTY; }
            set { if (value != null) this._PARTY = value; }
        }
    }

    [XmlType("DELIVERYTERMS")]
    [XmlRoot(ElementName = "DELIVERYTERMS")]
    public class DeliveryTerms
    {
        private string _INCOTERMS;
        [XmlElement("INCOTERMS")]
        public virtual string INCOTERMS
        {
            get { return this._INCOTERMS; }
            set { if (value != null) this._INCOTERMS = value; }
        }
    }

    [XmlType("SHIPMENT")]
    [XmlRoot(ElementName = "SHIPMENT")]
    public class Shipment
    {
        private GoodsItem _GOODSITEM;
        [XmlElement("GOODSITEM")]
        public virtual GoodsItem GOODSITEM
        {
            get { return this._GOODSITEM; }
            set { if (value != null) this._GOODSITEM = value; }
        }

        private ShipmentStage _SHIPMENTSTAGE;
        [XmlElement("SHIPMENTSTAGE")]
        public virtual ShipmentStage SHIPMENTSTAGE
        {
            get { return this._SHIPMENTSTAGE; }
            set { if (value != null) this._SHIPMENTSTAGE = value; }
        }

        private TransportHandlingUnit _TRANSPORTHANDLINGUNIT;
        [XmlElement("TRANSPORTHANDLINGUNIT")]
        public virtual TransportHandlingUnit TRANSPORTHANDLINGUNIT
        {
            get { return this._TRANSPORTHANDLINGUNIT; }
            set { if (value != null) this._TRANSPORTHANDLINGUNIT = value; }
        }
    }

    [XmlType("GOODSITEM")]
    [XmlRoot(ElementName = "GOODSITEM")]
    public class GoodsItem
    {
        private string _REQUIREDCUSTOMSID;
        [XmlElement("REQUIREDCUSTOMSID")]
        public virtual string REQUIREDCUSTOMSID
        {
            get { return this._REQUIREDCUSTOMSID; }
            set { if (value != null) this._REQUIREDCUSTOMSID = value; }
        }

        [XmlElement("GROSSWEIGHTMEASURE")]
        public virtual decimal GROSSWEIGHTMEASURE { get; set; }

        [XmlElement("NETWEIGHTMEASURE")]
        public virtual decimal NETWEIGHTMEASURE { get; set; }

        private string _WEIGHTMEASURE_UNIT;
        [XmlElement("WEIGHTMEASURE_UNIT")]
        public virtual string WEIGHTMEASURE_UNIT
        {
            get { return this._WEIGHTMEASURE_UNIT; }
            set { if (value != null) this._WEIGHTMEASURE_UNIT = value; }
        }

        [XmlElement("INSURANCEVALUEAMOUNT")]
        public virtual decimal INSURANCEVALUEAMOUNT { get; set; }

        [XmlElement("FREEONBOARDVALUEAMOUNT")]
        public virtual decimal FREEONBOARDVALUEAMOUNT { get; set; }

        private string _AMOUNT_CURRENCY;
        [XmlElement("AMOUNT_CURRENCY")]
        public virtual string AMOUNT_CURRENCY
        {
            get { return this._AMOUNT_CURRENCY; }
            set { if (value != null) this._AMOUNT_CURRENCY = value; }
        }
    }

    [XmlType("SHIPMENTSTAGE")]
    [XmlRoot(ElementName = "SHIPMENTSTAGE")]
    public class ShipmentStage
    {
        private string _TRANSPORTMODECODE;
        [XmlElement("TRANSPORTMODECODE")]
        public virtual string TRANSPORTMODECODE
        {
            get { return this._TRANSPORTMODECODE; }
            set { if (value != null) this._TRANSPORTMODECODE = value; }
        }
    }

    [XmlType("TRANSPORTHANDLINGUNIT")]
    [XmlRoot(ElementName = "TRANSPORTHANDLINGUNIT")]
    public class TransportHandlingUnit
    {
        private ActualPackage _ACTUALPACKAGE;
        [XmlElement("ACTUALPACKAGE")]
        public virtual ActualPackage ACTUALPACKAGE
        {
            get { return this._ACTUALPACKAGE; }
            set { if (value != null) this._ACTUALPACKAGE = value; }
        }
    }

    [XmlType("ACTUALPACKAGE")]
    [XmlRoot(ElementName = "ACTUALPACKAGE")]
    public class ActualPackage
    {
        private string _ID;
        [XmlElement("ID")]
        public virtual string ID
        {
            get { return this._ID; }
            set { if (value != null) this._ID = value; }
        }

        [XmlElement("QUANTITY")]
        public virtual decimal QUANTITY { get; set; }

        private string _QUANTITY_UNIT;
        [XmlElement("QUANTITY_UNIT")]
        public virtual string QUANTITY_UNIT
        {
            get { return this._QUANTITY_UNIT; }
            set { if (value != null) this._QUANTITY_UNIT = value; }
        }

        private string _PACKAGINGTYPECODE;
        [XmlElement("PACKAGINGTYPECODE")]
        public virtual string PACKAGINGTYPECODE
        {
            get { return this._PACKAGINGTYPECODE; }
            set { if (value != null) this._PACKAGINGTYPECODE = value; }
        }
    }
}
