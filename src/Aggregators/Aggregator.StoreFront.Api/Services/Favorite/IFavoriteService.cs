using Aggregator.StoreFront.Api.Models.Favorite;
using Joker.Response;

namespace Aggregator.StoreFront.Api.Services.Favorite;

public interface IFavoriteService
{
    Task<JokerBaseResponse<FavoriteCampaignModel>> AddFavoriteCampaignAsync(AddCampaignModel model);
    Task<JokerBaseResponse<FavoriteStoreModel>> AddFavoriteStoreAsync(AddStoreModel model);
}