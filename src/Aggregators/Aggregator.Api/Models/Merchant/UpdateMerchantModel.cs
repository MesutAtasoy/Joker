namespace Aggregator.Api.Models.Merchant
{
    public class UpdateMerchantModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slogan { get; set; }
        public string WebSiteUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string TaxNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
    }
}