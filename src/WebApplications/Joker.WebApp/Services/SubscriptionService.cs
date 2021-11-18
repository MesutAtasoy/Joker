using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Subscription;

namespace Joker.WebApp.Services;

public class SubscriptionService : BaseService, ISubscriptionService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly HttpClient _httpClient;
    private readonly ILogger<SubscriptionService> _logger;

    public SubscriptionService(IHttpClientFactory clientFactory, ILogger<SubscriptionService> logger)
         :base(logger)
    {
        _clientFactory = clientFactory;
        _logger = logger;
        _httpClient = _clientFactory.CreateClient("GatewayApi");
    }

    public async Task<List<SubscriptionViewModel>> GetSubscriptions(Guid merchantId)
    {
        var responseMessage = await _httpClient.GetAsync($"/subscription/api/Subscriptions/{merchantId}");
        if (!responseMessage.IsSuccessStatusCode)
        {
            var response = await responseMessage.Content.ReadAsStringAsync();
            _logger.LogError(response);
            throw new ArgumentException("Subscription Service can not respond success response");
        }

        var subscriptions = await responseMessage.Content.ReadFromJsonAsync<List<SubscriptionViewModel>>();

        return subscriptions;
    }
}