using System;
using System.Threading.Tasks;
using Joker.WebApp.ViewModels;
using Joker.WebApp.ViewModels.Campaign;
using Joker.WebApp.ViewModels.Campaign.Request;
using Joker.WebApp.ViewModels.Shared;

namespace Joker.WebApp.Services.Abstract
{
    public interface ICampaignService
    {
        Task<PagedListViewModel<CampaignViewModel>> GetCampaigns(Guid merchantId, int page = 1, int pageSize = 20);
        Task<JokerBaseResponseViewModel<CampaignViewModel>> CreateAsync(CreateCampaignViewModel viewModel);
        Task<JokerBaseResponseViewModel<CampaignViewModel>> UpdateAsync(UpdateCampaignViewModel viewModel);
        Task<CampaignViewModel> GetByIdAsync(Guid id);
    }
}