using MediatR;
using Merchant.Application.Merchants.Dto;

namespace Merchant.Application.Merchants.Commands.UpdateMerchant;

public class UpdateMerchantCommandHandler : IRequestHandler<UpdateMerchantCommand, MerchantDto>
{
    private readonly MerchantManager _merchantManager;

    public UpdateMerchantCommandHandler(MerchantManager merchantManager)
    {
        _merchantManager = merchantManager;
    }

    public async Task<MerchantDto> Handle(UpdateMerchantCommand request, CancellationToken cancellationToken)
    {
        return await _merchantManager.UpdateAsync(request);
    }
}