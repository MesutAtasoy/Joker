using AutoMapper;
using Favorite.Application.Stores.Dto;
using Favorite.Core.Entities;

namespace Favorite.Application.Stores
{
    public class StoreMappingProfile : Profile
    {
        public StoreMappingProfile()
        {
            CreateMap<FavoriteStore, FavoriteStoreDto>();
            CreateMap<Store, StoreDto>();
        }
    }
}