using System;
using System.Threading.Tasks;
using Joker.Repositories;

namespace Campaign.Domain.CampaignAggregate.Repositories
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
        Task<Campaign> GetByIdAsync(Guid id);
        Task UpdateMerchantNameAsync(Guid merchantId, string merchantName);
    }
}