namespace Joker.BackOffice.ViewModels.Notification;

public class UserNotificationViewModel
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
}