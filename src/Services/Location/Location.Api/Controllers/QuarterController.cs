using System.Threading.Tasks;
using Location.Application.Quarters.Queries.GetQuarter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Location.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/Quarters")]
    public class QuarterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuarterController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Returns quarters
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiVersion("1")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Quarters([FromQuery] GetQuarterQuery query)
            => Ok(await _mediator.Send(query));
    }
}