using System.Threading.Tasks;
using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Merchant.Request;
using Microsoft.AspNetCore.Mvc;

namespace Joker.WebApp.Controllers
{
    public class MerchantController : Controller
    {
        private readonly IManagementApiService _managementApiService;

        public MerchantController(IManagementApiService managementApiService)
        {
            _managementApiService = managementApiService;
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
            return View();
        }
    }
}