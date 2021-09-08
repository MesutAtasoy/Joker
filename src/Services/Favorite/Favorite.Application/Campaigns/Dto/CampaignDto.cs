using System;

namespace Favorite.Application.Campaigns.Dto
{
    public class CampaignDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string SlugKey { get; set; }
    }
}