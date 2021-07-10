using System.Threading.Tasks;
using Campaign.Api.Grpc;
using Campaign.Application.Campaigns;
using Campaign.Application.Campaigns.Command.CreateCampaign;
using Campaign.Application.Campaigns.Command.DeleteCampaign;
using Campaign.Application.Campaigns.Command.UpdateCampaign;
using Campaign.Application.Campaigns.Dto;
using Campaign.Application.Campaigns.Dto.Request;
using Campaign.Application.Shared.Dto;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Joker.Extensions;

namespace Campaign.Api.GrpcServices
{
    public class CampaignGrpcService : CampaignApiGrpcService.CampaignApiGrpcServiceBase
    {
        private readonly CampaignManager _campaignManager;

        public CampaignGrpcService(CampaignManager campaignManager)
        {
            _campaignManager = campaignManager;
        }

        public override async Task<CampaignMessage> CreateCampaign(CreateCampaignMessage request,
            ServerCallContext context)
        {
            var response = await _campaignManager.CreateAsync(new CreateCampaignCommand
            {
                Title = request.Title,
                Description = request.Description,
                Condition = request.Condition,
                Code = request.Code,
                Store = new IdNameDto
                {
                    RefId = request.Store.Id.ToGuid(),
                    Name = request.Store.Name
                },
                Channel = request.Channel,
                BusinessDirectory = new IdNameDto
                {
                    RefId = request.BusinessDirectory.Id.ToGuid(),
                    Name = request.BusinessDirectory.Name
                },
                EndTime = request.EndTime.ToDateTime(),
                StartTime = request.StartTime.ToDateTime(),
                PreviewImageUrl = request.PreviewImageUrl
            });

            return As(response);
        }

        public override async Task<CampaignMessage> UpdateCampaign(UpdateCampaignMessage request,
            ServerCallContext context)
        {
            var updateCampaignDto = new UpdateCampaignDto
            {
                Title = request.Campaign.Title,
                Description = request.Campaign.Description,
                Condition = request.Campaign.Condition,
                Code = request.Campaign.Code,
                PreviewImageUrl = request.Campaign.PreviewImageUrl,
            };

            var response = await _campaignManager.UpdateAsync(new UpdateCampaignCommand(request.Id.ToGuid(), updateCampaignDto));

            return As(response);
        }

        public override async Task<DeleteCampaignMessage> DeleteCampaign(ByIdMessage request, ServerCallContext context)
        {
            var response = await _campaignManager.DeleteAsync(new DeleteCampaignCommand(request.Id.ToGuid()));

            return new DeleteCampaignMessage
            {
                Succeed = response
            };
        }

        public override async Task<CampaignMessage> GetById(ByIdMessage request, ServerCallContext context)
        {
            var response = await _campaignManager.GetByIdAsync(request.Id.ToGuid());
            
            return As(response);
        }

        #region Converters

        public CampaignMessage As(CampaignDto campaign)
        {
            return new ()
            {
                Channel = campaign.Channel,
                Code = campaign.Code,
                Condition = campaign.Condition,
                Description = campaign.Description,
                Id = campaign.Id.ToString(),
                Slug = campaign.Slug,
                SlugKey = campaign.SlugKey,
                Store = new IdName
                {
                    Id = campaign.Store.RefId.ToString(),
                    Name = campaign.Store.Name
                },
                BusinessDirectory = new IdName
                {
                    Id = campaign.BusinessDirectory.RefId.ToString(),
                    Name = campaign.BusinessDirectory.Name
                },
                Title = campaign.Title,
                CreatedDate = campaign.CreatedDate.ToTimestamp(),
                EndTime = campaign.EndTime?.ToTimestamp(),
                StartTime = campaign.StartTime?.ToTimestamp(),
                ModifiedDate = campaign.ModifiedDate?.ToTimestamp(),
                PreviewImageUrl = campaign.PreviewImageUrl
            };
        }

        #endregion
    }
}