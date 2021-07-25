using Campaign.Api.Interceptors;
using Campaign.Infrastructure;
using IdentityServer4.AccessTokenValidation;
using Joker.CAP;
using Joker.Consul;
using Joker.Mongo;
using Joker.Mongo.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Campaign.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJokerGrpc(this IServiceCollection services)
        {
            services.AddTransient<GrpcExceptionInterceptor>();
            services.AddGrpc(x => x.Interceptors.Add<GrpcExceptionInterceptor>());
            return services;
        }

        public static IServiceCollection AddJokerMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongo(x => configuration.GetSection("Mongo").Bind(x));
            services.AddMongoContext<CampaignContext>();
            services.AddMongoDomainRepositories();
            return services;
        }

        public static IServiceCollection AddJokerConsul(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterConsulServices(x => configuration.GetSection("ServiceDiscovery").Bind(x));
            return services;
        }

        public static IServiceCollection AddJokerEventBus(this IServiceCollection services,
            IConfiguration configuration)
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

        public static IServiceCollection AddJokerAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration["urls:identityapi"];
                    options.ApiName = "campaignapi";
                    options.ApiSecret = "apisecret";
                });

            return services;
        }
    }
}