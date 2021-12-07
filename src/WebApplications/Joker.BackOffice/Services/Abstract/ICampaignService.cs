using Joker.BackOffice.ViewModels.Campaign;
using Joker.BackOffice.ViewModels.Campaign.Request;
using Joker.BackOffice.ViewModels.Shared;

namespace Joker.BackOffice.Services.Abstract;

public interface ICampaignService
{
    Task<PagedListViewModel<CampaignViewModel>> GetCampaignsAsync(int page = 1, int pageSize = 20);
    Task<PagedListViewModel<CampaignViewModel>> GetCampaigns(Guid merchantId, int page = 1, int pageSize = 20);
    Task<JokerBaseResponseViewModel<CampaignViewModel>> CreateAsync(CreateCampaignViewModel viewModel);
    Task<JokerBaseResponseViewModel<CampaignViewModel>> UpdateAsync(UpdateCampaignViewModel viewModel);
    Task<CampaignViewModel> GetByIdAsync(Guid id);
}