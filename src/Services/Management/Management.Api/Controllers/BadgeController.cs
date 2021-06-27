using System.Threading.Tasks;
using Management.Application.Badges.Queries.GetBadges;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Management.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/Badges")]
    public class BadgeController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public BadgeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Returns currencies
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
            => Ok(await _mediator.Send(new GetBadgesQuery()));
    }
}