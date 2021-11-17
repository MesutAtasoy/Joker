using Joker.EntityFrameworkCore.Repositories;
using Management.Core.Entities;
using Management.Core.Repositories;

namespace Management.Infrastructure.Repositories;

public class PricingPlanRepository : EntityFrameworkCoreRepository<ManagementContext, PricingPlan>,
    IPricingPlanRepository
{
    public PricingPlanRepository(ManagementContext context)
        : base(context)
    {
    }
}