using System.ComponentModel.DataAnnotations;

namespace Joker.BackOffice.ViewModels.Merchant.Request;

public class UpdateMerchantViewModel
{
    public string Id { get; set; }
        
    [Required]
    public string Name { get; set; }
        
    public string Slogan { get; set; }
        
    public string WebSiteUrl { get; set; }
        
    public string PhoneNumber { get; set; }
        
    public string TaxNumber { get; set; }
        
    public string Email { get; set; }
        
    public string Description { get; set; }
}