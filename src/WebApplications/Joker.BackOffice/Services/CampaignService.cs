using Joker.BackOffice.Services.Abstract;
using Joker.BackOffice.ViewModels.Campaign;
using Joker.BackOffice.ViewModels.Campaign.Request;
using Joker.BackOffice.ViewModels.Shared;

namespace Joker.BackOffice.Services;

public class CampaignService : BaseService, ICampaignService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly HttpClient _httpClient;
    private readonly ILogger<CampaignService> _logger;

    public CampaignService(IHttpClientFactory clientFactory, 
        ILogger<CampaignService> logger) : base(logger)
    {
        _clientFactory = clientFactory;
        _logger = logger;
        _httpClient = _clientFactory.CreateClient("GatewayApi");
    }

    public async Task<PagedListViewModel<CampaignViewModel>> GetCampaignsAsync(int page = 1, int pageSize = 20)
    {
        var responseMessage = await _httpClient.GetAsync($"campaign/api/Campaigns");
        if (!responseMessage.IsSuccessStatusCode)
        {
            var response = await responseMessage.Content.ReadAsStringAsync();
            _logger.LogError(response);
            throw new ArgumentException("Merchant Service can not respond success response");
        }

        var campaigns = await responseMessage.Content.ReadFromJsonAsync<PagedListViewModel<CampaignViewModel>>();

        return campaigns;
    }

    public async Task<PagedListViewModel<CampaignViewModel>> GetCampaigns(Guid merchantId, int page = 1,
        int pageSize = 20)
    {
        var responseMessage = await _httpClient.GetAsync($"campaign/api/Campaigns/Merchants/{merchantId}");
        if (!responseMessage.IsSuccessStatusCode)
        {
            var response = await responseMessage.Content.ReadAsStringAsync();
            _logger.LogError(response);
            throw new ArgumentException("Merchant Service can not respond success response");
        }

        var campaigns = await responseMessage.Content.ReadFromJsonAsync<PagedListViewModel<CampaignViewModel>>();

        return campaigns;
    }

    public async Task<JokerBaseResponseViewModel<CampaignViewModel>> CreateAsync(CreateCampaignViewModel viewModel)
    {
        var responseMessage = await _httpClient.PostAsJsonAsync("aggregator/api/Campaigns", viewModel);
        return await HandleRequestAsync<CampaignViewModel>(responseMessage);
    }

    public async Task<JokerBaseResponseViewModel<CampaignViewModel>> UpdateAsync(UpdateCampaignViewModel viewModel)
    {
        var responseMessage = await _httpClient.PutAsJsonAsync($"aggregator/api/Campaigns", viewModel);
        return await HandleRequestAsync<CampaignViewModel>(responseMessage);
    }

    public async Task<CampaignViewModel> GetByIdAsync(Guid id)
    {
        var responseMessage = await _httpClient.GetAsync($"campaign/api/Campaigns/{id}");
        if (!responseMessage.IsSuccessStatusCode)
        {
            var response = await responseMessage.Content.ReadAsStringAsync();
            _logger.LogError(response);
            throw new ArgumentException("Campaign Service can not respond success response");
        }

        var campaign = await responseMessage.Content.ReadFromJsonAsync<CampaignViewModel>();

        return campaign;
    }
}