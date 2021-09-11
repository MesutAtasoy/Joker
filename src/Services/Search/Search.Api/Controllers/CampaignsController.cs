using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Search.Application.Campaigns.Queries.GetCampaignsByParam;

namespace Search.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/Campaigns")]
    public class CampaignsController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public CampaignsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns searched campaigns
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetCampaignsByParamQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
    
}