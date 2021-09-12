using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Favorite.Application.Stores.Dto;
using MediatR;

namespace Favorite.Application.Stores.Queries.GetStoresByStoreId
{
    public class GetStoresByStoreIdQueryHandler :  IRequestHandler<GetStoresByStoreIdQuery, List<FavoriteStoreDto>>
    {
        private readonly FavoriteStoreManager _storeManager;
        
        public GetStoresByStoreIdQueryHandler(FavoriteStoreManager storeManager)
        {
            _storeManager = storeManager;
        }
        public async Task<List<FavoriteStoreDto>> Handle(GetStoresByStoreIdQuery request, CancellationToken cancellationToken)
        {
            return await _storeManager.GetStoresByStoreIdAsync(request.StoreId.ToString());
        }
    }
}