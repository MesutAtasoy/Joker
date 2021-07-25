using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Joker.WebApp.Models;
using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Search.Request;
using Microsoft.AspNetCore.Authorization;

namespace Joker.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISearchService _searchService;

        public HomeController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public async Task<IActionResult> Index()
        {
            var campaigns = await _searchService.SearchCampaignAsync(new CampaignSearchRequest
            {
                PageSize = 5,
                Page = 1
            });
            return View(campaigns);
        }

        [Authorize]
        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}