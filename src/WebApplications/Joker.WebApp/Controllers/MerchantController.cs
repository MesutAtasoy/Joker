using System.Threading.Tasks;
using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Merchant.Request;
using Microsoft.AspNetCore.Mvc;

namespace Joker.WebApp.Controllers
{
    public class MerchantController : Controller
    {
        private readonly IManagementApiService _managementApiService;
        private readonly IMerchantService _merchantService;

        public MerchantController(IManagementApiService managementApiService,
         IMerchantService merchantService)
        {
            _managementApiService = managementApiService;
            _merchantService = merchantService;
        }

        public async Task<ActionResult> New(string id)
        {
            var pricingPlan = await _managementApiService.GetPricingPlanAsync(id);
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
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", response.Message);
            }
            
            return View(model);
        }
    }
}