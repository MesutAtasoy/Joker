using AutoMapper;
using Campaign.Application.Shared.Dto;
using Campaign.Domain.Refs;

namespace Campaign.Application.Shared
{
    public class SharedMappingProfile : Profile
    {
        public SharedMappingProfile()
        {
            CreateMap<BadgeRef, IdNameDto>();
            CreateMap<StoreRef, IdNameDto>();
            CreateMap<BusinessDirectoryRef, IdNameDto>();
        }
    }
}