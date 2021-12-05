using Joker.Extensions.Models;
using MediatR;
using Merchant.Application.Stores.Dto;

namespace Merchant.Application.Stores.Queries.GetStores;

public class GetStoresQuery: IRequest<PagedList<StoreDto>>
{
    public GetStoresQuery(PaginationFilter filter)
    {
        Filter = filter;
    }
    
    public PaginationFilter Filter { get; }
}