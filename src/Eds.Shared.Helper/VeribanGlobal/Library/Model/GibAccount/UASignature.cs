using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibAccount
{
    // Bu elemana belgelerde kullanılan mali mühür/elektronik imza ile ilgili bilgiler girilir.
    [XmlType("Signature")]
    public class UASignature
    {
        [XmlAttribute("qualifyingAgencyId")]
        public string QualifyingAgencyId { get; set; }
    }
}
