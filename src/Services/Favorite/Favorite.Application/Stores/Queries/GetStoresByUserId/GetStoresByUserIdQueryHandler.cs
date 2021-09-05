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
        private readonly IFavoriteStoreRepository _repository;
        private readonly IMapper _mapper;
        
        public GetStoresByUserIdQueryHandler(IFavoriteStoreRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<FavoriteStoreDto>> Handle(GetStoresByUserIdQuery request, CancellationToken cancellationToken)
        {
            var stores = await _repository.GetStoresByUserIdAsync(request.UserId);

            return _mapper.Map<List<FavoriteStoreDto>>(stores);
        }
    }
}