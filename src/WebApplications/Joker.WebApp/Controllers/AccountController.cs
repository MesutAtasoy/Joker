using Joker.WebApp.Services.Abstract;
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

    public AccountController(IUserService userService,  
        IFavoriteService favoriteService)
    {
        _userService = userService;
        _favoriteService = favoriteService;
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
}