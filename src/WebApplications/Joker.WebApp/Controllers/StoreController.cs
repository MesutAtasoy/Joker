using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels.Favorite.Request;
using Joker.WebApp.ViewModels.Search.Request;
using Joker.WebApp.ViewModels.Shared;
using Joker.WebApp.ViewModels.Store;
using Joker.WebApp.ViewModels.Store.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joker.WebApp.Controllers
{
    public class StoreController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMerchantService _merchantService;
        private readonly ILocationService _locationService;
        private readonly ISearchService _searchService;
        private readonly IFavoriteService _favoriteService;

        public StoreController(IUserService userService,
            IMerchantService merchantService,
            ILocationService locationService,
            ISearchService searchService, 
            IFavoriteService favoriteService)
        {
            _userService = userService;
            _merchantService = merchantService;
            _locationService = locationService;
            _searchService = searchService;
            _favoriteService = favoriteService;
        }


        public async Task<IActionResult> Explore(Guid id)
        {
            var store = await _merchantService.GetStoreByIdAsync(id);
            if (store == null)
            {
                return NotFound("store is not found");
            }

            var campaigns = await _searchService.SearchCampaignAsync(new CampaignSearchRequest
            {
                StoreId = id.ToString()
            });

            var exploreViewModel = new StoreExploreViewModel
            {
                Store = store,
                Campaigns = campaigns
            };
            return View(exploreViewModel);
        }
        
        [HttpPost]
        public async Task<JsonResult> AddFavoriteStore(Guid storeId, string storeName)
        {
            var response = await _favoriteService.AddFavoriteStoreAsync(new AddFavoriteStoreViewModel
            {
                Id = storeId,
                Name = storeName
            });

            var isSuccess = response.StatusCode == 200;
            return new JsonResult(isSuccess);
        }

        // GET
        [Authorize(Roles = "PaidUser")]
        public async Task<IActionResult> New()
        {
            var country = await _locationService.GetCountryAsync();

            ViewBag.Countries = new List<IdNameViewModel>
            {
                new IdNameViewModel { Id = Guid.Empty, Name = "" },
                country
            };

            return View(new CreateStoreViewModel
            {
                MerchantId = _userService.GetOrganizationId()
            });
        }

        [Authorize(Roles = "PaidUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(CreateStoreViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _merchantService.CreateStoreAsync(model);
                if (result.StatusCode == 200)
                {
                    return RedirectToAction("MyStores", "Account");
                }

                ModelState.AddModelError("", result.Message);
            }

            var country = await _locationService.GetCountryAsync();

            ViewBag.Countries = new List<IdNameViewModel>
            {
                new() { Id = Guid.Empty, Name = "" },
                country
            };

            return View(model);
        }


        [Authorize(Roles = "PaidUser")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var store = await _merchantService.GetStoreByIdAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            var merchantId = _userService.GetOrganizationId();
            if (store.Merchant.RefId != merchantId)
            {
                return NotFound();
            }

            return View(new UpdateStoreViewModel
            {
                Id = store.Id,
                Description = store.Description,
                Email = store.Email,
                Name = store.Name,
                Slogan = store.Slogan,
                PhoneNumber = store.PhoneNumber
            });
        }

        [Authorize(Roles = "PaidUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateStoreViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _merchantService.UpdateStoreAsync(viewModel);
                if (result.StatusCode == 200)
                {
                    return RedirectToAction("MyStores", "Account");
                }

                ModelState.AddModelError("", result.Message);
            }

            return View(viewModel);
        }


        [HttpGet]
        public async Task<JsonResult> GetCities(Guid countryId)
        {
            List<IdNameViewModel> models = new List<IdNameViewModel>
            {
                new IdNameViewModel { Id = Guid.Empty, Name = "" }
            };
            if (countryId == Guid.Empty)
            {
                return Json(models);
            }

            var cities = await _locationService.GetCitiesAsync(countryId);
            models.AddRange(cities);
            return Json(models);
        }

        [HttpGet]
        public async Task<JsonResult> GetDistricts(Guid cityId)
        {
            List<IdNameViewModel> models = new List<IdNameViewModel>
            {
                new IdNameViewModel { Id = Guid.Empty, Name = "" }
            };

            if (cityId == Guid.Empty)
            {
                return Json(models);
            }

            var districts = await _locationService.GetDistrictsAsync(cityId);
            models.AddRange(districts);
            return Json(models);
        }

        [HttpGet]
        public async Task<JsonResult> GetNeighborhoods(Guid districtId)
        {
            List<IdNameViewModel> models = new List<IdNameViewModel>
            {
                new IdNameViewModel { Id = Guid.Empty, Name = "" }
            };
            if (districtId == Guid.Empty)
            {
                return Json(models);
            }

            var neighborhoods = await _locationService.GetNeighborhoodsAsync(districtId);
            models.AddRange(neighborhoods);
            return Json(models);
        }

        [HttpGet]
        public async Task<JsonResult> GetQuarters(Guid neighborhoodId)
        {
            List<IdNameViewModel> models = new List<IdNameViewModel>
            {
                new IdNameViewModel { Id = Guid.Empty, Name = "" }
            };
            if (neighborhoodId == Guid.Empty)
            {
                return Json(models);
            }

            var quarters = await _locationService.GetQuartersAsync(neighborhoodId);
            models.AddRange(quarters);
            return Json(models);
        }
    }
}