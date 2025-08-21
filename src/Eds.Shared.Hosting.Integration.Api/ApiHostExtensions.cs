using Eds.Shared.Hosting.Workers;
using HsnSoft.Base;
using HsnSoft.Base.Application.Dtos;
using HsnSoft.Base.AspNetCore;
using HsnSoft.Base.AspNetCore.Hosting.Loader;
using HsnSoft.Base.Data;
using HsnSoft.Base.MultiTenancy;
using HsnSoft.Base.Timing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eds.Shared.Hosting.Integration.Api;

public static class ApiHostExtensions
{
    public static IServiceCollection ConfigureApiHost(this IServiceCollection services, IConfiguration configuration)
    {
        Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

        // Set paging limit value
        PagedLimitedResultRequestDto.MaxMaxResultCount = 1000000;

        // Set filter limit value
        SearchLimitedResultRequestDto.MaxMaxResultCount = 20;

        // services.Configure<BaseMultiTenancyOptions>(options =>
        // {
        //     options.IsEnabled = true;
        // });

        services.ConfigureSharedHost(configuration);

        services.AddBaseAspNetCoreContextCollection();
        services.AddBaseAspNetCoreJsonLocalization();
        services.AddBaseMultiTenancyServiceCollection();
        services.AddBaseTimingServiceCollection();

        services.AddControllers();

        // Loader functionality
        services.AddTransient<IBasicLoader, AppBasicLoader>();
        services.AddTransient<IBasicDataSeeder, DefaultBasicDataSeeder>();
        services.AddHostedService<LoaderHostedService>();

        return services;
    }
}