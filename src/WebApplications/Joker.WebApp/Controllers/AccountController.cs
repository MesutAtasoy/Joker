using System.Threading.Tasks;
using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Merchant.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joker.WebApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IMerchantService _merchantService;
        private readonly ICampaignService _campaignService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IFavoriteService _favoriteService;
        private readonly IUserService _userService;

        public AccountController(IMerchantService merchantService,
            IUserService userService, 
            ICampaignService campaignService, 
            ISubscriptionService subscriptionService, 
            IFavoriteService favoriteService)
        {
            _merchantService = merchantService;
            _userService = userService;
            _campaignService = campaignService;
            _subscriptionService = subscriptionService;
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
        
        [Authorize(Roles = "PaidUser")]
        public async Task<IActionResult> MyMerchant()
        {
            var merchantId = _userService.GetOrganizationId();
            var merchant = await _merchantService.GetByIdAsync(merchantId);
            var updateMerchantViewModel = new UpdateMerchantViewModel
            {
                Id = merchantId.ToString(),
                Name = merchant.Name,
                Description = merchant.Description,
                Email = merchant.Email,
                Slogan = merchant.Slogan,
                PhoneNumber = merchant.PhoneNumber,
                TaxNumber = merchant.TaxNumber,
                WebSiteUrl = merchant.WebSiteUrl
            };
            return View(updateMerchantViewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "PaidUser")]
        public async Task<IActionResult> MyMerchant(UpdateMerchantViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _merchantService.UpdateAsync(model);
                return RedirectToAction("Index", "Account");
            }
            
            return View(model);
        }
        
        public async Task<IActionResult> MyFavoriteCampaigns()
        {
            var userId = _userService.GetUserId();
            var favoriteCampaigns = await _favoriteService.GetFavoriteCampaignAsync(userId);
            return View(favoriteCampaigns);
        }

        [Authorize(Roles = "PaidUser")]
        public async Task<IActionResult> MyStores(int page = 1)
        {
            var merchantId = _userService.GetOrganizationId();
            var stores = await _merchantService.GetStoresAsync(merchantId, page);
            return View(stores);
        }
        
        [Authorize(Roles = "PaidUser")]
        public async Task<IActionResult> MyCampaigns(int page = 1)
        {
            var merchantId = _userService.GetOrganizationId();
            var campaigns = await _campaignService.GetCampaigns(merchantId, page);
            return View(campaigns);
        }
        
        [Authorize(Roles = "PaidUser")]
        public async Task<IActionResult> MySubscriptions()
        {
            var merchantId = _userService.GetOrganizationId();
            var subscriptions = await _subscriptionService.GetSubscriptions(merchantId);
            return View(subscriptions);
        }
    }
}