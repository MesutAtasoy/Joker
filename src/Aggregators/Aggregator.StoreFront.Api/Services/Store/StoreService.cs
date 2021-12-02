using Aggregator.StoreFront.Api.Models.Store;
using Aggregator.StoreFront.Api.Services.BaseGrpc;
using AutoMapper;
using Merchant.Api.Grpc;

namespace Aggregator.StoreFront.Api.Services.Store;

public class StoreService : IStoreService
{
    private readonly MerchantApiGrpcService.MerchantApiGrpcServiceClient _merchantApiGrpcServiceClient;
    private readonly IMapper _mapper;
    private readonly IBaseGrpcProvider _grpcProvider;
    
    public StoreService(MerchantApiGrpcService.MerchantApiGrpcServiceClient merchantApiGrpcServiceClient,
        IMapper mapper, 
        IBaseGrpcProvider grpcProvider)
    {
        _merchantApiGrpcServiceClient = merchantApiGrpcServiceClient;
        _mapper = mapper;
        _grpcProvider = grpcProvider;
    }

    public async Task<StoreModel> GetByIdAsync(Guid id)
    {
        var headers = await _grpcProvider.GetDefaultHeadersAsync();
        
        var store = await _merchantApiGrpcServiceClient.GetStoreByIdAsync(new ByIdMessage { Id = id.ToString() },
            headers);

        return _mapper.Map<StoreModel>(store);
    }
}