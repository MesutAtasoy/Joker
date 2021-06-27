using System.Threading.Tasks;
using Management.Application.PaymentMethods.Queries.GetPaymentMethod;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Management.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/PaymentMethods")]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public PaymentMethodController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Returns currencies
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
            => Ok(await _mediator.Send(new GetPaymentMethodQuery()));
    }
}