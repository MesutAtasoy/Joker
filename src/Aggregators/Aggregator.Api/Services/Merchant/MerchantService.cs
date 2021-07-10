using System;
using System.Threading.Tasks;
using Aggregator.Api.Models.Merchant;
using Joker.Extensions;
using Merchant.Api.Grpc;

namespace Aggregator.Api.Services.Merchant
{
    public class MerchantService : IMerchantService
    {
        private readonly MerchantApiGrpcService.MerchantApiGrpcServiceClient _merchantApiGrpcServiceClient;

        public MerchantService(MerchantApiGrpcService.MerchantApiGrpcServiceClient merchantApiGrpcServiceClient)
        {
            _merchantApiGrpcServiceClient = merchantApiGrpcServiceClient;
        }

        public async Task<MerchantModel> CreateAsync(CreateMerchantModel request)
        {
            var response = await _merchantApiGrpcServiceClient.CreateMerchantAsync(new CreateMerchantMessage
            {
                Name = request.Name,
                Description = request.Description,
                Email = request.Email,
                Slogan = request.Slogan,
                PhoneNumber = request.PhoneNumber,
                TaxNumber = request.TaxNumber,
                WebsiteUrl = request.WebSiteUrl
            });

            return As(response);
        }

        public async Task<MerchantModel> UpdateAsync(UpdateMerchantModel createMerchantModel)
        {
            var response = await _merchantApiGrpcServiceClient.UpdateMerchantAsync(new UpdateMerchantMessage
            {
                MerchantId = createMerchantModel.Id,
                Merchant = new UpdateMerchantItemMessage
                {
                    Name = createMerchantModel.Name,
                    Description = createMerchantModel.Description,
                    Email = createMerchantModel.Email,
                    Slogan = createMerchantModel.Slogan,
                    PhoneNumber = createMerchantModel.PhoneNumber,
                    TaxNumber = createMerchantModel.TaxNumber,
                    WebsiteUrl = createMerchantModel.WebSiteUrl
                }
            });
            
            return As(response);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response =  await _merchantApiGrpcServiceClient.DeleteMerchantAsync(new ByIdMessage {Id = id.ToString()});
            return response.IsSucceed;
        }

        public async Task<MerchantModel> GetById(Guid id)
        {
            var merchant = await _merchantApiGrpcServiceClient.GetMerchantByIdAsync(new ByIdMessage {Id = id.ToString()});
            return As(merchant);
        }


        #region Model Converters
        private MerchantModel As(MerchantMessage merchantMessage)
        {
            return new()
            {
                Id = merchantMessage.Id.ToGuid(),
                Name = merchantMessage.Name,
                Description = merchantMessage.Description,
                Email = merchantMessage.Email,
                Slogan = merchantMessage.Slogan,
                TaxNumber = merchantMessage.TaxNumber,
                PhoneNumber = merchantMessage.PhoneNumber,
                EmailConfirmed = merchantMessage.EmailConfirmed,
                WebSiteUrl = merchantMessage.WebsiteUrl,
                CreatedDate = merchantMessage.CreatedDate.ToDateTime(),
                ModifiedDate = merchantMessage.ModifiedDate.ToDateTime()
            };
        }
        #endregion
    }
}