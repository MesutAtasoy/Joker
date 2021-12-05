using Joker.BackOffice.ViewModels.Subscription;

namespace Joker.BackOffice.Services.Abstract;

public interface ISubscriptionService
{
    Task<List<SubscriptionViewModel>> GetSubscriptions(Guid merchantId);
}