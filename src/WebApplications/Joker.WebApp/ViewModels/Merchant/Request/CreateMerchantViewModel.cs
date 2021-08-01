using System;

namespace Joker.WebApp.ViewModels.Merchant.Request
{
    public class CreateMerchantViewModel
    {
        public string Name { get; set; }
        public string Slogan { get; set; }
        public string WebSiteUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string TaxNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public Guid PricingPlanId { get; set; }
    }
}