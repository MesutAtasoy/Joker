using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Favorite.Application.Campaigns.Dto;
using Favorite.Core.Repositories;
using MediatR;

namespace Favorite.Application.Campaigns.Queries.GetCampaignsByUserId
{
    public class GetCampaignsByUserIdQueryHandler :  IRequestHandler<GetCampaignsByUserIdQuery, List<FavoriteCampaignDto>>
    {
        private readonly IFavoriteCampaignRepository _repository;
        private readonly IMapper _mapper;
        
        public GetCampaignsByUserIdQueryHandler(IFavoriteCampaignRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<FavoriteCampaignDto>> Handle(GetCampaignsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var campaigns = await _repository.GetCampaignsByUserIdAsync(request.UserId);

            return _mapper.Map<List<FavoriteCampaignDto>>(campaigns);
        }
    }
}