using System;
using Joker.Domain.DomainEvent;

namespace Merchant.Domain.StoreAggregate.Events
{
    public class StoreNameUpdatedEvent : DomainEvent
    {
        private StoreNameUpdatedEvent(){}
        
        public StoreNameUpdatedEvent(Guid storeId, string oldName, string newName)
        {
            StoreId = storeId;
            OldName = oldName;
            NewName = newName;
        }
        
        public Guid StoreId { get; private set; }
        public string OldName { get; private set; }
        public string NewName { get; private set; }
    }
}