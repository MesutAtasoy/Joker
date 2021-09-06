using System.Threading.Tasks;
using Joker.WebApp.ViewModels.Favorite;
using Joker.WebApp.ViewModels.Favorite.Request;
using Joker.WebApp.ViewModels.Shared;

namespace Joker.WebApp.Services.Abstract
{
    public interface IFavoriteService
    {
        Task<JokerBaseResponseViewModel<FavoriteCampaignViewModel>> AddFavoriteCampaignAsync(AddFavoriteCampaignViewModel model);
        Task<JokerBaseResponseViewModel<FavoriteStoreViewModel>> AddFavoriteStoreAsync(AddFavoriteStoreViewModel model);
    }
}