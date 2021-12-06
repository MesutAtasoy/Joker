using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Merchant;
using Joker.WebApp.ViewModels.Merchant.Request;
using Joker.WebApp.ViewModels.Shared;

namespace Joker.WebApp.Services;

public class MerchantService : BaseService, IMerchantService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly HttpClient _httpClient;
    private readonly ILogger<MerchantService> _logger;

    public MerchantService(IHttpClientFactory clientFactory, ILogger<MerchantService> logger) 
        :base(logger)
    {
        _clientFactory = clientFactory;
        _logger = logger;
        _httpClient = _clientFactory.CreateClient("GatewayApi");
    }

    public async Task<JokerBaseResponseViewModel<MerchantViewModel>> CreateAsync(CreateMerchantViewModel createMerchantViewModel)
    {
        var responseMessage = await _httpClient.PostAsJsonAsync("aggregator/storefront/api/Merchant", createMerchantViewModel);
        return await HandleRequestAsync<MerchantViewModel>(responseMessage);
    }
}