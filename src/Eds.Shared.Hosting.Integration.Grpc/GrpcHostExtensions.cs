using Eds.Shared.Hosting.Workers;
using HsnSoft.Base;
using HsnSoft.Base.AspNetCore;
using HsnSoft.Base.AspNetCore.Hosting.Loader;
using HsnSoft.Base.Data;
using HsnSoft.Base.MultiTenancy;
using HsnSoft.Base.Timing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eds.Shared.Hosting.Integration.Grpc;

public static class GrpcHostExtensions
{
    public static IServiceCollection ConfigureGrpcHost(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureSharedHost(configuration);

        services.AddBaseAspNetCoreContextCollection();
        services.AddBaseAspNetCoreJsonLocalization();
        services.AddBaseMultiTenancyServiceCollection();
        services.AddBaseTimingServiceCollection();

        services.AddGrpc();

        // Loader functionality
        services.AddTransient<IBasicLoader, AppBasicLoader>();
        services.AddTransient<IBasicDataSeeder, DefaultBasicDataSeeder>();
        services.AddHostedService<LoaderHostedService>();

        return services;
    }
}