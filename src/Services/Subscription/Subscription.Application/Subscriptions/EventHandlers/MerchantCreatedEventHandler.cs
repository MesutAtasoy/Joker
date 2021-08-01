using System.Threading.Tasks;
using DotNetCore.CAP;
using Joker.CAP.IntegrationEvent;
using Subscription.Application.Subscriptions.Events;
using Subscription.Domain.Refs;
using Subscription.Domain.SubscriptionAggregate.Repositories;
using Subscription.Infrastructure.Factories;

namespace Subscription.Application.Subscriptions.EventHandlers
{
    public class MerchantCreatedEventHandler : CAPIntegrationEventHandler<MerchantCreatedEvent>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public MerchantCreatedEventHandler(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }
        
        [CapSubscribe(nameof(MerchantCreatedEvent))]
        public override async Task Handle(MerchantCreatedEvent @event)
        {
            var subscriptionId = IdGenerationFactory.Create();

            var pricingPlan = PricingPlanRef.Create(@event.PricingPlanId, @event.PricingPlanName);
            var merchant = MerchantRef.Create(@event.Id, @event.Name);
            
            var subscription = new Domain.SubscriptionAggregate.Subscription(subscriptionId, pricingPlan);
            
            subscription.Subscribe(merchant, @event.UserId);

            await _subscriptionRepository.AddAsync(subscription);
        }
    }
}