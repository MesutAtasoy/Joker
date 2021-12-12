using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Favorite.Application.Services.Notification;
using Favorite.Application.Services.User;
using Favorite.Application.Stores.Commands.CreateFavoriteStore;
using Favorite.Application.Stores.Dto;
using Favorite.Core.Entities;
using Favorite.Core.Repositories;

namespace Favorite.Application.Stores
{
    public class FavoriteStoreManager
    {
        private readonly IFavoriteStoreRepository _favoriteStoreRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        
        public FavoriteStoreManager(IFavoriteStoreRepository favoriteStoreRepository,
            IMapper mapper, 
            IUserService userService,
            INotificationService notificationService)
        {
            _favoriteStoreRepository = favoriteStoreRepository;
            _mapper = mapper;
            _userService = userService;
            _notificationService = notificationService;
        }


        public async Task<FavoriteStoreDto> AddFavoriteStoreAsync(CreateFavoriteStoreCommand request)
        {
            var favoriteStore = new FavoriteStore
            {
                Store = new Store
                {
                    Id = request.Id,
                    Name = request.Name,
                    Slug = request.Slug,
                    SlugKey = request.SlugKey,
                    OrganizationId = request.OrganizationId
                },
                UserInfo = new User
                {
                    Id = _userService.GetUserId().ToString(),
                    Username = _userService.GetGivenName()
                },
                CreatedDate = DateTime.UtcNow
            };

            await _favoriteStoreRepository.AddFavoriteStoreAsync(favoriteStore);

            await _notificationService.SendAsync(request.OrganizationId, "New Like Store", $"The store ({request.Name}) is liked by someone.");

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