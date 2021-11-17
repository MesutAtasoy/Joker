using Joker.CAP;
using Joker.Identity.EventHandlers;
using Joker.Identity.Events;
using Joker.Identity.Models;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;


namespace Joker.Identity.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJokerIdentityContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<JokerIdentityDbContext>(options => options.UseNpgsql(configuration["ConnectionString"]));
        return services;
    }

    public static IServiceCollection AddJokerCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
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

    public static IServiceCollection AddJokerEvents(this IServiceCollection services)
    {
        services.RegisterCAPEvents(typeof(SubscribedEvent));
        services.RegisterCAPEventHandlers(typeof(SubscribedEventHandler));
        return services;
    }
        
    public static IServiceCollection AddJokerOpenTelemetry(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOpenTelemetryTracing(
            (builder) => builder
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddJaegerExporter(j =>
                {
                    j.AgentHost = configuration["jaeger:host"];
                    j.AgentPort = int.Parse(configuration["jaeger:port"]);
                })
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("JokerIdentity"))
        );

        return services;
    }

}