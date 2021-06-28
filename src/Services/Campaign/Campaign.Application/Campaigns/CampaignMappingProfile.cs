using AutoMapper;
using Campaign.Application.Campaigns.Dto;
using Campaign.Domain.CampaignAggregate;

namespace Campaign.Application.Campaigns
{
    public class CampaignMappingProfile : Profile
    {
        public CampaignMappingProfile()
        {
            CreateMap<Domain.CampaignAggregate.Campaign, CampaignDto>();
            CreateMap<Domain.CampaignAggregate.Campaign, CampaignListDto>();
            CreateMap<CampaignBadge, CampaignBadgeDto>();
            CreateMap<CampaignGallery, CampaignGalleryDto>();
        }
    }
}