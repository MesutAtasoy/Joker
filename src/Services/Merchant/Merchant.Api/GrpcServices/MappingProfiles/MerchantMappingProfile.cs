using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Joker.Extensions;
using Merchant.Api.Grpc;
using Merchant.Application.Merchants.Commands.CreateMerchant;
using Merchant.Application.Merchants.Dto;
using Merchant.Application.Merchants.Dto.Requests;
using Merchant.Application.Shared.Dto;

namespace Merchant.Api.GrpcServices.MappingProfiles;

public class MerchantMappingProfile : Profile
{
    public MerchantMappingProfile()
    {
        CreateMap<IdNameDto, IdNameMessage>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.RefId.ToString()));
        
        CreateMap<IdNameMessage,IdNameDto>()
            .ForMember(dest => dest.RefId, src => src.MapFrom(m => m.Id.ToGuid()));

        CreateMap<MerchantDto, MerchantMessage>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.Id.ToString()))
            .ForMember(dest => dest.OrganizationId, src => src.MapFrom(m => m.OrganizationId.ToString()))
            .ForMember(dest => dest.CreatedDate, src => src.MapFrom(m => m.CreatedDate.ToTimestamp()));

        CreateMap<CreateMerchantMessage, CreateMerchantCommand>()
            .ForMember(dest => dest.OrganizationId, src => src.MapFrom(m => m.OrganizationId.ToGuid()));

        CreateMap<UpdateMerchantItemMessage, UpdateMerchantDto>();
    }
}