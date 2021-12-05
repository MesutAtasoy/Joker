using Campaign.Application.Campaigns.Command.CreateCampaign;
using Campaign.Application.Campaigns.Command.DeleteCampaign;
using Campaign.Application.Campaigns.Command.UpdateCampaign;
using Campaign.Application.Campaigns.Dto.Request;
using Campaign.Application.Campaigns.Queries.GetCampaignById;
using Campaign.Application.Campaigns.Queries.GetCampaigns;
using Campaign.Application.Campaigns.Queries.GetCampaignsByMerchantId;
using Joker.Extensions.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Campaign.Api.Controllers;

[ApiVersion("1")]
[Route("api/Campaigns")]
[Authorize(Policy = "ScopePolicy")]
public class CampaignController : ControllerBase
{
    private readonly IMediator _mediator;

    public CampaignController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult> GetCampaigns([FromQuery] PaginationFilter filter)
    {
        return Ok(await _mediator.Send(new GetCampaignsQuery(filter)));
    }

    [HttpGet("Merchants/{id}")]
    public async Task<ActionResult> GetByMerchantIdAsync(Guid id, [FromQuery] PaginationFilter filter)
    {
        return Ok(await _mediator.Send(new GetCampaignsByMerchantIdQuery(id, filter)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetAsync(Guid id)
    {
        return Ok(await _mediator.Send(new GetCampaignByIdQuery(id)));
    }

    [HttpPost]
    public async Task<ActionResult> AddAsync([FromBody] CreateCampaignCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] UpdateCampaignDto command)
    {
        return Ok(await _mediator.Send(new UpdateCampaignCommand(id, command)));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        return Ok(await _mediator.Send(new DeleteCampaignCommand(id)));
    }
}