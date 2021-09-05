using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Favorite.Application.Campaigns.Dto;
using MediatR;

namespace Favorite.Application.Campaigns.Queries.GetCampaignsByCampaignId
{
    public class GetCampaignsByCampaignIdQueryHandler :  IRequestHandler<GetCampaignsByCampaignIdQuery, List<FavoriteCampaignDto>>
    {
        private readonly FavoriteCampaignManager _manager;
        
        public GetCampaignsByCampaignIdQueryHandler(FavoriteCampaignManager manager)
        {
            _manager = manager;
        }
        
        public async Task<List<FavoriteCampaignDto>> Handle(GetCampaignsByCampaignIdQuery request, CancellationToken cancellationToken)
        {
            return await _manager.GetCampaignsByCampaignIdAsync(request.CampaignId.ToString());
        }
    }
}