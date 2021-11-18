using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Favorite;
using Joker.WebApp.ViewModels.Favorite.Request;
using Joker.WebApp.ViewModels.Shared;

namespace Joker.WebApp.Services;

public class FavoriteService : BaseService, IFavoriteService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly HttpClient _httpClient;
    private readonly ILogger<FavoriteService> _logger;

    public FavoriteService(IHttpClientFactory clientFactory, ILogger<FavoriteService> logger) 
        : base(logger)
    {
        _clientFactory = clientFactory;
        _logger = logger;
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

    public async Task<List<FavoriteCampaignViewModel>> GetFavoriteCampaignAsync(Guid userId)
    {
        var responseMessage = await _httpClient.GetAsync($"favorite/api/Campaigns/Users/{userId}");
            
        if (!responseMessage.IsSuccessStatusCode)
        {
            var response = await responseMessage.Content.ReadAsStringAsync();
            _logger.LogError(response);
            throw new ArgumentException("Favorite Service can not respond success response");
        }

        var favoriteCampaigns = await responseMessage.Content.ReadFromJsonAsync<List<FavoriteCampaignViewModel>>();

        return favoriteCampaigns;
    }

    public async Task<List<FavoriteStoreViewModel>> GetFavoriteStoreAsync(Guid userId)
    {
        var responseMessage = await _httpClient.GetAsync($"favorite/api/Stores/Users/{userId}");
            
        if (!responseMessage.IsSuccessStatusCode)
        {
            var response = await responseMessage.Content.ReadAsStringAsync();
            _logger.LogError(response);
            throw new ArgumentException("Favorite Service can not respond success response");
        }

        var favoriteStores = await responseMessage.Content.ReadFromJsonAsync<List<FavoriteStoreViewModel>>();

        return favoriteStores;        
    }
}