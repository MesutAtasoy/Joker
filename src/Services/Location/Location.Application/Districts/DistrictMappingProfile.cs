using AutoMapper;
using Location.Application.Districts.Dto;
using Location.Core.Entities;

namespace Location.Application.Districts
{
    public class DistrictMappingProfile : Profile
    {
        public DistrictMappingProfile()
        {
            CreateMap<District, DistrictDto>();
        }
    }
}