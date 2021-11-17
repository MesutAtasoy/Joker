using Aggregator.Api.Models.Shared;

namespace Aggregator.Api.Models.Favorite;

public class FavoriteCampaignModel
{
    public CampaignModel Campaign { get; set; }
    public UserModel UserInfo { get; set; }
    public DateTime CreatedDate { get; set; }
}