using Aggregator.StoreFront.Api.Models.Merchant.Requests;
using Aggregator.StoreFront.Api.Services.Identity;
using Aggregator.StoreFront.Api.Services.Management;
using Aggregator.StoreFront.Api.Services.Merchant;
using Joker.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.StoreFront.Api.Controllers;

[ApiVersion("1")]
[Route("api/Merchant")]
[Authorize(Policy = "ScopePolicy")]
public class MerchantController : ControllerBase
{
    private readonly IMerchantService _merchantService;
    private readonly IManagementService _managementService;
    private readonly IIdentityService _identityService;

    public MerchantController(IMerchantService merchantService,
        IManagementService managementService,
        IIdentityService identityService)
    {
        _merchantService = merchantService;
        _managementService = managementService;
        _identityService = identityService;
    }


    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] CreateMerchantModel model)
    {
        var pricingPlan = await _managementService.GetPricingPlanByIdAsync(model.PricingPlanId);
        if (pricingPlan == null)
        {
            return NotFound(new JokerBaseResponse<object>(null, 404, "Pricing plan is not found"));
        }

        var organizationResult = await _identityService.CreateOrganization(model.Name);
        if (!organizationResult.IsSucceed)
        {
            return StatusCode(400, new JokerBaseResponse<object>
            {
                Message = "Merchant User Can not created",
                StatusCode = 400
            });
        }
        
        var response = await _merchantService.CreateAsync(model, 
            pricingPlan.Id,
            pricingPlan.Name,
            organizationResult.Response.OrganizationId);
        
        //ToDo: Depends on response, roll back scenerios needs to be wrote. 
        return StatusCode(response.StatusCode, response);
    }
}