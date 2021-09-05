using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Favorite.Application.Campaigns.Dto;
using MediatR;

namespace Favorite.Application.Campaigns.Queries.GetCampaignsByUserId
{
    public class GetCampaignsByUserIdQueryHandler :  IRequestHandler<GetCampaignsByUserIdQuery, List<FavoriteCampaignDto>>
    {
        private readonly FavoriteCampaignManager _manager;
        
        public GetCampaignsByUserIdQueryHandler(FavoriteCampaignManager manager)
        {
            _manager = manager;
        }
        
        public async Task<List<FavoriteCampaignDto>> Handle(GetCampaignsByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _manager.GetCampaignsByUserIdAsync(request.UserId);
        }
    }
}