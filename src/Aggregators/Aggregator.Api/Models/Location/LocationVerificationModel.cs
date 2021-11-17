namespace Aggregator.Api.Models.Location;

public class LocationVerificationModel
{
    public Guid CountryId { get; set; }
    public Guid CityId { get; set; }
    public Guid DistrictId { get; set; }
    public Guid NeighborhoodId { get; set; }
    public Guid QuarterId { get; set; }
}