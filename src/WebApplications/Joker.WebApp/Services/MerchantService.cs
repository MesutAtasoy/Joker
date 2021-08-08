using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Merchant;
using Joker.WebApp.ViewModels.Merchant.Request;

namespace Joker.WebApp.Services
{
    public class MerchantService : IMerchantService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient _httpClient;
        
        public MerchantService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _httpClient = _clientFactory.CreateClient("GatewayApi");
        }
        
        public async Task<MerchantViewModel> CreateAsync(CreateMerchantViewModel createMerchantViewModel)
        {   
            var responseMessage = await _httpClient.PostAsJsonAsync("aggregator/api/Merchants", createMerchantViewModel);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new ArgumentException("Merchant Service can not respond success response");
            }
            
            var merchant = await responseMessage.Content.ReadFromJsonAsync<MerchantViewModel>();

            return merchant;
        }

        public async Task<MerchantViewModel> UpdateAsync(UpdateMerchantViewModel updateMerchantViewModel)
        {
            var responseMessage = await _httpClient.PutAsJsonAsync($"aggregator/api/Merchants", updateMerchantViewModel);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new ArgumentException("Merchant Service can not respond success response");
            }
            
            var merchant = await responseMessage.Content.ReadFromJsonAsync<MerchantViewModel>();

            return merchant;        
        }

        public async Task<MerchantViewModel> GetByIdAsync(Guid id)
        {
            var responseMessage = await _httpClient.GetAsync($"merchant/api/Merchants/{id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new ArgumentException("Merchant Service can not respond success response");
            }
            
            var merchant = await responseMessage.Content.ReadFromJsonAsync<MerchantViewModel>();
            
            return merchant;
        }
    }
}