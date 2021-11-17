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
using Microsoft.AspNetCore.Authorization;

namespace Campaign.Api.GrpcServices;

[Authorize(Policy = "ScopePolicy")]
public class CampaignGrpcService : CampaignApiGrpcService.CampaignApiGrpcServiceBase
{
    private readonly CampaignManager _campaignManager;

    public CampaignGrpcService(CampaignManager campaignManager)
    {
        _campaignManager = campaignManager;
    }

    public override async Task<CampaignBaseGrpcResponse> CreateCampaign(CreateCampaignMessage request,
        ServerCallContext context)
    {
        try
        {
            var response = await _campaignManager.CreateAsync(new CreateCampaignCommand
            {
                Title = request.Title,
                Description = request.Description,
                Condition = request.Condition,
                Code = request.Code,
                Merchant = new IdNameDto
                {
                    RefId = request.Merchant.Id.ToGuid(),
                    Name = request.Merchant.Name
                },
                Store = new IdNameDto
                {
                    RefId = request.Store.Id.ToGuid(),
                    Name = request.Store.Name
                },
                BusinessDirectory = new IdNameDto
                {
                    RefId = request.BusinessDirectory.Id.ToGuid(),
                    Name = request.BusinessDirectory.Name
                },
                EndTime = request.EndTime?.ToDateTime(),
                StartTime = request.StartTime?.ToDateTime()
            });

            var campaignMessage = As(response);
            return new CampaignBaseGrpcResponse
            {
                Data = Any.Pack(campaignMessage),
                Message = " ",
                Status = 200
            };
        }
        catch (Exception e)
        {
            return new CampaignBaseGrpcResponse
            {
                Data = null,
                Message = e.Message,
                Status = 400
            };
        }
    }

    public override async Task<CampaignBaseGrpcResponse> UpdateCampaign(UpdateCampaignMessage request,
        ServerCallContext context)
    {
        var updateCampaignDto = new UpdateCampaignDto
        {
            Title = request.Campaign.Title,
            Description = request.Campaign.Description,
            Condition = request.Campaign.Condition,
            Code = request.Campaign.Code,
            EndTime = request.Campaign.EndTime?.ToDateTime(),
            StartTime = request.Campaign.StartTime?.ToDateTime()
        };

        try
        {
            var response =
                await _campaignManager.UpdateAsync(
                    new UpdateCampaignCommand(request.Id.ToGuid(), updateCampaignDto));
            var campaignMessage = As(response);
            return new CampaignBaseGrpcResponse
            {
                Data = Any.Pack(campaignMessage),
                Message = " ",
                Status = 200
            };
        }
        catch (Exception e)
        {
            return new CampaignBaseGrpcResponse
            {
                Data = null,
                Message = e.Message,
                Status = 400
            };
        }
    }

    public override async Task<CampaignBaseGrpcResponse> DeleteCampaign(ByIdMessage request,
        ServerCallContext context)
    {
        try
        {
            var response = await _campaignManager.DeleteAsync(new DeleteCampaignCommand(request.Id.ToGuid()));

            var deleteCampaignMessage = new DeleteCampaignMessage { Succeed = response };
            return new CampaignBaseGrpcResponse
            {
                Data = Any.Pack(deleteCampaignMessage),
                Message = " ",
                Status = 200
            };
        }
        catch (Exception e)
        {
            return new CampaignBaseGrpcResponse
            {
                Data = null,
                Message = e.Message,
                Status = 400
            };
        }
    }

    public override async Task<CampaignMessage> GetById(ByIdMessage request, ServerCallContext context)
    {
        var response = await _campaignManager.GetByIdAsync(request.Id.ToGuid());

        return As(response);
    }

    #region Converters

    public CampaignMessage As(CampaignDto campaign)
    {
        return new()
        {
            Channel = campaign.Channel ?? " ",
            Code = campaign.Code ?? " ",
            Condition = campaign.Condition ?? " ",
            Description = campaign.Description ?? " ",
            Id = campaign.Id.ToString(),
            Slug = campaign.Slug,
            SlugKey = campaign.SlugKey,
            Store = new IdNameMessage
            {
                Id = campaign?.Store?.RefId.ToString() ?? "",
                Name = campaign.Store?.Name
            },
            Merchant = new IdNameMessage
            {
                Id = campaign.Merchant?.RefId.ToString() ?? "",
                Name = campaign.Merchant?.Name ?? ""
            },
            BusinessDirectory = new IdNameMessage
            {
                Id = campaign.BusinessDirectory?.RefId.ToString() ?? "",
                Name = campaign.BusinessDirectory?.Name ?? ""
            },
            Title = campaign.Title ?? "",
            CreatedDate = campaign.CreatedDate.ToTimestamp(),
            EndTime = campaign.EndTime?.ToTimestamp(),
            StartTime = campaign.StartTime?.ToTimestamp(),
            ModifiedDate = campaign.ModifiedDate?.ToTimestamp(),
            PreviewImageUrl = campaign.PreviewImageUrl ?? " "
        };
    }

    #endregion
}