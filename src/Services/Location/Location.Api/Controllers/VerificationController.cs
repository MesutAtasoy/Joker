using System.Threading.Tasks;
using Location.Application.Quarters.Commands.Verification;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Location.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/Verification")]
    public class VerificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public VerificationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Validates the location
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Verification([FromBody] LocationVerificationCommand command)
            => Ok(await _mediator.Send(command));
    }
}