using System;
using Campaign.Application.Campaigns.Dto;
using MediatR;

namespace Campaign.Application.Campaigns.Queries.GetCampaignById
{
    public class GetCampaignByIdQuery : IRequest<CampaignDto>
    {
        public GetCampaignByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}