using Management.Core.Entities;
using MediatR;

namespace Management.Application.PricingPlans.Queries.GetPricingPlanBySlug;

public class GetPricingPlanBySlugQuery :  IRequest<PricingPlan>
{
    public GetPricingPlanBySlugQuery(string slug)
    {
        Slug = slug;
    }

    public string Slug { get; }
}