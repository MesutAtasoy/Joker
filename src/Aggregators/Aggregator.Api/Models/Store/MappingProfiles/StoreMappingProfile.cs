using Aggregator.Api.Models.Shared;
using Aggregator.Api.Models.Store.Requests;
using AutoMapper;
using Joker.Extensions;
using Merchant.Api.Grpc;

namespace Aggregator.Api.Models.Store.MappingProfiles;

public class StoreMappingProfile : Profile
{
    public StoreMappingProfile()
    {
        CreateMap<StoreMessage, StoreModel>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.Id.ToGuid()))
            .ForMember(dest => dest.CreatedDate, src => src.MapFrom(m => m.CreatedDate.ToDateTime()))
            .ForMember(dest => dest.ModifiedDate, src => src.MapFrom(m => m.ModifiedDate.ToDateTime()));

        CreateMap<StoreLocationMessage, StoreLocationModel>()
            .ReverseMap();

        CreateMap<UpdateStoreModel, UpdateStoreItemMessage>();
        
        CreateMap<CreateStoreModel, CreateStoreMessage>()
            .ForMember(dest => dest.MerchantId, src => src.MapFrom(m => m.MerchantId.ToString()));
        
        CreateMap<IdNameMessage, IdNameModel>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.Id.ToGuid()))
            .ReverseMap();
    }
}