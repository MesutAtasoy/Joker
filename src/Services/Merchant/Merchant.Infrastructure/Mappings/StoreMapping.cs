using Joker.Mongo.Mapping;
using Merchant.Domain.StoreAggregate;
using MongoDB.Bson.Serialization;

namespace Merchant.Infrastructure.Mappings
{
    public class StoreMapping: MongoDbClassMap<Store>
    {
        protected override void Map(BsonClassMap<Store> map)
        {
            map.AutoMap();
            map.MapIdProperty(x => x.Id);
            
            map.MapField("_storeFAQs").SetElementName("StoreFAQs");
            map.MapField("_storeBusinessHours").SetElementName("StoreBusinessHours");
        }
    }
}