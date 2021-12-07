using Aggregator.StoreFront.Api.Models.Shared;

namespace Aggregator.StoreFront.Api.Models.Store;

public class StoreLocationModel
{
    public IdNameModel Country { get; set; }
    public IdNameModel City { get; set; }
    public IdNameModel District { get; set; }
    public IdNameModel Quarter { get; set; }
    public IdNameModel Neighborhood { get; set; }
    public string Address { get; set; }
}