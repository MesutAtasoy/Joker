using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Merchant;
using Joker.WebApp.ViewModels.Merchant.Request;
using Joker.WebApp.ViewModels.Shared;
using Joker.WebApp.ViewModels.Store;
using Joker.WebApp.ViewModels.Store.Request;

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
        var responseMessage = await _httpClient.PostAsJsonAsync("aggregator/api/Merchants", createMerchantViewModel);
        return await HandleRequestAsync<MerchantViewModel>(responseMessage);
    }

    public async Task<JokerBaseResponseViewModel<MerchantViewModel>> UpdateAsync(UpdateMerchantViewModel updateMerchantViewModel)
    {
        var responseMessage = await _httpClient.PutAsJsonAsync($"aggregator/api/Merchants", updateMerchantViewModel);
        return await HandleRequestAsync<MerchantViewModel>(responseMessage);
    }

    public async Task<MerchantViewModel> GetByIdAsync(Guid id)
    {
        var responseMessage = await _httpClient.GetAsync($"merchant/api/Merchants/{id}");
        if (!responseMessage.IsSuccessStatusCode)
        {
            var response = await responseMessage.Content.ReadAsStringAsync();
            _logger.LogError(response);
            throw new ArgumentException("Merchant Service can not respond success response");
        }

        var merchant = await responseMessage.Content.ReadFromJsonAsync<MerchantViewModel>();

        return merchant;
    }

    public async Task<PagedListViewModel<StoreViewModel>> GetStoresAsync(Guid merchantId, int page = 1, int pageSize = 20)
    {
        var responseMessage = await _httpClient.GetAsync($"merchant/api/Merchants/{merchantId}/Stores?PageNumber={page}&PageSize={pageSize}");
        if (!responseMessage.IsSuccessStatusCode)
        {
            var response = await responseMessage.Content.ReadAsStringAsync();
            _logger.LogError(response);
            throw new ArgumentException("Merchant Service can not respond success response");
        }

        var stores = await responseMessage.Content.ReadFromJsonAsync<PagedListViewModel<StoreViewModel>>();

        return stores;
    }

    public async Task<JokerBaseResponseViewModel<StoreViewModel>> CreateStoreAsync(CreateStoreViewModel createStoreViewModel)
    {
        var responseMessage = await _httpClient.PostAsJsonAsync("aggregator/api/Stores", createStoreViewModel);
        return await HandleRequestAsync<StoreViewModel>(responseMessage);
    }

    public async Task<JokerBaseResponseViewModel<StoreViewModel>> UpdateStoreAsync(UpdateStoreViewModel updateStoreViewModel)
    {
        var responseMessage = await _httpClient.PutAsJsonAsync($"aggregator/api/Stores", updateStoreViewModel);
        return await HandleRequestAsync<StoreViewModel>(responseMessage);
    }

    public async Task<StoreViewModel> GetStoreByIdAsync(Guid id)
    {
        var responseMessage = await _httpClient.GetAsync($"merchant/api/Stores/{id}");
        if (!responseMessage.IsSuccessStatusCode)
        {
            var response = await responseMessage.Content.ReadAsStringAsync();
            _logger.LogError(response);
            throw new ArgumentException("Merchant Service can not respond success response");
        }

        var store = await responseMessage.Content.ReadFromJsonAsync<StoreViewModel>();

        return store;
    }
}