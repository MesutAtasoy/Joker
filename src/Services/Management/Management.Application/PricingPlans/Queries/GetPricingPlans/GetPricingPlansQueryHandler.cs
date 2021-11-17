using Management.Core.Entities;
using Management.Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Management.Application.PricingPlans.Queries.GetPricingPlans;

public class GetPricingPlansQueryHandler : IRequestHandler<GetPricingPlansQuery, List<PricingPlan>>
{
    private readonly IPricingPlanRepository _pricingPlanRepository;

    public GetPricingPlansQueryHandler(IPricingPlanRepository pricingPlanRepository)
    {
        _pricingPlanRepository = pricingPlanRepository ;
    }

    public async Task<List<PricingPlan>> Handle(GetPricingPlansQuery request, CancellationToken cancellationToken)
    {
        var pricingPlans = await _pricingPlanRepository.Get().Where(x => !x.IsDeleted)
            .Include(x => x.Currency)
            .ToListAsync(cancellationToken);

        return pricingPlans;
    }
}