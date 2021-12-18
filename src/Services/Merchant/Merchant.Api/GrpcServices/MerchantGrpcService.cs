using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Joker.Extensions;
using Merchant.Api.Grpc;
using Merchant.Application.Merchants;
using Merchant.Application.Merchants.Commands.CreateMerchant;
using Merchant.Application.Merchants.Commands.DeleteMerchant;
using Merchant.Application.Merchants.Commands.UpdateMerchant;
using Merchant.Application.Merchants.Dto.Requests;
using Merchant.Application.Stores;
using Merchant.Application.Stores.Commands.CreateStore;
using Merchant.Application.Stores.Commands.DeleteStore;
using Merchant.Application.Stores.Commands.UpdateLocation;
using Merchant.Application.Stores.Commands.UpdateStore;
using Merchant.Application.Stores.Dto;
using Merchant.Application.Stores.Dto.Request;
using Microsoft.AspNetCore.Authorization;

namespace Merchant.Api.GrpcServices;

[Authorize(Policy = "ScopePolicy")]
public class MerchantGrpcService : MerchantApiGrpcService.MerchantApiGrpcServiceBase
{
    private readonly StoreManager _storeManager;
    private readonly MerchantManager _merchantManager;
    private readonly IMapper _mapper;

    public MerchantGrpcService(StoreManager storeManager,
        MerchantManager merchantManager,
        IMapper mapper)
    {
        _storeManager = storeManager;
        _merchantManager = merchantManager;
        _mapper = mapper;
    }

    public override async Task<MerchantBaseGrpcResponse> CreateMerchant(CreateMerchantMessage request,
        ServerCallContext context)
    {
        try
        {
            var createMerchantCommand = _mapper.Map<CreateMerchantCommand>(request);
            
            var response = await _merchantManager.CreateAsync(createMerchantCommand);

            var merchantMessage = _mapper.Map<MerchantMessage>(response);
            
            return new MerchantBaseGrpcResponse
            {
                Data = Any.Pack(merchantMessage),
                Message = " ",
                Status = 200
            };
        }
        catch (Exception e)
        {
            return new MerchantBaseGrpcResponse
            {
                Data = null,
                Message = e.Message,
                Status = 400
            };
        }
        
    }

    public override async Task<MerchantBaseGrpcResponse> UpdateMerchant(UpdateMerchantMessage request,
        ServerCallContext context)
    {
        try
        {
            var updateMerchantModel = _mapper.Map<UpdateMerchantDto>(request.Merchant);

            var response = await _merchantManager.UpdateAsync(new UpdateMerchantCommand(request.MerchantId.ToGuid(),
                updateMerchantModel));

            var merchantMessage = _mapper.Map<MerchantMessage>(response);
            
            return new MerchantBaseGrpcResponse
            {
                Data = Any.Pack(merchantMessage),
                Message = " ",
                Status = 200
            };
        }
        catch (Exception e)
        {
            return new MerchantBaseGrpcResponse
            {
                Data = null,
                Message = e.Message,
                Status = 400
            };
        }
          
    }

    public override async Task<MerchantBaseGrpcResponse> DeleteMerchant(ByIdMessage request,
        ServerCallContext context)
    {
        try
        {
            var isSucceed = await _merchantManager.DeleteAsync(new DeleteMerchantCommand(request.Id.ToGuid()));
            var deleteMerchantResponseMessage = new DeleteMerchantResponseMessage { IsSucceed = isSucceed };
            return new MerchantBaseGrpcResponse
            {
                Data = Any.Pack(deleteMerchantResponseMessage),
                Message = " ",
                Status = 200
            };
        }
        catch (Exception e)
        {
            return new MerchantBaseGrpcResponse
            {
                Data = null,
                Message = e.Message,
                Status = 400
            };
        }
    }

    public override async Task<MerchantMessage> GetMerchantById(ByIdMessage request,
        ServerCallContext context)
    {
        var response = await _merchantManager.GetByIdAsync(request.Id.ToGuid());
        return _mapper.Map<MerchantMessage>(response);
    }

    public override async Task<MerchantBaseGrpcResponse> CreateStore(CreateStoreMessage request,
        ServerCallContext context)
    {
        try
        {
            var createStoreCommand = _mapper.Map<CreateStoreCommand>(request);
            
            var response = await _storeManager.CreateAsync(createStoreCommand);

            var storeMessage = _mapper.Map<StoreMessage>(response);
            return new MerchantBaseGrpcResponse
            {
                Data = Any.Pack(storeMessage),
                Message = " ",
                Status = 200
            };
        }
        catch (Exception e)
        {
            return new MerchantBaseGrpcResponse
            {
                Data = null,
                Message = e.Message,
                Status = 400
            };
        }
            
    }

    public override async Task<MerchantBaseGrpcResponse> UpdateStore(UpdateStoreMessage request,
        ServerCallContext context)
    {
        try
        {
            var updateStoreModel = _mapper.Map<UpdateStoreDto>(request.Store);

            var response = await _storeManager.UpdateAsync(new UpdateStoreCommand(request.StoreId.ToGuid(), updateStoreModel));

            var storeMessage = _mapper.Map<StoreMessage>(response);
            return new MerchantBaseGrpcResponse
            {
                Data = Any.Pack(storeMessage),
                Message = " ",
                Status = 200
            };
        }
        catch (Exception e)
        {
            return new MerchantBaseGrpcResponse
            {
                Data = null,
                Message = e.Message,
                Status = 400
            };
        }
           
    }

    public override async Task<MerchantBaseGrpcResponse> UpdateLocation(UpdateStoreLocationMessage request, ServerCallContext context)
    {
        var storeLocation = _mapper.Map<StoreLocationDto>(request.Location);
        
        try
        {
            var response = await _storeManager.UpdateLocationAsync(new UpdateLocationCommand(request.StoreId.ToGuid(), storeLocation));

            var storeLocationMessage = _mapper.Map<StoreLocationMessage>(response);
                
            return new MerchantBaseGrpcResponse
            {
                Data = Any.Pack(storeLocationMessage),
                Message = " ",
                Status = 200
            };
        }
        catch (Exception e)
        {
            return new MerchantBaseGrpcResponse
            {
                Data = null,
                Message = e.Message,
                Status = 400
            };
        }
    }

    public override async Task<MerchantBaseGrpcResponse> DeleteStore(ByIdMessage request,
        ServerCallContext context)
    {
        try
        {
            var isSucceed = await _storeManager.DeleteAsync(new DeleteStoreCommand(request.Id.ToGuid()));
            
            var deleteStoreResponseMessage = new DeleteStoreResponseMessage{ IsSucceed = isSucceed };
            return new MerchantBaseGrpcResponse
            {
                Data = Any.Pack(deleteStoreResponseMessage),
                Message = " ",
                Status = 200
            };
        }
        catch (Exception e)
        {
            return new MerchantBaseGrpcResponse
            {
                Data = null,
                Message = e.Message,
                Status = 400
            };
        }
    }

    public override async Task<StoreMessage> GetStoreById(ByIdMessage request,
        ServerCallContext context)
    {
        var store = await _storeManager.GetByIdAsync(request.Id.ToGuid());
        return _mapper.Map<StoreMessage>(store);
    }
}