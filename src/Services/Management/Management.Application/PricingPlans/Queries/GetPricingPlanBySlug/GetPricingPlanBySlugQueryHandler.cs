using Management.Core.Entities;
using Management.Core.Repositories;
using MediatR;

namespace Management.Application.PricingPlans.Queries.GetPricingPlanBySlug;

public class GetPricingPlanBySlugQueryHandler: IRequestHandler<GetPricingPlanBySlugQuery, PricingPlan>
{
    private readonly IPricingPlanRepository _pricingPlanRepository;

    public GetPricingPlanBySlugQueryHandler(IPricingPlanRepository pricingPlanRepository)
    {
        _pricingPlanRepository = pricingPlanRepository;
    }

    public async Task<PricingPlan> Handle(GetPricingPlanBySlugQuery request, CancellationToken cancellationToken)
    {
        return await _pricingPlanRepository.FirstOrDefaultAsync(x=>x.Slug.Equals(request.Slug) && !x.IsDeleted);
    }
}