using Aggregator.Api.Models.Campaign;
using Aggregator.Api.Services.Campaign;
using Aggregator.Api.Services.Management;
using Aggregator.Api.Services.Store;
using Joker.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.Api.Controllers;

[ApiVersion("1")]
[Route("api/Campaigns")]
[Authorize(Policy = "ScopePolicy")]
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
        var store = await _storeService.GetByIdAsync(model.Store.Id);

        if (store == null)
        {
            return NotFound(new JokerBaseResponse<object>(null, 404, "Store is not found"));
        }

        model.Store.Name = store.Name;
        model.Merchant.Name = store.Merchant.Name;
            
        var businessDirectory = await _managementService.GetBusinessDirectoryByIdAsync(model.BusinessDirectory.Id);
        if (businessDirectory == null)
        {
            return NotFound(new JokerBaseResponse<object>(null, 404, "Business directory is not found"));
        }

        model.BusinessDirectory.Name = businessDirectory.Name;
            
        var response = await _campaignService.CreateAsync(model);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync([FromBody] UpdateCampaignModel model)
    {
        var response = await _campaignService.UpdateAsync(model);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        var response = await _campaignService.DeleteAsync(id);
        return StatusCode(response.StatusCode, response);
    }
}