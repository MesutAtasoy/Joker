using AutoMapper;
using Favorite.Application.Shared.Dto;
using Favorite.Core.Entities;
using Favorite.Core.Entities.Shared;

namespace Favorite.Application.Shared
{
    public class SharedMappingProfile : Profile
    {
        public SharedMappingProfile()
        {
            CreateMap<IdNameRef, IdNameDto>();
            CreateMap<User, UserDto>();
        }
    }
}