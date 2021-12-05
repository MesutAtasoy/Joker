using Joker.BackOffice.Services.Abstract;
using Joker.BackOffice.ViewModels.Campaign.Request;
using Joker.BackOffice.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Joker.BackOffice.Controllers;

public class CampaignController : BaseController
{
    private readonly IManagementApiService _managementApiService;
    private readonly IUserService _userService;
    private readonly IMerchantService _merchantService;
    private readonly ICampaignService _campaignService;

    public CampaignController(IManagementApiService managementApiService,
        IUserService userService,
        IMerchantService merchantService,
        ICampaignService campaignService)
    {
        _managementApiService = managementApiService;
        _userService = userService;
        _merchantService = merchantService;
        _campaignService = campaignService;
    }

    public async Task<ActionResult> Index()
    {
        var campaigns = await _campaignService.GetCampaignsAsync();
        return View(campaigns);
    }
    
    public async Task<IActionResult> New()
    {
        await FillViewBagInNewAsync();

        return View(new CreateCampaignViewModel
        {
            Channel = "WEB"
        });
    }

    public async Task FillViewBagInNewAsync()
    {
        var businessDirectories = await _managementApiService.GetBusinessDirectoriesAsync();
        var merchants = await _merchantService.GetMerchants();
        
        ViewBag.BusinessDirectories = businessDirectories;
        
        List<IdNameViewModel> models = new List<IdNameViewModel>
        {
            new (){ Id = Guid.Empty, Name = "" }
        };
        var merchantList = merchants.Select(x => new IdNameViewModel { Id = x.Id, Name = x.Name }).ToList();
        models.AddRange(merchantList);
        
        ViewBag.Merchants = models;
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> New(CreateCampaignViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _campaignService.CreateAsync(model);
            if (result.StatusCode == 200)
            {
                return RedirectToAction("Index", "Campaign");
            }
                
            ModelState.AddModelError("", result.Message);
            await FillViewBagInNewAsync();
        }

        await FillViewBagInNewAsync();
        
        return View(model);
    }
    
    public async Task<IActionResult> Edit(Guid id)
    {
        var campaign = await _campaignService.GetByIdAsync(id);
        if (campaign == null)
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
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateCampaignViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _campaignService.UpdateAsync(model);
            if (result.StatusCode == 200)
            {
                return RedirectToAction("Index", "Campaign");
            }
                
            ModelState.AddModelError("", result.Message);
        }

        return View(model);
    }
    
    
    [HttpGet]
    public async Task<JsonResult> GetStores(Guid merchantId)
    {
        List<IdNameViewModel> models = new List<IdNameViewModel>
        {
            new (){ Id = Guid.Empty, Name = "" }
        };
        
        if (merchantId == Guid.Empty)
        {
            return Json(models);
        }

        var pagedListViewModel = await _merchantService.GetStoresAsync(merchantId, 1, Int32.MaxValue);
        models.AddRange(pagedListViewModel.Data.Select(x => new IdNameViewModel
        {
            Id = x.Id,
            Name = x.Name
        }));
        
        return Json(models);
    }
}