using MediatR;
using Notification.Application.UserNotifications.Dto;

namespace Notification.Application.UserNotifications.Queries.GetUserNotifications;

public class GetUserNotificationsQuery : IRequest<List<UserNotificationDto>>
{
    public GetUserNotificationsQuery(Guid ownerId)
    {
        OwnerId = ownerId;
    }

    public Guid OwnerId { get; }
    
}