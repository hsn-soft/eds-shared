using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    //Kurumun kayıtlı olduğu organizasyon hakkında bilgileri tutar. Örneğin sanayi odası veya ticaret odası.
    [XmlType("CorporateRegistrationScheme", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class CorporateRegistrationScheme
    {
        //Kayıt yeri numarası girilebilir.
        [XmlElement("ID")]
        public CombineId ID { get; set; }

        //Kayıt yeri ismi girilebilir.
        [XmlElement("Name")]
        public string Name { get; set; }

        //Kayıt yeri tipi girilebilir
        [XmlElement("CorporateRegistrationTypeCode")]
        public string CorporateRegistrationTypeCode { get; set; }

        //Kayıt yeri adresi girilebilir. Bknz. Address
        [XmlElement("JuridictionRegionAddress", Type = typeof(Address), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual Address JuridictionRegionAddress { get; set; }
    }
}
