using Aggregator.StoreFront.Api.Models.Shared;
using AutoMapper;
using Joker.Extensions;
using Merchant.Api.Grpc;

namespace Aggregator.StoreFront.Api.Models.Store.MappingProfiles;

public class StoreMappingProfile : Profile
{
    public StoreMappingProfile()
    {
        CreateMap<StoreMessage, StoreModel>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.Id.ToGuid()))
            .ForMember(dest => dest.CreatedDate, src => src.MapFrom(m => m.CreatedDate.ToDateTime()));

        CreateMap<StoreLocationMessage, StoreLocationModel>();

        CreateMap<IdNameMessage, IdNameModel>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.Id.ToGuid()));
    }
}