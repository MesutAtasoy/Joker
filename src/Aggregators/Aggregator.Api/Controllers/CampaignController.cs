using System;
using System.Threading.Tasks;
using Aggregator.Api.Models.Campaign;
using Aggregator.Api.Services.Campaign;
using Aggregator.Api.Services.Management;
using Aggregator.Api.Services.Store;
using Joker.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/Campaigns")]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;
        private readonly IStoreService _storeService;
        private readonly IManagementService _managementService;


        public CampaignController(ICampaignService campaignService,
            IStoreService storeService, 
            IManagementService managementService)
        {
            _campaignService = campaignService;
            _storeService = storeService;
            _managementService = managementService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] CreateCampaignModel model)
        {
            var store = await _storeService.GetByIdAsync(model.Store.Id.ToGuid());

            if (store == null)
            {
                return BadRequest("Store is not found");
            }

            model.Store.Name = store.Name;

            var businessDirectory = await _managementService.GetBusinessDirectoryByIdAsync(model.BusinessDirectory.Id.ToGuid());
            if (businessDirectory == null)
            {
                return BadRequest("Business directory is not found");
            }

            model.BusinessDirectory.Name = businessDirectory.Name;
            
            var merchant = await _campaignService.CreateAsync(model);
            return Ok(merchant);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateCampaignModel model)
        {
            var merchant = await _campaignService.UpdateAsync(model);
            return Ok(merchant);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var result = await _campaignService.DeleteAsync(id);
            return Ok(result);
        }
    }
}