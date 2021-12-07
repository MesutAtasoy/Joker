using MediatR;
using Merchant.Application.Merchants.Dto;

namespace Merchant.Application.Merchants.Queries.GetMerchants;

public class GetMerchantsQuery: IRequest<List<MerchantDto>>
{
    public GetMerchantsQuery()
    { }
}