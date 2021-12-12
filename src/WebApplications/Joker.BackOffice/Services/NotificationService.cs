using Joker.BackOffice.Services.Abstract;
using Joker.BackOffice.ViewModels.Notification;

namespace Joker.BackOffice.Services;

public class NotificationService : BaseService, INotificationService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly HttpClient _httpClient;
    private readonly ILogger<SubscriptionService> _logger;

    public NotificationService(IHttpClientFactory clientFactory, ILogger<SubscriptionService> logger)
        : base(logger)
    {
        _clientFactory = clientFactory;
        _logger = logger;
        _httpClient = _clientFactory.CreateClient("GatewayApi");
    }

    public async Task<List<UserNotificationViewModel>> GetUserNotificationsAsync(Guid ownerId, bool? isRead = null)
    {
        string? type = isRead switch
        {
            false => "unread",
            true => "read",
            null => "all"
        };

        var responseMessage = await _httpClient.GetAsync($"/notification/api/UserNotifications/{ownerId}/{type}");
        if (!responseMessage.IsSuccessStatusCode)
        {
            var response = await responseMessage.Content.ReadAsStringAsync();
            _logger.LogError(response);
            throw new ArgumentException("Notification Service can not respond success response");
        }

        var notifications = await responseMessage.Content.ReadFromJsonAsync<List<UserNotificationViewModel>>();

        return notifications;
    }
}