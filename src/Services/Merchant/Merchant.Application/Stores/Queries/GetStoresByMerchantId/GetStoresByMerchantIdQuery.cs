using System;
using System.Collections.Generic;
using Joker.Extensions.Models;
using MediatR;
using Merchant.Application.Stores.Dto;

namespace Merchant.Application.Stores.Queries.GetStoresByMerchantId
{
    public class GetStoresByMerchantIdQuery : IRequest<PagedList<StoreDto>>
    {
        public GetStoresByMerchantIdQuery(Guid merchantId, PaginationFilter filter)
        {
            MerchantId = merchantId;
            Filter = filter;
        }

        public Guid MerchantId { get; }
        public PaginationFilter Filter { get; }
    }
}