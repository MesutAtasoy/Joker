using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Favorite.Application.Campaigns.Commands.CreateFavoriteCampaign;
using Favorite.Application.Campaigns.Dto;
using Favorite.Core.Entities;
using Favorite.Core.Entities.Shared;
using Favorite.Core.Repositories;

namespace Favorite.Application.Campaigns
{
    public class FavoriteCampaignManager
    {
        private readonly IFavoriteCampaignRepository _favoriteCampaignRepository;
        private readonly IMapper _mapper;

        public FavoriteCampaignManager(IFavoriteCampaignRepository favoriteCampaignRepository,
            IMapper mapper)
        {
            _favoriteCampaignRepository = favoriteCampaignRepository;
            _mapper = mapper;
        }


        public async Task<FavoriteCampaignDto> AddFavoriteCampaignAsync(CreateFavoriteCampaignCommand request)
        {
            var favoriteCampaign = new FavoriteCampaign
            {
                Campaign = new IdNameRef
                {
                    Id = request.Campaign.Id,
                    Name = request.Campaign.Name
                },
                UserInfo = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = Guid.NewGuid().ToString()
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