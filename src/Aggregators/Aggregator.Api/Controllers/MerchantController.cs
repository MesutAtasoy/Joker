using System;
using System.Threading.Tasks;
using Aggregator.Api.Models.Merchant;
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
        
        public MerchantController(IMerchantService merchantService)
        {
            _merchantService = merchantService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody]CreateMerchantModel model)
        {
            var merchant = await _merchantService.CreateAsync(model);
            return Ok(new JokerBaseResponse<MerchantModel>(merchant));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateMerchantModel model)
        {
            var merchant = await _merchantService.UpdateAsync(model);
            return Ok(new JokerBaseResponse<MerchantModel>(merchant));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var result = await _merchantService.DeleteAsync(id);
            return Ok(new JokerBaseResponse<bool>(result));
        }
    }
}