using System.Runtime.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.VeribanServiceDto.Page
{
    [DataContract]
    public class Serial
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Prefix { get; set; }
        [DataMember]
        public int LastNumber { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
        [DataMember]
        public string IsActive { get; set; }
    }
}
