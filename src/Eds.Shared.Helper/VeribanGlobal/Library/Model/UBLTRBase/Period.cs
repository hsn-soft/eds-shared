using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Belgelerde dönem kullanılması halinde dönem bu elemanda gösterilir.
    [XmlType("Period", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class Period
    {
        // Dönemin belli bir tarih aralığı olarak belirlenmesi halinde “StartDate” ve “EndDate” elemanları kullanılacaktır, bunun
        // dışında dönem, süre olarak belirtiliyorsa ölçüsü belirtilerek “DurationMeasure” elemanı kullanılacaktır.

        // Dönemin başladığı tarih
        public string StartDate { get; set; }

        //Dönemin başladığı zaman
        public string StartTime { get; set; }

        // Dönemin bittiği tarih
        public string EndDate { get; set; }

        //Dönemin bittiği zaman
        public string EndTime { get; set; }

        // Dönem süresi numerik olarak, dönem aralığı tipi’de “unitCode” attribute değerine
        // yıl için “ANN”, ay için “MON”, gün için “DAY” ve saat için “HUR” girilmesi gerekmektedir.
        public DurationMeasure DurationMeasure { get; set; }

        // Dönemin açıklaması serbest metin olarak girilecektir.
        public string Description { get; set; }
    }

    [XmlType("DurationMeasure")]
    public class DurationMeasure
    {
        [XmlText()]
        public Decimal Value { get; set; }

        [XmlAttribute("UnitCode")]
        public string UnitCode { get; set; }
    }
}
