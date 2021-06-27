using System;
using System.Threading.Tasks;
using Joker.Repositories;

namespace Merchant.Domain.MerchantAggregate.Repositories
{
    public interface IMerchantRepository : IRepository<Merchant>
    {
        Task<Merchant> GetByIdAsync(Guid id);
    }
}