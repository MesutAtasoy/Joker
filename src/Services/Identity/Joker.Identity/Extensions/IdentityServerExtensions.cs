using Joker.Identity.Constants;
using Joker.Identity.Models;
using Joker.Identity.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Joker.Identity.Extensions
{
    public static class IdentityServerExtensions
    {
        public static IServiceCollection AddJokerIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddInMemoryIdentityResources(IdentityConfig.Ids)
                .AddInMemoryApiResources(IdentityConfig.ApiResources)
                .AddInMemoryApiScopes(IdentityConfig.ApiScopes)
                .AddInMemoryClients(IdentityConfig.Clients(configuration))
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<ProfileService>();
            
            builder.AddDeveloperSigningCredential();

            return services;
        }
    }
}