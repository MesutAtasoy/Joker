using System;
using System.Collections.Generic;
using Favorite.Application.Campaigns.Dto;
using MediatR;

namespace Favorite.Application.Campaigns.Queries.GetCampaignsByCampaignId
{
    public class GetCampaignsByCampaignIdQuery : IRequest<List<FavoriteCampaignDto>>
    {
        public GetCampaignsByCampaignIdQuery(Guid campaignId)
        {
            CampaignId = campaignId;
        }

        public Guid CampaignId { get;  }
    }
}