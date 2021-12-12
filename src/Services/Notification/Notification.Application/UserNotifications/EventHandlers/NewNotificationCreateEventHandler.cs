using DotNetCore.CAP;
using Joker.CAP.IntegrationEvent;
using Notification.Application.UserNotifications.Events;
using Notification.Core.Models;
using Notification.Core.Repositories;

namespace Notification.Application.UserNotifications.EventHandlers;

public class NewNotificationCreateEventHandler: CAPIntegrationEventHandler<NewNotificationCreateEvent>
{
    private readonly IUserNotificationRepository _repository;

    public NewNotificationCreateEventHandler(IUserNotificationRepository repository)
    {
        _repository = repository;
    }

    [CapSubscribe(nameof(NewNotificationCreateEvent))]
    public override async Task Handle(NewNotificationCreateEvent @event)
    {
        await _repository.AddAsync(new UserNotification
        {
            OwnerId = @event.OwnerId,
            Title = @event.Title,
            Content = @event.Content,
            CreatedDate = DateTime.UtcNow,
            IsRead = false,
        });
    }
}