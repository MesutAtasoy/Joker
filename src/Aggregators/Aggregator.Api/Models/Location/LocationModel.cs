using Aggregator.Api.Models.Shared;

namespace Aggregator.Api.Models.Location;

public class LocationModel
{
    public IdNameModel Country { get; set; }
    public IdNameModel City { get; set; }
    public IdNameModel District { get; set; }
    public IdNameModel Neighborhood { get; set; }
    public IdNameModel Quarter { get; set; }
    public bool IsValid { get; set; }
}