using Eds.Shared.Helper.VeribanGlobal.Library.Common;
using Eds.Shared.Helper.VeribanGlobal.Library.Model.GibUser;

namespace Eds.Shared.Contracts;

public static class FakeStore
{

    public static bool IsFakeRegisterNumber(GlobalEnums.RegisterNumberType registerType, string registerNo)
    {
        int controlNumber = registerType == GlobalEnums.RegisterNumberType.Commercial ? 10 : 11;
        try
        {
            for (int i = 0; i < 10; i++)
            {
                if (registerNo.Equals(i.ToString().PadLeft(controlNumber, i.ToString()[0])))
                {
                    return true;
                }
            }
        }
        catch(Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }

        return false;
    }

    public static IEnumerable<Tenant> Tenants => new[]
    {
        new Tenant { Id = FakeTenantIds.TenantAAAId, TenantTitle = "AAA Tenant", TenantShortName = "TenantAAA" },
        new Tenant { Id = FakeTenantIds.TenantZZZId, TenantTitle = "ZZZ Tenant", TenantShortName = "TenantZZZ" }
    };

    public static IEnumerable<GIBUserAccount> GIBUserAccounts => new[]
    {
        new GIBUserAccount { Identifier = "9205121120", DocumentType = nameof(GibAliasDocumentTypes.Invoice), AliasName = "urn:mail:ankara_sube_pk@aaa.com.tr", AliasCreationTime = DateTime.UtcNow },
        new GIBUserAccount { Identifier = "9205121120", DocumentType = nameof(GibAliasDocumentTypes.Invoice), AliasName = "urn:mail:istanbul_sube_pk@aaa.com.tr", AliasCreationTime = DateTime.UtcNow },
        new GIBUserAccount { Identifier = "1288331521", DocumentType = nameof(GibAliasDocumentTypes.Invoice), AliasName = "urn:mail:defaultpk@zzz.com.tr", AliasCreationTime = DateTime.UtcNow }
    };

    public static IEnumerable<FirmConfigSystem> FirmConfigSystems => new[]
    {
        new FirmConfigSystem { FirmId = FakeClientIds.AAAAnkaraClientId, EdsSystemType = (byte)GlobalEnums.VeribanSystemType.EINVOICE, IsActive = 1 },
        new FirmConfigSystem { FirmId = FakeClientIds.AAAIstanbulClientId, EdsSystemType = (byte)GlobalEnums.VeribanSystemType.EINVOICE, IsActive = 1 },
        new FirmConfigSystem { FirmId = FakeClientIds.ZZZClientId, EdsSystemType = (byte)GlobalEnums.VeribanSystemType.EINVOICE, IsActive = 1 }
    };

    public static IEnumerable<Firm> Firms => new[]
    {
        new Firm
        {
            Id = FakeClientIds.AAAAnkaraClientId,
            TenantId = FakeTenantIds.TenantAAAId,
            IsBranchFirm = true,
            BranchCode = "0001",
            ParentFirmUniqueId = null,
            ProfileDomain = "AAA-Ankara",
            Title = "AAA Firması Ankara",
            RegisterSectorType = (byte)GlobalEnums.RegisterSectorType.PrivateSector,
            RegisterType = (byte)GlobalEnums.RegisterNumberType.Commercial,
            RegisterNumber = "9205121120",
            FirmGBAlias = "urn:mail:ankara_sube_gb@aaa.com.tr",
            FirmPKAlias = "urn:mail:ankara_sube_pk@aaa.com.tr",
        },
        new Firm
        {
            Id = FakeClientIds.AAAIstanbulClientId,
            TenantId = FakeTenantIds.TenantAAAId,
            IsBranchFirm = true,
            BranchCode = "0002",
            ParentFirmUniqueId = FakeClientIds.AAAAnkaraClientId,
            ProfileDomain = "AAA-Istanbul",
            Title = "AAA Firması Istanbul",
            RegisterSectorType = (byte)GlobalEnums.RegisterSectorType.PrivateSector,
            RegisterType = (byte)GlobalEnums.RegisterNumberType.Commercial,
            RegisterNumber = "9205121120",
            FirmGBAlias = "urn:mail:istanbul_sube_gb@aaa.com.tr",
            FirmPKAlias = "urn:mail:istanbul_sube_pk@aaa.com.tr",
        },
        new Firm
        {
            Id = FakeClientIds.ZZZClientId,
            TenantId = FakeTenantIds.TenantZZZId,
            IsBranchFirm = false,
            BranchCode = null,
            ParentFirmUniqueId = null,
            ProfileDomain = "ZZZ",
            Title = "ZZZ Firması",
            RegisterSectorType = (byte)GlobalEnums.RegisterSectorType.PrivateSector,
            RegisterType = (byte)GlobalEnums.RegisterNumberType.Commercial,
            RegisterNumber = "1288331521",
            FirmGBAlias = "urn:mail:defaultgb@zzz.com.tr",
            FirmPKAlias = "urn:mail:defaultpk@zzz.com.tr",
        }
    };
}

public class Firm
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public bool IsBranchFirm { get; set; }
    public string BranchCode { get; set; }
    public Guid? ParentFirmUniqueId { get; set; }

    public string ProfileDomain { get; set; }

    public string Title { get; set; }
    public byte RegisterSectorType { get; set; }
    public byte RegisterType { get; set; }
    public string RegisterNumber { get; set; }
    public string TaxOffice { get; set; }
    public string MersisNo { get; set; }
    public string TicaretSicilNo { get; set; }
    public string NaceCode { get; set; }

    public string FirmGBAlias { get; set; }
    public string FirmPKAlias { get; set; }
}

public class Tenant
{
    public Guid Id { get; set; }
    public string TenantTitle { get; set; }
    public string TenantShortName { get; set; }
}

public static class FakeTenantIds
{
    public static readonly Guid TenantAAAId = Guid.Parse("97422b81-74da-4532-a230-9e4fb0c0dede");
    public static readonly Guid TenantZZZId = Guid.Parse("0b16bed1-ffdb-4066-a33d-c6ee074b72e7");
}

public static class FakeClientIds
{
    public static readonly Guid AAAAnkaraClientId = Guid.Parse("4fe789ab-0652-4e7b-bd35-07019058081d");
    public static readonly Guid AAAIstanbulClientId = Guid.Parse("54a50c2d-3ad0-41f2-99a2-3df95f508395");
    public static readonly Guid ZZZClientId = Guid.Parse("e8265c9c-e52c-4daa-8dbe-30874e5fc02c");
}

public class FirmConfigSystem
{
    public Guid FirmId { get; set; }
    public byte EdsSystemType { get; set; }
    public byte IsActive { get; set; }
    public bool IsAccessDeActivation { get; set; }
    public byte AccountProcessType { get; set; }
    public byte TransferPlatformType { get; set; }
}

public class GIBUserAccount
{
    public string Identifier { get; set; }
    public string DocumentType { get; set; }
    public string AliasName { get; set; }
    public DateTime AliasCreationTime { get; set; }
}