using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    //Malların alıcıya gönderimlesi için satıcıdan teslim alınması kapsamında zaman ve mekan bilgileri girilir.
    [XmlType("Despatch", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class Despatch
    {
        //İlgili gönderimi belge içerisinde tekil olarak tanımlar.
        [XmlElement("ID")]
        public CombineId ID { get; set; }

        //Gerçekleşen gönderim tarihi girilir.
        [XmlElement("ActualDespatchDate")]
        public string ActualDespatchDate { get; set; }

        //Gerçekleşen gönderim zamanı girilir.
        [XmlElement("ActualDespatchTime")]
        public string ActualDespatchTime { get; set; }

        //Serbest metin olarak gönderime yönelik açıklamalar girilir.
        [XmlElement("Instructions")]
        public string Instructions { get; set; }

        //Malların gönderim için alınacağı adres girilir. Bknz. Address
        [XmlElement("DespatchAddress", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Address DespatchAddress { get; set; }

        //Malları satıcıdan teslim alacak taraf bilgileri girilir. Bknz. Party
        [XmlElement("DespatchParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Party DespatchParty { get; set; }

        //Malları satıcıdan teslim alacak tarafın iletişim bilgileri girilir. Bknz. Contact
        [XmlElement("Contact", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Contact Contact { get; set; }

        //Tahmini teslim alış dönemi girilir. Bknz. Period
        [XmlElement("EstimatedDespatchPeriod", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Period EstimatedDespatchPeriod { get; set; }
    }
}
