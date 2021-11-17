using MediatR;
using Microsoft.AspNetCore.Mvc;
using Search.Application.Stores.Queries.GetStoresByParam;

namespace Search.Api.Controllers;

[ApiVersion("1")]
[Route("api/Stores")]
public class StoresController : ControllerBase
{
    private readonly IMediator _mediator;
        
    public StoresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Returns searched stores
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] GetStoresByParamQuery query)
    {
        return Ok(await _mediator.Send(query));
    }
}