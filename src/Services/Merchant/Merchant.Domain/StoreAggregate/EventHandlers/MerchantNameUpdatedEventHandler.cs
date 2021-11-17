using DotNetCore.CAP;
using Joker.CAP.DomainEvent;
using Merchant.Domain.MerchantAggregate.Events;
using Merchant.Domain.StoreAggregate.Repositories;

namespace Merchant.Domain.StoreAggregate.EventHandlers;

public class MerchantNameUpdatedEventHandler : CAPDomainEventHandler<MerchantNameUpdatedEvent>
{
    private readonly IStoreRepository _storeRepository;

    public MerchantNameUpdatedEventHandler(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    [CapSubscribe(nameof(MerchantNameUpdatedEvent))]
    public override async Task Handle(MerchantNameUpdatedEvent domainEvent)
    {
        await _storeRepository.UpdateMerchantNameAsync(domainEvent.MerchantId, domainEvent.NewName);
    }
}