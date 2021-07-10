using System;
using System.Threading.Tasks;
using Aggregator.Api.Models.Merchant;
using Merchant.Api.Grpc;

namespace Aggregator.Api.Services.Merchant
{
    public class MerchantService : IMerchantService
    {
        private MerchantApiGrpcService.MerchantApiGrpcServiceClient _merchantApiGrpcServiceClient;

        public MerchantService(MerchantApiGrpcService.MerchantApiGrpcServiceClient merchantApiGrpcServiceClient)
        {
            _merchantApiGrpcServiceClient = merchantApiGrpcServiceClient;
        }

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