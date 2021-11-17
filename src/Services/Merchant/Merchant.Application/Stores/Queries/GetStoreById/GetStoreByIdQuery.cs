using MediatR;
using Merchant.Application.Stores.Dto;

namespace Merchant.Application.Stores.Queries.GetStoreById;

public class GetStoreByIdQuery : IRequest<StoreDto>
{
    public GetStoreByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}