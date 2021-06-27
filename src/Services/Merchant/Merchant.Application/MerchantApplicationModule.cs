using AutoMapper;
using Joker.CAP;
using Merchant.Application.Merchants;
using Merchant.Domain.MerchantAggregate.Repositories;
using Merchant.Domain.StoreAggregate.Repositories;
using Merchant.Infrastructure;
using Merchant.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Merchant.Application
{
    public static class MerchantApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped<IMerchantRepository, MerchantRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            
            //Event and EventHandlers
            services.RegisterCAPEvents(typeof(MerchantApplicationModule));
            services.RegisterCAPEventHandlers(typeof(MerchantApplicationModule));

            //Automapper
            services.AddAutoMapper(typeof(MerchantMappingProfile));
            
            MerchantContext.ApplyConfiguration();
            
            return services;
        }
    }
}