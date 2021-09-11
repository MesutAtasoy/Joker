using System;
using Joker.EventBus;

namespace Favorite.Application.Campaigns.Events
{
    public class CampaignCreatedNotificationEvent : IntegrationEvent
    {
        private CampaignCreatedNotificationEvent(){}

        public CampaignCreatedNotificationEvent(
            Guid storeId,
            string storeName,
            Guid campaignId, 
            string campaignTitle,
            string campaignSlug,
            string userId,
            string userName)
        {
            StoreId = storeId;
            StoreName = storeName;
            CampaignId = campaignId;
            CampaignTitle = campaignTitle;
            CampaignSlug = campaignSlug;
            UserId = userId;
            UserName = userName;
        }
        
        public Guid StoreId { get; private set; }
        public string StoreName { get; private set; }
        public Guid CampaignId { get; private set; }
        public string CampaignTitle { get; private set; }
        public string CampaignSlug { get; private set; }
        public string UserId  { get; private set; }
        public string UserName  { get; private set; }
    }
}