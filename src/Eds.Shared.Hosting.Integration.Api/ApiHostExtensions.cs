using Eds.Shared.Helper;
using Eds.Shared.Hosting.Workers;
using HsnSoft.Base;
using HsnSoft.Base.Application.Dtos;
using HsnSoft.Base.AspNetCore.Hosting.Loader;
using HsnSoft.Base.Data;
using HsnSoft.Base.Reflection;
using HsnSoft.Base.Validation.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

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

        services.ConfigureSharedAspNetCoreHost(configuration);

        // Loader functionality
        services.AddTransient<IBasicLoader, AppBasicLoader>();
        services.AddTransient<IBasicDataSeeder, DefaultBasicDataSeeder>();
        services.AddHostedService<LoaderHostedService>();

        return services;
    }

    public static void UseLocalization(this IApplicationBuilder app, Type serviceResourceType)
    {
        EnumHelper.Configure(app.ApplicationServices.GetService<IStringLocalizerFactory>(), serviceResourceType);
        LocalizedModelValidator.Configure(app.ApplicationServices.GetService<IStringLocalizerFactory>(), [
            serviceResourceType,
            typeof(ValidationResource)
        ]);
    }
}