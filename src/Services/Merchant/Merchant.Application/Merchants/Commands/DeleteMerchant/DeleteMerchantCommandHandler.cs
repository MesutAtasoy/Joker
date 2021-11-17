using MediatR;

namespace Merchant.Application.Merchants.Commands.DeleteMerchant;

public class DeleteMerchantCommandHandler : IRequestHandler<DeleteMerchantCommand, bool>
{
    private readonly MerchantManager _merchantManager;
    public DeleteMerchantCommandHandler(MerchantManager merchantManager)
    {
        _merchantManager = merchantManager;
    }
        
    public async Task<bool> Handle(DeleteMerchantCommand request, CancellationToken cancellationToken)
    {
        return await _merchantManager.DeleteAsync(request);
    }
}