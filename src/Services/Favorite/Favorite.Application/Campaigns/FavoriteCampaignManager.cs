using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Favorite.Application.Campaigns.Commands.CreateFavoriteCampaign;
using Favorite.Application.Campaigns.Dto;
using Favorite.Application.Services;
using Favorite.Core.Entities;
using Favorite.Core.Repositories;

namespace Favorite.Application.Campaigns
{
    public class FavoriteCampaignManager
    {
        private readonly IFavoriteCampaignRepository _favoriteCampaignRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public FavoriteCampaignManager(IFavoriteCampaignRepository favoriteCampaignRepository,
            IUserService userService,
            IMapper mapper)
        {
            _favoriteCampaignRepository = favoriteCampaignRepository;
            _userService = userService;
            _mapper = mapper;
        }


        public async Task<FavoriteCampaignDto> AddFavoriteCampaignAsync(CreateFavoriteCampaignCommand request)
        {
            var favoriteCampaign = new FavoriteCampaign
            {
                Campaign = new Campaign
                {
                    Id = request.Id,
                    Title = request.Title,
                    Slug = request.Slug,
                    SlugKey = request.SlugKey
                },
                UserInfo = new User
                {
                    Id = _userService.GetUserId().ToString(),
                    Username = _userService.GetGivenName()
                },
                CreatedDate = DateTime.UtcNow
            };

            await _favoriteCampaignRepository.AddFavoriteCampaignAsync(favoriteCampaign);
            return _mapper.Map<FavoriteCampaignDto>(favoriteCampaign);
        }

        public async Task<List<FavoriteCampaignDto>> GetCampaignsByCampaignIdAsync(string campaignId)
        {
            var campaigns =await _favoriteCampaignRepository.GetCampaignsByCampaignIdAsync(campaignId);

            return _mapper.Map<List<FavoriteCampaignDto>>(campaigns);
        }
        
        public async Task<List<FavoriteCampaignDto>> GetCampaignsByUserIdAsync(string userId)
        {
            var campaigns =await _favoriteCampaignRepository.GetCampaignsByUserIdAsync(userId);

            return _mapper.Map<List<FavoriteCampaignDto>>(campaigns);
        }
    }
}