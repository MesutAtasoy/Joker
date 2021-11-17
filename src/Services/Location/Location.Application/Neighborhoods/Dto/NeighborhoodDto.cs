namespace Location.Application.Neighborhoods.Dto;

public class NeighborhoodDto
{
    public Guid Id { get; set; }
    public Guid CountryId { get; set; }
    public Guid CityId { get; set; }
    public Guid DistrictId { get; set; }
    public string Name { get; set; }
}