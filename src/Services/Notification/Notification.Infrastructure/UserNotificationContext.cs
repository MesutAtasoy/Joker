using Joker.Mongo.Context;
using Joker.Mongo.Mapping;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Notification.Infrastructure;

public class UserNotificationContext : MongoContext
{
    public UserNotificationContext(IMongoDatabase database) 
        : base(database)
    {
    }
    
    public static void ApplyConfiguration()
    {
#pragma warning disable 618
        BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
#pragma warning restore 618

        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        BsonClassMappingConfiguration.ApplyConfigurationsFromAssembly(typeof(UserNotificationContext).Assembly);
    }
}