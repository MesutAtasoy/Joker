using System;
using System.Threading.Tasks;
using Joker.Repositories;

namespace Merchant.Domain.StoreAggregate.Repositories
{
    public interface IStoreRepository : IRepository<Store>
    {
        Task<Store> GetByIdAsync(Guid id);
        Task UpdateMerchantNameAsync(Guid merchantId, string merchantName);
    }
}