using AutoMapper;
using Location.Application.Quarters.Dto;
using Location.Core.Entities;

namespace Location.Application.Quarters
{
    public class QuarterMappingProfile : Profile 
    {
        public QuarterMappingProfile()
        {
            CreateMap<Quarter, QuarterDto>();
        }
    }
}