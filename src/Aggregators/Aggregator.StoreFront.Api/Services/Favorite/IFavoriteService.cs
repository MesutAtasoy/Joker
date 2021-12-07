using Aggregator.StoreFront.Api.Models.Favorite;
using Aggregator.StoreFront.Api.Models.Favorite.Requests;
using Joker.Response;

namespace Aggregator.StoreFront.Api.Services.Favorite;

public interface IFavoriteService
{
    Task<JokerBaseResponse<FavoriteCampaignModel>> AddFavoriteCampaignAsync(AddFavoriteCampaignRequestModel requestModel);
    Task<JokerBaseResponse<FavoriteStoreModel>> AddFavoriteStoreAsync(AddFavoriteStoreRequestModel requestModel);
}