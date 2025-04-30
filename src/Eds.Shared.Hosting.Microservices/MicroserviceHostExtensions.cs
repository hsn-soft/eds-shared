using System.Text.Json.Serialization;
using Eds.Shared.Contracts.Cache;
using Eds.Shared.Helper;
using Eds.Shared.Hosting.Microservices.Cache;
using Eds.Shared.Hosting.Microservices.Filters;
using Eds.Shared.Hosting.Microservices.Handlers;
using Eds.Shared.Hosting.Microservices.Middlewares;
using Eds.Shared.Hosting.Microservices.Workers;
using Eds.Shared.Hosting.Worker;
using HsnSoft.Base;
using HsnSoft.Base.Application.Dtos;
using HsnSoft.Base.AspNetCore.Hosting.Loader;
using HsnSoft.Base.Data;
using HsnSoft.Base.Domain.Repositories;
using HsnSoft.Base.EventBus.RabbitMQ.Configs;
using HsnSoft.Base.Reflection;
using HsnSoft.Base.Validation.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using RabbitMQ.Client;
using StackExchange.Redis;

namespace Eds.Shared.Hosting.Microservices;

public static class MicroserviceHostExtensions
{
    public static IServiceCollection ConfigureMicroserviceHost(this IServiceCollection services, IConfiguration configuration)
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

    public static IServiceCollection AddAdvancedController(this IServiceCollection services, IConfiguration configuration, Type type)
    {
        services.Configure<MicroserviceHostingSettings>(configuration.GetSection("HostingSettings"));

        services.AddControllers(options =>
            {
                options.Filters.Add(typeof(RequestResponseActionFilterAttribute));
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            })
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
        AddMicroserviceRedis(services, configuration);
        services.AddTransient<ICachePermissionGrantRepository, CachePermissionGrantRepository>();
        services.AddHostedService<SynchServicePermissionStoreBackgroundService>();

        return services;
    }

    public static IServiceCollection AddMicroserviceUserTenantChecker(this IServiceCollection services) => services.AddScoped<UserTenantCheckerMiddleware>();
    public static void UseUserTenantChecker(this IApplicationBuilder app) => app.UseMiddleware<UserTenantCheckerMiddleware>();

    private static void AddMicroserviceRedis(this IServiceCollection services, IConfiguration configuration)
    {
        // services.Configure<BaseDistributedCacheOptions>(options =>
        // {
        //     options.KeyPrefix = "HsNsH:";
        // });

        // var dataProtectionBuilder = services.AddDataProtection().SetApplicationName("eShop");
        // var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
        // dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "eShop-Protection-Keys");

        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var redisConf = ConfigurationOptions.Parse(configuration["Redis:Configuration"] ?? throw new InvalidOperationException(), true);
            redisConf.ResolveDns = true;

            return ConnectionMultiplexer.Connect(redisConf);
        });

        // var connectionString = Configuration["Redis:Configuration"];
        // var multiplexer = ConnectionMultiplexer.Connect(connectionString);
        // services.AddSingleton<IConnectionMultiplexer>(sp => multiplexer);

        services.AddSingleton(typeof(IRedisRepository<>), typeof(RedisRepository<>));
    }

    public static void UseLocalization(this IApplicationBuilder app, Type serviceResourceType)
    {
        EnumHelper.Configure(app.ApplicationServices.GetService<IStringLocalizerFactory>(), serviceResourceType);
        LocalizedModelValidator.Configure(app.ApplicationServices.GetService<IStringLocalizerFactory>(), [
            serviceResourceType,
            typeof(ValidationResource)
        ]);
    }

    public static IServiceCollection AddMicroserviceHealthChecks(this IServiceCollection services, IConfiguration configuration, string serviceName,
        bool checkMongo = false, string mongoConnectionName = null,
        bool checkPostgresql = false, string postgresqlConnectionName = null,
        bool checkRedis = false,
        bool checkBroker = false)
    {
        var healtCheckPrefix = serviceName ?? "service";
        var serviceProvider = services.BuildServiceProvider();
        var hcBuilder = services.AddHealthChecks();

        hcBuilder.AddCheck("self-check", () => HealthCheckResult.Healthy(), tags: ["dependencies"]);

        // hcBuilder.AddUrlGroup
        // (
        //     new Uri(configuration["AuthServer:Authority"] ?? throw new InvalidOperationException()),
        //     name: $"{healtCheckPrefix}-auth-check",
        //     tags: new[] { "auth" }
        // );

        if (checkMongo)
        {
            hcBuilder.AddMongoDb(_ =>
                {
                    var mongoUrl = MongoUrl.Create(configuration.GetConnectionString(mongoConnectionName ?? throw new ArgumentNullException(nameof(mongoConnectionName))) ?? throw new InvalidOperationException());
                    return new MongoClient(MongoClientSettings.FromConnectionString(mongoUrl.Url)).GetDatabase(mongoUrl.DatabaseName);
                },
                name: $"{healtCheckPrefix}-mongo-check",
                tags: ["dependencies", "database"]
            );
            // hcBuilder.AddMongoDb(
            //     mongodbConnectionString: configuration.GetConnectionString(mongoConnectionName) ?? throw new InvalidOperationException(),
            //     name: $"{healtCheckPrefix}-mongo-check",
            //     tags: new[] { "dependencies", "database" }
            // );
        }

        if (checkPostgresql)
        {
            hcBuilder.AddNpgSql(
                connectionString: configuration.GetConnectionString(postgresqlConnectionName ?? throw new ArgumentNullException(nameof(postgresqlConnectionName))) ?? throw new InvalidOperationException(),
                name: $"{healtCheckPrefix}-postgresql-check",
                tags: ["dependencies", "database"]
            );
        }

        if (checkRedis)
        {
            // builder.AddRedis(configuration["Redis:Configuration"], name: "redis", tags: new[] { "dependencies" });
            var redisConnector = serviceProvider.GetRequiredService<IConnectionMultiplexer>();
            hcBuilder.AddRedis(
                connectionMultiplexer: redisConnector,
                name: $"{healtCheckPrefix}-redis-check",
                tags: ["dependencies", "database"]
            );
        }

        if (checkBroker)
        {
            var rabbitMq = new RabbitMqConnectionSettings();
            configuration.Bind("RabbitMQ:Connection", rabbitMq);
            hcBuilder.AddRabbitMQ
            (
                sp =>
                {
                    var conSettings = sp.GetRequiredService<IOptions<RabbitMqConnectionSettings>>();
                    return new ConnectionFactory
                    {
                        HostName = conSettings.Value.HostName,
                        Port = conSettings.Value.Port,
                        UserName = conSettings.Value.UserName,
                        Password = conSettings.Value.Password,
                        VirtualHost = conSettings.Value.VirtualHost,
                        RequestedHeartbeat = TimeSpan.FromSeconds(60)
                    }.CreateConnectionAsync().GetAwaiter().GetResult();
                },
                //amqp://user:pass@host:10000/vhost	"user"	"pass"	"host"	10000	"vhost"
                //$"amqp://{rabbitMq.UserName}:{rabbitMq.Password}@{rabbitMq.HostName}:{rabbitMq.Port}",
                name: $"{healtCheckPrefix}-rabbitmq-check",
                tags: ["dependencies", "broker"]
            );

            // var connectionSettings = serviceProvider.GetRequiredService<IOptions<KafkaConnectionSettings>>().Value;
            // var producerConfig = new ProducerConfig
            // {
            //     BootstrapServers = $"{connectionSettings.HostName}:{connectionSettings.Port}",
            // };
            // hcBuilder.AddKafka(producerConfig, name: $"{healtCheckPrefix}-kafka-check", tags: new[] { "dependencies" });
        }

        return services;
    }

    public static void UseMicroserviceHealthChecks(this IApplicationBuilder app)
    {
        // app.UseHealthChecks("/StartupCheck", new HealthCheckOptions { Predicate = _ => true });
        // app.UseHealthChecks("/LivenessCheck", new HealthCheckOptions { Predicate = _ => true });
        // app.UseHealthChecks("/ReadinessCheck", new HealthCheckOptions { Predicate = _ => true });

        app.UseHealthChecks("/StartupCheck", new HealthCheckOptions
        {
            Predicate = r => r.Tags.Contains("dependencies"),
            ResponseWriter = CustomHealthCheckResponse
        });
        app.UseHealthChecks("/LivenessCheck", new HealthCheckOptions { Predicate = r => r.Name.Equals("self-check") });
        app.UseHealthChecks("/ReadinessCheck", new HealthCheckOptions
        {
            Predicate = r => r.Tags.Contains("dependencies"),
            //ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            ResponseWriter = CustomHealthCheckResponse
        });
    }

    private static async Task CustomHealthCheckResponse(HttpContext context, HealthReport report)
    {
        context.Response.ContentType = "application/json";
        var result = System.Text.Json.JsonSerializer.Serialize(
            new
            {
                status = report.Status.ToString(),
                checks = report.Entries.Select(e => new
                {
                    name = e.Key,
                    status = e.Value.Status.ToString(),
                    exception = e.Value.Exception?.Message,
                    duration = e.Value.Duration
                })
            });

        await context.Response.WriteAsync(result);
    }
}