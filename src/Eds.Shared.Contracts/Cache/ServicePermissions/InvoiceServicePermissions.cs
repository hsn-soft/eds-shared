using HsnSoft.Base.Reflection;

namespace Eds.Shared.Contracts.Cache.ServicePermissions;

public static class InvoiceServicePermissions
{
    private const string GroupName = "InvoiceService.";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(InvoiceServicePermissions));
    }

    public static class SalesInvoices
    {
        private const string DomainName = $"{GroupName}{nameof(SalesInvoices)}";

        public const string PageView = $"{DomainName}:PageView";
        public const string Create = $"{DomainName}:Create";
        public const string Update = $"{DomainName}:Update";
        public const string Delete = $"{DomainName}:Delete";
    }
}


public static class InvoiceOperationPermissions
{
    private const string GroupName = "InvoiceOperation.";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(InvoiceOperationPermissions));
    }
}