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
    }
}