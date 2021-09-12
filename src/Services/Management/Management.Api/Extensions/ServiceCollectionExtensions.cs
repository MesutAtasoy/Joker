using Joker.Consul;
using Joker.EntityFrameworkCore;
using Management.Api.Interceptors;
using Management.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Management.Api.Extensions
{
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
            services.RegisterConsulServices(x => configuration.GetSection("ServiceDiscovery").Bind(x));
            return services;
        }

        public static IServiceCollection AddJokerContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddJokerNpDbContext<ManagementContext>(x =>
            {
                x.ConnectionString = configuration["connectionString"];
                x.EnableMigration = true;
                x.MaxRetryCount = 3;
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
                    .AddGrpcClientInstrumentation()
                    .AddJaegerExporter(j =>
                    {
                        j.AgentHost = configuration["jaeger:host"];
                        j.AgentPort = int.Parse(configuration["jaeger:port"]);
                    })
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("ManagementApi"))
            );

            return services;
        }
    }
}