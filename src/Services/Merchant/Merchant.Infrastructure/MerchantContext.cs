using Joker.Mongo.Domain.Context;
using Joker.Mongo.Mapping;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Merchant.Infrastructure
{
    public class MerchantContext : MongoDomainContext
    {
        public MerchantContext(IMongoDatabase database) 
            : base(database)
        {
        }
        
        public static void ApplyConfiguration()
        {
#pragma warning disable 618
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
#pragma warning restore 618

            var sss = BsonSerializer.SerializerRegistry.GetSerializer<GuidSerializer>();
            
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            
            BsonClassMappingConfiguration.ApplyConfigurationsFromAssembly(typeof(MerchantContext).Assembly);
        }
    }
}