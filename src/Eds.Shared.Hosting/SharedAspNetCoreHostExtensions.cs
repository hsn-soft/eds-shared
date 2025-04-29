using System.Reflection;
using System.Security.Cryptography;
using Eds.Shared.Hosting.Middlewares;
using HsnSoft.Base;
using HsnSoft.Base.AspNetCore;
using HsnSoft.Base.AspNetCore.Logging;
using HsnSoft.Base.AspNetCore.Mvc.Services;
using HsnSoft.Base.AspNetCore.Security.Claims;
using HsnSoft.Base.AspNetCore.Serilog;
using HsnSoft.Base.AspNetCore.Serilog.Persistent;
using HsnSoft.Base.AspNetCore.Tracing;
using HsnSoft.Base.Authorization;
using HsnSoft.Base.EventBus;
using HsnSoft.Base.EventBus.Logging;
using HsnSoft.Base.EventBus.RabbitMQ;
using HsnSoft.Base.EventBus.RabbitMQ.Configs;
using HsnSoft.Base.EventBus.RabbitMQ.Connection;
using HsnSoft.Base.EventBus.SubManagers;
using HsnSoft.Base.Logging;
using HsnSoft.Base.MultiTenancy;
using HsnSoft.Base.Security.Claims;
using HsnSoft.Base.Timing;
using HsnSoft.Base.Tracing;
using HsnSoft.Base.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Eds.Shared.Hosting;

public static class SharedAspNetCoreHostExtensions
{
    // All hostings
    public static IServiceCollection ConfigureSharedHost(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();
        services.AddEndpointsApiExplorer();

        services.AddHttpContextAccessor();
        services.AddScoped<IActionContextAccessor, ActionContextAccessor>();

        services.AddSingleton<IBaseLogger, BaseLogger>();
        services.AddSingleton<IFrameworkLogger, FrameworkLogger>();

        services.Configure<HostingSettings>(configuration.GetSection("HostingSettings"));
        services.AddSingleton<IRequestResponseLogger, RequestLogger>();
        services.AddScoped<RequestResponseLoggerMiddleware>();

        services.AddScoped<SearchEngineAgentMiddleware>();

        return services;
    }

    // Microservices and Mvc Apps Base
    public static IServiceCollection ConfigureSharedAspNetCoreHost(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureSharedHost(configuration);

        services.AddBaseAspNetCoreContextCollection();
        services.AddBaseAspNetCoreJsonLocalization();
        services.AddBaseMultiTenancyServiceCollection();
        services.AddBaseTimingServiceCollection();

        services.AddControllers();

        return services;
    }

    public static IServiceCollection AddMvcRazorRender(this IServiceCollection services)
    {
        services.AddScoped<IRazorRenderService, RazorRenderService>();

        return services;
    }

    public static IServiceCollection AddJwtServerAuthentication(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env, string audience)
    {
        services.AddSingleton<RsaSecurityKey>(_ =>
        {
            var rsa = RSA.Create();
            try
            {
                rsa.FromXmlString(File.ReadAllText("./public_key.xml"));
            }
            catch (IOException ioException)
            {
                throw new BaseException("You need to provide public_key.xml to use auth", ioException);
            }

            return new RsaSecurityKey(rsa);
        });

        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // For disable Account/Login redirect when unauthorized identity jwt server
            })
            .AddJwtBearer(options =>
            {
                // var jwtSecretKey = configuration["JwtAuthServer:JwtServerSecretKey"];
                // if (string.IsNullOrWhiteSpace(jwtSecretKey)) throw new InvalidOperationException("Unknown JWT Server Key");
                // var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey));

                var asymmetricPublicKey = services.BuildServiceProvider().GetRequiredService<RsaSecurityKey>();

                options.RequireHttpsMetadata = env.IsHhsProduction() && Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                options.Audience = audience; // Api audience
                options.IncludeErrorDetails = !env.IsHhsProduction();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true, // JWTs are required to have "aud" property set for Api audience

                    ValidateIssuer = env.IsHhsProduction(),
                    ValidIssuer = configuration["AuthServer:Authority"],

                    RequireExpirationTime = true, // JWTs are required to have "exp" property set
                    ValidateLifetime = true, // The "exp" will be validated
                    ClockSkew = TimeSpan.Zero,

                    RequireSignedTokens = true,
                    ValidateIssuerSigningKey = true,
                    // IssuerSigningKey = symmetricKey
                    IssuerSigningKey = asymmetricPublicKey,

                    ValidTypes = ["JWT"]
                };
            });

        return services;
    }

    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services, string[] servicePermissions)
    {
        services.AddBaseAuthorizationServiceCollection();

        services.AddAuthorization(options =>
        {
            foreach (var permissionPolicyName in servicePermissions)
            {
                options.AddPolicy(permissionPolicyName, policyBuilder =>
                {
                    // policyBuilder.RequireAuthenticatedUser();
                    // policyBuilder.RequireClaim("role");
                    policyBuilder.RequireUserPermission(permissionPolicyName);
                });
            }
        });

        return services;
    }

    public static IServiceCollection AddCorsSettings(this IServiceCollection services, string corsName)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(corsName, policy =>
            {
                policy.SetIsOriginAllowed(isOriginAllowed: _ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        return services;
    }

    public static void UseCorsSettings(this IApplicationBuilder app, string corsName)
    {
        app.UseCors(corsName);
    }


    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
    {
        services.AddRabbitMqEventBus(configuration);

        // Add All Event Handlers
        services.AddEventHandlers(assembly);

        return services;
    }

    private static void AddRabbitMqEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        // Add configuration objects
        services.Configure<RabbitMqConnectionSettings>(configuration.GetSection("RabbitMq:Connection"));
        services.Configure<RabbitMqEventBusConfig>(configuration.GetSection("RabbitMq:EventBus"));

        // Add event bus instances
        services.AddHttpContextAccessor();
        services.AddSingleton<ICurrentPrincipalAccessor, HttpContextCurrentPrincipalAccessor>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddSingleton<ITraceAccesor, HttpContextTraceAccessor>();
        services.AddSingleton<IEventBusLogger, EventBusLogger>();
        services.AddSingleton<IRabbitMqPersistentConnection, RabbitMqPersistentConnection>();
        services.AddSingleton<IEventBusSubscriptionManager, InMemoryEventBusSubscriptionManager>();
        services.AddSingleton<IEventBus, EventBusRabbitMq>(sp => new EventBusRabbitMq(sp));
    }

    private static void AddEventHandlers(this IServiceCollection services, Assembly assembly)
    {
        var refType = typeof(IIntegrationEventHandler);
        var types = assembly.GetTypes()
            .Where(p => refType.IsAssignableFrom(p) && p is { IsInterface: false, IsAbstract: false });

        foreach (var type in types.ToList())
        {
            services.AddTransient(type);
        }
    }

    public static void UseEventBus(this IApplicationBuilder app, Assembly assembly, Dictionary<string, ushort> eventFetchCounts = null)
    {
        var refType = typeof(IIntegrationEventHandler);
        var eventHandlerTypes = assembly.GetTypes()
            .Where(p => refType.IsAssignableFrom(p) && p is { IsInterface: false, IsAbstract: false }).ToList();

        if (eventHandlerTypes is not { Count: > 0 }) return;

        var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

        foreach (var eventHandlerType in eventHandlerTypes)
        {
            var eventType = eventHandlerType.GetInterfaces().First(x => x.IsGenericType).GenericTypeArguments[0];

            ushort fetchCount = 0;
            if (eventFetchCounts is { Count: > 0 })
            {
                if (eventFetchCounts.TryGetValue(eventType.Name, out var eventFetchCount))
                {
                    fetchCount = eventFetchCount;
                }
            }

            eventBus.Subscribe(eventType, eventHandlerType, fetchCount < 1 ? (ushort)1 : fetchCount);
        }
    }

}