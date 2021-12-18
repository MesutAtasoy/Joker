using Aggregator.StoreFront.Api.Models.Shared;
using AutoMapper;
using Campaign.Api.Grpc;
using Joker.Extensions;

namespace Aggregator.StoreFront.Api.Models.Campaign.MappingProfiles;

public class CampaignMappingProfile : Profile
{
    public CampaignMappingProfile()
    {
        CreateMap<CampaignMessage, CampaignModel>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.Id.ToGuid()))
            .ForMember(dest => dest.EndTime, src => src.MapFrom(m => m.EndTime.ToDateTime()))
            .ForMember(dest => dest.StartTime, src => src.MapFrom(m => m.StartTime.ToDateTime()))
            .ForMember(dest => dest.CreatedDate, src => src.MapFrom(m => m.CreatedDate.ToDateTime()));

        CreateMap<IdNameMessage, IdNameModel>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.Id.ToGuid()));
    }
}