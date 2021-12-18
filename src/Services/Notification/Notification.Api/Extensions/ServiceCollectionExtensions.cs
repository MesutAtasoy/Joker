using IdentityServer4.AccessTokenValidation;
using Joker.CAP;
using Joker.Consul;
using Joker.Mongo;
using Notification.Infrastructure;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Notification.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJokerMongo(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongo(x => configuration.GetSection("Mongo").Bind(x));
        services.AddMongoContext<UserNotificationContext>();
        return services;
    }
        
    public static IServiceCollection AddJokerConsul(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddConsulServices(x => configuration.GetSection("ServiceDiscovery").Bind(x));
        return services;
    }

    public static IServiceCollection AddJokerEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJokerCAP(capOptions =>
        {
            capOptions.UseRabbitMQ(x =>
            {
                x.Password = configuration["rabbitMQSettings:password"];
                x.UserName = configuration["rabbitMQSettings:username"];
                x.HostName = configuration["rabbitMQSettings:host"];
                x.Port = int.Parse(configuration["rabbitMQSettings:port"]);
            });

            capOptions.UseMongoDB(opt => // Persistence
            {
                opt.DatabaseConnection = configuration["mongo:ConnectionString"];
                opt.DatabaseName = configuration["mongo:DefaultDatabaseName"] + "-eventHistories";
                opt.PublishedCollection = "PublishedEvents";
                opt.ReceivedCollection = "ReceivedEvents";
            });

            capOptions.UseDashboard();
            capOptions.FailedRetryCount = 3;
            capOptions.FailedRetryInterval = 60;
        });
        return services;
    }

    public static IServiceCollection AddJokerAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = configuration["urls:identityapi"];
                options.ApiName = "notificationapi";
                options.ApiSecret = "apisecret";
                options.SupportedTokens = SupportedTokens.Reference;
                options.RequireHttpsMetadata = false;
            });

        return services;
    }

    public static IServiceCollection AddJokerAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization();
        return services;
    }
        
    public static IServiceCollection AddJokerOpenTelemetry(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOpenTelemetryTracing(
            (builder) => builder
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddGrpcClientInstrumentation()
                .AddJaegerExporter(j =>
                {
                    j.AgentHost = configuration["jaeger:host"];
                    j.AgentPort = int.Parse(configuration["jaeger:port"]);
                })
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("NotificationApi"))
        );

        return services;
    }
}