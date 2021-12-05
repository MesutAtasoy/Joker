using Joker.BackOffice.ViewModels.Shared;

namespace Joker.BackOffice.ViewModels.Subscription;

public class SubscriptionViewModel
{
    public Guid Id { get; set; }
    public RefIdNameViewModel PricingPlan { get; set; }
    public RefIdNameViewModel Merchant { get; set; }
    public string ActivationCode { get; set; }
    public DateTime ActivationDate { get; set; }
    public DateTime ValidityDate { get; set; }
}