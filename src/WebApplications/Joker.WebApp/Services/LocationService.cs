using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Shared;

namespace Joker.WebApp.Services
{
    public class LocationService : ILocationService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient _httpClient;

        public LocationService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _httpClient = _clientFactory.CreateClient("GatewayApi");
        }
        
        public async Task<IdNameViewModel> GetCountryAsync()
        {
            var responseMessage = await _httpClient.GetAsync($"location/api/Counties");
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new ArgumentException("Management Service can not respond success response");
            }

            var counties = await responseMessage.Content.ReadFromJsonAsync<IdNameViewModel>();

            return counties;
        }

        public async Task<List<IdNameViewModel>> GetCitiesAsync(Guid countryId)
        {
            var responseMessage = await _httpClient.GetAsync($"location/api/Cities?CountryId={countryId}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new ArgumentException("Management Service can not respond success response");
            }

            var cities = await responseMessage.Content.ReadFromJsonAsync<List<IdNameViewModel>>();

            return cities;
        }

        public async Task<List<IdNameViewModel>> GetDistrictsAsync(Guid cityId)
        {
            var responseMessage = await _httpClient.GetAsync($"location/api/Districts?CityId={cityId}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new ArgumentException("Management Service can not respond success response");
            }

            var districts = await responseMessage.Content.ReadFromJsonAsync<List<IdNameViewModel>>();

            return districts;
        }

        public async Task<List<IdNameViewModel>> GetNeighborhoodsAsync(Guid districtId)
        {
            var responseMessage = await _httpClient.GetAsync($"location/api/Neighborhoods?DistrictId={districtId}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new ArgumentException("Management Service can not respond success response");
            }

            var neighborhoods = await responseMessage.Content.ReadFromJsonAsync<List<IdNameViewModel>>();

            return neighborhoods;
        }

        public async Task<List<IdNameViewModel>> GetQuartersAsync(Guid neighborhoodId)
        {
            var responseMessage = await _httpClient.GetAsync($"location/api/Quarters?NeighborhoodId={neighborhoodId}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new ArgumentException("Management Service can not respond success response");
            }

            var quarters = await responseMessage.Content.ReadFromJsonAsync<List<IdNameViewModel>>();

            return quarters;
        }
    }
}