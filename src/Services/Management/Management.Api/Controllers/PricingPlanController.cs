using System;
using System.Threading.Tasks;
using Management.Application.PricingPlans.Queries.GetPricingPlanById;
using Management.Application.PricingPlans.Queries.GetPricingPlanBySlug;
using Management.Application.PricingPlans.Queries.GetPricingPlans;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Management.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/PricingPlans")]
    public class PricingPlanController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public PricingPlanController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Returns currencies
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
            => Ok(await _mediator.Send(new GetPricingPlansQuery()));
        
        /// <summary>
        /// Returns currencies
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
            => Ok(await _mediator.Send(new GetPricingPlanByIdQuery(id)));
        
        
        /// <summary>
        /// Returns currencies
        /// </summary>
        [HttpGet("BySlug/{slug}")]
        public async Task<IActionResult> GetBySlugAsync(string slug)
            => Ok(await _mediator.Send(new GetPricingPlanBySlugQuery(slug)));
    }
}