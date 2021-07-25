using Joker.Consul;
using Joker.EntityFrameworkCore;
using Location.Api.Interceptors;
using Location.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Location.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJokerGrpc(this IServiceCollection services)
        {
            services.AddTransient<GrpcExceptionInterceptor>();
            services.AddGrpc(x => x.Interceptors.Add<GrpcExceptionInterceptor>());
            return services;
        }

        public static IServiceCollection AddJokerContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddJokerNpDbContext<LocationContext>(x =>
            {
                x.ConnectionString = configuration["connectionString"];
                x.EnableMigration = true;
                x.MaxRetryCount = 3;
            });
            return services;
        }
        
        public static IServiceCollection AddJokerConsul(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterConsulServices(x => configuration.GetSection("ServiceDiscovery").Bind(x));
            return services;
        }
    }
}