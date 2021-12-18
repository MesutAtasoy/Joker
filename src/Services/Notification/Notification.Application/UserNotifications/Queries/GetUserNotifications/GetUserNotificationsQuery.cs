using MediatR;
using Notification.Application.UserNotifications.Dto;

namespace Notification.Application.UserNotifications.Queries.GetUserNotifications;

public class GetUserNotificationsQuery : IRequest<List<UserNotificationDto>>
{
    public GetUserNotificationsQuery(Guid ownerId, bool? isRead)
    {
        OwnerId = ownerId;
        IsRead = isRead;
    }

    public Guid OwnerId { get; }
    public bool? IsRead { get; }
}