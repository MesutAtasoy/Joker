using Joker.WebApp.ViewModels.Search;

namespace Joker.WebApp.ViewModels.Store;

public class StoreExploreViewModel
{
    public StoreSearchResponse Store { get; set; }
    public SearchBaseResponse<CampaignSearchResponse> Campaigns { get; set; }
}