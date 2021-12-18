namespace Aggregator.StoreFront.Api.Models.Favorite.Requests;

public class AddFavoriteCampaignRequestModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string SlugKey { get; set; }
    public Guid OrganizationId { get; set; }
}