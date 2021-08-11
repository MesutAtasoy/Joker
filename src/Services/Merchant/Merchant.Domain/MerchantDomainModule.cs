

using Joker.CAP;
using Microsoft.Extensions.DependencyInjection;

namespace Merchant.Domain
{
    public static class MerchantDomainModule
    {
        public static IServiceCollection AddDomainModule(this IServiceCollection services)
        {
            services.RegisterCAPEvents(typeof(MerchantDomainModule));
            services.RegisterCAPEventHandlers(typeof(MerchantDomainModule));
            return services;
        }
    }
}