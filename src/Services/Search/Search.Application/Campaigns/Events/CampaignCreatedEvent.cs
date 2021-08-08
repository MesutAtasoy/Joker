using System;
using Joker.EventBus;

namespace Search.Application.Campaigns.Events
{
    public class CampaignCreatedEvent : IntegrationEvent
    {
        private CampaignCreatedEvent(){}

        public Guid Id { get; private set; }
        public Guid StoreId { get; private set; }
        public string StoreName { get; private set; }
        public Guid MerchantId { get; private set; }
        public string MerchantName { get; private set; }
        public Guid BusinessDirectoryId { get; private set; }
        public string BusinessDirectoryName { get; private set; }
        public string Slug { get; private set; }
        public string SlugKey { get; private set; }
        public string Title { get; private set; }
        public string Code { get; private set; }
        public string Description { get; private set; }
        public string Condition { get; private set; }
        public string PreviewImageUrl { get; private set; }
        public DateTime? StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
        public string Channel { get; private set; }
    }
}