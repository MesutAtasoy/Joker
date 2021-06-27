using System;
using System.Threading.Tasks;
using Joker.EventBus;
using Joker.Mongo.Domain.Context;
using Joker.Mongo.Domain.Repository;
using Merchant.Domain.StoreAggregate;
using Merchant.Domain.StoreAggregate.Repositories;

namespace Merchant.Infrastructure.Repositories
{
    public class StoreRepository : MongoDomainDomainRepository<Store>, IStoreRepository
    {
        public StoreRepository(IMongoDomainContext domainContext, IEventDispatcher eventDispatcher) 
            : base(domainContext, eventDispatcher)
        {
        }

        public async Task<Store> GetByIdAsync(Guid id)
        {
            return await base.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
        }
    }
}