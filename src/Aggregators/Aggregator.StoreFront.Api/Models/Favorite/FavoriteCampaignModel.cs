using Aggregator.StoreFront.Api.Models.Shared;

namespace Aggregator.StoreFront.Api.Models.Favorite;

public class FavoriteCampaignModel
{
    public FavoriteCampaignItemModel FavoriteCampaignItem { get; set; }
    public UserModel UserInfo { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class FavoriteCampaignItemModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string SlugKey { get; set; }
}