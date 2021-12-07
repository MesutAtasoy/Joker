using System.ComponentModel.DataAnnotations;

namespace Joker.BackOffice.ViewModels.Merchant.Request;

public class CreateMerchantViewModel
{
    [Required] 
    public string Name { get; set; }
        
    public string Slogan { get; set; }
        
    public string WebSiteUrl { get; set; }
        
    public string PhoneNumber { get; set; }
        
    public string TaxNumber { get; set; }

    [EmailAddress]
    public string Email { get; set; }
        
    public string Description { get; set; }
        
    public Guid PricingPlanId { get; set; }
}