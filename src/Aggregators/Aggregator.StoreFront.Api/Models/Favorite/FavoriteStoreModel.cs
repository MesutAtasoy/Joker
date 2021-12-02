using Aggregator.StoreFront.Api.Models.Shared;

namespace Aggregator.StoreFront.Api.Models.Favorite;

public class FavoriteStoreModel
{
    public FavoriteStoreItemModel FavoriteStoreItem { get; set; }
    public UserModel UserInfo { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class FavoriteStoreItemModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string SlugKey { get; set; }
}