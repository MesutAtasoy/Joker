using System;
using Joker.EventBus;
using Joker.Identity.Dto;

namespace Joker.Identity.Events
{
    public class SubscribedEvent : IntegrationEvent
    {
        private SubscribedEvent() { }
        
        public Guid Id { get; private set; }
        public IdNameDto PricingPlan { get; private set; }
        public IdNameDto Merchant { get; private set; }
        public string ActivationCode { get; private set; }
        public DateTime ActivationDate { get; private set; }
        public Guid UserId { get; private set; }
    }
}