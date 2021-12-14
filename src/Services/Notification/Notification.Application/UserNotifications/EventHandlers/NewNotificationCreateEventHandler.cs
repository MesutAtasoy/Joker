using DotNetCore.CAP;
using Joker.CAP.IntegrationEvent;
using Joker.EventBus;
using Notification.Application.UserNotifications.Events;
using Notification.Core.Models;
using Notification.Core.Repositories;

namespace Notification.Application.UserNotifications.EventHandlers;

public class NewNotificationCreateEventHandler: CAPIntegrationEventHandler<NewNotificationCreateEvent>
{
    private readonly IUserNotificationRepository _repository;
    private readonly IEventDispatcher _eventDispatcher;
    
    public NewNotificationCreateEventHandler(IUserNotificationRepository repository,
        IEventDispatcher eventDispatcher)
    {
        _repository = repository;
        _eventDispatcher = eventDispatcher;
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

        await _eventDispatcher.Dispatch(new NewNotificationCreatedEvent(@event.OwnerId, @event.Title, @event.Content));
    }
}