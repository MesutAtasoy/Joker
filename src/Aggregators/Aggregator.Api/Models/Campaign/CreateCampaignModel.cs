using System;
using Aggregator.Api.Models.Shared;

namespace Aggregator.Api.Models.Campaign
{
    public class CreateCampaignModel
    {
        public IdName Store { get; set; }
        public IdName Merchant { get; set; }
        public IdName BusinessDirectory { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Condition { get; set; }
        public string PreviewImageUrl { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Channel { get; set; }
    }
}