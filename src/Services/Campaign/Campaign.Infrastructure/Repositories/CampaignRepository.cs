using System;
using System.Threading.Tasks;
using Campaign.Domain.CampaignAggregate.Repositories;
using Campaign.Domain.Refs;
using Joker.EventBus;
using Joker.Mongo.Domain.Context;
using Joker.Mongo.Domain.Repository;
using MongoDB.Driver;

namespace Campaign.Infrastructure.Repositories
{
    public class CampaignRepository : MongoDomainDomainRepository<Domain.CampaignAggregate.Campaign>,
        ICampaignRepository
    {
        private readonly IMongoCollection<Domain.CampaignAggregate.Campaign> _collection;

        public CampaignRepository(IMongoDomainContext domainContext, IEventDispatcher eventDispatcher)
            : base(domainContext, eventDispatcher)
        {
            _collection =  domainContext.Database.GetCollection<Domain.CampaignAggregate.Campaign>(nameof(Domain.CampaignAggregate.Campaign));
        }

        public async Task<Domain.CampaignAggregate.Campaign> GetByIdAsync(Guid id)
        {
            return await base.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
        }

        public async Task UpdateMerchantNameAsync(Guid merchantId, string merchantName)
        {
            var filter = Builders<Domain.CampaignAggregate.Campaign>.Filter.Eq(x => x.Merchant.RefId, merchantId);
            var update = Builders<Domain.CampaignAggregate.Campaign>.Update.Set(x=>x.Merchant, MerchantRef.Create(merchantId, merchantName));
            await _collection.UpdateManyAsync(filter, update);
        }

        public async Task UpdateStoreNameAsync(Guid storeId, string storeName)
        {
            var filter = Builders<Domain.CampaignAggregate.Campaign>.Filter.Eq(x => x.Store.RefId, storeId);
            var update = Builders<Domain.CampaignAggregate.Campaign>.Update.Set(x=>x.Store, StoreRef.Create(storeId, storeName));
            await _collection.UpdateManyAsync(filter, update);
        }
    }
}