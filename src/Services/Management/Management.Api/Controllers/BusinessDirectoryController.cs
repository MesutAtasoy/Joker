using System.Threading.Tasks;
using Management.Application.BusinessDirectories.Queries.GetBusinessDirectories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Management.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/BusinessDirectories")]
    public class BusinessDirectoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public BusinessDirectoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns business directories
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
            => Ok(await _mediator.Send(new GetBusinessDirectoriesQuery()));
    }
}