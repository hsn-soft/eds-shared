using System.Text.Json.Serialization;
using Eds.Shared.Contracts.Cache;
using Eds.Shared.Hosting.Microservices.Cache;
using Eds.Shared.Hosting.Microservices.Filters;
using Eds.Shared.Hosting.Microservices.Handlers;
using Eds.Shared.Hosting.Microservices.Middlewares;
using Eds.Shared.Hosting.Microservices.Workers;
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
using Newtonsoft.Json;

namespace Eds.Shared.Hosting.Microservices;

public static class MicroserviceHostExtensions
{
    public static IServiceCollection ConfigureMicroserviceHost(this IServiceCollection services, IConfiguration configuration, Type type)
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

        services.Configure<MicroserviceHostingSettings>(configuration.GetSection("HostingSettings"));

        services.AddControllers(options => { options.Filters.Add<RequestResponseActionFilterAttribute>(); })
            .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; })
            // Added for functional tests
            .AddApplicationPart(type.Assembly)
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                var hostingSettings = new MicroserviceHostingSettings();
                configuration.Bind("HostingSettings", hostingSettings);

                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.DefaultIgnoreCondition = hostingSettings.IgnoreNullValueForJsonResponse ? JsonIgnoreCondition.Always : JsonIgnoreCondition.Never;
            })
            .AddNewtonsoftJson(options =>
            {
                var hostingSettings = new MicroserviceHostingSettings();
                configuration.Bind("HostingSettings", hostingSettings);

                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                options.SerializerSettings.NullValueHandling = hostingSettings.IgnoreNullValueForJsonResponse ? NullValueHandling.Ignore : NullValueHandling.Include;
            });

        services.AddSingleton<IResponseExceptionHandler, ResponseExceptionHandler>();
        services.AddScoped<GlobalExceptionHandlerMiddleware>();

        // Service permission store worker
        services.AddSingleton<IServicePermissionProvider, DefaultServicePermissionProvider>();
        services.AddHostingRedis(configuration);
        services.AddTransient<ICachePermissionGrantRepository, CachePermissionGrantRepository>();
        services.AddHostedService<SynchServicePermissionStoreBackgroundService>();

        // Loader functionality
        services.AddTransient<IBasicLoader, AppBasicLoader>();
        services.AddTransient<IBasicDataSeeder, DefaultBasicDataSeeder>();
        services.AddHostedService<LoaderHostedService>();

        return services;
    }
}