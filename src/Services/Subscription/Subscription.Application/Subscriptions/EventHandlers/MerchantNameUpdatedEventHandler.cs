using System.Threading.Tasks;
using DotNetCore.CAP;
using Joker.CAP.IntegrationEvent;
using Subscription.Application.Subscriptions.Events;
using Subscription.Domain.Refs;
using Subscription.Domain.SubscriptionAggregate.Repositories;

namespace Subscription.Application.Subscriptions.EventHandlers
{
    public class MerchantNameUpdatedEventHandler: CAPIntegrationEventHandler<MerchantNameUpdatedEvent>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public MerchantNameUpdatedEventHandler(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }
        
        [CapSubscribe(nameof(MerchantNameUpdatedEvent))]
        public override async Task Handle(MerchantNameUpdatedEvent @event)
        {   
            var subscriptions = await _subscriptionRepository.GetAsync(x => x.Merchant.RefId == @event.MerchantId);

            var merchant = MerchantRef.Create(@event.MerchantId, @event.NewName);
            
            foreach (var subscription in subscriptions)
            {
                subscription.UpdateMerchant(merchant);
                await _subscriptionRepository.UpdateAsync(subscription.Id, subscription);
            }
        }
    }
}