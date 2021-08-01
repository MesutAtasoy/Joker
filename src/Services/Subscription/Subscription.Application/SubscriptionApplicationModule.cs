using AutoMapper;
using Joker.CAP;
using Microsoft.Extensions.DependencyInjection;
using Subscription.Application.Subscriptions;
using Subscription.Domain.SubscriptionAggregate.Repositories;
using Subscription.Infrastructure;
using Subscription.Infrastructure.Repositories;

namespace Subscription.Application
{
    public static class SubscriptionApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

            //Event and EventHandlers
            services.RegisterCAPEvents(typeof(SubscriptionApplicationModule));
            services.RegisterCAPEventHandlers(typeof(SubscriptionApplicationModule));

            //Automapper
            services.AddAutoMapper(typeof(SubscriptionMappingProfile));

            SubscriptionContext.ApplyConfiguration();

            return services;
        }
    }
}