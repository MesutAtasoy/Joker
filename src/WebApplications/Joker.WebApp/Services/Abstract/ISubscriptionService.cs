using Joker.WebApp.ViewModels.Subscription;

namespace Joker.WebApp.Services.Abstract;

public interface ISubscriptionService
{
    Task<List<SubscriptionViewModel>> GetSubscriptions(Guid merchantId);
}