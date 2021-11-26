using Aggregator.StoreFront.Api.Models.Shared;

namespace Aggregator.StoreFront.Api.Models.Favorite;

public class FavoriteStoreModel
{
    public StoreModel Store { get; set; }
    public UserModel UserInfo { get; set; }
    public DateTime CreatedDate { get; set; }
}