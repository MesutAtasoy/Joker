using Joker.EventBus;
using Joker.Mongo.Domain.Context;
using Joker.Mongo.Domain.Repository;
using Subscription.Domain.SubscriptionAggregate.Repositories;

namespace Subscription.Infrastructure.Repositories;

public class SubscriptionRepository : MongoDomainDomainRepository<Domain.SubscriptionAggregate.Subscription>, ISubscriptionRepository
{
    public SubscriptionRepository(IMongoDomainContext domainContext, IEventDispatcher eventDispatcher) 
        : base(domainContext, eventDispatcher)
    {
    }
}