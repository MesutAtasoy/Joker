using System.Threading.Tasks;
using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels;
using Joker.WebApp.ViewModels.Search.Request;
using Microsoft.AspNetCore.Mvc;

namespace Joker.WebApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        // GET
        public async Task<IActionResult> Index(string q)
        {
            var campaigns = await _searchService.SearchCampaignAsync(new CampaignSearchRequest
            {
                q = q
            });

            return View(new SearchIndexViewModel{q = q, Campaigns = campaigns});
    }
    }
}