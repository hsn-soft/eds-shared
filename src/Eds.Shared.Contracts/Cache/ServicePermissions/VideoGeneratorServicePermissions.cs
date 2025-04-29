using HsnSoft.Base.Reflection;

namespace Eds.Shared.Contracts.Cache.ServicePermissions;

public static class VideoGeneratorServicePermissions
{
    private const string GroupName = "VideoGeneratorService.";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(VideoGeneratorServicePermissions));
    }
}

public static class VideoGeneratorOperationPermissions
{
    private const string GroupName = "VideoGeneratorOperation.";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(VideoGeneratorOperationPermissions));
    }
}