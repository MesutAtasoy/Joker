using Aggregator.Api.Models.Merchant;
using Aggregator.Api.Services.Management;
using Aggregator.Api.Services.Merchant;
using Joker.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.Api.Controllers;

[ApiVersion("1")]
[Route("api/Merchants")]
[Authorize(Policy = "ScopePolicy")]
public class MerchantController : ControllerBase
{
    private readonly IMerchantService _merchantService;

    public MerchantController(IMerchantService merchantService)
    {
        _merchantService = merchantService;
    }


    [HttpPut]
    public async Task<ActionResult> UpdateAsync([FromBody] UpdateMerchantModel model)
    {
        var response = await _merchantService.UpdateAsync(model);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        var result = await _merchantService.DeleteAsync(id);
        return StatusCode(result.StatusCode, result);
    }
}