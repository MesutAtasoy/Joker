using System;

namespace Location.Application.Quarters.Dto
{
    public class QuarterDto
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public Guid CityId { get; set; }
        public Guid DistrictId { get; set; }
        public Guid NeighborhoodId { get; set; }
        public string Name { get; set; }
    }
}