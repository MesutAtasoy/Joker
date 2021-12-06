using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Merchant.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joker.WebApp.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly IFavoriteService _favoriteService;
    private readonly IUserService _userService;
    private readonly IManagementService _managementService;
    private readonly IMerchantService _merchantService;

    public AccountController(IUserService userService,  
        IFavoriteService favoriteService, 
        IManagementService managementService, 
        IMerchantService merchantService)
    {
        _userService = userService;
        _favoriteService = favoriteService;
        _managementService = managementService;
        _merchantService = merchantService;
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            
        var homeUrl = Url.Action(nameof(HomeController.Index), "Home");
            
        return new SignOutResult(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = homeUrl });
    }

    public ActionResult Login()
    {
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }
        
    public async Task<IActionResult> MyFavoriteCampaigns()
    {
        var userId = _userService.GetUserId();
        var favoriteCampaigns = await _favoriteService.GetFavoriteCampaignAsync(userId);
        return View(favoriteCampaigns);
    }
        
    public async Task<IActionResult> MyFavoriteStores()
    {
        var userId = _userService.GetUserId();
        var favoriteStores = await _favoriteService.GetFavoriteStoreAsync(userId);
        return View(favoriteStores);
    }

    
    public async Task<ActionResult> New(string id)
    {
        var pricingPlan = await _managementService.GetPricingPlanAsync(id);
        ViewData["PricingPlan"] = pricingPlan;
        return View();
    }
        
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> New(CreateMerchantViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _merchantService.CreateAsync(model);
            if (response.StatusCode == 200)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            
                var homeUrl = Url.Action(nameof(HomeController.Index), "Home");
            
                return new SignOutResult(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = homeUrl });
            }

            ModelState.AddModelError("", response.Message);
        }
            
        return View(model);
    }
}