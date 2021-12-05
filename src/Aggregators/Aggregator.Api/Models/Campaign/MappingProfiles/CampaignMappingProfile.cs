using Aggregator.Api.Models.Shared;
using AutoMapper;
using Campaign.Api.Grpc;
using Google.Protobuf.WellKnownTypes;
using Joker.Extensions;

namespace Aggregator.Api.Models.Campaign.MappingProfiles;

public class CampaignMappingProfile : Profile
{
    public CampaignMappingProfile()
    {
        CreateMap<CampaignMessage, CampaignModel>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.Id.ToGuid()))
            .ForMember(dest => dest.EndTime, src => src.MapFrom(m => m.EndTime.ToDateTime()))
            .ForMember(dest => dest.StartTime, src => src.MapFrom(m => m.StartTime.ToDateTime()))
            .ForMember(dest => dest.CreatedDate, src => src.MapFrom(m => m.CreatedDate.ToDateTime()))
            .ForMember(dest => dest.ModifiedDate, src => src.MapFrom(m => m.ModifiedDate.ToDateTime()));

        CreateMap<IdNameMessage, IdNameModel>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.Id.ToGuid()))
            .ReverseMap();

        CreateMap<CreateCampaignModel, CreateCampaignMessage>()
            .ForMember(dest => dest.StartTime,
                src => src.MapFrom(m => Timestamp.FromDateTime(m.StartTime ?? DateTime.Now)))
            .ForMember(dest => dest.EndTime,
                src => src.MapFrom(m => Timestamp.FromDateTime(m.EndTime ?? DateTime.Now)));

        CreateMap<UpdateCampaignModel, UpdateCampaignMessageItem>()
            .ForMember(dest => dest.StartTime,
                src => src.MapFrom(m => Timestamp.FromDateTime(m.StartTime ?? DateTime.Now)))
            .ForMember(dest => dest.EndTime,
                src => src.MapFrom(m => Timestamp.FromDateTime(m.EndTime ?? DateTime.Now)));
    }
}