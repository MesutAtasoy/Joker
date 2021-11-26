using Aggregator.StoreFront.Api.Models.Store;

namespace Aggregator.StoreFront.Api.Services.Store;

public interface IStoreService
{
    Task<StoreModel> GetByIdAsync(Guid id);
}