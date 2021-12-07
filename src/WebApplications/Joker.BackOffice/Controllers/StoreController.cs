using Joker.BackOffice.Services.Abstract;
using Joker.BackOffice.ViewModels.Shared;
using Joker.BackOffice.ViewModels.Store.Request;
using Microsoft.AspNetCore.Mvc;

namespace Joker.BackOffice.Controllers;

public class StoreController : BaseController
{
    private readonly IUserService _userService;
    private readonly IMerchantService _merchantService;
    private readonly ILocationService _locationService;

    public StoreController(IUserService userService,
        IMerchantService merchantService,
        ILocationService locationService)
    {
        _userService = userService;
        _merchantService = merchantService;
        _locationService = locationService;
    }


    public async Task<ActionResult> Index()
    {
        var stores = await _merchantService.GetStoresAsync();
        return View(stores);
    }

    public async Task FillViewBagInNewAsync()
    {
        var country = await _locationService.GetCountryAsync();

        var merchants = await _merchantService.GetMerchants();

        var listViewMerchants = new List<IdNameViewModel>
        {
            new IdNameViewModel { Id = Guid.Empty, Name = "" },
        };
        
        listViewMerchants.AddRange(merchants.Select(x=> new IdNameViewModel
        {
            Id = x.Id,
            Name = x.Name
        }));

        ViewBag.Merchants = listViewMerchants;
        ViewBag.Countries = new List<IdNameViewModel>
        {
            new IdNameViewModel { Id = Guid.Empty, Name = "" },
            country
        };
    }
    
    public async Task<IActionResult> New()
    {
        await FillViewBagInNewAsync();

        return View(new CreateStoreViewModel
        {
            MerchantId = _userService.GetOrganizationId()
        });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> New(CreateStoreViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _merchantService.CreateStoreAsync(model);
            if (result.StatusCode == 200)
            {
                return RedirectToAction("Index", "Store");
            }

            ModelState.AddModelError("", result.Message);
        }
        
        await FillViewBagInNewAsync();

        return View(model);
    }
    
    public async Task<IActionResult> Edit(Guid id)
    {
        var store = await _merchantService.GetStoreByIdAsync(id);
        if (store == null)
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateStoreViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _merchantService.UpdateStoreAsync(viewModel);
            if (result.StatusCode == 200)
            {
                return RedirectToAction("Index", "Store");
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