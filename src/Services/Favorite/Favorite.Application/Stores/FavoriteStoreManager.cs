using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Favorite.Application.Stores.Commands.CreateFavoriteStore;
using Favorite.Application.Stores.Dto;
using Favorite.Core.Entities;
using Favorite.Core.Entities.Shared;
using Favorite.Core.Repositories;

namespace Favorite.Application.Stores
{
    public class FavoriteStoreManager
    {
        private readonly IFavoriteStoreRepository _favoriteStoreRepository;
        private readonly IMapper _mapper;

        public FavoriteStoreManager(IFavoriteStoreRepository favoriteStoreRepository,
            IMapper mapper)
        {
            _favoriteStoreRepository = favoriteStoreRepository;
            _mapper = mapper;
        }


        public async Task<FavoriteStoreDto> AddFavoriteStoreAsync(CreateFavoriteStoreCommand request)
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

            await _favoriteStoreRepository.AddFavoriteStoreAsync(favoriteStore);
            return _mapper.Map<FavoriteStoreDto>(favoriteStore);
        }

        public async Task<List<FavoriteStoreDto>> GetStoresByStoreIdAsync(string storeId)
        {
            var stores =await _favoriteStoreRepository.GetStoresByStoreIdAsync(storeId);

            return _mapper.Map<List<FavoriteStoreDto>>(stores);
        }
        
        public async Task<List<FavoriteStoreDto>> GetStoresByUserIdAsync(string userId)
        {
            var stores =await _favoriteStoreRepository.GetStoresByUserIdAsync(userId);

            return _mapper.Map<List<FavoriteStoreDto>>(stores);
        }
    }
}