using Aggregator.StoreFront.Api.Models.Campaign;
using Aggregator.StoreFront.Api.Services.BaseGrpc;
using AutoMapper;
using Campaign.Api.Grpc;

namespace Aggregator.StoreFront.Api.Services.Campaign;

public class CampaignService : ICampaignService
{
    private readonly CampaignApiGrpcService.CampaignApiGrpcServiceClient _campaignApiGrpcServiceClient;
    private readonly IBaseGrpcProvider _grpcProvider;
    private readonly IMapper _mapper;
    
    public CampaignService(CampaignApiGrpcService.CampaignApiGrpcServiceClient campaignApiGrpcServiceClient,
        IMapper mapper, 
        IBaseGrpcProvider grpcProvider)
    {
        _campaignApiGrpcServiceClient = campaignApiGrpcServiceClient;
        _mapper = mapper;
        _grpcProvider = grpcProvider;
    }
    
    public async Task<CampaignModel> GetByIdAsync(Guid id)
    {
        var headers = await _grpcProvider.GetDefaultHeadersAsync();

        var campaign = await _campaignApiGrpcServiceClient.GetByIdAsync(new ByIdMessage { Id = id.ToString() }, headers);
        return _mapper.Map<CampaignModel>(campaign) ;
    }
}