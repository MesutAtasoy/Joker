namespace Aggregator.Api.Models.Favorite;

public class AddCampaignModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string SlugKey { get; set; }
}