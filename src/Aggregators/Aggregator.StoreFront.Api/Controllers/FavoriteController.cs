using Aggregator.StoreFront.Api.Models.Favorite.Requests;
using Aggregator.StoreFront.Api.Services.Campaign;
using Aggregator.StoreFront.Api.Services.Favorite;
using Aggregator.StoreFront.Api.Services.Store;
using Joker.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.StoreFront.Api.Controllers;

[ApiVersion("1")]
[Route("api/Favorites")]
[Authorize(Policy = "ScopePolicy")]
public class FavoriteController : ControllerBase
{
    private readonly IFavoriteService _favoriteService;
    private readonly ICampaignService _campaignService;
    private readonly IStoreService _storeService;
        
    public FavoriteController(IFavoriteService favoriteService,
        ICampaignService campaignService, IStoreService storeService)
    {
        _favoriteService = favoriteService;
        _campaignService = campaignService;
        _storeService = storeService;
    }


    [HttpPost("Campaigns")]
    public async Task<ActionResult> AddFavoriteCampaign([FromBody] AddFavoriteCampaignRequestModel requestModel)
    {
        var campaign = await _campaignService.GetByIdAsync(requestModel.Id);
        if (campaign == null)
        {
            return NotFound(new JokerBaseResponse<object>(null, 404, "Campaign is not found"));
        }

        requestModel.Title = campaign.Title;
        requestModel.Slug = campaign.Slug;
        requestModel.SlugKey = campaign.SlugKey;

        var response = await _favoriteService.AddFavoriteCampaignAsync(requestModel);

        return StatusCode(response.StatusCode, response);
    }
        
    [HttpPost("Stores")]
    public async Task<ActionResult> AddFavoriteStore([FromBody] AddFavoriteStoreRequestModel requestModel)
    {
        var store = await _storeService.GetByIdAsync(requestModel.Id);
        if (store == null)
        {
            return NotFound(new JokerBaseResponse<object>(null, 404, "Campaign is not found"));
        }

        requestModel.Name = store.Name;
           
        var response = await _favoriteService.AddFavoriteStoreAsync(requestModel);

        return StatusCode(response.StatusCode, response);
    }
}