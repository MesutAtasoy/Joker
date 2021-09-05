using AutoMapper;
using Favorite.Application.Shared;
using Favorite.Core.Repositories;
using Favorite.Infrastructure.Repositories;
using Joker.CAP;
using Microsoft.Extensions.DependencyInjection;

namespace Favorite.Application
{
    public static class FavoriteApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            //Event and EventHandlers
            services.RegisterCAPEvents(typeof(FavoriteApplicationModule));
            services.RegisterCAPEventHandlers(typeof(FavoriteApplicationModule));

            //Automapper
            services.AddAutoMapper(typeof(SharedMappingProfile));
            
            services.AddTransient<IFavoriteCampaignRepository, FavoriteCampaignRepository>();
            services.AddTransient<IFavoriteStoreRepository, FavoriteStoreRepository>();
            return services;
        }
    }
}