namespace Aggregator.Api.Models.Merchant;

public class MerchantModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slogan { get; set; }
    public string Slug { get; set; }
    public string SlugKey { get; set; }
    public string WebSiteUrl { get; set; }
    public string PhoneNumber { get; set; }
    public string TaxNumber { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public bool EmailConfirmed { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}