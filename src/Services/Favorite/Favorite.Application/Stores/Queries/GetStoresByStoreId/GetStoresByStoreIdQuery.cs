using System;
using System.Collections.Generic;
using Favorite.Application.Stores.Dto;
using MediatR;

namespace Favorite.Application.Stores.Queries.GetStoresByStoreId
{
    public class GetStoresByStoreIdQuery : IRequest<List<FavoriteStoreDto>>
    {
        public GetStoresByStoreIdQuery(Guid storeId)
        {
            StoreId = StoreId;
        }

        public Guid StoreId { get;  }
    }
}