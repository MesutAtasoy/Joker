using System;
using Campaign.Domain.Refs;
using Joker.Domain.Entities;
using Joker.Exceptions;

namespace Campaign.Domain.CampaignAggregate
{
    public class CampaignBadge : DomainEntity
    {
        private CampaignBadge (){}
        
        public CampaignBadge(BadgeRef badge,
            DateTime expiryTime)
        {
            Check.NotNull(badge, nameof(badge));
            
            Badge = badge;
            ExpiryTime = expiryTime;
        }

        public BadgeRef Badge { get; private set; }
        public DateTime ExpiryTime { get; private set; }
    }
}