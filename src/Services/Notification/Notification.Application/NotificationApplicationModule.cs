using AutoMapper;
using Joker.CAP;
using Microsoft.Extensions.DependencyInjection;
using Notification.Application.UserNotifications;
using Notification.Core.Repositories;
using Notification.Infrastructure;
using Notification.Infrastructure.Repositories;

namespace Notification.Application;

public static class NotificationApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        //Repositories
        services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();
            
        //Event and EventHandlers
        services.RegisterCAPEvents(typeof(NotificationApplicationModule));
        services.RegisterCAPEventHandlers(typeof(NotificationApplicationModule));

        //Automapper
        services.AddAutoMapper(typeof(UserNotificationMappingProfile));
      
        UserNotificationContext.ApplyConfiguration();
            
        return services;
    }
}