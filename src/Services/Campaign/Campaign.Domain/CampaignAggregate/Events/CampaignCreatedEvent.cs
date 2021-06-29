using Joker.Domain.DomainEvent;

namespace Campaign.Domain.CampaignAggregate.Events
{
    public class CampaignCreatedEvent : DomainEvent
    {
        private CampaignCreatedEvent(){}
        
        public CampaignCreatedEvent(Campaign campaign)
        {
            Campaign = campaign;
        }

        public Campaign Campaign { get; private set; }
    }
}