using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Campaign;
using Joker.WebApp.ViewModels.Favorite.Request;
using Joker.WebApp.ViewModels.Search.Request;
using Microsoft.AspNetCore.Mvc;

namespace Joker.WebApp.Controllers;

public class CampaignController : Controller
{
    private readonly ISearchService _searchService;
    private readonly IFavoriteService _favoriteService;
        
    public CampaignController(ISearchService searchService,
        IFavoriteService favoriteService)
    {
        _searchService = searchService;
        _favoriteService = favoriteService;
    }

    public async Task<IActionResult> Explore(string id)
    {
        var campaignViewModel = new CampaignDetailViewModel();
            
        var campaignResponse = await _searchService.SearchCampaignAsync(new CampaignSearchRequest
        {
            Slug = id
        });

        var campaign = campaignResponse.Documents.FirstOrDefault();

        if (campaign == null)
        {
            return NotFound("campaign is not found");
        }

        campaignViewModel.Campaign = campaign;

        var storeResponse  = await _searchService.SearchStoreAsync(new StoreSearchRequest
        {
            StoreId = campaign.StoreId
        });

        campaignViewModel.Store = storeResponse.Documents.FirstOrDefault();

        return View(campaignViewModel);
    }


    [HttpPost]
    public async Task<JsonResult> AddFavoriteCampaign(Guid campaignId, string campaignName)
    {
        var response = await _favoriteService.AddFavoriteCampaignAsync(new AddFavoriteCampaignViewModel
        {
            Id = campaignId,
            Title = campaignName
        });


        var isSuccess = response.StatusCode == 200;
        return new JsonResult(isSuccess);
    }
}