using AutoMapper;
using Favorite.Application.Campaigns.Dto;
using Favorite.Core.Entities;

namespace Favorite.Application.Campaigns
{
    public class CampaignMappingProfile : Profile
    {
        public CampaignMappingProfile()
        {
            CreateMap<FavoriteCampaign, FavoriteCampaignDto>();
        }
    }
}