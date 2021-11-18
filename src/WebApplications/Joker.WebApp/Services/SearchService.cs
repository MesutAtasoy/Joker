using Joker.WebApp.Extensions;
using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Search;
using Joker.WebApp.ViewModels.Search.Request;

namespace Joker.WebApp.Services;

public class SearchService : ISearchService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly HttpClient _httpClient;
    private readonly ILogger<SearchService> _logger;

    public SearchService(IHttpClientFactory clientFactory, 
        ILogger<SearchService> logger)
    {
        _clientFactory = clientFactory;
        _logger = logger;
        _httpClient = _clientFactory.CreateClient("GatewayApi");
    }

    public async Task<SearchBaseResponse<CampaignSearchResponse>> SearchCampaignAsync(CampaignSearchRequest request)
    {
        var queryString = UrlExtensions.GetUrlQueryString(request);

        var requestUri = string.IsNullOrEmpty(queryString)
            ? "search/api/campaigns"
            : $"search/api/campaigns?{queryString}";
            
        var responseMessage = await _httpClient.GetAsync(requestUri);
            
        if (!responseMessage.IsSuccessStatusCode)
        {
            var response = await responseMessage.Content.ReadAsStringAsync();
            _logger.LogError(response);
            throw new ArgumentException("Search Service can not respond success response");
        }

        var campaigns =
            await responseMessage.Content.ReadFromJsonAsync<SearchBaseResponse<CampaignSearchResponse>>();

        return campaigns;
    }

    public async Task<SearchBaseResponse<StoreSearchResponse>> SearchStoreAsync(StoreSearchRequest request)
    {
        var queryString = UrlExtensions.GetUrlQueryString(request);
            
        var requestUri = string.IsNullOrEmpty(queryString)
            ? "search/api/stores"
            : $"search/api/stores?{queryString}";
            
        var responseMessage = await _httpClient.GetAsync(requestUri);
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new ArgumentException("Search Service can not respond success response");
        }

        var stores = await responseMessage.Content.ReadFromJsonAsync<SearchBaseResponse<StoreSearchResponse>>();

        return stores;
    }
}