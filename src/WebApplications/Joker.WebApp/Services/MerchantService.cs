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
        
        public MerchantService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            
        }
        public async Task<MerchantViewModel> CreateAsync(CreateMerchantViewModel createMerchantViewModel)
        {   
            var client = _clientFactory.CreateClient("GatewayApi");

            var responseMessage = await client.PostAsJsonAsync("aggregator/api/Merchants", createMerchantViewModel);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var sss = responseMessage.Content.ReadAsStringAsync(); 
                throw new ArgumentException("Merchant Service can not respond success response");
            }
            
            var merchant = await responseMessage.Content.ReadFromJsonAsync<MerchantViewModel>();

            return merchant;
        }
    }
}