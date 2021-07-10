using System;
using System.Threading.Tasks;
using Aggregator.Api.Models.Campaign;
using Campaign.Api.Grpc;
using Google.Protobuf.WellKnownTypes;
using Joker.Extensions;

namespace Aggregator.Api.Services.Campaign
{
    public class CampaignService : ICampaignService
    {
        private readonly CampaignApiGrpcService.CampaignApiGrpcServiceClient _campaignApiGrpcServiceClient;

        public CampaignService(CampaignApiGrpcService.CampaignApiGrpcServiceClient campaignApiGrpcServiceClient)
        {
            _campaignApiGrpcServiceClient = campaignApiGrpcServiceClient;
        }

        public async Task<CampaignModel> CreateAsync(CreateCampaignModel request)
        {
            var response = await _campaignApiGrpcServiceClient.CreateCampaignAsync(new CreateCampaignMessage
            {
                Title = request.Title,
                Condition = request.Condition,
                Channel = request.Channel,
                Code = request.Code,
                Description = request.Description,
                Store = new IdName
                {
                    Id = request.Store.Id,
                    Name = request.Store.Name
                },
                BusinessDirectory = new IdName
                {
                    Id = request.BusinessDirectory.Id,
                    Name = request.BusinessDirectory.Name
                },
                EndTime = request.EndTime?.ToTimestamp(),
                StartTime = request.StartTime?.ToTimestamp(),
                PreviewImageUrl = request.PreviewImageUrl
            });
            
            return As(response);
        }

        public async Task<CampaignModel> UpdateAsync(UpdateCampaignModel request)
        {
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

            });

            return As(response);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response =  await _campaignApiGrpcServiceClient.DeleteCampaignAsync(new ByIdMessage {Id = id.ToString()});
            return response.Succeed;        
        }

        public async Task<CampaignModel> GetByIdAsync(Guid id)
        {
            var campaign = await _campaignApiGrpcServiceClient.GetByIdAsync(new ByIdMessage {Id = id.ToString()});
            return As(campaign);
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
                    Id = campaignMessage.BusinessDirectory.Id,
                    Name = campaignMessage.BusinessDirectory.Name
                },
                Store = new Models.Shared.IdName()
                {
                    Id = campaignMessage.Store.Id,
                    Name = campaignMessage.Store.Name
                },
                EndTime = campaignMessage.EndTime.ToDateTime(),
                StartTime = campaignMessage.StartTime.ToDateTime(),
                PreviewImageUrl = campaignMessage.PreviewImageUrl,
                CreatedDate = campaignMessage.CreatedDate.ToDateTime(),
                ModifiedDate = campaignMessage.ModifiedDate.ToDateTime(),
            };
        }

        #endregion
    }
}