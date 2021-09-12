using Joker.CAP;
using Joker.Consul;
using Joker.ElasticSearch;
using Joker.ElasticSearch.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Search.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
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

        public static IServiceCollection AddJokerConsul(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.RegisterConsulServices(x => configuration.GetSection("ServiceDiscovery").Bind(x));
            return services;
        }

        public static IServiceCollection AddElasticService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddJokerElasticService(x =>
            {
                x.ConnectionString = new ElasticSearchConnectionString
                {
                    HostUrl = configuration["elasticSearch:host"]
                };
            });
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
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("SearchApi"))
            );

            return services;
        }
    }
}