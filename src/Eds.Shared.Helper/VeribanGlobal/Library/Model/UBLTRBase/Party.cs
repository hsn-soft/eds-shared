using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Tarafları (kurum ve şahıslar) tanımlamak için kullanılır.
    [XmlType("Party")]
    public class Party
    {
        // Tarafın web sayfası adresi metin olarak girilir.
        [XmlElement("WebsiteURI", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string WebSiteURI { get; set; }

        //Tarafın ana faaliyet (NACE) kodu girilecektir.
        [XmlElement("IndustryClassificationCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string IndustryClassificationCode { get; set; }

        // Tarafın vergi kimlik numarası veya TC kimlik numarası metin olarak girilir. UBL-TR’de
        // “PartyIdentification/ID” elemanının “schemeID” attribute’u zorunludur. Bunun kontrolleri XSD şeması seviyesinde değil,
        // ikinci aşama Schematron kural seviyesinde yapılacaktır. “schemeID” vergi kimlik numarası için “VKN” ve TC kimlik
        // numarası için “TCKN” değerlerini alabilir. Bknz. Kod Listeleri.
        private List<PartyIdentification> _partyIdentification;
        [XmlElement("PartyIdentification", Type = typeof(PartyIdentification), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<PartyIdentification> PartyIdentification
        {
            get { return this._partyIdentification; }
            set { if (value != null) this._partyIdentification = value; else this._partyIdentification = new List<PartyIdentification>(); }
        }


        // Taraf eğer kurum ise kurum ismi bu elemana metin olarak girilir.
        [XmlElement("PartyName")]
        public PartyName PartyName { get; set; }

        // Tarafın adresi girilir.
        [XmlElement("PostalAddress")]
        public virtual Address PostalAddress { get; set; }

        // Tarafın var ise depo bilgileri girilir.
        [XmlElement("PhysicalLocation")]
        public virtual Location PhysicalLocation { get; set; }

        // Tarafın vergi kimlik numarası girilmişse bu alana vergi dairesi adı girilir.
        [XmlElement("PartyTaxScheme")]
        public virtual PartyTaxScheme PartyTaxScheme { get; set; }

        //Tarafın diğer kayıtlı olduğu yerlerin bilgileri ve kayıtlı olduğu yerlerdeki kayıt numaraları detaylı olarak girilecektir. Bknz. PartyLegalEntity.
        private List<PartyLegalEntity> _partyLegalEntities;
        [XmlElement("PartyLegalEntity", Type = typeof(PartyLegalEntity), Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public virtual List<PartyLegalEntity> PartyLegalEntities
        {
            get { return this._partyLegalEntities; }
            set { if (value != null) this._partyLegalEntities = value; else this._partyLegalEntities = new List<PartyLegalEntity>(); }
        }

        // Tarafın iletişim bilgileri girilir.
        [XmlElement("Contact")]
        public virtual Contact Contact { get; set; }

        // Taraf eğer şahıssa bu eleman kullanılır.
        [XmlElement("Person")]
        public virtual Person Person { get; set; }

        // Tarafın şubesine ait bilgiler bu elemana girilir.
        [XmlElement("AgentParty")]
        public virtual Party AgentParty { get; set; }
    }
}
