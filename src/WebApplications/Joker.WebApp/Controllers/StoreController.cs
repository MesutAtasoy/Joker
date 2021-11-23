using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Favorite.Request;
using Joker.WebApp.ViewModels.Search.Request;
using Joker.WebApp.ViewModels.Store;
using Microsoft.AspNetCore.Mvc;

namespace Joker.WebApp.Controllers;

public class StoreController : Controller
{
    private readonly ISearchService _searchService;
    private readonly IFavoriteService _favoriteService;

    public StoreController(ISearchService searchService, 
        IFavoriteService favoriteService)
    {
        _searchService = searchService;
        _favoriteService = favoriteService;
    }

    public async Task<IActionResult> Explore(Guid id)
    {
        var store = await _searchService.SearchStoreAsync(new StoreSearchRequest
        {
            StoreId = id
        });
        if (store.TotalDocumentCount == 0)
        {
            return NotFound("store is not found");
        }

        var campaigns = await _searchService.SearchCampaignAsync(new CampaignSearchRequest
        {
            StoreId = id.ToString()
        });

        var exploreViewModel = new StoreExploreViewModel
        {
            Store = store.Documents.FirstOrDefault(),
            Campaigns = campaigns
        };
        return View(exploreViewModel);
    }
        
    [HttpPost]
    public async Task<JsonResult> AddFavoriteStore(Guid storeId, string storeName)
    {
        var response = await _favoriteService.AddFavoriteStoreAsync(new AddFavoriteStoreViewModel
        {
            Id = storeId,
            Name = storeName
        });

        var isSuccess = response.StatusCode == 200;
        return new JsonResult(isSuccess);
    }
}