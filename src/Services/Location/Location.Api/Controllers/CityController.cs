using Location.Application.Cities.Queries.GetCity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Location.Api.Controllers;

[ApiVersion("1")]
[Route("api/Cities")]
public class CityController : ControllerBase
{
    private readonly IMediator _mediator;
        
    public CityController(IMediator mediator)
    {
        _mediator = mediator;
    }
        
    /// <summary>
    /// Returns cities
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Cities([FromQuery] GetCityQuery query)
        => Ok(await _mediator.Send(query));
}