using System;
using System.Threading.Tasks;
using Favorite.Application.Campaigns.Commands.CreateFavoriteCampaign;
using Favorite.Application.Campaigns.Queries.GetCampaignsByCampaignId;
using Favorite.Application.Campaigns.Queries.GetCampaignsByUserId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Favorite.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/Campaigns")]
    public class CampaignController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CampaignController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new favorite campaign
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateFavoriteCampaignCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        
        /// <summary>
        /// Returns favorite campaigns by campaign Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Campaigns/{id}")]
        public async Task<IActionResult> GetByCampaignIdAsync(Guid id)
        {
            return Ok(await _mediator.Send(new GetCampaignsByCampaignIdQuery(id)));
        }
        
        /// <summary>
        /// Returns favorite campaigns by campaign Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Users/{id}")]
        public async Task<IActionResult> GetByUserIdAsync(string id)
        {
            return Ok(await _mediator.Send(new GetCampaignsByUserIdQuery(id)));
        }
    }
}