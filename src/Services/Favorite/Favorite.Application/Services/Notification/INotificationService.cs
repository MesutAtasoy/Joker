using System;
using System.Threading.Tasks;

namespace Favorite.Application.Services.Notification;

public interface INotificationService
{
    Task SendAsync(Guid ownerId, string title, string content);
}