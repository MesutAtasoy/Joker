using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Campaign;
using Joker.WebApp.ViewModels.Campaign.Request;
using Joker.WebApp.ViewModels.Favorite.Request;
using Joker.WebApp.ViewModels.Search.Request;
using Joker.WebApp.ViewModels.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joker.WebApp.Controllers;

public class CampaignController : Controller
{
    private readonly ISearchService _searchService;
    private readonly IManagementApiService _managementApiService;
    private readonly IUserService _userService;
    private readonly IMerchantService _merchantService;
    private readonly ICampaignService _campaignService;
    private readonly IFavoriteService _favoriteService;
        
    public CampaignController(ISearchService searchService,
        IManagementApiService managementApiService,
        IUserService userService,
        IMerchantService merchantService,
        ICampaignService campaignService, IFavoriteService favoriteService)
    {
        _searchService = searchService;
        _managementApiService = managementApiService;
        _userService = userService;
        _merchantService = merchantService;
        _campaignService = campaignService;
        _favoriteService = favoriteService;
    }


    [Authorize(Roles = "PaidUser")]
    public async Task<IActionResult> New()
    {
        var merchantId = _userService.GetOrganizationId();
        var merchantName = _userService.GetOrganizationName();
        var stores = await _merchantService.GetStoresAsync(merchantId, 1, Int32.MaxValue);
        var businessDirectories = await _managementApiService.GetBusinessDirectoriesAsync();
        ViewBag.BusinessDirectories = businessDirectories;
        ViewBag.Stores = stores.Data;
        return View(new CreateCampaignViewModel
        {
            Merchant = new IdNameViewModel
            {
                Id = merchantId,
                Name = merchantName
            },
            Channel = "WEB"
        });
    }
        
    [Authorize(Roles = "PaidUser")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> New(CreateCampaignViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _campaignService.CreateAsync(model);
            if (result.StatusCode == 200)
            {
                return RedirectToAction("MyCampaigns", "Account");
            }
                
            ModelState.AddModelError("", result.Message);
            var merchantId = _userService.GetOrganizationId();
            var stores = await _merchantService.GetStoresAsync(merchantId, 1, Int32.MaxValue);
            var businessDirectories = await _managementApiService.GetBusinessDirectoriesAsync();
            ViewBag.BusinessDirectories = businessDirectories;
            ViewBag.Stores = stores.Data;
                
        }
        return View(model);
    }
        
    [Authorize(Roles = "PaidUser")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var campaign = await _campaignService.GetByIdAsync(id);
        if (campaign == null)
        {
            return NotFound();
        }
            
        var merchantId = _userService.GetOrganizationId();
        if (campaign.Merchant.RefId != merchantId)
        {
            return NotFound();
        }

        return View(new UpdateCampaignViewModel
        {
            CampaignId = campaign.Id,
            Code = campaign.Code,
            Condition = campaign.Condition,
            Description = campaign.Description,
            Title = campaign.Title
        });
    }
        
    [Authorize(Roles = "PaidUser")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateCampaignViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _campaignService.UpdateAsync(model);
            if (result.StatusCode == 200)
            {
                return RedirectToAction("MyCampaigns", "Account");
            }
                
            ModelState.AddModelError("", result.Message);
        }

        return View(model);
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