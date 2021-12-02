using Aggregator.StoreFront.Api.Models.Merchant;
using Aggregator.StoreFront.Api.Models.Merchant.Requests;
using Aggregator.StoreFront.Api.Services.BaseGrpc;
using AutoMapper;
using Joker.Response;
using Merchant.Api.Grpc;

namespace Aggregator.StoreFront.Api.Services.Merchant;

public class MerchantService : IMerchantService
{
    private readonly MerchantApiGrpcService.MerchantApiGrpcServiceClient _merchantApiGrpcServiceClient;
    private readonly IBaseGrpcProvider _grpcProvider;
    private readonly IMapper _mapper;

    public MerchantService(MerchantApiGrpcService.MerchantApiGrpcServiceClient merchantApiGrpcServiceClient,
        IMapper mapper, IBaseGrpcProvider grpcProvider)
    {
        _merchantApiGrpcServiceClient = merchantApiGrpcServiceClient;
        _mapper = mapper;
        _grpcProvider = grpcProvider;
    }

    public async Task<JokerBaseResponse<MerchantModel>> CreateAsync(CreateMerchantModel request,
        string pricingPlanId, 
        string pricingPlanName,
        Guid organizationId)
    {
        var headers = await _grpcProvider.GetDefaultHeadersAsync();

        var createMerchantMessage = _mapper.Map<CreateMerchantMessage>(request);
        
        createMerchantMessage.OrganizationId = organizationId.ToString();
        createMerchantMessage.PricingPlan = new IdNameMessage { Id = pricingPlanId, Name = pricingPlanName };

        var response = await _merchantApiGrpcServiceClient.CreateMerchantAsync(createMerchantMessage, headers);

        if (response.Status != 200)
        {
            return new JokerBaseResponse<MerchantModel>(null, response.Status, response.Message);
        }

        var merchant = response.Data.Unpack<MerchantMessage>();

        var merchantModel = _mapper.Map<MerchantModel>(merchant);

        return new JokerBaseResponse<MerchantModel>(merchantModel, 200);
    }
}