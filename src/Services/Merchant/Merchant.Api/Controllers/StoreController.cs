using System;
using System.Threading.Tasks;
using MediatR;
using Merchant.Application.Stores.Commands.AddBusinessHour;
using Merchant.Application.Stores.Commands.AddFaq;
using Merchant.Application.Stores.Commands.CreateStore;
using Merchant.Application.Stores.Commands.DeleteStore;
using Merchant.Application.Stores.Commands.RemoveBusinessHour;
using Merchant.Application.Stores.Commands.RemoveFaq;
using Merchant.Application.Stores.Commands.UpdateLocation;
using Merchant.Application.Stores.Commands.UpdateStore;
using Merchant.Application.Stores.Dto;
using Merchant.Application.Stores.Dto.Request;
using Merchant.Application.Stores.Queries.GetStoreById;
using Microsoft.AspNetCore.Mvc;

namespace Merchant.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/Stores")]
    public class StoreController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StoreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns merchant by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return Ok(await _mediator.Send(new GetStoreByIdQuery(id)));
        }

        /// <summary>
        /// Create a new store
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateStoreCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        
        /// <summary>
        /// Updates a store
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateStoreDto command)
        {
            return Ok(await _mediator.Send(new UpdateStoreCommand(id,command)));
        }
        
        /// <summary>
        /// Updates store's location
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}/Location")]
        public async Task<IActionResult> UpdateLocationAsync(Guid id, [FromBody] StoreLocationDto command)
        {
            return Ok(await _mediator.Send(new UpdateLocationCommand(id,command)));
        }

        /// <summary>
        /// Deletes a store
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteStoreCommand(id)));
        }

        /// <summary>
        /// Add business hour to store
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{id}/BusinessHours")]
        public async Task<IActionResult> AddBusinessHourAsync(Guid id, [FromBody] AddBusinessHourDto command)
        {
            return Ok(await _mediator.Send(new AddBusinessHourCommand(id, command)));
        }

        /// <summary>
        /// Remove business hour
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        [HttpDelete("{id}/BusinessHours/{dayOfWeek}")]
        public async Task<IActionResult> RemoveBusinessHourAsync(Guid id, int dayOfWeek)
        {
            return Ok(await _mediator.Send(new RemoveBusinessHourCommand(id, dayOfWeek)));
        }
        
        /// <summary>
        /// Add FAQ to store
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{id}/FAQs")]
        public async Task<IActionResult> AddFAQAsync(Guid id, [FromBody] AddFaqDto command)
        {
            return Ok(await _mediator.Send(new AddFaqCommand(id, command)));
        }

        /// <summary>
        /// Remove FAQ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FAQId"></param>
        /// <returns></returns>
        [HttpDelete("{id}/FAQs/{FAQId}")]
        public async Task<IActionResult> RemoveFAQAsync(Guid id, Guid FAQId)
        {
            return Ok(await _mediator.Send(new RemoveFaqCommand(id, FAQId)));
        }
    }
}