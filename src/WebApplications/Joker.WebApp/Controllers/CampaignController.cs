using System.Linq;
using System.Threading.Tasks;
using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels;
using Joker.WebApp.ViewModels.Search.Request;
using Microsoft.AspNetCore.Mvc;

namespace Joker.WebApp.Controllers
{
    public class CampaignsController : Controller
    {
        private readonly ISearchService _searchService;

        public CampaignsController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        // GET
        public async Task<IActionResult> Explore(string id)
        {
            var campaignViewModel = new CampaignDetailViewModel();
            
            var campaignResponse = await _searchService.SearchCampaignAsync(new CampaignSearchRequest
            {
                Slug = id
            });

            var campaign = campaignResponse.Documents.FirstOrDefault();

            if (campaign == null)
            {
                return NotFound("campaign is not found");
            }

            campaignViewModel.Campaign = campaign;

            var storeResponse  = await _searchService.SearchStoreAsync(new StoreSearchRequest
            {
                StoreId = campaign.StoreId
            });

            campaignViewModel.Store = storeResponse.Documents.FirstOrDefault();

            return View(campaignViewModel);
        }
    }
}