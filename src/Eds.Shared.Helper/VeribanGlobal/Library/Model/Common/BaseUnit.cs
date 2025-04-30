using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.Common
{
    public class BaseUnit
    {
        [XmlAttribute("unitCode")]
        public string UnitCode { get; set; }

        [XmlText]
        public Decimal Value { get; set; }
    }
}
