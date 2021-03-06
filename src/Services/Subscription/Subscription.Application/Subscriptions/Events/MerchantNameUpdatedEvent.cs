using Joker.EventBus;

namespace Subscription.Application.Subscriptions.Events;

public class MerchantNameUpdatedEvent : IntegrationEvent
{
    private MerchantNameUpdatedEvent(){}
        
    public Guid MerchantId { get; private set; }
    public string OldName { get; private set; }
    public string NewName { get; private set; }
}