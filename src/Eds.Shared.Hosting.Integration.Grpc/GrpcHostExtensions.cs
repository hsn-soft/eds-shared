using Eds.Shared.Hosting.Worker;
using HsnSoft.Base;
using HsnSoft.Base.AspNetCore.Hosting.Loader;
using HsnSoft.Base.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eds.Shared.Hosting.Integration.Grpc;

public static class GrpcHostExtensions
{
    public static IServiceCollection ConfigureGrpcHost(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureSharedHost(configuration);
        services.AddGrpc();

        // Loader functionality
        services.AddTransient<IBasicLoader, AppBasicLoader>();
        services.AddTransient<IBasicDataSeeder, DefaultBasicDataSeeder>();
        services.AddHostedService<LoaderHostedService>();

        return services;
    }
}