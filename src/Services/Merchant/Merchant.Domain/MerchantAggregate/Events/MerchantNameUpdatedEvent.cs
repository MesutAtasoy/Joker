using System;
using Joker.Domain.DomainEvent;

namespace Merchant.Domain.MerchantAggregate.Events
{
    public class MerchantNameUpdatedEvent : DomainEvent
    {
        private MerchantNameUpdatedEvent(){}
        
        public MerchantNameUpdatedEvent(Guid merchantId, string oldName, string newName)
        {
            MerchantId = merchantId;
            OldName = oldName;
            NewName = newName;
        }
        
        public Guid MerchantId { get; private set; }
        public string OldName { get; private set; }
        public string NewName { get; private set; }
    }
}