using System;
using Aggregator.Api.Models.Shared;

namespace Aggregator.Api.Models.Campaign
{
    public class CampaignModel
    {
        public Guid Id { get; set; }
        public IdName Store { get; set; }
        public IdName BusinessDirectory { get; set; }
        public string Slug { get; set; }
        public string SlugKey { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Condition { get; set; }
        public string PreviewImageUrl { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Channel { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}