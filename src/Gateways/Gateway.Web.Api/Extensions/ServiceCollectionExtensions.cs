using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Gateway.Web.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
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
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("GatewayWebApi"))
            );

            return services;
        }
    }
}