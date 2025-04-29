using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Siparişe ait bilgiler girilecektir.
    [XmlType("OrderReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class OrderReference
    {
        [XmlIgnore]
        public CombineId ID { get; set; }
        [XmlElement(ElementName = "ID")]
        public CombineId IDValue
        {
            get
            {
                return this.ID;
            }
            set
            {
                if (value == null)
                {
                    this.ID = new CombineId();
                }
                else
                {
                    this.ID = value;
                }
            }
        }

        //Satıcının verdiği sipariş numarası girilecektir.
        public string SalesOrderID { get; set; }

        // Sipariş tarihi girilecektir.
        public string IssueDate { get; set; }

        //Sipariş tipi girilecektir.
        public string OrderTypeCode { get; set; }

        // Referans verilen ya da eklenen belgelere ilişkin bilgiler girilecektir.
        [XmlElement("DocumentReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual DocumentReference DocumentReference { get; set; }
    }
}
