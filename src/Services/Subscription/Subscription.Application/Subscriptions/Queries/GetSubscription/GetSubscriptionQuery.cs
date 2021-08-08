using System;
using System.Collections.Generic;
using MediatR;
using Subscription.Application.Subscriptions.Dto;

namespace Subscription.Application.Subscriptions.Queries.GetSubscription
{
    public class GetSubscriptionQuery : IRequest<List<SubscriptionDto>>
    {
        public GetSubscriptionQuery(Guid merchantId)
        {
            MerchantId = merchantId;
        }

        public Guid MerchantId { get; }
    }
}