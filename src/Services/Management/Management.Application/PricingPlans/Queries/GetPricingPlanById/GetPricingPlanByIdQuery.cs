using Management.Core.Entities;
using MediatR;

namespace Management.Application.PricingPlans.Queries.GetPricingPlanById;

public class GetPricingPlanByIdQuery : IRequest<PricingPlan>
{
    public GetPricingPlanByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}