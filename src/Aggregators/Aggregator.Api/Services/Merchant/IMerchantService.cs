using System;
using System.Threading.Tasks;
using Aggregator.Api.Models.Merchant;

namespace Aggregator.Api.Services.Merchant
{
    public interface IMerchantService
    {
        Task<MerchantModel> CreateAsync(CreateMerchantModel createMerchantModel, string pricingPlanId,
            string pricingPlanName);

        Task<MerchantModel> UpdateAsync(UpdateMerchantModel createMerchantModel);
        Task<bool> DeleteAsync(Guid id);
        Task<MerchantModel> GetById(Guid id);
    }
}