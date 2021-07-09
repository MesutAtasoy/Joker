using System.Linq;
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

        public override async Task<CampaignListMessage> CreateCampaign(CreateCampaignMessage request,
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
                PreviewImageUrl = request.PreviewImageUrl,
                Galleries = request.Galleries.Select(x => new CampaignGalleryDto
                {
                    Order = x.Order,
                    ImageUrl = x.ImageUrl
                }).ToList()
            });

            return new CampaignListMessage
            {
                Channel = response.Channel,
                Code = response.Code,
                Condition = response.Condition,
                Description = response.Description,
                Id = response.Id.ToString(),
                Slug = response.Slug,
                SlugKey = response.SlugKey,
                Store = new IdName
                {
                    Id = response.Store.RefId.ToString(),
                    Name = response.Store.Name
                },
                Title = response.Title,
                CreatedDate = response.CreatedDate.ToTimestamp(),
                EndTime = response.EndTime?.ToTimestamp(),
                StartTime = response.StartTime?.ToTimestamp(),
                ModifiedDate = response.ModifiedDate?.ToTimestamp(),
                PreviewImageUrl = response.PreviewImageUrl
            };
        }

        public override async Task<CampaignListMessage> UpdateCampaign(UpdateCampaignMessage request,
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

            var response =
                await _campaignManager.UpdateAsync(new UpdateCampaignCommand(request.Id.ToGuid(), updateCampaignDto));

            return new CampaignListMessage
            {
                Channel = response.Channel,
                Code = response.Code,
                Condition = response.Condition,
                Description = response.Description,
                Id = response.Id.ToString(),
                Slug = response.Slug,
                SlugKey = response.SlugKey,
                Store = new IdName
                {
                    Id = response.Store.RefId.ToString(),
                    Name = response.Store.Name
                },
                Title = response.Title,
                CreatedDate = response.CreatedDate.ToTimestamp(),
                EndTime = response.EndTime?.ToTimestamp(),
                StartTime = response.StartTime?.ToTimestamp(),
                ModifiedDate = response.ModifiedDate?.ToTimestamp(),
                PreviewImageUrl = response.PreviewImageUrl
            };
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

            var message = new CampaignMessage
            {
                Channel = response.Channel,
                Code = response.Code,
                Condition = response.Condition,
                Description = response.Description,
                Id = response.Id.ToString(),
                Slug = response.Slug,
                SlugKey = response.SlugKey,
                Store = new IdName
                {
                    Id = response.Store.RefId.ToString(),
                    Name = response.Store.Name
                },
                Title = response.Title,
                CreatedDate = response.CreatedDate.ToTimestamp(),
                EndTime = response.EndTime?.ToTimestamp(),
                StartTime = response.StartTime?.ToTimestamp(),
                ModifiedDate = response.ModifiedDate?.ToTimestamp(),
                PreviewImageUrl = response.PreviewImageUrl
            };

            message.CampaignGalleries.AddRange(response.CampaignGalleries.Select(x => new CampaignGalleryMessage
            {
                Order = x.Order ?? 0,
                ImageUrl = x.ImageUrl
            }).ToList());
            
            return message;
        }
    }
}