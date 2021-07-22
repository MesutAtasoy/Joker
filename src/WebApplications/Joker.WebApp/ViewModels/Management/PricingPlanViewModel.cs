using System;

namespace Joker.WebApp.ViewModels.Management
{
    public class PricingPlanViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CurrencyId { get; set; }
        public CurrencyViewModel Currency { get; set; }
    }
}