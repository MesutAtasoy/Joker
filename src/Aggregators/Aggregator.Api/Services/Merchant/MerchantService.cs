using System;
using System.Threading.Tasks;
using Aggregator.Api.Models.Merchant;

namespace Aggregator.Api.Services.Merchant
{
    public class MerchantService : IMerchantService
    {
        public Task<MerchantModel> CreateAsync(CreateMerchantModel createMerchantModel)
        {
            throw new NotImplementedException();
        }

        public Task<MerchantModel> UpdateAsync(UpdateMerchantModel createMerchantModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<MerchantModel> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}