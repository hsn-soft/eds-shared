using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibEnvelope
{
    [XmlType("Partner")]
    public class PartnerModel
    {
        // Bu eleman gönderici tarafı e-fatura uygulamasında tanımlayan biricik (unique) değeri içermektedir. Birden fazla etikete
        // ihtiyaç duyulmadığı sürece, zarfı oluşturan Gönderici Birim ise “defaultgb” değeri, Posta Kutusu ise “defaultpk” değeri kullanılmalıdır.

        //Kullanıcı açma kapama formu içerisinde Identifier: 
        //Özel Entegratör için : usergb, 
        //fatura saklama hizmeti verecekler için : archive, 
        //earşiv hizmeti için : earchive, 
        //ebilet hizmeti için : eticket 
        //sabit değeri yazılmalıdır.

        [XmlElement("Identifier")]
        public string Identifier { get; set; }

        private List<ContactInformationModel> _contactInformations;
        [XmlElement("ContactInformation", Type = typeof(ContactInformationModel))]
        public virtual List<ContactInformationModel> ContactInformations
        {
            get { return this._contactInformations; }
            set { if (value != null) this._contactInformations = value; else this._contactInformations = new List<ContactInformationModel>(); }
        }
    }
}
