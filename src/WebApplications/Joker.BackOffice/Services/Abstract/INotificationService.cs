using Joker.BackOffice.ViewModels.Notification;

namespace Joker.BackOffice.Services.Abstract;

public interface INotificationService
{
    Task<List<UserNotificationViewModel>> GetUserNotificationsAsync(Guid ownerId, bool? isRead = null);
}