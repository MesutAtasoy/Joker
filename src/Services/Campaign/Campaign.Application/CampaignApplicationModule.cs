using AutoMapper;
using Campaign.Application.Shared;
using Campaign.Domain.CampaignAggregate.Repositories;
using Campaign.Infrastructure;
using Campaign.Infrastructure.Repositories;
using Joker.CAP;
using Microsoft.Extensions.DependencyInjection;

namespace Campaign.Application
{
    public static class CampaignApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped<ICampaignRepository, CampaignRepository>();

            //Event and EventHandlers
            services.RegisterCAPEvents(typeof(CampaignApplicationModule));
            services.RegisterCAPEventHandlers(typeof(CampaignApplicationModule));
            
            //Automapper
            services.AddAutoMapper(typeof(SharedMappingProfile));
            
            CampaignContext.ApplyConfiguration();
            
            return services;
        }
    }
}