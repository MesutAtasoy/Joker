using System;
using Joker.EventBus;

namespace Search.Application.Shared.Events;

public class StoreNameUpdatedEvent : IntegrationEvent
{
    private StoreNameUpdatedEvent(){}
    public Guid StoreId { get; private set; }
    public string OldName { get; private set; }
    public string NewName { get; private set; }
}