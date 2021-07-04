using AutoMapper;
using Location.Application.Neighborhoods.Dto;
using Location.Core.Entities;

namespace Location.Application.Neighborhoods
{
    public class NeighborhoodMappingProfile : Profile
    {
        public NeighborhoodMappingProfile()
        {
            CreateMap<Neighborhood, NeighborhoodDto>();
        }
    }
}