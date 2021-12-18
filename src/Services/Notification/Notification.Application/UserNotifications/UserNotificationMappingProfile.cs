using AutoMapper;
using Notification.Application.UserNotifications.Dto;
using Notification.Core.Models;

namespace Notification.Application.UserNotifications;

public class UserNotificationMappingProfile : Profile
{
    public UserNotificationMappingProfile()
    {
        CreateMap<UserNotification, UserNotificationDto>();
    }
}