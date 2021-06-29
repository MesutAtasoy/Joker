using AutoMapper;
using Merchant.Application.Shared.Dto;
using Merchant.Domain.Refs;

namespace Merchant.Application.Shared
{
    public class SharedMappingProfile : Profile
    {
        public SharedMappingProfile()
        {
            CreateMap<CountryRef, IdNameDto>();
            CreateMap<CityRef, IdNameDto>();
            CreateMap<NeighborhoodRef, IdNameDto>();
            CreateMap<QuarterRef, IdNameDto>();
            CreateMap<DistrictRef, IdNameDto>();
            CreateMap<MerchantRef, IdNameDto>();
        }
    }
}