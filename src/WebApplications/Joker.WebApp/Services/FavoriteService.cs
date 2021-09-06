using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Favorite;
using Joker.WebApp.ViewModels.Favorite.Request;
using Joker.WebApp.ViewModels.Shared;

namespace Joker.WebApp.Services
{
    public class FavoriteService : BaseService, IFavoriteService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient _httpClient;

        public FavoriteService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _httpClient = _clientFactory.CreateClient("GatewayApi");
        }

        
        public async Task<JokerBaseResponseViewModel<FavoriteCampaignViewModel>> AddFavoriteCampaignAsync(AddFavoriteCampaignViewModel model)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync("aggregator/api/Favorites/Campaigns", model);
            return await HandleRequestAsync<FavoriteCampaignViewModel>(responseMessage);
        }

        public async Task<JokerBaseResponseViewModel<FavoriteStoreViewModel>> AddFavoriteStoreAsync(AddFavoriteStoreViewModel model)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync("aggregator/api/Favorites/Stores", model);
            return await HandleRequestAsync<FavoriteStoreViewModel>(responseMessage);
        }
    }
}