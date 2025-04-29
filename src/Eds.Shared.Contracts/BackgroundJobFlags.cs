namespace Eds.Shared.Contracts;

public static class BackgroundJobFlags
{
    public static bool TriggerSynchPermissionCacheStore { get; set; } = false;
    public static bool TriggerSynchPermissionServiceStore { get; set; } = false;
}