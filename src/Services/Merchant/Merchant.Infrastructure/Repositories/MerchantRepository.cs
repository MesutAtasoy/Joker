using System;
using System.Threading.Tasks;
using Joker.EventBus;
using Joker.Mongo.Domain.Context;
using Joker.Mongo.Domain.Repository;
using Merchant.Domain.MerchantAggregate.Repositories;

namespace Merchant.Infrastructure.Repositories
{
    public class MerchantRepository : MongoDomainDomainRepository<Domain.MerchantAggregate.Merchant>, IMerchantRepository
    {
        public MerchantRepository(IMongoDomainContext domainContext, IEventDispatcher eventDispatcher) 
            : base(domainContext, eventDispatcher)
        {
        }

        public Task<Domain.MerchantAggregate.Merchant> GetByIdAsync(Guid id)
        {
            return base.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
        }
    }
}