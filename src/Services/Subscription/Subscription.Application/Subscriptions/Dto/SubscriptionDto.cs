using Subscription.Domain.Refs;

namespace Subscription.Application.Subscriptions.Dto;

public class SubscriptionDto
{
    public Guid Id { get; set; }
    public PricingPlanRef PricingPlan { get; set; }
    public MerchantRef Merchant { get; set; }
    public string ActivationCode { get; set; }
    public DateTime ActivationDate { get; set; }
    public DateTime ValidityDate { get; set; }
}