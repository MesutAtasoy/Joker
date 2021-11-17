using AutoMapper;
using Location.Application.Cities.Dto;
using Location.Core.Entities;

namespace Location.Application.Cities;

public class CityMappingProfile : Profile
{
    public CityMappingProfile()
    {
        CreateMap<City, CityDto>();
    }
}