using Location.Application.Districts.Queries.GetDistrict;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Location.Api.Controllers;

[ApiVersion("1")]
[Route("api/Districts")]
public class DistrictController : ControllerBase
{
    private readonly IMediator _mediator;
        
    public DistrictController(IMediator mediator)
    {
        _mediator = mediator;
    }
        
        
    /// <summary>
    /// Returns districts
    /// </summary>
    [HttpGet]
    [ApiVersion("1")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Districts([FromQuery] GetDistrictQuery query)
        => Ok(await _mediator.Send(query));
}