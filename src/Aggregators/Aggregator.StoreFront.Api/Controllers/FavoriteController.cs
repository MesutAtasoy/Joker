using Aggregator.StoreFront.Api.Models.Favorite;
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
    public async Task<ActionResult> AddFavoriteCampaign([FromBody] AddCampaignModel model)
    {
        var campaign = await _campaignService.GetByIdAsync(model.Id);
        if (campaign == null)
        {
            return NotFound(new JokerBaseResponse<object>(null, 404, "Campaign is not found"));
        }

        model.Title = campaign.Title;
        model.Slug = campaign.Slug;
        model.SlugKey = campaign.SlugKey;

        var response = await _favoriteService.AddFavoriteCampaignAsync(model);

        return StatusCode(response.StatusCode, response);
    }
        
    [HttpPost("Stores")]
    public async Task<ActionResult> AddFavoriteStore([FromBody] AddStoreModel model)
    {
        var store = await _storeService.GetByIdAsync(model.Id);
        if (store == null)
        {
            return NotFound(new JokerBaseResponse<object>(null, 404, "Campaign is not found"));
        }

        model.Name = store.Name;
           
        var response = await _favoriteService.AddFavoriteStoreAsync(model);

        return StatusCode(response.StatusCode, response);
    }
}