using System;
using System.Threading.Tasks;
using Joker.EventBus;
using Joker.Mongo.Domain.Context;
using Joker.Mongo.Domain.Repository;
using Merchant.Domain.Refs;
using Merchant.Domain.StoreAggregate;
using Merchant.Domain.StoreAggregate.Repositories;
using MongoDB.Driver;

namespace Merchant.Infrastructure.Repositories
{
    public class StoreRepository : MongoDomainDomainRepository<Store>, IStoreRepository
    {
        private readonly IMongoCollection<Store> _collection;
        
        public StoreRepository(IMongoDomainContext domainContext, IEventDispatcher eventDispatcher) 
            : base(domainContext, eventDispatcher)
        {
            _collection =  domainContext.Database.GetCollection<Store>(nameof(Store));
        }

        public async Task<Store> GetByIdAsync(Guid id)
        {
            return await base.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
        }

        public async Task UpdateMerchantNameAsync(Guid merchantId, string merchantName)
        {
            var filter = Builders<Store>.Filter.Eq(x => x.Merchant.RefId, merchantId);
            var update = Builders<Store>.Update.Set(x=>x.Merchant, MerchantRef.Create(merchantId, merchantName));
            await _collection.UpdateManyAsync(filter, update);
        }
    }
}