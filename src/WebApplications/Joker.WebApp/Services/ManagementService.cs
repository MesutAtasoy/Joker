using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Management;

namespace Joker.WebApp.Services
{
    public class ManagementApiService : IManagementApiService
    {
        private readonly IHttpClientFactory _clientFactory;
        
        public ManagementApiService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        
        public async Task<List<PricingPlanViewModel>> GetPricingPlansAsync()
        {
            var client = _clientFactory.CreateClient("GatewayApi");

            var responseMessage = await client.GetAsync("management/api/PricingPlans");
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new ArgumentException("Management Service can not respond success response");
            }
            
            var pricingPlans = await responseMessage.Content.ReadFromJsonAsync<List<PricingPlanViewModel>>();

            return pricingPlans;
        }
    }
}