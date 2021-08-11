using System.Threading.Tasks;
using Campaign.Application.Campaigns.Events;
using Campaign.Domain.CampaignAggregate.Repositories;
using DotNetCore.CAP;
using Joker.CAP.IntegrationEvent;

namespace Campaign.Application.Campaigns.EventHandlers
{
    public class StoreNameUpdatedEventHandler : CAPIntegrationEventHandler<StoreNameUpdatedEvent>
    {
        private readonly ICampaignRepository _repository;

        public StoreNameUpdatedEventHandler(ICampaignRepository repository)
        {
            _repository = repository;
        }

        [CapSubscribe(nameof(StoreNameUpdatedEvent))]
        public override async Task Handle(StoreNameUpdatedEvent @event)
        {
            await _repository.UpdateStoreNameAsync(@event.StoreId, @event.NewName);
        }
    }
}