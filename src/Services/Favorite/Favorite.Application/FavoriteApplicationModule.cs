using AutoMapper;
using Favorite.Application.Campaigns;
using Favorite.Application.Services.Notification;
using Favorite.Application.Shared;
using Favorite.Application.Stores;
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

            //Managers
            services.AddTransient<FavoriteCampaignManager>();
            services.AddTransient<FavoriteStoreManager>();
            
            services.AddTransient<IFavoriteCampaignRepository, FavoriteCampaignRepository>();
            services.AddTransient<IFavoriteStoreRepository, FavoriteStoreRepository>();
            
            //Services 
            services.AddTransient<INotificationService, NotificationService>();
            
            return services;
        }
    }
}