using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Joker.Extensions;
using Merchant.Api.Grpc;
using Merchant.Application.Shared.Dto;
using Merchant.Application.Stores.Commands.CreateStore;
using Merchant.Application.Stores.Dto;
using Merchant.Application.Stores.Dto.Request;

namespace Merchant.Api.GrpcServices.MappingProfiles;

public class StoreMappingProfile : Profile
{
    public StoreMappingProfile()
    {
        CreateMap<IdNameDto, IdNameMessage>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.RefId.ToString()));
        
        CreateMap<IdNameMessage,IdNameDto>()
            .ForMember(dest => dest.RefId, src => src.MapFrom(m => m.Id.ToGuid()));

        CreateMap<StoreDto, StoreMessage>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.Id.ToString()))
            .ForMember(dest => dest.OrganizationId, src => src.MapFrom(m => m.OrganizationId.ToString()))
            .ForMember(dest => dest.CreatedDate, src => src.MapFrom(m => m.CreatedDate.ToTimestamp()));

        CreateMap<StoreLocationDto, StoreLocationMessage>()
            .ReverseMap();
        
        CreateMap<CreateStoreMessage, CreateStoreCommand>()
            .ForMember(dest => dest.MerchantId, src => src.MapFrom(m => m.MerchantId.ToGuid()));

        CreateMap<UpdateStoreItemMessage, UpdateStoreDto>();
    }
}