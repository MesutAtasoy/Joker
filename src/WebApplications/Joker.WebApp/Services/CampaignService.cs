using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Campaign;
using Joker.WebApp.ViewModels.Campaign.Request;
using Joker.WebApp.ViewModels.Shared;

namespace Joker.WebApp.Services;

public class CampaignService : BaseService, ICampaignService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly HttpClient _httpClient;

    public CampaignService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _httpClient = _clientFactory.CreateClient("GatewayApi");
    }

    public async Task<PagedListViewModel<CampaignViewModel>> GetCampaigns(Guid merchantId, int page = 1,
        int pageSize = 20)
    {
        var responseMessage = await _httpClient.GetAsync($"campaign/api/Campaigns/Merchants/{merchantId}");
        if (!responseMessage.IsSuccessStatusCode)
        {
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
            throw new ArgumentException("Campaign Service can not respond success response");
        }

        var campaign = await responseMessage.Content.ReadFromJsonAsync<CampaignViewModel>();

        return campaign;
    }
}