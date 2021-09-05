using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Favorite.Application.Campaigns.Dto;
using Favorite.Application.Campaigns.Queries.GetCampaignsByCampaignId;
using Favorite.Core.Repositories;
using MediatR;

namespace Favorite.Application.Stores.Queries.GetStoresByStoreId
{
    public class GetCampaignsByCampaignIdQueryHandler :  IRequestHandler<GetCampaignsByCampaignIdQuery, List<FavoriteCampaignDto>>
    {
        private readonly IFavoriteCampaignRepository _repository;
        private readonly IMapper _mapper;
        
        public GetCampaignsByCampaignIdQueryHandler(IFavoriteCampaignRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<FavoriteCampaignDto>> Handle(GetCampaignsByCampaignIdQuery request, CancellationToken cancellationToken)
        {
            var campaigns = await _repository.GetCampaignsByCampaignIdAsync(request.CampaignId.ToString());

            return _mapper.Map<List<FavoriteCampaignDto>>(campaigns);
        }
    }
}