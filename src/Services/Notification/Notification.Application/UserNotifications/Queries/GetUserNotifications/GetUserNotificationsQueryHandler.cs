using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Notification.Application.UserNotifications.Dto;
using Notification.Core.Repositories;

namespace Notification.Application.UserNotifications.Queries.GetUserNotifications;

public class GetUserNotificationsQueryHandler : IRequestHandler<GetUserNotificationsQuery, List<UserNotificationDto>>
{
    private readonly IUserNotificationRepository _notificationRepository;
    private readonly IMapper _mapper;
    
    public GetUserNotificationsQueryHandler(IUserNotificationRepository notificationRepository,
        IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }
    
    public async Task<List<UserNotificationDto>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        var list = _notificationRepository.Get()
            .Where(x => !x.IsDeleted && x.OwnerId == request.OwnerId)
            .ProjectTo<UserNotificationDto>(_mapper.ConfigurationProvider)
            .ToList();

        return list;
    }
}