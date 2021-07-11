using System;
using System.Threading.Tasks;
using Aggregator.Api.Models.Location;
using Aggregator.Api.Models.Store;
using Aggregator.Api.Services.Location;
using Aggregator.Api.Services.Store;
using Joker.Extensions;
using Joker.Response;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/Stores")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly ILocationService _locationService;
        
        public StoreController(IStoreService storeService,
            ILocationService locationService)
        {
            _storeService = storeService;
            _locationService = locationService;
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] CreateStoreModel model)
        {
            var locationResponse = await _locationService.ValidateAsync(new LocationVerificationModel
            {
                CountryId = model.Location.Country.Id.ToGuid(),
                CityId = model.Location.City.Id.ToGuid(),
                DistrictId = model.Location.District.Id.ToGuid(),
                NeighborhoodId = model.Location.Neighborhood.Id.ToGuid(),
                QuarterId= model.Location.Quarter.Id.ToGuid(),
            });

            if (!locationResponse.IsValid)
            {
                return BadRequest(new JokerBaseResponse<object>(null, 400, "Location is invalid"));
            }

            model.Location.Country.Name = locationResponse.Country.Name;
            model.Location.City.Name = locationResponse.City.Name;
            model.Location.District.Name = locationResponse.District.Name;
            model.Location.Neighborhood.Name = locationResponse.Neighborhood.Name;
            model.Location.Quarter.Name = locationResponse.Quarter.Name;
            
            var store = await _storeService.CreateAsync(model);
            return Ok(new JokerBaseResponse<StoreModel>(store));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateStoreModel model)
        {
            var store = await _storeService.UpdateAsync(model);
            return Ok(new JokerBaseResponse<StoreModel>(store));
        }
        
        [HttpPut("Location")]
        public async Task<ActionResult> UpdateLocationAsync([FromBody] UpdateStoreLocationModel model)
        {
            var locationResponse = await _locationService.ValidateAsync(new LocationVerificationModel
            {
                CountryId = model.Location.Country.Id.ToGuid(),
                CityId = model.Location.City.Id.ToGuid(),
                DistrictId = model.Location.District.Id.ToGuid(),
                NeighborhoodId = model.Location.Neighborhood.Id.ToGuid(),
                QuarterId= model.Location.Quarter.Id.ToGuid(),
            });

            if (!locationResponse.IsValid)
            {
                return BadRequest(new JokerBaseResponse<object>(null, 400, "Location is invalid"));
            }

            model.Location.Country.Name = locationResponse.Country.Name;
            model.Location.City.Name = locationResponse.City.Name;
            model.Location.District.Name = locationResponse.District.Name;
            model.Location.Neighborhood.Name = locationResponse.Neighborhood.Name;
            model.Location.Quarter.Name = locationResponse.Quarter.Name;
            
            var store = await _storeService.UpdateLocationAsync(model);
            return Ok(new JokerBaseResponse<StoreLocationModel>(store));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var result = await _storeService.DeleteAsync(id);
            return Ok(new JokerBaseResponse<bool>(result));
        }
    }
}