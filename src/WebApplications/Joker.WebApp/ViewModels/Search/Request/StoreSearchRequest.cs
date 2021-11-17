namespace Joker.WebApp.ViewModels.Search.Request;

public class StoreSearchRequest : SearchBaseRequest
{
    public Guid? StoreId { get; set; }
    public Guid? CountryId { get; set; }
    public Guid? CityId { get; set; }
    public Guid? DistrictId { get; set; }
    public Guid? NeighborhoodId { get; set; }
    public Guid? QuarterId { get; set; }
}