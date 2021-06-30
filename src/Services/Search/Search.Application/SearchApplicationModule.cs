using Joker.CAP;
using Microsoft.Extensions.DependencyInjection;

namespace Search.Application
{
    public static class SearchApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            //Event and EventHandlers
            services.RegisterCAPEvents(typeof(SearchApplicationModule));
            services.RegisterCAPEventHandlers(typeof(SearchApplicationModule));

            return services;
        } 
    }
}