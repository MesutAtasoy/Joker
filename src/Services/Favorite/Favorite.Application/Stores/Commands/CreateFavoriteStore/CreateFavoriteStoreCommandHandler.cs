using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Favorite.Application.Stores.Dto;
using Favorite.Core.Entities;
using Favorite.Core.Entities.Shared;
using Favorite.Core.Repositories;
using MediatR;

namespace Favorite.Application.Stores.Commands.CreateFavoriteStore
{
    public class CreateFavoriteStoreCommandHandler : IRequestHandler<CreateFavoriteStoreCommand, FavoriteStoreDto>
    {
        private readonly IFavoriteStoreRepository _storeRepository;
        private readonly IMapper _mapper;
        
        public CreateFavoriteStoreCommandHandler(IFavoriteStoreRepository storeRepository,
            IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }
        
        public async Task<FavoriteStoreDto> Handle(CreateFavoriteStoreCommand request, CancellationToken cancellationToken)
        {
            var favoriteStore = new FavoriteStore
            {
                Store = new IdNameRef
                {
                    Id = request.Store.Id,
                    Name = request.Store.Name
                },
                UserInfo = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = Guid.NewGuid().ToString()
                },
                CreatedDate = DateTime.UtcNow
            };

            await _storeRepository.AddFavoriteStoreAsync(favoriteStore);

            return _mapper.Map<FavoriteStoreDto>(favoriteStore);
        }
    }
}