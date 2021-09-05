using System;
using System.Collections.Generic;
using Favorite.Application.Campaigns.Dto;
using MediatR;

namespace Favorite.Application.Campaigns.Queries.GetCampaignsByUserId
{
    public class GetCampaignsByUserIdQuery : IRequest<List<FavoriteCampaignDto>>
    {
        public GetCampaignsByUserIdQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get;  }
    }
}