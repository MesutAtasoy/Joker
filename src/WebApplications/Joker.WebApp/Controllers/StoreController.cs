using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Joker.WebApp.Services.Abstract;
using Joker.WebApp.ViewModels;
using Joker.WebApp.ViewModels.Store.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joker.WebApp.Controllers
{
    public class StoreController : Controller
    {
        private readonly IUserService _userService;
        private readonly IManagementApiService _managementApiService;
        private readonly IMerchantService _merchantService;
        

        public StoreController(IUserService userService, 
            IManagementApiService managementApiService, 
            IMerchantService merchantService)
        {
            _userService = userService;
            _managementApiService = managementApiService;
            _merchantService = merchantService;
        }

        // GET
        [Authorize(Roles = "PaidUser")]
        public async Task<IActionResult> New()
        {
            var country  = await _managementApiService.GetCountryAsync();
            
            ViewBag.Countries = new List<IdNameViewModel>
            {
                new IdNameViewModel { Id = Guid.Empty, Name = ""},
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
                await _merchantService.CreateStoreAsync(model);
                return RedirectToAction("MyStores", "Account");
            }
            
            var country  = await _managementApiService.GetCountryAsync();
            
            ViewBag.Countries = new List<IdNameViewModel>
            {
                new IdNameViewModel { Id = Guid.Empty, Name = ""},
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
                await _merchantService.UpdateStoreAsync(viewModel);
                return RedirectToAction("MyStores", "Account");
            }

            return View(viewModel);
        }

        

        [HttpGet]
        public async Task<JsonResult> GetCities(Guid countryId)
        {
            List<IdNameViewModel> models = new List<IdNameViewModel>
            {
                new IdNameViewModel { Id = Guid.Empty, Name = ""}
            };
            if (countryId == Guid.Empty)
            {
                return Json(models);
            }
            var cities =  await _managementApiService.GetCitiesAsync(countryId);
            models.AddRange(cities);
            return Json(models);
        }
        
        [HttpGet]
        public async Task<JsonResult> GetDistricts(Guid cityId)
        {
            List<IdNameViewModel> models = new List<IdNameViewModel>
            {
                new IdNameViewModel { Id = Guid.Empty, Name = ""}
            };
            
            if (cityId == Guid.Empty)
            {
                return Json(models);
            }
            
            var districts =  await _managementApiService.GetDistrictsAsync(cityId);
            models.AddRange(districts);
            return Json(models);
        }
        
        [HttpGet]
        public async Task<JsonResult> GetNeighborhoods(Guid districtId)
        {
            List<IdNameViewModel> models = new List<IdNameViewModel>
            {
                new IdNameViewModel { Id = Guid.Empty, Name = ""}
            };
            if (districtId == Guid.Empty)
            {
                return Json(models);
            }
            
            var neighborhoods =  await _managementApiService.GetNeighborhoodsAsync(districtId);
            models.AddRange(neighborhoods);
            return Json(models);

        }
        
        [HttpGet]
        public async Task<JsonResult> GetQuarters(Guid neighborhoodId)
        {
            List<IdNameViewModel> models = new List<IdNameViewModel>
            {
                new IdNameViewModel { Id = Guid.Empty, Name = ""}
            };
            if (neighborhoodId == Guid.Empty)
            {
                return Json(models);
            }
            
            var quarters =  await _managementApiService.GetQuartersAsync(neighborhoodId);
            models.AddRange(quarters);
            return Json(models);
        }
    }
}