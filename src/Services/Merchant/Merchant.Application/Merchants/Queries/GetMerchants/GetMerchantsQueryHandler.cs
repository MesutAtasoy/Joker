using MediatR;
using Merchant.Application.Merchants.Dto;

namespace Merchant.Application.Merchants.Queries.GetMerchants;

public class GetMerchantsQueryHandler: IRequestHandler<GetMerchantsQuery, List<MerchantDto>>
{
    private readonly MerchantManager _merchantManager;

    public GetMerchantsQueryHandler(MerchantManager merchantManager)
    {
        _merchantManager = merchantManager;
    }

    public async Task<List<MerchantDto>> Handle(GetMerchantsQuery request, CancellationToken cancellationToken)
    {
        return await _merchantManager.GetMerchantsAsync();
    }
}