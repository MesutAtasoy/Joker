using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Management.Core.Entities;
using Management.Core.Repositories;
using MediatR;

namespace Management.Application.PricingPlans.Queries.GetPricingPlans
{
    public class GetPricingPlansQueryHandler : IRequestHandler<GetPricingPlansQuery, List<PricingPlan>>
    {
        private readonly IPricingPlanRepository _pricingPlanRepository;

        public GetPricingPlansQueryHandler(IPricingPlanRepository pricingPlanRepository)
        {
            _pricingPlanRepository = pricingPlanRepository ;
        }

        public async Task<List<PricingPlan>> Handle(GetPricingPlansQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_pricingPlanRepository.Get(x=>!x.IsDeleted).ToList());
        }
    }
}