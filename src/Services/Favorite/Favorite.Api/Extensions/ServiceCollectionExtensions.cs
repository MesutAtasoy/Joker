using Couchbase.Extensions.DependencyInjection;
using Couchbase.Linq;
using Favorite.Api.Interceptors;
using Favorite.Application.Services;
using Favorite.Infrastructure.Initializers;
using IdentityServer4.AccessTokenValidation;
using Joker.CAP;
using Joker.Consul;
using Joker.Mvc;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Favorite.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJokerGrpc(this IServiceCollection services)
    {
        services.AddTransient<GrpcExceptionInterceptor>();
        services.AddGrpc(x => x.Interceptors.Add<GrpcExceptionInterceptor>());
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

    public static IServiceCollection AddJokerCouchbase(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCouchbase(x =>
        {
            x.UserName = configuration.GetValue<string>("Couchbase:UserName");
            x.Password = configuration.GetValue<string>("Couchbase:Password");
            x.ConnectionString = configuration.GetValue<string>("Couchbase:ConnectionString");
            x.AddLinq();
        });

        return services;
    }

    public static IServiceCollection AddCouchbaseInitializers(this IServiceCollection services)
    {
        services.AddTransient<IBucketInitializers, BucketInitializers>();
        services.AddStartupInitializer();
        services.AddInitializers(typeof(IBucketInitializers));

        return services;
    }

    public static IServiceCollection AddJokerAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserService, UserService>();
            
        services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = configuration["urls:identityapi"];
                options.ApiName = "favoriteapi";
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
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("FavoriteApi"))
        );

        return services;
    }
}