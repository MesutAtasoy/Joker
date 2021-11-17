using Joker.WebApp.ViewModels.Search;

namespace Joker.WebApp.ViewModels;

public class SearchIndexViewModel
{
    public string q { get; set; }
    public SearchBaseResponse<CampaignSearchResponse> Campaigns { get; set; }
}