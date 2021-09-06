using System;
using System.Threading.Tasks;
using Aggregator.Api.Extensions;
using Aggregator.Api.Models.Merchant;
using Grpc.Core;
using Joker.Extensions;
using Joker.Response;
using Merchant.Api.Grpc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Aggregator.Api.Services.Merchant
{
    public class MerchantService : IMerchantService
    {
        private readonly MerchantApiGrpcService.MerchantApiGrpcServiceClient _merchantApiGrpcServiceClient;
        private readonly IHttpContextAccessor _contextAccessor;

        public MerchantService(MerchantApiGrpcService.MerchantApiGrpcServiceClient merchantApiGrpcServiceClient,
            IHttpContextAccessor contextAccessor)
        {
            _merchantApiGrpcServiceClient = merchantApiGrpcServiceClient;
            _contextAccessor = contextAccessor;
        }

        public async Task<JokerBaseResponse<MerchantModel>> CreateAsync(CreateMerchantModel request,
            string pricingPlanId, string pricingPlanName)
        {
            var headers = await GetHeaders();
            var response = await _merchantApiGrpcServiceClient.CreateMerchantAsync(new CreateMerchantMessage
            {
                Name = request.Name?? "",
                Description = request.Description?? "",
                Email = request.Email?? "",
                Slogan = request.Slogan?? "",
                PhoneNumber = request.PhoneNumber?? "",
                TaxNumber = request.TaxNumber?? "",
                WebsiteUrl = request.WebSiteUrl?? "",
                PricingPlan = new IdNameMessage
                {
                    Id = pricingPlanId,
                    Name = pricingPlanName
                }
            }, headers);

            if (response.Status != 200)
            {
                return new JokerBaseResponse<MerchantModel>(null, response.Status, response.Message);
            }

            var merchant = response.Data.Unpack<MerchantMessage>();
            return new JokerBaseResponse<MerchantModel>(As(merchant), 200);
        }

        public async Task<JokerBaseResponse<MerchantModel>> UpdateAsync(UpdateMerchantModel createMerchantModel)
        {
            var headers = await GetHeaders();
            var response = await _merchantApiGrpcServiceClient.UpdateMerchantAsync(new UpdateMerchantMessage
            {
                MerchantId = createMerchantModel.Id,
                Merchant = new UpdateMerchantItemMessage
                {
                    Name = createMerchantModel.Name?? "",
                    Description = createMerchantModel.Description?? "",
                    Email = createMerchantModel.Email?? "",
                    Slogan = createMerchantModel.Slogan?? "",
                    PhoneNumber = createMerchantModel.PhoneNumber?? "",
                    TaxNumber = createMerchantModel.TaxNumber?? "",
                    WebsiteUrl = createMerchantModel.WebSiteUrl?? ""
                }
            }, headers);


            if (response.Status != 200)
            {
                return new JokerBaseResponse<MerchantModel>(null, response.Status, response.Message);
            }

            var merchant = response.Data.Unpack<MerchantMessage>();
            return new JokerBaseResponse<MerchantModel>(As(merchant), 200);
        }

        public async Task<JokerBaseResponse<bool>> DeleteAsync(Guid id)
        {
            var headers = await GetHeaders();

            var response =
                await _merchantApiGrpcServiceClient.DeleteMerchantAsync(new ByIdMessage { Id = id.ToString() },
                    headers);
            if (response.Status != 200)
            {
                return new JokerBaseResponse<bool>(false, response.Status, response.Message);
            }

            var deleteCampaignMessage = response.Data.Unpack<DeleteMerchantResponseMessage>();
            return new JokerBaseResponse<bool>(deleteCampaignMessage.IsSucceed, 200);
        }

        public async Task<MerchantModel> GetById(Guid id)
        {
            var headers = await GetHeaders();

            var merchant =
                await _merchantApiGrpcServiceClient.GetMerchantByIdAsync(new ByIdMessage { Id = id.ToString() },
                    headers);
            
            return As(merchant);
        }

        private async Task<Metadata> GetHeaders()
        {
            var accessToken =
                await _contextAccessor?.HttpContext?.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            var headers = new Metadata();
            if (!string.IsNullOrEmpty(accessToken))
            {
                headers.Add("Authorization", $"Bearer {accessToken}");
            }

            return headers;
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
                ModifiedDate = merchantMessage.ModifiedDate?.ToDateTime()
            };
        }

        #endregion
    }
}