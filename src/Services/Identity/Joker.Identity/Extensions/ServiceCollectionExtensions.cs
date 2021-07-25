using Joker.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Joker.Identity.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJokerIdentityContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<JokerIdentityDbContext>(options => options.UseNpgsql(configuration["ConnectionString"]));
            return services;
        }
    }
}