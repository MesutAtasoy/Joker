using System.Threading.Tasks;
using Location.Application.Countries.Queries.GetCountry;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Location.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/Counties")]
    public class CountryController : ControllerBase
    {

        private readonly IMediator _mediator;
        
        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Returns countries
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Countries([FromQuery] GetCountryQuery query)
            => Ok(await _mediator.Send(query));
    }
}