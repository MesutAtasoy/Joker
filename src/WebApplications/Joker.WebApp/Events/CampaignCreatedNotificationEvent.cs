using Joker.EventBus;

namespace Joker.WebApp.Events;

public class CampaignCreatedNotificationEvent : IntegrationEvent
{
    private CampaignCreatedNotificationEvent(){}
        
    public Guid StoreId { get; private set; }
    public string StoreName { get; private set; }
    public Guid CampaignId { get; private set; }
    public string CampaignTitle { get; private set; }
    public string CampaignSlug { get; private set; }
    public string UserId  { get; private set; }
    public string UserName  { get; private set; }
}