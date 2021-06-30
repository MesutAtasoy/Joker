using System.Threading.Tasks;
using DotNetCore.CAP;
using Joker.CAP.IntegrationEvent;
using Search.Application.Campaigns.Events;
using Search.Core.IndexManagers.Campaign;
using Search.Core.IndexModels;

namespace Search.Application.Campaigns.EventHandlers
{
    public class CampaignUpdatedEventHandler : CAPIntegrationEventHandler<CampaignUpdatedEvent>
    {
        private readonly ICampaignIndexManager _campaignIndexManager;
        
        public CampaignUpdatedEventHandler(ICampaignIndexManager campaignIndexManager)
        {
            _campaignIndexManager = campaignIndexManager;
        }
        
        [CapSubscribe(nameof(CampaignUpdatedEvent))]
        public override async Task Handle(CampaignUpdatedEvent @event)
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
                PreviewImageUrl = @event.PreviewImageUrl
            };

            await _campaignIndexManager.AddOrUpdateAsync(campaignIndexModel);
        }
    }
}