using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Joker.WebApp.Extensions;
using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Search;
using Joker.WebApp.ViewModels.Search.Request;

namespace Joker.WebApp.Services
{
    public class SearchService : ISearchService
    {
        private readonly IHttpClientFactory _clientFactory;

        public SearchService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<SearchBaseResponse<CampaignSearchResponse>> SearchCampaignAsync(CampaignSearchRequest request)
        {
            var client = _clientFactory.CreateClient("GatewayApi");
            var queryString = UrlExtensions.GetUrlQueryString(request);

            var requestUri = string.IsNullOrEmpty(queryString)
                ? "search/api/campaigns"
                : $"search/api/campaigns?{queryString}";
            
            var responseMessage = await client.GetAsync(requestUri);
            
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new ArgumentException("Search Service can not respond success response");
            }

            var campaigns =
                await responseMessage.Content.ReadFromJsonAsync<SearchBaseResponse<CampaignSearchResponse>>();

            return campaigns;
        }

        public async Task<SearchBaseResponse<StoreSearchResponse>> SearchStoreAsync(StoreSearchRequest request)
        {
            var client = _clientFactory.CreateClient("GatewayApi");
            var queryString = UrlExtensions.GetUrlQueryString(request);
            
            var requestUri = string.IsNullOrEmpty(queryString)
                ? "search/api/stores"
                : $"search/api/stores?{queryString}";
            
            var responseMessage = await client.GetAsync(requestUri);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new ArgumentException("Search Service can not respond success response");
            }

            var stores = await responseMessage.Content.ReadFromJsonAsync<SearchBaseResponse<StoreSearchResponse>>();

            return stores;
        }
    }
}