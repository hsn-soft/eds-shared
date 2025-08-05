using Eds.Shared.Hosting.Workers;
using HsnSoft.Base;
using HsnSoft.Base.AspNetCore.Hosting.Loader;
using HsnSoft.Base.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eds.Shared.Hosting.Integration.WcfCore;

public static class WcfCoreHostExtensions
{
    public static IServiceCollection ConfigureWcfCoreHost(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureSharedHost(configuration);

        // Loader functionality
        services.AddTransient<IBasicLoader, AppBasicLoader>();
        services.AddTransient<IBasicDataSeeder, DefaultBasicDataSeeder>();
        services.AddHostedService<LoaderHostedService>();

        return services;
    }
}