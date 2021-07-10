using AutoMapper;
using Merchant.Application.Stores.Dto;
using Merchant.Domain.StoreAggregate;

namespace Merchant.Application.Stores
{
    public class StoreMappingProfile : Profile
    {
        public StoreMappingProfile()
        {
            CreateMap<Store, StoreDto>();
            CreateMap<Store, StoreDto>();
            CreateMap<StoreLocation, StoreLocationDto>();
        }
    }
}