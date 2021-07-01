using Joker.CAP;
using Joker.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Search.Application.Campaigns.Initializers;
using Search.Application.Stores.Initializers;

namespace Search.Application
{
    public static class SearchApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            //Initializers
            services.AddScoped<ICampaignInitializer, CampaignInitializer>();
            services.AddScoped<IStoreInitializer, StoreInitializer>();
            
            services.AddInitializers(typeof(ICampaignInitializer), typeof(IStoreInitializer));
            
            //Event and EventHandlers
            services.RegisterCAPEvents(typeof(SearchApplicationModule));
            services.RegisterCAPEventHandlers(typeof(SearchApplicationModule));

            return services;
        } 
    }
}