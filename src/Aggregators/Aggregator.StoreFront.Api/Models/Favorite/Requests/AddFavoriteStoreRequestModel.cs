namespace Aggregator.StoreFront.Api.Models.Favorite.Requests;

public class AddFavoriteStoreRequestModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string SlugKey { get; set; }
    public Guid OrganizationId { get; set; }
}