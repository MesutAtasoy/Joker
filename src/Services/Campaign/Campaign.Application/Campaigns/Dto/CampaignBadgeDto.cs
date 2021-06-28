using System;
using Campaign.Application.Shared.Dto;

namespace Campaign.Application.Campaigns.Dto
{
    public class CampaignBadgeDto
    {
        public IdNameDto Badge { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}