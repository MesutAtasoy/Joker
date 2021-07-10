using System;
using System.Collections.Generic;
using MediatR;
using Merchant.Application.Stores.Dto;

namespace Merchant.Application.Stores.Queries.GetStoresByMerchantId
{
    public class GetStoresByMerchantIdQuery : IRequest<List<StoreDto>>
    {
        public GetStoresByMerchantIdQuery(Guid merchantId)
        {
            MerchantId = merchantId;
        }

        public Guid MerchantId { get; }
    }
}