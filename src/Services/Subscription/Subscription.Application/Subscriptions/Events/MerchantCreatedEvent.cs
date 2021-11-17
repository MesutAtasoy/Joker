using Joker.EventBus;

namespace Subscription.Application.Subscriptions.Events;

public class MerchantCreatedEvent : IntegrationEvent
{
    private MerchantCreatedEvent()
    {
    }
        
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid PricingPlanId { get; private set; }
    public string PricingPlanName { get; private set; }
    public Guid UserId { get; private set; }
}