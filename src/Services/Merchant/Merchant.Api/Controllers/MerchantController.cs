using Joker.Extensions.Models;
using MediatR;
using Merchant.Application.Merchants.Commands.CreateMerchant;
using Merchant.Application.Merchants.Commands.DeleteMerchant;
using Merchant.Application.Merchants.Commands.UpdateMerchant;
using Merchant.Application.Merchants.Dto.Requests;
using Merchant.Application.Merchants.Queries.GetMerchantById;
using Merchant.Application.Stores.Queries.GetStoresByMerchantId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Merchant.Api.Controllers;

[ApiVersion("1")]
[Route("api/Merchants")]
[Authorize(Policy = "ScopePolicy")]
public class MerchantController : ControllerBase
{
    private readonly IMediator _mediator;

    public MerchantController(IMediator mediator)
    {
        _mediator = mediator;
    }
        
        
    /// <summary>
    /// Returns merchant by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        return Ok(await _mediator.Send(new GetMerchantByIdQuery(id)));
    }


    /// <summary>
    /// Returns stores
    /// </summary>
    /// <param name="id"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet("{id}/Stores")]
    public async Task<ActionResult> GetStoresAsync(Guid id, PaginationFilter filter)
    {
        return Ok(await _mediator.Send(new GetStoresByMerchantIdQuery(id, filter)));
    }
        
        
    /// <summary>
    /// Create a new merchant
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateMerchantCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
        
    /// <summary>
    /// Updates a merchant
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateMerchantDto command)
    {
        return Ok(await _mediator.Send(new UpdateMerchantCommand(id,command)));
    }
        
        
    /// <summary>
    /// Deletes a merchant
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        return Ok(await _mediator.Send(new DeleteMerchantCommand(id)));
    }
}