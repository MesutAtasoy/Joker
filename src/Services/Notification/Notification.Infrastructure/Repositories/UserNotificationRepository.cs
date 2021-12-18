using Joker.Mongo.Context;
using Joker.Mongo.Repository;
using Notification.Core.Models;
using Notification.Core.Repositories;

namespace Notification.Infrastructure.Repositories;

public class UserNotificationRepository : MongoRepository<UserNotification>,
    IUserNotificationRepository 
{
    public UserNotificationRepository(IMongoContext context) 
        : base(context)
    {
    }
}