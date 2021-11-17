using Joker.WebApp.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joker.WebApp.Controllers;

[Authorize]
public class SubscriptionController : Controller
{
    private readonly IManagementApiService _managementApiService;
        
    public SubscriptionController(IManagementApiService managementApiService)
    {
        _managementApiService = managementApiService;
    }
        
    public async Task<IActionResult> Index()
    {
        if (User.IsInRole("PaidUser"))
        {
            return RedirectToAction("Index", "Account");
        }
        var pricingPlans = await _managementApiService.GetPricingPlansAsync();
        return View(pricingPlans);
    }
}