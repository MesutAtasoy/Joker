using System.Text.Json;
using Aggregator.Api.Models.Merchant;
using Aggregator.Api.Services.BaseGrpc;
using AutoMapper;
using Joker.Response;
using Merchant.Api.Grpc;

namespace Aggregator.Api.Services.Merchant;

public class MerchantService : IMerchantService
{
    private readonly MerchantApiGrpcService.MerchantApiGrpcServiceClient _merchantApiGrpcServiceClient;
    private readonly IBaseGrpcProvider _grpcProvider;
    private readonly ILogger<MerchantService> _logger;
    private readonly IMapper _mapper;

    public MerchantService(MerchantApiGrpcService.MerchantApiGrpcServiceClient merchantApiGrpcServiceClient,
        ILogger<MerchantService> logger,
        IBaseGrpcProvider grpcProvider, IMapper mapper)
    {
        _merchantApiGrpcServiceClient = merchantApiGrpcServiceClient;
        _logger = logger;
        _grpcProvider = grpcProvider;
        _mapper = mapper;
    }

    public async Task<JokerBaseResponse<MerchantModel>> UpdateAsync(UpdateMerchantModel updateMerchantModel)
    {
        var headers = await _grpcProvider.GetDefaultHeadersAsync();

        var updateMerchantItemMessage = _mapper.Map<UpdateMerchantItemMessage>(updateMerchantModel);

        var request = new UpdateMerchantMessage
        {
            MerchantId = updateMerchantModel.Id,
            Merchant = updateMerchantItemMessage
        };

        var response = await _merchantApiGrpcServiceClient.UpdateMerchantAsync(request, headers);

        if (response.Status != 200)
        {
            return new JokerBaseResponse<MerchantModel>(null, response.Status, response.Message);
        }

        var merchant = response.Data.Unpack<MerchantMessage>();

        var merchantModel = _mapper.Map<MerchantModel>(merchant);

        return new JokerBaseResponse<MerchantModel>(merchantModel, 200);
    }


    public async Task<JokerBaseResponse<bool>> DeleteAsync(Guid id)
    {
        var headers = await _grpcProvider.GetDefaultHeadersAsync();

        var response =
            await _merchantApiGrpcServiceClient.DeleteMerchantAsync(new ByIdMessage { Id = id.ToString() },
                headers);
        if (response.Status != 200)
        {
            return new JokerBaseResponse<bool>(false, response.Status, response.Message);
        }

        var deleteCampaignMessage = response.Data.Unpack<DeleteMerchantResponseMessage>();
        return new JokerBaseResponse<bool>(deleteCampaignMessage.IsSucceed, 200);
    }

    public async Task<MerchantModel> GetById(Guid id)
    {
        var headers = await _grpcProvider.GetDefaultHeadersAsync();

        var merchant =
            await _merchantApiGrpcServiceClient.GetMerchantByIdAsync(new ByIdMessage { Id = id.ToString() },
                headers);

        var merchantModel = _mapper.Map<MerchantModel>(merchant);

        return merchantModel;
    }
}