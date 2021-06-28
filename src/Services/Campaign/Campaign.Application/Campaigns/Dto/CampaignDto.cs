using System.Collections.Generic;

namespace Campaign.Application.Campaigns.Dto
{
    public class CampaignDto : CampaignListDto
    {
        public List<CampaignGalleryDto> CampaignGalleries { get; set; }
        public List<CampaignBadgeDto> Badges { get; set; }
    }
}