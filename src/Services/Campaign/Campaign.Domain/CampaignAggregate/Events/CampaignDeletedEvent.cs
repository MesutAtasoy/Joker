using System;
using Joker.Domain.DomainEvent;

namespace Campaign.Domain.CampaignAggregate.Events
{
    public class CampaignDeletedEvent : DomainEvent
    {
        public CampaignDeletedEvent(Guid campaignId, string title)
        {
            CampaignId = campaignId;
            Title = title;
        }

        public Guid CampaignId { get; private set; }
        public string Title { get; private set; }
    }
}