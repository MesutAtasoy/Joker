using System.Threading;
using System.Threading.Tasks;
using Management.Core.Entities;
using Management.Core.Repositories;
using MediatR;

namespace Management.Application.PricingPlans.Queries.GetPricingPlanById
{
    public class GetPricingPlanByIdQueryHandler: IRequestHandler<GetPricingPlanByIdQuery, PricingPlan>
    {
        private readonly IPricingPlanRepository _pricingPlanRepository;

        public GetPricingPlanByIdQueryHandler(IPricingPlanRepository pricingPlanRepository)
        {
            _pricingPlanRepository = pricingPlanRepository;
        }

        public async Task<PricingPlan> Handle(GetPricingPlanByIdQuery request, CancellationToken cancellationToken)
        {
            return await _pricingPlanRepository.FirstOrDefaultAsync(x=>x.Id == request.Id && !x.IsDeleted);
        }
    }
}