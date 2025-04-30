using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    [XmlType("DespatchLine", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class DespatchLine
    {
        [XmlElement("ID")]
        public CombineId ID { get; set; }

        private List<String> _notes;
        [XmlElement("Note", Type = typeof(String), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public virtual List<String> Notes
        {
            get { return this._notes; }
            set { if (value != null) this._notes = value; else this._notes = new List<String>(); }
        }

        [XmlElement("DeliveredQuantity")]
        public virtual BaseUnit DeliveredQuantity { get; set; }

        [XmlElement("OutstandingQuantity")]
        public virtual BaseUnit OutstandingQuantity { get; set; }

        private List<String> _outstandingReasons;
        [XmlElement("OutstandingReason", Type = typeof(String), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public virtual List<String> OutstandingReasons
        {
            get { return this._outstandingReasons; }
            set { if (value != null) this._outstandingReasons = value; else this._outstandingReasons = new List<String>(); }
        }

        [XmlElement("OversupplyQuantity")]
        public virtual BaseUnit OversupplyQuantity { get; set; }

        private List<OrderLineReference> _orderLineReferences;
        [XmlElement("OrderLineReference", Type = typeof(OrderLineReference), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<OrderLineReference> OrderLineReferences
        {
            get { return this._orderLineReferences; }
            set { if (value != null) this._orderLineReferences = value; else this._orderLineReferences = new List<OrderLineReference>(); }
        }

        private List<DocumentReference> _documentReferences;
        [XmlElement("DocumentReference", Type = typeof(DocumentReference), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<DocumentReference> DocumentReferences
        {
            get { return this._documentReferences; }
            set { if (value != null) this._documentReferences = value; else this._documentReferences = new List<DocumentReference>(); }
        }

        [XmlElement("Item", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Item Item { get; set; }

        private List<Shipment> _shipments;
        [XmlElement("Shipment", Type = typeof(Shipment), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<Shipment> Shipments
        {
            get { return this._shipments; }
            set { if (value != null) this._shipments = value; else this._shipments = new List<Shipment>(); }
        }
    }
}
