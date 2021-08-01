using System;
using Joker.Domain.DomainEvent;
using Subscription.Domain.Refs;

namespace Subscription.Domain.SubscriptionAggregate.Events
{
    public class SubscribedEvent : DomainEvent
    {
        public SubscribedEvent(Guid id, 
            PricingPlanRef pricingPlan,
            MerchantRef merchant,
            string activationCode,
            DateTime activationDate,
            Guid userId)
        {
            Id = id;
            PricingPlan = pricingPlan;
            Merchant = merchant;
            ActivationCode = activationCode;
            ActivationDate = activationDate;
            UserId = userId;
        }

        public Guid Id { get; private set; }
        public PricingPlanRef PricingPlan { get; private set; }
        public MerchantRef Merchant { get; private set; }
        public string ActivationCode { get; private set; }
        public DateTime ActivationDate { get; private set; }
        public Guid UserId { get; private set; }
    }
}