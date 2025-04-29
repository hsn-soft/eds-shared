using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.VeribanServiceDto.Page
{
    [DataContract]
    public class CustomerInformation
    {
        [DataMember]
        public string UniqueId { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string RegisterNumber { get; set; }
        [DataMember]
        public string TaxOffice { get; set; }
        [DataMember]
        public string MersisNo { get; set; }
        [DataMember]
        public string TicaretSicilNo { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string WebSite { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Fax { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string CityName { get; set; }
        [DataMember]
        public string CityCode { get; set; }
        [DataMember]
        public string Subcity { get; set; }
        [DataMember]
        public string District { get; set; }
        [DataMember]
        public string Town { get; set; }
        [DataMember]
        public string Street { get; set; }
        [DataMember]
        public string BuildInfo { get; set; }
        [DataMember]
        public string PostalCode { get; set; }
        [DataMember]
        public string ContactCode { get; set; }
        [DataMember]
        public string ContactName { get; set; }
    }

    [DataContract]
    public class CustomerInfoUpdate
    {
        [DataMember]
        public string TaxOffice { get; set; }
        [DataMember]
        public string MersisNo { get; set; }
        [DataMember]
        public string TicaretSicilNo { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string WebSite { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Fax { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string CityName { get; set; }
        [DataMember]
        public string CityCode { get; set; }
        [DataMember]
        public string Subcity { get; set; }
        [DataMember]
        public string District { get; set; }
        [DataMember]
        public string Town { get; set; }
        [DataMember]
        public string Street { get; set; }
        [DataMember]
        public string BuildInfo { get; set; }
        [DataMember]
        public string PostalCode { get; set; }
        [DataMember]
        public string ContactCode { get; set; }
        [DataMember]
        public string ContactName { get; set; }
    }
}
