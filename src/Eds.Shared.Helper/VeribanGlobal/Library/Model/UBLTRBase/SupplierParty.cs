using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Satıcı bilgilerini tutan elemandır.
    [XmlType("SupplierParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class SupplierParty
    {
        // Party: Satıcı tarafı tanımlar ve aşağıdaki kısıtlar uygulanır:
        //  • “Party/PartyIdentification”: Satıcının kurum olması durumunda vergi kimlik numarası girilmesi zorunludur.
        //  Satıcının şahıs olması durumunda TC kimlik numarası girilmesi zorunludur.
        //  • “Party/PartyName” elemanı satıcının kurum olması durumunda zorunludur.
        //  • Tarafın vergi kimlik numarası girilmişse bu alana vergi dairesi adı girilir. 
        //  • “Party/Person”: Tarafın şahıs olması durumunda bu eleman zorunludur.
        [XmlElement("Party", Type = typeof(Party), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Party Party { get; set; }
    }
}
