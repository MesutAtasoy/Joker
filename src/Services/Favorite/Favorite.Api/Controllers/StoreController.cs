using Favorite.Application.Stores.Commands.CreateFavoriteStore;
using Favorite.Application.Stores.Queries.GetStoresByStoreId;
using Favorite.Application.Stores.Queries.GetStoresByUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Favorite.Api.Controllers;

[ApiVersion("1")]
[Route("api/Stores")]
[Authorize]
public class StoreController : ControllerBase
{
    private readonly IMediator _mediator;

    public StoreController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Create a new favorite Store
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateFavoriteStoreCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
        
    /// <summary>
    /// Returns favorite Stores by Store Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("Stores/{id}")]
    public async Task<IActionResult> GetByStoreIdAsync(Guid id)
    {
        return Ok(await _mediator.Send(new GetStoresByStoreIdQuery(id)));
    }
        
    /// <summary>
    /// Returns favorite Stores by Store Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("Users/{id}")]
    public async Task<IActionResult> GetByUserIdAsync(string id)
    {
        return Ok(await _mediator.Send(new GetStoresByUserIdQuery(id)));
    }
}