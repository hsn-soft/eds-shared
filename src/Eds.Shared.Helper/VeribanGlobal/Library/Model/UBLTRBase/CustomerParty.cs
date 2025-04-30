using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Alıcı tarafın bilgilerini tutan elemandır.
    [XmlType("CustomerParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class CustomerParty
    {
        // “Party/PartyIdentification”: Alıcının kurum olması durumunda vergi kimlik numarası girilmesi zorunludur. Alıcının şahıs olması
        // durumunda TC kimlik numarası girilmesi zorunludur. Tarafın vergi kimlik numarası girilmişse bu alana vergi dairesi adı girilir.

        // “Party/Person”: Tarafın şahıs olması durumunda bu eleman zorunludur.
        [XmlElement("Party", Type = typeof(Party), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Party Party { get; set; }
    }
}
