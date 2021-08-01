using MediatR;
using Merchant.Application.Merchants.Dto;
using Merchant.Application.Shared.Dto;

namespace Merchant.Application.Merchants.Commands.CreateMerchant
{
    public class CreateMerchantCommand : IRequest<MerchantDto>
    {
        public string Name { get; set; }
        public string Slogan { get; set; }
        public string WebSiteUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string TaxNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public IdNameDto PricingPlan { get; set; }
    }
}