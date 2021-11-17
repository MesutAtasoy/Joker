namespace Management.Core.Entities;

public partial class Currency : BaseEntityModel
{
    public Currency()
    {
        PricingPlans = new List<PricingPlan>();
    }
    public string Code { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public virtual ICollection<PricingPlan> PricingPlans { get; set; }
}