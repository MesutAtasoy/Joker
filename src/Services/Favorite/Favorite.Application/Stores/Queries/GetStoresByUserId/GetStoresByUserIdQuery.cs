using System.Collections.Generic;
using Favorite.Application.Stores.Dto;
using MediatR;

namespace Favorite.Application.Stores.Queries.GetStoresByUserId
{
    public class GetStoresByUserIdQuery : IRequest<List<FavoriteStoreDto>>
    {
        public GetStoresByUserIdQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get;  }
    }
}