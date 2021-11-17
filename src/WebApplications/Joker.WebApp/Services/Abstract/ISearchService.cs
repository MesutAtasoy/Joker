using Joker.WebApp.ViewModels.Search;
using Joker.WebApp.ViewModels.Search.Request;

namespace Joker.WebApp.Services.Abstract;

public interface ISearchService
{
    Task<SearchBaseResponse<CampaignSearchResponse>> SearchCampaignAsync(CampaignSearchRequest request);
    Task<SearchBaseResponse<StoreSearchResponse>> SearchStoreAsync(StoreSearchRequest request);
}