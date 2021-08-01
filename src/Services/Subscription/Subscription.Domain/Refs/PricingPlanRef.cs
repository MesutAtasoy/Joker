using System;
using Joker.Exceptions;
using Subscription.Domain.Refs.Base;

namespace Subscription.Domain.Refs
{
    public class PricingPlanRef : IdNameRef
    {
        public static PricingPlanRef Create(Guid refId, string name)
        {
            Check.NotNullOrEmpty(name, nameof(name));

            return new PricingPlanRef
            {
                RefId = refId,
                Name = name
            };
        }
    }
}