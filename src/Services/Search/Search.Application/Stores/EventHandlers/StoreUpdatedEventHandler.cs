using System.Threading.Tasks;
using DotNetCore.CAP;
using Joker.CAP.IntegrationEvent;
using Search.Application.Stores.Events;
using Search.Core.IndexManagers.Store;
using Search.Core.IndexModels;

namespace Search.Application.Stores.EventHandlers
{
    public class StoreUpdatedEventHandler : CAPIntegrationEventHandler<StoreUpdatedEvent>
    {
        private readonly IStoreIndexManager _storeIndexManager;
        
        public StoreUpdatedEventHandler(IStoreIndexManager storeIndexManager)
        {
            _storeIndexManager = storeIndexManager;
        }
        
        [CapSubscribe(nameof(StoreUpdatedEvent))]
        public override async Task Handle(StoreUpdatedEvent @event)
        {
            var storeIndexModel = new StoreIndexModel
            {
                Id = @event.Id,
                Name = @event.Name,
                Description = @event.Description,
                Email = @event.Email,
                Slogan = @event.Slogan,
                CountryId = @event.CountryId,
                CountryName = @event.CountryName,
                CityId = @event.CityId,
                CityName = @event.CityName,
                DistrictId = @event.DistrictId,
                DistrictName = @event.DistrictName,
                NeighborhoodId = @event.NeighborhoodId,
                NeighborhoodName = @event.NeighborhoodName,
                QuarterId = @event.QuarterId,
                QuarterName = @event.QuarterName,
                Address = @event.Address,
                PhoneNumber = @event.PhoneNumber
            };

            await _storeIndexManager.AddOrUpdateAsync(storeIndexModel);
        }
    }
}