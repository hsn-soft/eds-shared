using System.Xml.Serialization;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.VeribanContract
{
    [XmlRoot("ApplicationForm")]
    public class TransferContractDataModel
    {
        [XmlElement("CustomerData")]
        public virtual CustomerInfo CustomerData { get; set; }
        [XmlElement("Package")]
        public virtual PackageInfo Package { get; set; }
        [XmlElement("Company")]
        public virtual CompanyInfo Company { get; set; }
        public TransferContractDataModel()
        {
            CustomerData = new CustomerInfo();
            Package = new PackageInfo();
            Package.Projects = new ProjectInfo();
            Package.Projects.PackageDetail = new PackageDetailInfo();
            Package.Projects.PackageDetail.InvoiceDiscount = new Discount();
            Package.Projects.PackageDetail.ArchiveDiscount = new Discount();
            Package.Projects.PackageDetail.DespatchDiscount = new Discount();
            Package.Projects.PackageDetail.TicketDiscount = new Discount();
            Package.Projects.PackageDetail.SMMDiscount = new Discount();
            Package.Projects.PackageDetail.MMDiscount = new Discount();
            Package.Projects.PackageDetail.BookDiscount = new Discount();
            Package.Projects.PackageDetail.BookStorageDiscount = new Discount();
            Package.Projects.PackageDetail.OKCZRPRTDiscount = new Discount();
            Package.Projects.PackageDetail.BookYearly = new YearlyPrice();
            Package.Projects.PackageDetail.BookStorageYearly = new YearlyPrice();
            Package.Projects.PackageDetail.OKCZRPRTYearly = new YearlyPrice();
            Package.Projects.SmtpInfo = new SmtpInfo();
            Company = new CompanyInfo();
            Company.Address = new AddressInfo();
            Company.CompanyOfficials = new CompanyOfficialsInfo();
            Company.ErpInformation = new ErpInformationInfo();
            Company.PortalUsers = new PortalUsersInfo();
            Company.UseOfIntegrationModel = new UseOfIntegrationModelInfo();
        }

    }

    public class CustomerInfo
    {
        [XmlElement("Person")]
        public string Person { get; set; }

        [XmlElement("Title")]
        public string Title { get; set; }

        [XmlElement("VknTckn")]
        public string VknTckn { get; set; }

        [XmlElement("SubeKodu")]
        public string SubeKodu { get; set; }

        [XmlElement("Email")]
        public string Email { get; set; }

        [XmlElement("TaxAdmin")]
        public string TaxAdmin { get; set; }

        [XmlElement("TradeRegisterNumber")]
        public string TradeRegisterNumber { get; set; }

        [XmlElement("MersisNo")]
        public string MersisNo { get; set; }

    }
    public class PackageInfo
    {
        [XmlElement("Projects")]
        public ProjectInfo Projects { get; set; }

    }
    public class ProjectInfo
    {
        [XmlElement("SelectInvoice")]
        public string SelectInvoice { get; set; }
        [XmlElement("SelectInvoiceName")]
        public string SelectInvoiceName { get; set; }
        [XmlElement("PackageUseEInvoice")]
        public string PackageUseEInvoice { get; set; }
        [XmlElement("InvoicePackageUseName")]
        public string InvoicePackageUseName { get; set; }




        [XmlElement("SelectDespatch")]
        public string SelectDespatch { get; set; }
        [XmlElement("SelectDespatchName")]
        public string SelectDespatchName { get; set; }
        [XmlElement("PackageUseEDespatch")]
        public string PackageUseEDespatch { get; set; }
        [XmlElement("DespatchPackageUseName")]
        public string DespatchPackageUseName { get; set; }




        [XmlElement("SelectArchive")]
        public string SelectArchive { get; set; }
        [XmlElement("SelectArchiveName")]
        public string SelectArchiveName { get; set; }
        [XmlElement("PackageUseEArchive")]
        public string PackageUseEArchive { get; set; }
        [XmlElement("ArchivePackageUseName")]
        public string ArchivePackageUseName { get; set; }



        [XmlElement("SelectSMM")]
        public string SelectSMM { get; set; }
        [XmlElement("SelectSMMName")]
        public string SelectSMMName { get; set; }
        [XmlElement("PackageUseESMM")]
        public string PackageUseESMM { get; set; }
        [XmlElement("SMMPackageUseName")]
        public string SMMPackageUseName { get; set; }

        [XmlElement("SelectMM")]
        public string SelectMM { get; set; }
        [XmlElement("SelectMMName")]
        public string SelectMMName { get; set; }
        [XmlElement("PackageUseEMM")]
        public string PackageUseEMM { get; set; }
        [XmlElement("MMPackageUseName")]
        public string MMPackageUseName { get; set; }


        [XmlElement("SelectBook")]
        public string SelectBook { get; set; }
        [XmlElement("SelectBookName")]
        public string SelectBookName { get; set; }
        [XmlElement("PackageUseEBook")]
        public string PackageUseEBook { get; set; }
        [XmlElement("BookPackageUseName")]
        public string BookPackageUseName { get; set; }

        [XmlElement("SelectBookStorage")]
        public string SelectBookStorage { get; set; }
        [XmlElement("SelectBookStorageName")]
        public string SelectBookStorageName { get; set; }
        [XmlElement("PackageUseEBookStorage")]
        public string PackageUseEBookStorage { get; set; }
        [XmlElement("BookStoragePackageUseName")]
        public string BookStoragePackageUseName { get; set; }

        [XmlElement("SelectTicket")]
        public string SelectTicket { get; set; }
        [XmlElement("SelectTicketName")]
        public string SelectTicketName { get; set; }
        [XmlElement("PackageUseTicket")]
        public string PackageUseTicket { get; set; }
        [XmlElement("PackageUseTicketName")]
        public string PackageUseTicketName { get; set; }


        [XmlElement("SelectOKCZRPRT")]
        public string SelectOKCZRPRT { get; set; }
        [XmlElement("SelectOKCZRPRTName")]
        public string SelectOKCZRPRTName { get; set; }
        [XmlElement("PackageUseOKCZRPRT")]
        public string PackageUseOKCZRPRT { get; set; }
        [XmlElement("PackageUseOKCZRPRTName")]
        public string PackageUseOKCZRPRTName { get; set; }


        [XmlElement("PackageDetail")]
        public PackageDetailInfo PackageDetail { get; set; }

        [XmlElement("SmtpInfo")]
        public SmtpInfo SmtpInfo { get; set; }

    }
    public class SmtpInfo
    {
        [XmlElement("SmtpServer")]
        public string SmtpServer { get; set; }
        [XmlElement("MailAddress")]
        public string MailAddress { get; set; }
        [XmlElement("MailPassword")]
        public string MailPassword { get; set; }
        [XmlElement("MailSmtpPort")]
        public string MailSmtpPort { get; set; }
    }
    public class PackageDetailInfo
    {
        [XmlElement("InvoiceMonthlyPackage")]
        public string InvoiceMonthlyPackage { get; set; }
        [XmlElement("InvoiceMonthlyPackageName")]
        public string InvoiceMonthlyPackageName { get; set; }

        [XmlElement("InvoiceCreditPackage")]
        public string InvoiceCreditPackage { get; set; }

        [XmlElement("InvoiceCreditPackageName")]
        public string InvoiceCreditPackageName { get; set; }


        [XmlElement("OKCZRPRTMonthlyPackage")]
        public string OKCZRPRTMonthlyPackage { get; set; }
        [XmlElement("OKCZRPRTMonthlyPackageName")]
        public string OKCZRPRTMonthlyPackageName { get; set; }


        [XmlElement("ArchiveMonthlyPackage")]
        public string ArchiveMonthlyPackage { get; set; }
        [XmlElement("ArchiveMonthlyPackageName")]
        public string ArchiveMonthlyPackageName { get; set; }
        [XmlElement("ArchiveCreditPackage")]
        public string ArchiveCreditPackage { get; set; }
        [XmlElement("ArchiveCreditPackageName")]
        public string ArchiveCreditPackageName { get; set; }



        [XmlElement("DespatchMothlyPackage")]
        public string DespatchMonthlyPackage { get; set; }
        [XmlElement("DespatchMonthlyPackageName")]
        public string DespatchMonthlyPackageName { get; set; }
        [XmlElement("DespatchCreditPackage")]
        public string DespatchCreditPackage { get; set; }
        [XmlElement("DespatchCreditPackageName")]
        public string DespatchCreditPackageName { get; set; }



        [XmlElement("TicketMonthlyPackage")]
        public string TicketMonthlyPackage { get; set; }
        [XmlElement("TicketMonthlyPackageName")]
        public string TicketMonthlyPackageName { get; set; }
        [XmlElement("TicketCreditPackage")]
        public string TicketCreditPackage { get; set; }
        [XmlElement("TicketCreditPackageName")]
        public string TicketCreditPackageName { get; set; }


        [XmlElement("SMMCreditPackage")]
        public string SMMCreditPackage { get; set; }
        [XmlElement("SMMCreditPackageName")]
        public string SMMCreditPackageName { get; set; }
        [XmlElement("SMMMonthlyPackage")]
        public string SMMMonthlyPackage { get; set; }
        [XmlElement("SMMMonthlyPackageName")]
        public string SMMMonthlyPackageName { get; set; }



        [XmlElement("MMCreditPackage")]
        public string MMCreditPackage { get; set; }
        [XmlElement("MMCreditPackageName")]
        public string MMCreditPackageName { get; set; }
        [XmlElement("MMMonthlyPackage")]
        public string MMMonthlyPackage { get; set; }
        [XmlElement("MMMonthlyPackageName")]
        public string MMMonthlyPackageName { get; set; }

        [XmlElement("BookCreditPackage")]
        public string BookCreditPackage { get; set; }
        [XmlElement("BookCreditPackageName")]
        public string BookCreditPackageName { get; set; }
        [XmlElement("BookMonthlyPackage")]
        public string BookMonthlyPackage { get; set; }
        [XmlElement("BookMonthlyPackageName")]
        public string BookMonthlyPackageName { get; set; }

        [XmlElement("BookStorageMonthlyPackage")]
        public string BookStorageMonthlyPackage { get; set; }
        [XmlElement("BookStorageMonthlyPackageName")]
        public string BookStorageMonthlyPackageName { get; set; }
        [XmlElement("BookStorageCreditPackage")]
        public string BookStorageCreditPackage { get; set; }
        [XmlElement("BookStorageCreditPackageName")]
        public string BookStorageCreditPackageName { get; set; }



        [XmlElement("InvoicePackageDetail")]
        public Discount InvoiceDiscount { get; set; }

        [XmlElement("ArchivePackageDetail")]
        public Discount ArchiveDiscount { get; set; }
        [XmlElement("DespatchPackageDetail")]
        public Discount DespatchDiscount { get; set; }
        [XmlElement("TicketPackageDetail")]
        public Discount TicketDiscount { get; set; }
        [XmlElement("SMMPackageDetail")]
        public Discount SMMDiscount { get; set; }
        [XmlElement("MMPackageDetail")]
        public Discount MMDiscount { get; set; }
        [XmlElement("BookPackageDetail")]
        public Discount BookDiscount { get; set; }

        [XmlElement("BookStoragePackageDetail")]
        public Discount BookStorageDiscount { get; set; }
        [XmlElement("OKCZRPRTPackageDetail")]
        public Discount OKCZRPRTDiscount { get; set; }

        [XmlElement("BookYearly")]
        public YearlyPrice BookYearly { get; set; }
        [XmlElement("BookStorageYearly")]
        public YearlyPrice BookStorageYearly { get; set; }
        [XmlElement("OKCZRPRTYearly")]
        public YearlyPrice OKCZRPRTYearly { get; set; }




    }
    public class AddressInfo
    {
        [XmlElement("County")]
        public string County { get; set; }
        [XmlElement("District")]
        public string District { get; set; }
        [XmlElement("PostalCode")]
        public string PostalCode { get; set; }
        [XmlElement("AddressText")]
        public string AddressText { get; set; }
        [XmlElement("Phone")]
        public string Phone { get; set; }
        [XmlElement("Gsm")]
        public string Gsm { get; set; }
        [XmlElement("Fax")]
        public string Fax { get; set; }

        [XmlElement("WebSite")]
        public string WebSite { get; set; }
    }
    public class ErpInformationInfo
    {
        [XmlElement("ErpName")]
        public string ErpName { get; set; }
        [XmlElement("ErpSolutionPartnerName")]
        public string ErpSolutionPartnerName { get; set; }
    }
    public class UseOfIntegrationModelInfo
    {
        [XmlElement("WebServis")]
        public string WebServis { get; set; }
        [XmlElement("Portal")]
        public string Portal { get; set; }
        [XmlElement("Connector")]
        public string Connector { get; set; }
        [XmlElement("ConnectorDiscountPrice")]
        public string ConnectorDiscountPrice { get; set; }

    }
    public class CompanyOfficialsInfo
    {
        [XmlElement("CompanyFirstPersonName")]
        public string CompanyFirstPersonName { get; set; }
        [XmlElement("CompanyFirstPersonSurName")]
        public string CompanyFirstPersonSurName { get; set; }
        [XmlElement("CompanyFirstPersonGsm")]
        public string CompanyFirstPersonGsm { get; set; }
        [XmlElement("CompanyFirstPersonEmail")]
        public string CompanyFirstPersonEmail { get; set; }
        [XmlElement("CompanySecondPersonName")]
        public string CompanySecondPersonName { get; set; }
        [XmlElement("CompanySecondPersonSurName")]
        public string CompanySecondPersonSurName { get; set; }
        [XmlElement("CompanySecondPersonGsm")]
        public string CompanySecondPersonGsm { get; set; }
        [XmlElement("CompanySecondPersonEmail")]
        public string CompanySecondPersonEmail { get; set; }

        [XmlElement("CompanyThreePersonName")]
        public string CompanyThreePersonName { get; set; }
        [XmlElement("CompanyThreePersonSurName")]
        public string CompanyThreePersonSurName { get; set; }
        [XmlElement("CompanyThreePersonGsm")]
        public string CompanyThreePersonGsm { get; set; }
        [XmlElement("CompanyThreePersonEmail")]
        public string CompanyThreePersonEmail { get; set; }

        [XmlElement("CompanyFourPersonName")]
        public string CompanyFourPersonName { get; set; }
        [XmlElement("CompanyFourPersonSurName")]
        public string CompanyFourPersonSurName { get; set; }
        [XmlElement("CompanyFourPersonGsm")]
        public string CompanyFourPersonGsm { get; set; }
        [XmlElement("CompanyFourPersonEmail")]
        public string CompanyFourPersonEmail { get; set; }

        [XmlElement("CompanyFivePersonName")]
        public string CompanyFivePersonName { get; set; }
        [XmlElement("CompanyFivePersonSurName")]
        public string CompanyFivePersonSurName { get; set; }
        [XmlElement("CompanyFivePersonGsm")]
        public string CompanyFivePersonGsm { get; set; }
        [XmlElement("CompanyFivePersonEmail")]
        public string CompanyFivePersonEmail { get; set; }
    }
    public class PortalUsersInfo
    {
        [XmlElement("UserFirstPerson")]
        public string UserFirstPerson { get; set; }
        [XmlElement("UserSecondPerson")]
        public string UserSecondPerson { get; set; }
        [XmlElement("UserThreePerson")]
        public string UserThreePerson { get; set; }
        [XmlElement("UserFourPerson")]
        public string UserFourPerson { get; set; }
    }
    public class CompanyInfo
    {
        [XmlElement("Address")]
        public AddressInfo Address { get; set; }
        [XmlElement("ErpInformation")]
        public ErpInformationInfo ErpInformation { get; set; }
        [XmlElement("UseOfIntegrationModel")]
        public UseOfIntegrationModelInfo UseOfIntegrationModel { get; set; }
        [XmlElement("CompanyOfficials")]
        public CompanyOfficialsInfo CompanyOfficials { get; set; }
        [XmlElement("PortalUsers")]
        public PortalUsersInfo PortalUsers { get; set; }
    }

    public class Discount
    {
        public string PackageDesc { get; set; }
        public string PackageAmount { get; set; }
        public string DiscountAmount { get; set; }
        public string Note { get; set; }
        public string PackageId { get; set; }
    }

    public class YearlyPrice
    {
        public string Message { get; set; }
        public string Price { get; set; }
    }
}
