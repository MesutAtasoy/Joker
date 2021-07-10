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
        }
    }
}