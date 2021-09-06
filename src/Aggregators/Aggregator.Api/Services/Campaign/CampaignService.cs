using System;
using System.Threading.Tasks;
using Aggregator.Api.Models.Campaign;
using Campaign.Api.Grpc;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Joker.Extensions;
using Joker.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Aggregator.Api.Services.Campaign
{
    public class CampaignService : ICampaignService
    {
        private readonly CampaignApiGrpcService.CampaignApiGrpcServiceClient _campaignApiGrpcServiceClient;
        private readonly IHttpContextAccessor _contextAccessor;

        public CampaignService(CampaignApiGrpcService.CampaignApiGrpcServiceClient campaignApiGrpcServiceClient,
            IHttpContextAccessor contextAccessor)
        {
            _campaignApiGrpcServiceClient = campaignApiGrpcServiceClient;
            _contextAccessor = contextAccessor;
        }

        public async Task<JokerBaseResponse<CampaignModel>> CreateAsync(CreateCampaignModel request)
        {
            var headers = await GetHeaders();

            var response = await _campaignApiGrpcServiceClient.CreateCampaignAsync(new CreateCampaignMessage
            {
                Title = request.Title ?? "",
                Condition = request.Condition ?? "",
                Code = request.Code ?? "",
                Description = request.Description ?? "",
                Store = new IdNameMessage
                {
                    Id = request.Store?.Id.ToString() ?? "",
                    Name = request.Store?.Name ?? ""
                },
                Merchant = new IdNameMessage
                {
                    Id = request.Merchant?.Id.ToString() ?? "",
                    Name = request.Merchant?.Name ?? ""
                },
                BusinessDirectory = new IdNameMessage
                {
                    Id = request.BusinessDirectory?.Id.ToString() ?? "",
                    Name = request.BusinessDirectory?.Name ?? ""
                },
                EndTime = request.EndTime?.ToTimestamp(),
                StartTime = request.StartTime?.ToTimestamp()
            }, headers);

            if (response.Status != 200)
            {
                return new JokerBaseResponse<CampaignModel>(null, response.Status, response.Message);
            }

            var campaign = response.Data.Unpack<CampaignMessage>();
            return new JokerBaseResponse<CampaignModel>(As(campaign), 200);
        }

        public async Task<JokerBaseResponse<CampaignModel>> UpdateAsync(UpdateCampaignModel request)
        {
            var headers = await GetHeaders();

            var response = await _campaignApiGrpcServiceClient.UpdateCampaignAsync(new UpdateCampaignMessage
            {
                Id = request.CampaignId.ToString(),
                Campaign = new UpdateCampaignMessageItem
                {
                    Title = request.Title ?? "",
                    Condition = request.Condition ?? "",
                    Code = request.Code ?? "",
                    Description = request.Description ?? "",
                    EndTime = request.EndTime?.ToTimestamp(),
                    StartTime = request.StartTime?.ToTimestamp()
                }
            }, headers);

            if (response.Status != 200)
            {
                return new JokerBaseResponse<CampaignModel>(null, response.Status, response.Message);
            }

            var campaign = response.Data.Unpack<CampaignMessage>();
            return new JokerBaseResponse<CampaignModel>(As(campaign), 200);
        }

        public async Task<JokerBaseResponse<bool>> DeleteAsync(Guid id)
        {
            var headers = await GetHeaders();

            var response = await _campaignApiGrpcServiceClient.DeleteCampaignAsync(
                new ByIdMessage { Id = id.ToString() },
                headers);


            if (response.Status != 200)
            {
                return new JokerBaseResponse<bool>(false, response.Status, response.Message);
            }

            var deleteCampaignMessage = response.Data.Unpack<DeleteCampaignMessage>();
            return new JokerBaseResponse<bool>(deleteCampaignMessage.Succeed, 200);
        }

        public async Task<CampaignModel> GetByIdAsync(Guid id)
        {
            var headers = await GetHeaders();

            var campaign =
                await _campaignApiGrpcServiceClient.GetByIdAsync(new ByIdMessage { Id = id.ToString() }, headers);
            return As(campaign);
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

        #region Converters

        private CampaignModel As(CampaignMessage campaignMessage)
        {
            return new ()
            {
                Id = campaignMessage.Id.ToGuid(),
                Title = campaignMessage.Title,
                Condition = campaignMessage.Condition,
                Channel = campaignMessage.Channel,
                Code = campaignMessage.Code,
                Description = campaignMessage.Description,
                BusinessDirectory = new Models.Shared.IdNameModel
                {
                    Id = campaignMessage.BusinessDirectory?.Id.ToGuid() ?? Guid.Empty,
                    Name = campaignMessage.BusinessDirectory?.Name
                },
                Store = new Models.Shared.IdNameModel()
                {
                    Id = campaignMessage.Store?.Id.ToGuid() ?? Guid.Empty,
                    Name = campaignMessage.Store?.Name
                },
                Merchant = new Models.Shared.IdNameModel
                {
                    Id = campaignMessage.Merchant?.Id.ToGuid() ?? Guid.Empty,
                    Name = campaignMessage.Merchant?.Name
                },
                EndTime = campaignMessage.EndTime?.ToDateTime(),
                StartTime = campaignMessage.StartTime?.ToDateTime(),
                PreviewImageUrl = campaignMessage.PreviewImageUrl,
                CreatedDate = campaignMessage.CreatedDate.ToDateTime(),
                ModifiedDate = campaignMessage.ModifiedDate?.ToDateTime(),
                Slug = campaignMessage.Slug,
                SlugKey = campaignMessage.SlugKey
            };
        }

        #endregion
    }
}