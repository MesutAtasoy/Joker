using System;
using System.Threading.Tasks;
using Aggregator.Api.Models.Campaign;
using Campaign.Api.Grpc;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Joker.Extensions;
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

        public async Task<CampaignModel> CreateAsync(CreateCampaignModel request)
        {
            var headers = await GetHeaders();
            var response = await _campaignApiGrpcServiceClient.CreateCampaignAsync(new CreateCampaignMessage
            {
                Title = request.Title,
                Condition = request.Condition,
                Channel = request.Channel,
                Code = request.Code,
                Description = request.Description,
                Store = new IdName
                {
                    Id = request.Store.Id.ToString(),
                    Name = request.Store.Name
                },
                BusinessDirectory = new IdName
                {
                    Id = request.BusinessDirectory.Id.ToString(),
                    Name = request.BusinessDirectory.Name
                },
                EndTime = request.EndTime?.ToTimestamp(),
                StartTime = request.StartTime?.ToTimestamp(),
                PreviewImageUrl = request.PreviewImageUrl
            }, headers);

            return As(response);
        }

        public async Task<CampaignModel> UpdateAsync(UpdateCampaignModel request)
        {
            var headers = await GetHeaders();

            var response = await _campaignApiGrpcServiceClient.UpdateCampaignAsync(new UpdateCampaignMessage
            {
                Id = request.CampaignId.ToString(),
                Campaign = new UpdateCampaignMessageItem
                {
                    Title = request.Title,
                    Condition = request.Condition,
                    Code = request.Code,
                    Description = request.Description,
                    PreviewImageUrl = request.PreviewImageUrl
                }
            }, headers);

            return As(response);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var headers = await GetHeaders();

            var response = await _campaignApiGrpcServiceClient.DeleteCampaignAsync(new ByIdMessage {Id = id.ToString()}, headers);
            return response.Succeed;
        }

        public async Task<CampaignModel> GetByIdAsync(Guid id)
        {
            var headers = await GetHeaders();

            var campaign = await _campaignApiGrpcServiceClient.GetByIdAsync(new ByIdMessage {Id = id.ToString()}, headers);
            return As(campaign);
        }

        private async Task<Metadata> GetHeaders()
        {
            var accessToken = await _contextAccessor?.HttpContext?.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

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
            return new()
            {
                Id = campaignMessage.Id.ToGuid(),
                Title = campaignMessage.Title,
                Condition = campaignMessage.Condition,
                Channel = campaignMessage.Channel,
                Code = campaignMessage.Code,
                Description = campaignMessage.Description,
                BusinessDirectory = new Models.Shared.IdName
                {
                    Id = campaignMessage.BusinessDirectory.Id.ToGuid(),
                    Name = campaignMessage.BusinessDirectory.Name
                },
                Store = new Models.Shared.IdName()
                {
                    Id = campaignMessage.Store.Id.ToGuid(),
                    Name = campaignMessage.Store.Name
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