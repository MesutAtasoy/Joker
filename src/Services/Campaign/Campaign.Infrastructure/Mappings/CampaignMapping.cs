using Joker.Mongo.Mapping;
using MongoDB.Bson.Serialization;

namespace Campaign.Infrastructure.Mappings
{
    public class CampaignMapping: MongoDbClassMap<Domain.CampaignAggregate.Campaign>
    {
        protected override void Map(BsonClassMap<Domain.CampaignAggregate.Campaign> map)
        {
            map.AutoMap();
            map.MapIdProperty(x => x.Id);
            
            map.MapField("_campaignBadges").SetElementName("CampaignBadges");
            map.MapField("_campaignGalleries").SetElementName("CampaignGalleries");
        }
    }
}