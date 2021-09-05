using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Favorite.Application.Stores.Dto;
using Favorite.Core.Repositories;
using MediatR;

namespace Favorite.Application.Stores.Queries.GetStoresByUserId
{
    public class GetStoresByUserIdQueryHandler :  IRequestHandler<GetStoresByUserIdQuery, List<FavoriteStoreDto>>
    {
        private readonly FavoriteStoreManager _storeManager;
        
        public GetStoresByUserIdQueryHandler(FavoriteStoreManager storeManager)
        {
            _storeManager = storeManager;
        }
        public async Task<List<FavoriteStoreDto>> Handle(GetStoresByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _storeManager.GetStoresByUserIdAsync(request.UserId);
        }
    }
}