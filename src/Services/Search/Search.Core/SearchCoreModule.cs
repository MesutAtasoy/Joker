using Joker.ElasticSearch.Service;
using Microsoft.Extensions.DependencyInjection;
using Search.Core.IndexManagers;
using Search.Core.IndexManagers.Campaign;
using Search.Core.IndexManagers.Store;

namespace Search.Core
{
    public static class SearchCoreModule
    {
        public static IServiceCollection AddCoreModule(this IServiceCollection services)
        {
            services.AddTransient<IElasticSearchManager, ElasticIndexManager>();
            services.AddTransient<ICampaignIndexManager, CampaignIndexManager>();
            services.AddTransient<IStoreIndexManager, StoreIndexManager>();
            return services;
        }
    }
}