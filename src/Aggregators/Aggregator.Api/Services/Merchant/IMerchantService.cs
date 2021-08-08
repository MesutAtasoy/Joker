using System;
using System.Threading.Tasks;
using Aggregator.Api.Models.Merchant;
using Joker.Response;

namespace Aggregator.Api.Services.Merchant
{
    public interface IMerchantService
    {
        Task<JokerBaseResponse<MerchantModel>> CreateAsync(CreateMerchantModel createMerchantModel,
            string pricingPlanId,
            string pricingPlanName);
        Task<JokerBaseResponse<MerchantModel>> UpdateAsync(UpdateMerchantModel createMerchantModel);
        Task<JokerBaseResponse<bool>> DeleteAsync(Guid id);
        Task<MerchantModel> GetById(Guid id);
    }
}