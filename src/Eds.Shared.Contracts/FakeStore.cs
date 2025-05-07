namespace Eds.Shared.Contracts;

public static class FakeStore
{
    public static IEnumerable<Tenant> Tenants => new[]
    {
        new Tenant { Id = FakeTenantIds.TenantAId, TenantTitle = "AAA Tenant", TenantShortName = "TenantAAA" },
        new Tenant { Id = FakeTenantIds.TenantZId, TenantTitle = "ZZZ Tenant", TenantShortName = "TenantZZZ" }
    };

    public static IEnumerable<Client> Clients => new[]
    {
        new Client
        {
            Id = FakeClientIds.AbcClientId,
            Tenant = Tenants.Single(x => x.Id == FakeTenantIds.TenantAId),
            ClientVKN = "12345678901",
            ClientTitle = "AAA Abc Client",
            ClientShortName = "AAA Abc Client",
            IsCenterBranch = true,
            BranchNumber = "0001",
            OutboxAlias = "urn:mail:defaultgb@abc.aaa.com",
            InboxAlias = "urn:mail:defaultpk@abc.aaa.com"
        },
        new Client
        {
            Id = FakeClientIds.DefClientId,
            Tenant = Tenants.Single(x => x.Id == FakeTenantIds.TenantAId),
            ClientVKN = "12345678902",
            ClientTitle = "AAA Def Client",
            ClientShortName = "AAA Def Client",
            IsCenterBranch = false,
            BranchNumber = "0002",
            OutboxAlias = "urn:mail:defaultgb@def.aaa.com",
            InboxAlias = "urn:mail:defaultpk@def.aaa.com"
        },
        new Client
        {
            Id = FakeClientIds.XyzClientId,
            Tenant = Tenants.Single(x => x.Id == FakeTenantIds.TenantZId),
            ClientVKN = "12345678991",
            ClientTitle = "ZZZ Xyz Client",
            ClientShortName = "ZZZ Xyz Client",
            IsCenterBranch = true,
            BranchNumber = "0000",
            OutboxAlias = "urn:mail:defaultgb@zzz.com",
            InboxAlias = "urn:mail:defaultpk@zzz.com"
        }
    };
}

public class Client
{
    public Guid Id { get; set; }

    public Tenant Tenant { get; set; }

    public string ClientVKN { get; set; }
    public string ClientTitle { get; set; }
    public string ClientShortName { get; set; }

    public bool IsCenterBranch { get; set; }
    public string BranchNumber { get; set; }

    public string OutboxAlias { get; set; }
    public string InboxAlias { get; set; }
}

public class Tenant
{
    public Guid Id { get; set; }
    public string TenantTitle { get; set; }
    public string TenantShortName { get; set; }
}

public static class FakeTenantIds
{
    public static readonly Guid TenantAId = Guid.Parse("97422b81-74da-4532-a230-9e4fb0c0dede");
    public static readonly Guid TenantZId = Guid.Parse("0b16bed1-ffdb-4066-a33d-c6ee074b72e7");
}

public static class FakeClientIds
{
    public static readonly Guid AbcClientId = Guid.Parse("4fe789ab-0652-4e7b-bd35-07019058081d");
    public static readonly Guid DefClientId = Guid.Parse("54a50c2d-3ad0-41f2-99a2-3df95f508395");
    public static readonly Guid XyzClientId = Guid.Parse("e8265c9c-e52c-4daa-8dbe-30874e5fc02c");
}