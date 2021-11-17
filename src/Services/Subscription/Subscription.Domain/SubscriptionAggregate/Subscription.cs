using Joker.Domain.Entities;
using Joker.Exceptions;
using Joker.Extensions;
using Subscription.Domain.Refs;
using Subscription.Domain.SubscriptionAggregate.Events;

namespace Subscription.Domain.SubscriptionAggregate;

public class Subscription : DomainEntity
{
    public Subscription(Guid id, PricingPlanRef pricingPlan)
    {
        Check.NotEmpty(id, nameof(id));
        Check.NotNull(pricingPlan, nameof(pricingPlan));

        Id = id;
        PricingPlan = pricingPlan;
    }

    public Guid Id { get; private set; }
    public PricingPlanRef PricingPlan { get; private set; }
    public MerchantRef Merchant { get; private set; }
    public string ActivationCode { get; private set; }
    public DateTime ActivationDate { get; private set; }
    public DateTime ValidityDate { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTime CreatedDate { get; private set; }


    public void Subscribe(MerchantRef merchant, Guid userId)
    {
        Check.NotNull(merchant, nameof(merchant));
        Check.NotNull(userId, nameof(userId));

        Merchant = merchant;
        ActivationCode = IdGeneratorExtensions.GetNextIDThreadLocal();
        ActivationDate = DateTime.UtcNow;
        ValidityDate = DateTime.UtcNow.AddYears(1);
        CreatedBy = userId;
        CreatedDate = DateTime.UtcNow;
            
        AddDomainEvent(new SubscribedEvent(Id, PricingPlan, Merchant, ActivationCode, ActivationDate, CreatedBy));
    }

    public void UpdateMerchant(MerchantRef merchant)
    {
        Check.NotNull(merchant, nameof(merchant));
        Merchant = merchant;
    }
}