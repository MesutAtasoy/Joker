using AutoMapper;
using Campaign.Api.Grpc;
using Campaign.Application.Campaigns.Command.CreateCampaign;
using Campaign.Application.Campaigns.Dto;
using Campaign.Application.Campaigns.Dto.Request;
using Campaign.Application.Shared.Dto;
using Google.Protobuf.WellKnownTypes;
using Joker.Extensions;

namespace Campaign.Api.GrpcServices.MappingProfiles;

public class CampaignMappingProfile : Profile
{
    public CampaignMappingProfile()
    {
        CreateMap<IdNameDto, IdNameMessage>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.RefId.ToString()));
        
        CreateMap<IdNameMessage,IdNameDto>()
            .ForMember(dest => dest.RefId, src => src.MapFrom(m => m.Id.ToGuid()));
        
        CreateMap<CampaignDto, CampaignMessage>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.Id.ToString()))
            .ForMember(dest => dest.OrganizationId, src => src.MapFrom(m => m.OrganizationId.ToString()))
            .ForMember(dest => dest.CreatedDate, src => src.MapFrom(m => m.CreatedDate.ToTimestamp()))
            .ForMember(dest => dest.StartTime, src => src.MapFrom(m => m.StartTime.Value.ToTimestamp()))
            .ForMember(dest => dest.EndTime, src => src.MapFrom(m => m.EndTime.Value.ToTimestamp()));
        
        CreateMap<CreateCampaignMessage, CreateCampaignCommand>()
            .ForMember(dest => dest.StartTime, src => src.MapFrom(m => m.StartTime.ToDateTime()))
            .ForMember(dest => dest.EndTime, src => src.MapFrom(m => m.EndTime.ToDateTime()));
        
        CreateMap<UpdateCampaignMessageItem, UpdateCampaignDto>()
            .ForMember(dest => dest.StartTime, src => src.MapFrom(m => m.StartTime.ToDateTime()))
            .ForMember(dest => dest.EndTime, src => src.MapFrom(m => m.EndTime.ToDateTime()));

    }
}