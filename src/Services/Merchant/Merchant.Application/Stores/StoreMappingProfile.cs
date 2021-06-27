using AutoMapper;
using Merchant.Application.Stores.Dto;
using Merchant.Domain.StoreAggregate;

namespace Merchant.Application.Stores
{
    public class StoreMappingProfile : Profile
    {
        public StoreMappingProfile()
        {
            CreateMap<Store, StoreListDto>();
            CreateMap<Store, StoreDto>();
            CreateMap<StoreBusinessHour, StoreBusinessHourDto>();
            CreateMap<StoreFAQ, StoreFAQDto>();
            CreateMap<StoreLocation, StoreLocationDto>();
        }
    }
}