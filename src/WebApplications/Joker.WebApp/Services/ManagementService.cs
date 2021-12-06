using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Management;

namespace Joker.WebApp.Services;

public class ManagementApiService : IManagementService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly HttpClient _httpClient;

    public ManagementApiService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _httpClient = _clientFactory.CreateClient("GatewayApi");
    }

    public async Task<List<PricingPlanViewModel>> GetPricingPlansAsync()
    {
        var responseMessage = await _httpClient.GetAsync("management/api/PricingPlans");
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new ArgumentException("Management Service can not respond success response");
        }

        var pricingPlans = await responseMessage.Content.ReadFromJsonAsync<List<PricingPlanViewModel>>();

        return pricingPlans;
    }

    public async Task<PricingPlanViewModel> GetPricingPlanAsync(string slug)
    {
        var responseMessage = await _httpClient.GetAsync($"management/api/PricingPlans/BySlug/{slug}");
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new ArgumentException("Management Service can not respond success response");
        }

        var pricingPlans = await responseMessage.Content.ReadFromJsonAsync<PricingPlanViewModel>();

        return pricingPlans;
    }
}