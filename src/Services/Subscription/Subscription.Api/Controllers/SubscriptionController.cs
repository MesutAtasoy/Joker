using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Subscription.Application.Subscriptions.Queries.GetSubscription;

namespace Subscription.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/Subscriptions")]
    public class SubscriptionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SubscriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(Guid id)
        {
            return Ok(await _mediator.Send(new GetSubscriptionQuery(id)));
        }
    }
}