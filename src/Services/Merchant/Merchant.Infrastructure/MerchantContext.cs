using Joker.Mongo.Domain.Context;
using MongoDB.Driver;

namespace Merchant.Infrastructure
{
    public class MerchantContext : MongoDomainContext
    {
        public MerchantContext(IMongoDatabase database) 
            : base(database)
        {
        }
    }
}