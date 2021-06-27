using System.Threading.Tasks;
using Management.Application.Languages.Queries.GetLanguage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Management.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/Languages")]
    public class LanguageController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public LanguageController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Returns currencies
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
            => Ok(await _mediator.Send(new GetLanguageQuery()));
    }
}