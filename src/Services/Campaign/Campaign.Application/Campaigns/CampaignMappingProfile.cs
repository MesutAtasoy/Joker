using AutoMapper;
using Campaign.Application.Campaigns.Dto;

namespace Campaign.Application.Campaigns;

public class CampaignMappingProfile : Profile
{
    public CampaignMappingProfile()
    {
        CreateMap<Domain.CampaignAggregate.Campaign, CampaignDto>();
    }
}