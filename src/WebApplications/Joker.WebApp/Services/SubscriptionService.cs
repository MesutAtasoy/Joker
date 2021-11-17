using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Subscription;

namespace Joker.WebApp.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly HttpClient _httpClient;

    public SubscriptionService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _httpClient = _clientFactory.CreateClient("GatewayApi");
    }

    public async Task<List<SubscriptionViewModel>> GetSubscriptions(Guid merchantId)
    {
        var responseMessage = await _httpClient.GetAsync($"/subscription/api/Subscriptions/{merchantId}");
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new ArgumentException("Subscription Service can not respond success response");
        }

        var subscriptions = await responseMessage.Content.ReadFromJsonAsync<List<SubscriptionViewModel>>();

        return subscriptions;
    }
}