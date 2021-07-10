using System;
using System.Threading.Tasks;
using Aggregator.Api.Models.Store;

namespace Aggregator.Api.Services.Store
{
    public interface IStoreService
    {
        Task<StoreModel> CreateAsync(CreateStoreModel request);
        Task<StoreModel> UpdateAsync(UpdateStoreModel request);
        Task<StoreLocationModel> UpdateLocationAsync(UpdateStoreLocationModel request);
        Task<bool> DeleteAsync(Guid id);
        Task<StoreModel> GetByIdAsync(Guid id);
    }
}