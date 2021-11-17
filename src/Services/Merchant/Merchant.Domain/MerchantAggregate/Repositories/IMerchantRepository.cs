using Joker.Repositories;

namespace Merchant.Domain.MerchantAggregate.Repositories;

public interface IMerchantRepository : IRepository<Merchant>
{
    Task<Merchant> GetByIdAsync(Guid id);
}