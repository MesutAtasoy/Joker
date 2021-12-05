using Aggregator.Api.Models.Campaign;
using Aggregator.Api.Services.BaseGrpc;
using AutoMapper;
using Campaign.Api.Grpc;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Joker.Extensions;
using Joker.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Aggregator.Api.Services.Campaign;

public class CampaignService : ICampaignService
{
    private readonly CampaignApiGrpcService.CampaignApiGrpcServiceClient _campaignApiGrpcServiceClient;
    private readonly IMapper _mapper;
    private readonly IBaseGrpcProvider _grpcProvider;

    public CampaignService(CampaignApiGrpcService.CampaignApiGrpcServiceClient campaignApiGrpcServiceClient,
        IMapper mapper, 
        IBaseGrpcProvider grpcProvider)
    {
        _campaignApiGrpcServiceClient = campaignApiGrpcServiceClient;
        _mapper = mapper;
        _grpcProvider = grpcProvider;
    }

    public async Task<JokerBaseResponse<CampaignModel>> CreateAsync(CreateCampaignModel request)
    {
        var headers = await _grpcProvider.GetDefaultHeadersAsync();

        var createCampaignMessage = _mapper.Map<CreateCampaignMessage>(request);
     
        var response = await _campaignApiGrpcServiceClient.CreateCampaignAsync(createCampaignMessage, headers);

        if (response.Status != 200)
        {
            return new JokerBaseResponse<CampaignModel>(null, response.Status, response.Message);
        }

        var campaign = response.Data.Unpack<CampaignMessage>();
        
        var campaignModel = _mapper.Map<CampaignModel>(campaign);
        
        return new JokerBaseResponse<CampaignModel>(campaignModel, 200);
    }

    public async Task<JokerBaseResponse<CampaignModel>> UpdateAsync(UpdateCampaignModel request)
    {
        var headers = await _grpcProvider.GetDefaultHeadersAsync();

        var updateCampaignMessageItem = _mapper.Map<UpdateCampaignMessageItem>(request);
        
        var response = await _campaignApiGrpcServiceClient.UpdateCampaignAsync(new UpdateCampaignMessage
        {
            Id = request.CampaignId.ToString(),
            Campaign = updateCampaignMessageItem
        }, headers);

        if (response.Status != 200)
        {
            return new JokerBaseResponse<CampaignModel>(null, response.Status, response.Message);
        }

        var campaign = response.Data.Unpack<CampaignMessage>();
        
        var campaignModel = _mapper.Map<CampaignModel>(campaign);
        
        return new JokerBaseResponse<CampaignModel>(campaignModel, 200);
    }

    public async Task<JokerBaseResponse<bool>> DeleteAsync(Guid id)
    {
        var headers = await _grpcProvider.GetDefaultHeadersAsync();
        
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
        var headers = await _grpcProvider.GetDefaultHeadersAsync();
        
        var campaign = await _campaignApiGrpcServiceClient.GetByIdAsync(new ByIdMessage { Id = id.ToString() }, headers);

        return _mapper.Map<CampaignModel>(campaign);
    }
}