using MediatR;
using Merchant.Application.Merchants.Dto;

namespace Merchant.Application.Merchants.Queries.GetMerchantById;

public class GetMerchantByIdQuery : IRequest<MerchantDto>
{
    public GetMerchantByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}