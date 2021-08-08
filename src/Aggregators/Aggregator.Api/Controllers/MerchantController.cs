using System;
using System.Threading.Tasks;
using Aggregator.Api.Models.Merchant;
using Aggregator.Api.Services.Management;
using Aggregator.Api.Services.Merchant;
using Joker.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/Merchants")]
    [Authorize]
    public class MerchantController : ControllerBase
    {
        private readonly IMerchantService _merchantService;
        private readonly IManagementService _managementService;

        public MerchantController(IMerchantService merchantService,
            IManagementService managementService)
        {
            _merchantService = merchantService;
            _managementService = managementService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] CreateMerchantModel model)
        {
            var pricingPlan = await _managementService.GetPricingPlanByIdAsync(model.PricingPlanId);
            if (pricingPlan == null)
            {
                return BadRequest("Pricing plan is not found");
            }

            var response = await _merchantService.CreateAsync(model, pricingPlan.Id, pricingPlan.Name);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateMerchantModel model)
        {
            var response = await _merchantService.UpdateAsync(model);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var result = await _merchantService.DeleteAsync(id);
            return Ok(result);
        }
    }
}