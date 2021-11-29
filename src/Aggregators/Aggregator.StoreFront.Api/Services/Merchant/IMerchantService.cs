using Aggregator.StoreFront.Api.Models.Merchant;
using Joker.Response;

namespace Aggregator.StoreFront.Api.Services.Merchant;

public interface IMerchantService
{
    Task<JokerBaseResponse<MerchantModel>> CreateAsync(CreateMerchantModel createMerchantModel,
        string pricingPlanId,
        string pricingPlanName,
        Guid organizationId);
}