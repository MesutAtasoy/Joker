namespace Location.Application.Districts.Dto;

public class DistrictDto
{
    public Guid Id { get; set; }
    public Guid CountryId { get; set; }
    public Guid CityId { get; set; }
    public string Name { get; set; }
}