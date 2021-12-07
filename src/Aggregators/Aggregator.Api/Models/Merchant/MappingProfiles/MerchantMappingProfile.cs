using Aggregator.Api.Models.Shared;
using AutoMapper;
using Joker.Extensions;
using Merchant.Api.Grpc;

namespace Aggregator.Api.Models.Merchant.MappingProfiles;

public class MerchantMappingProfile : Profile
{
    public MerchantMappingProfile()
    {
        CreateMap<MerchantMessage, MerchantModel>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.Id.ToGuid()))
            .ForMember(dest => dest.CreatedDate, src => src.MapFrom(m => m.CreatedDate.ToDateTime()))
            .ForMember(dest => dest.ModifiedDate, src => src.MapFrom(m => m.ModifiedDate.ToDateTime()));

        CreateMap<CreateMerchantModel, CreateMerchantMessage>();

        CreateMap<UpdateMerchantModel, UpdateMerchantItemMessage>();
        
        CreateMap<IdNameMessage, IdNameModel>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.Id.ToGuid()));
    }
}