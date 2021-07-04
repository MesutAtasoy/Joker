using AutoMapper;
using Location.Application.Countries.Dto;
using Location.Core.Entities;

namespace Location.Application.Countries
{
    public class CountryMappingProfile : Profile
    {
        public CountryMappingProfile()
        {
            CreateMap<Country, CountryDto>();
        }
    }
}