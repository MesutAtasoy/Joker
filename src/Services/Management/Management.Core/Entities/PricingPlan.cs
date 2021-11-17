namespace Management.Core.Entities;

public partial class PricingPlan : BaseEntityModel
{
    public PricingPlan()
    {
    }

    public string Name { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Guid CurrencyId { get; set; }
    public virtual Currency Currency { get; set; }
}