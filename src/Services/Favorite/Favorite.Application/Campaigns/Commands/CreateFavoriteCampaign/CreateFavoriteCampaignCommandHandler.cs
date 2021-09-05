using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Favorite.Application.Campaigns.Dto;
using Favorite.Core.Entities;
using Favorite.Core.Entities.Shared;
using Favorite.Core.Repositories;
using MediatR;

namespace Favorite.Application.Campaigns.Commands.CreateFavoriteCampaign
{
    public class CreateFavoriteCampaignCommandHandler : IRequestHandler<CreateFavoriteCampaignCommand, FavoriteCampaignDto>
    {
        private readonly IFavoriteCampaignRepository _campaignRepository;
        private readonly IMapper _mapper;
        
        public CreateFavoriteCampaignCommandHandler(IFavoriteCampaignRepository campaignRepository,
            IMapper mapper)
        {
            _campaignRepository = campaignRepository;
            _mapper = mapper;
        }
        
        public async Task<FavoriteCampaignDto> Handle(CreateFavoriteCampaignCommand request, CancellationToken cancellationToken)
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

            await _campaignRepository.AddFavoriteCampaignAsync(favoriteCampaign);

            return _mapper.Map<FavoriteCampaignDto>(favoriteCampaign);
        }
    }
}