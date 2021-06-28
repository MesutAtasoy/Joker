using System;
using System.Threading.Tasks;
using Campaign.Domain.CampaignAggregate.Repositories;
using Joker.EventBus;
using Joker.Mongo.Domain.Context;
using Joker.Mongo.Domain.Repository;

namespace Campaign.Infrastructure.Repositories
{
    public class CampaignRepository : MongoDomainDomainRepository<Domain.CampaignAggregate.Campaign>,
        ICampaignRepository
    {
        public CampaignRepository(IMongoDomainContext domainContext, IEventDispatcher eventDispatcher)
            : base(domainContext, eventDispatcher)
        {
        }

        public async Task<Domain.CampaignAggregate.Campaign> GetByIdAsync(Guid id)
        {
            return await base.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
        }
    }
}