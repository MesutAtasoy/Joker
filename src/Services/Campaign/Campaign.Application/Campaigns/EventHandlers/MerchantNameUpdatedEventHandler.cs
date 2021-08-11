using System.Threading.Tasks;
using Campaign.Application.Campaigns.Events;
using Campaign.Domain.CampaignAggregate.Repositories;
using DotNetCore.CAP;
using Joker.CAP.IntegrationEvent;

namespace Campaign.Application.Campaigns.EventHandlers
{
    public class MerchantNameUpdatedEventHandler : CAPIntegrationEventHandler<MerchantNameUpdatedEvent>
    {
        private readonly ICampaignRepository _repository;

        public MerchantNameUpdatedEventHandler(ICampaignRepository repository)
        {
            _repository = repository;
        }

        [CapSubscribe(nameof(MerchantNameUpdatedEvent))]
        public override async Task Handle(MerchantNameUpdatedEvent @event)
        {
            await _repository.UpdateMerchantNameAsync(@event.MerchantId, @event.NewName);
        }
    }
}