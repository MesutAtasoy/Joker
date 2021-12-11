using System.Threading.Tasks;
using DotNetCore.CAP;
using Joker.CAP.IntegrationEvent;
using Search.Application.Campaigns.Events;
using Search.Core.IndexManagers.Campaign;
using Search.Core.IndexModels;

namespace Search.Application.Campaigns.EventHandlers;

public class CampaignCreatedEventHandler : CAPIntegrationEventHandler<CampaignCreatedEvent>
{
    private readonly ICampaignIndexManager _campaignIndexManager;
        
    public CampaignCreatedEventHandler(ICampaignIndexManager campaignIndexManager)
    {
        _campaignIndexManager = campaignIndexManager;
    }
        
    [CapSubscribe(nameof(CampaignCreatedEvent))]
    public override async Task Handle(CampaignCreatedEvent @event)
    {
        var campaignIndexModel = new CampaignIndexModel
        {
            Id = @event.Id,
            Code = @event.Code,
            Condition = @event.Condition,
            Description = @event.Description,
            Slug = @event.Slug,
            SlugKey = @event.SlugKey,
            Title = @event.Title,
            StartTime = @event.StartTime,
            EndTime = @event.EndTime,
            StoreId = @event.StoreId,
            StoreName = @event.StoreName,
            MerchantId = @event.MerchantId,
            MerchantName = @event.MerchantName,
            BusinessDirectoryId = @event.BusinessDirectoryId,
            BusinessDirectoryName = @event.BusinessDirectoryName,
            OrganizationId = @event.OrganizationId
        };

        await _campaignIndexManager.AddOrUpdateAsync(campaignIndexModel);
    }
}