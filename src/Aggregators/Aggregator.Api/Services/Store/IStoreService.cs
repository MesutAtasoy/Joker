using Aggregator.Api.Models.Store;
using Aggregator.Api.Models.Store.Requests;
using Joker.Response;

namespace Aggregator.Api.Services.Store;

public interface IStoreService
{
    Task<JokerBaseResponse<StoreModel>> CreateAsync(CreateStoreModel request);
    Task<JokerBaseResponse<StoreModel>> UpdateAsync(UpdateStoreModel request);
    Task<JokerBaseResponse<StoreLocationModel>> UpdateLocationAsync(UpdateStoreLocationModel request);
    Task<JokerBaseResponse<bool>> DeleteAsync(Guid id);
    Task<StoreModel> GetByIdAsync(Guid id);
}