using MediatR;
using Merchant.Application.Stores.Dto;

namespace Merchant.Application.Stores.Commands.UpdateStore;

public class UpdateStoreCommandHandler : IRequestHandler<UpdateStoreCommand, StoreDto>
{
    private readonly StoreManager _storeManager;
    public UpdateStoreCommandHandler( StoreManager storeManager)
    {
        _storeManager = storeManager;
    }
        
    public async Task<StoreDto> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
    {
        return await _storeManager.UpdateAsync(request);
    }
}