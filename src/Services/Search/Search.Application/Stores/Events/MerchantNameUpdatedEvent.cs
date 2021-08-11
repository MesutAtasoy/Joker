using System;
using Joker.EventBus;

namespace Search.Application.Stores.Events
{
    public class MerchantNameUpdatedEvent : IntegrationEvent
    {
        private MerchantNameUpdatedEvent(){}
        
        public Guid MerchantId { get; private set; }
        public string OldName { get; private set; }
        public string NewName { get; private set; }
    }
}