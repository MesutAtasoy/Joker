using System;
using System.Threading.Tasks;
using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Merchant;
using Joker.WebApp.ViewModels.Merchant.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace Joker.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMerchantService _merchantService;
        private readonly IUserService _userService;

        public AccountController(IMerchantService merchantService,
            IUserService userService)
        {
            _merchantService = merchantService;
            _userService = userService;
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            
            var homeUrl = Url.Action(nameof(HomeController.Index), "Home");
            
            return new SignOutResult(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = homeUrl });
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        
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
        public async Task<IActionResult> MyMerchant(UpdateMerchantViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _merchantService.UpdateAsync(model);
                return RedirectToAction("Index", "Account");
            }
            
            return View(model);
        }


        public async Task<IActionResult> MyStores(int page = 1)
        {
            var merchantId = _userService.GetOrganizationId();
            var stores = await _merchantService.GetStoresAsync(merchantId, page);
            return View(stores);
        }
    }
}