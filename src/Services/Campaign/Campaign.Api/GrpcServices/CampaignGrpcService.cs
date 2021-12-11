using AutoMapper;
using Campaign.Api.Grpc;
using Campaign.Application.Campaigns;
using Campaign.Application.Campaigns.Command.CreateCampaign;
using Campaign.Application.Campaigns.Command.DeleteCampaign;
using Campaign.Application.Campaigns.Command.UpdateCampaign;
using Campaign.Application.Campaigns.Dto.Request;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Joker.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Campaign.Api.GrpcServices;

[Authorize(Policy = "ScopePolicy")]
public class CampaignGrpcService : CampaignApiGrpcService.CampaignApiGrpcServiceBase
{
    private readonly CampaignManager _campaignManager;
    private readonly IMapper _mapper;

    public CampaignGrpcService(CampaignManager campaignManager,
        IMapper mapper)
    {
        _campaignManager = campaignManager;
        _mapper = mapper;
    }

    public override async Task<CampaignBaseGrpcResponse> CreateCampaign(CreateCampaignMessage request,
        ServerCallContext context)
    {
        try
        {
            var createCampaignCommand = _mapper.Map<CreateCampaignCommand>(request);
            
            var response = await _campaignManager.CreateAsync(createCampaignCommand);

            var campaignMessage = _mapper.Map<CampaignMessage>(response);
            
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
        var updateCampaignDto = _mapper.Map<UpdateCampaignDto>(request.Campaign);

        try
        {
            var response = await _campaignManager.UpdateAsync(new UpdateCampaignCommand(request.Id.ToGuid(), updateCampaignDto));
            var campaignMessage = _mapper.Map<CampaignMessage>(response);
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

        return _mapper.Map<CampaignMessage>(response);
    }
}