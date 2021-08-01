using System;
using Joker.Domain.DomainEvent;

namespace Merchant.Domain.MerchantAggregate.Events
{
    public class MerchantCreatedEvent : DomainEvent
    {
        private MerchantCreatedEvent()
        {
        }

        public MerchantCreatedEvent(Guid id, string name, Guid pricingPlanId, string pricingPlanName)
        {
            Id = id;
            Name = name;
            PricingPlanId = pricingPlanId;
            PricingPlanName = pricingPlanName;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid PricingPlanId { get; private set; }
        public string PricingPlanName { get; private set; }
    }
}