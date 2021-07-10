using System;
using System.Threading.Tasks;
using Aggregator.Api.Models.Merchant;
using Aggregator.Api.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/Merchants")]
    public class MerchantController : ControllerBase
    {
        private readonly IMerchantService _merchantService;
        
        public MerchantController(IMerchantService merchantService)
        {
            _merchantService = merchantService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody]CreateMerchantModel model)
        {
            var merchant = await _merchantService.CreateAsync(model);
            return Ok(merchant);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateMerchantModel model)
        {
            var merchant = await _merchantService.UpdateAsync(model);
            return Ok(merchant);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var result = await _merchantService.DeleteAsync(id);
            return Ok(result);
        }
    }
}