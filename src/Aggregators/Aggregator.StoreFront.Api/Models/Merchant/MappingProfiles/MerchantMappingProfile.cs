using Aggregator.StoreFront.Api.Models.Merchant.Requests;
using Aggregator.StoreFront.Api.Models.Shared;
using AutoMapper;
using Joker.Extensions;
using Merchant.Api.Grpc;

namespace Aggregator.StoreFront.Api.Models.Merchant.MappingProfiles;

public class MerchantMappingProfile : Profile
{
    public MerchantMappingProfile()
    {
        CreateMap<MerchantMessage, MerchantModel>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.Id.ToGuid()))
            .ForMember(dest => dest.CreatedDate, src => src.MapFrom(m => m.CreatedDate.ToDateTime()));

        CreateMap<CreateMerchantModel, CreateMerchantMessage>();
        
        CreateMap<IdNameMessage, IdNameModel>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.Id.ToGuid()));
    }
}