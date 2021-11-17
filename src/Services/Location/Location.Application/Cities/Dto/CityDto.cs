namespace Location.Application.Cities.Dto;

public class CityDto
{
    public Guid Id { get; set; }
    public Guid CountryId { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
}