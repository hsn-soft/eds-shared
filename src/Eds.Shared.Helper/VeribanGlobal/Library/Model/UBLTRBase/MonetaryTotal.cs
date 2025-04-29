using System.Xml.Serialization;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.Common;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.UBLTRBase
{
    // Parasal toplamlar ile genel tutarların girildiği elemandır.
    [XmlType("MonetaryTotal", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class MonetaryTotal
    {
        // Mal/hizmet miktarı ile Mal/hizmet birim fiyatının çarpımı ile bulunan tutarlar toplamı girilir.
        [XmlElement("LineExtensionAmount")]
        public UblBaseCurrency LineExtensionAmount { get; set; }

        // Vergiler hariç, ıskonto veya artırım dahil toplam tutar girilir.(Vergi Matrahı).
        [XmlElement("TaxExclusiveAmount")]
        public UblBaseCurrency TaxExclusiveAmount { get; set; }

        // Vergiler, ıskonto ve artırım dahil toplam tutar girilir.
        [XmlElement("TaxInclusiveAmount")]
        public UblBaseCurrency TaxInclusiveAmount { get; set; }

        /// <summary>
        ///     Toplam ıskonto tutarı girilir.
        /// </summary>
        [XmlElement("AllowanceTotalAmount")]
        public UblBaseCurrency AllowanceTotalAmount { get; set; }

        // Toplam fiyat artırımı tutarı girilir.
        [XmlElement("ChargeTotalAmount")]
        public UblBaseCurrency ChargeTotalAmount { get; set; }

        // Yuvarlama tutarı girilir.
        [XmlElement("PayableRoundingAmount")]
        public UblBaseCurrency PayableRoundingAmount { get; set; }

        // Ödenecek tutar girilir.
        [XmlElement("PayableAmount")]
        public UblBaseCurrency PayableAmount { get; set; }

    }
}