using System.Threading.Tasks;
using Management.Application.Currencies.Queries.GetCurrency;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Management.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/Currencies")]
    public class CurrencyController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public CurrencyController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Returns currencies
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
            => Ok(await _mediator.Send(new GetCurrencyQuery()));
    }
}