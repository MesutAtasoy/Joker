using Management.Core.Repositories;
using Management.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Management.Application;

public static class ManagementApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        //Repositories
        services.AddScoped<IBusinessDirectoryRepository, BusinessDirectoryRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();
        services.AddScoped<IPricingPlanRepository, PricingPlanRepository>();
        services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
        services.AddScoped<IBadgeRepository, BadgeRepository>();
            
        return services;
    } 
}