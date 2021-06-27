using System.Threading.Tasks;
using Location.Application.Neighborhoods.Queries.GetNeighborhood;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Location.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/Neighborhoods")]
    public class NeighborhoodController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public NeighborhoodController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Returns neighborhoods
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Neighborhoods([FromQuery] GetNeighborhoodQuery query)
            => Ok(await _mediator.Send(query));
    }
}