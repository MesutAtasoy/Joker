using Location.Application.Cities.Dto;
using Location.Application.Countries.Dto;
using Location.Application.Districts.Dto;
using Location.Application.Neighborhoods.Dto;

namespace Location.Application.Quarters.Dto
{
    public class LocationDto
    {
        public CountryDto Country { get; set; }
        public CityDto City { get; set; }
        public DistrictDto District { get; set; }
        public NeighborhoodDto Neighborhood { get; set; }
        public QuarterDto Quarter { get; set; }
        public bool IsValid { get; set; }
    }
}