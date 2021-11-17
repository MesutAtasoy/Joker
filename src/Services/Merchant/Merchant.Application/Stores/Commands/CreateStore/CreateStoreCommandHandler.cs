using MediatR;
using Merchant.Application.Stores.Dto;

namespace Merchant.Application.Stores.Commands.CreateStore;

public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, StoreDto>
{
    private readonly StoreManager _storeManager;

    public CreateStoreCommandHandler(StoreManager storeManager)
    {
        _storeManager = storeManager;
    }

    public async Task<StoreDto> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
    {
        return await _storeManager.CreateAsync(request);
    }
}