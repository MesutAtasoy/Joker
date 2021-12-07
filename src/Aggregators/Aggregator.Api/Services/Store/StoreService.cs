using Aggregator.Api.Models.Store;
using Aggregator.Api.Models.Store.Requests;
using Aggregator.Api.Services.BaseGrpc;
using AutoMapper;
using Joker.Response;
using Merchant.Api.Grpc;

namespace Aggregator.Api.Services.Store;

public class StoreService : IStoreService
{
    private readonly MerchantApiGrpcService.MerchantApiGrpcServiceClient _merchantApiGrpcServiceClient;
    private readonly IBaseGrpcProvider _grpcProvider;
    private readonly IMapper _mapper;
        

    public StoreService(MerchantApiGrpcService.MerchantApiGrpcServiceClient merchantApiGrpcServiceClient,
        IMapper mapper,
        IBaseGrpcProvider grpcProvider)
    {
        _merchantApiGrpcServiceClient = merchantApiGrpcServiceClient;
        _mapper = mapper;
        _grpcProvider = grpcProvider;
    }

    public async Task<JokerBaseResponse<StoreModel>> CreateAsync(CreateStoreModel request)
    {
        var headers = await _grpcProvider.GetDefaultHeadersAsync();

        var createStoreMessage = _mapper.Map<CreateStoreMessage>(request);

        var response = await _merchantApiGrpcServiceClient.CreateStoreAsync(createStoreMessage, headers);

        if (response.Status != 200)
        {
            return new JokerBaseResponse<StoreModel>(null, response.Status, response.Message);
        }

        var store = response.Data.Unpack<StoreMessage>();
        
        var storeModel = _mapper.Map<StoreModel>(store);
        
        return new JokerBaseResponse<StoreModel>(storeModel, 200);
    }

    public async Task<JokerBaseResponse<StoreModel>> UpdateAsync(UpdateStoreModel request)
    {
        var headers = await _grpcProvider.GetDefaultHeadersAsync();

        var updateStoreItemMessage = _mapper.Map<UpdateStoreItemMessage>(request);

        var response = await _merchantApiGrpcServiceClient.UpdateStoreAsync(new UpdateStoreMessage
        {
            StoreId = request.Id.ToString(),
            Store = updateStoreItemMessage
        }, headers);

        if (response.Status != 200)
        {
            return new JokerBaseResponse<StoreModel>(null, response.Status, response.Message);
        }

        var store = response.Data.Unpack<StoreMessage>();
        
        var storeModel = _mapper.Map<StoreModel>(store);
        
        return new JokerBaseResponse<StoreModel>(storeModel, 200);
    }

    public async Task<JokerBaseResponse<StoreLocationModel>> UpdateLocationAsync(UpdateStoreLocationModel request)
    {
        var headers = await _grpcProvider.GetDefaultHeadersAsync();

        var requestStoreLocationMessage = _mapper.Map<StoreLocationMessage>(request.Location);

        var response = await _merchantApiGrpcServiceClient.UpdateLocationAsync(new UpdateStoreLocationMessage
        {
            StoreId = request.Id.ToString(),
            Location = requestStoreLocationMessage
        }, headers);

        if (response.Status != 200)
        {
            return new JokerBaseResponse<StoreLocationModel>(null, response.Status, response.Message);
        }

        var storeLocationMessage = response.Data.Unpack<StoreLocationMessage>();

        var storeLocationModel = _mapper.Map<StoreLocationModel>(storeLocationMessage);
        
        return new JokerBaseResponse<StoreLocationModel>(storeLocationModel, 200);
    }

    public async Task<JokerBaseResponse<bool>> DeleteAsync(Guid id)
    {
        var headers = await _grpcProvider.GetDefaultHeadersAsync();

        var response =
            await _merchantApiGrpcServiceClient.DeleteStoreAsync(new ByIdMessage { Id = id.ToString() }, headers);

        if (response.Status != 200)
        {
            return new JokerBaseResponse<bool>(false, response.Status, response.Message);
        }

        var deleteCampaignMessage = response.Data.Unpack<DeleteStoreResponseMessage>();
        return new JokerBaseResponse<bool>(deleteCampaignMessage.IsSucceed, 200);
    }

    public async Task<StoreModel> GetByIdAsync(Guid id)
    {
        var headers = await _grpcProvider.GetDefaultHeadersAsync();
        var store = await _merchantApiGrpcServiceClient.GetStoreByIdAsync(new ByIdMessage { Id = id.ToString() },
            headers);

        var storeModel = _mapper.Map<StoreModel>(store);
        return storeModel;
    }
}