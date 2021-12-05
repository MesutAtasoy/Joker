using Joker.BackOffice.Services.Abstract;
using Joker.BackOffice.ViewModels.Merchant;
using Joker.BackOffice.ViewModels.Shared;
using Joker.BackOffice.ViewModels.Store;
using Joker.BackOffice.ViewModels.Store.Request;

namespace Joker.BackOffice.Services;

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

    public async Task<List<MerchantViewModel>> GetMerchants()
    {
        var responseMessage = await _httpClient.GetAsync($"merchant/api/Merchants");
        if (!responseMessage.IsSuccessStatusCode)
        {
            var response = await responseMessage.Content.ReadAsStringAsync();
            _logger.LogError(response);
            throw new ArgumentException("Merchant Service can not respond success response");
        }

        var merchants = await responseMessage.Content.ReadFromJsonAsync<List<MerchantViewModel>>();

        return merchants;
        
    }

    public async Task<PagedListViewModel<StoreViewModel>> GetStoresAsync(int page = 1, int pageSize = 20)
    {
        var responseMessage = await _httpClient.GetAsync($"merchant/api/Stores?PageNumber={page}&PageSize={pageSize}");
        if (!responseMessage.IsSuccessStatusCode)
        {
            var response = await responseMessage.Content.ReadAsStringAsync();
            _logger.LogError(response);
            throw new ArgumentException("Merchant Service can not respond success response");
        }

        var stores = await responseMessage.Content.ReadFromJsonAsync<PagedListViewModel<StoreViewModel>>();

        return stores;
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