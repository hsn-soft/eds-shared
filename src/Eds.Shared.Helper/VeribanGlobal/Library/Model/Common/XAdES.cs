using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.Common
{
    [XmlType("XAdES")]
    public class XAdES
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }

        [XmlElement("SignedInfo")]
        public virtual SignedInfo SignedInfo { get; set; }

        [XmlElement("SignatureValue")]
        public SignatureValue SignatureValue { get; set; }

        [XmlElement("KeyInfo")]
        public virtual KeyInfo KeyInfo { get; set; }

        [XmlElement("Object")]
        public virtual XAdESObject Object { get; set; }
    }

    [XmlType("SignedInfo")]
    public class SignedInfo
    {
        public SignedInfo()
        {
            this.CanonicalizationMethod = new XAdESMethod() { Algorithm = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments" };
            this.SignatureMethod = new XAdESMethod() { Algorithm = "http://www.w3.org/2000/09/xmldsig#rsa-sha1" };
        }

        [XmlAttribute("Id")]
        public string Id { get; set; }

        [XmlElement("CanonicalizationMethod")]
        public virtual XAdESMethod CanonicalizationMethod { get; set; }

        [XmlElement("SignatureMethod")]
        public virtual XAdESMethod SignatureMethod { get; set; }

        private List<Reference> _references;
        [XmlElement("Reference", Type = typeof(Reference))]
        public virtual List<Reference> References
        {
            get { return this._references; }
            set { if (value != null) this._references = value; else this._references = new List<Reference>(); }
        }
    }

    [XmlType("XAdESMethod")]
    public class XAdESMethod
    {
        [XmlAttribute("Algorithm")]
        public string Algorithm { get; set; }
    }

    [XmlType("Reference", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public class Reference
    {
        public Reference()
        {
            this.DigestMethod = new XAdESMethod() { Algorithm = "http://www.w3.org/2001/04/xmlenc#sha256" };
        }

        [XmlAttribute("Id")]
        public string Id { get; set; }

        [XmlAttribute("Type")]
        public string Type { get; set; }

        [XmlAttribute("URI")]
        public string URI { get; set; }

        [XmlElement("Transforms")]
        public virtual Transforms Transforms { get; set; }

        [XmlElement("DigestMethod")]
        public virtual XAdESMethod DigestMethod { get; set; }

        [XmlElement("DigestValue")]
        public string DigestValue { get; set; }
    }

    [XmlType("Transforms")]
    public class Transforms
    {
        public Transforms()
        {
            this.Transform = new XAdESMethod() { Algorithm = "http://www.w3.org/2000/09/xmldsig#enveloped-signature" };
        }

        [XmlElement("Transform")]
        public virtual XAdESMethod Transform { get; set; }
    }

    [XmlType("SignatureValue")]
    public class SignatureValue
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }

        [XmlText()]
        public string Name { get; set; }
    }

    [XmlType("KeyInfo")]
    public class KeyInfo
    {
        [XmlElement("KeyValue")]
        public virtual KeyValue KeyValue { get; set; }

        [XmlElement("X509Data")]
        public virtual X509Data X509Data { get; set; }
    }

    [XmlType("KeyValue")]
    public class KeyValue
    {
        [XmlElement("RSAKeyValue")]
        public virtual RsaKeyValue RSAKeyValue { get; set; }
    }

    [XmlType("RSAKeyValue")]
    public class RsaKeyValue
    {
        [XmlElement("Modulus")]
        public string Modulus { get; set; }

        [XmlElement("Exponent")]
        public string Exponent { get; set; }
    }

    [XmlType("X509Data", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public class X509Data
    {
        [XmlElement("X509SubjectName")]
        public string X509SubjectName { get; set; }

        [XmlElement("X509Certificate")]
        public string X509Certificate { get; set; }

        [XmlElement("X509IssuerName")]
        public string X509IssuerName { get; set; }

        [XmlElement("X509SerialNumber")]
        public string X509SerialNumber { get; set; }
    }

    [XmlType("XAdESObject", Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    public class XAdESObject
    {
        [XmlElement("QualifyingProperties")]
        public virtual QualifyingProperties QualifyingProperties { get; set; }
    }

    [XmlType("QualifyingProperties", Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    public class QualifyingProperties
    {
        [XmlAttribute("Target")]
        public string Target { get; set; }

        [XmlElement("SignedProperties")]
        public virtual SignedProperties SignedProperties { get; set; }
    }

    [XmlType("SignedProperties")]
    public class SignedProperties
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }

        [XmlElement("SignedSignatureProperties")]
        public virtual SignedSignatureProperties SignedSignatureProperties { get; set; }
    }

    [XmlType("SignedSignatureProperties")]
    public class SignedSignatureProperties
    {
        [XmlElement("SigningTime")]
        public string SigningTime { get; set; }

        [XmlElement("SigningCertificate")]
        public virtual SigningCertificate SigningCertificate { get; set; }

        [XmlElement("SignerRole")]
        public virtual SignerRole SignerRole { get; set; }
    }

    [XmlType("SigningCertificate")]
    public class SigningCertificate
    {
        [XmlElement("Cert")]
        public virtual Cert Cert { get; set; }
    }

    [XmlType("Cert")]
    public class Cert
    {
        [XmlElement("CertDigest")]
        public virtual Reference CertDigest { get; set; }

        [XmlElement("IssuerSerial")]
        public virtual X509Data IssuerSerial { get; set; }
    }

    [XmlType("SignerRole")]
    public class SignerRole
    {
        [XmlElement("ClaimedRoles")]
        public virtual ClaimedRoles ClaimedRoles { get; set; }
    }

    [XmlType("ClaimedRoles")]
    public class ClaimedRoles
    {
        [XmlElement("ClaimedRole")]
        public string ClaimedRole { get; set; }
    }
}
