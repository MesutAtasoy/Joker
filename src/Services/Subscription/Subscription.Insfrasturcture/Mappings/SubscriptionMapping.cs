using Joker.Mongo.Mapping;
using MongoDB.Bson.Serialization;

namespace Subscription.Infrastructure.Mappings
{
    public class SubscriptionMapping: MongoDbClassMap<Domain.SubscriptionAggregate.Subscription>
    {
        protected override void Map(BsonClassMap<Domain.SubscriptionAggregate.Subscription> map)
        {
            map.AutoMap();
            map.MapIdProperty(x => x.Id);
        }
    }
}