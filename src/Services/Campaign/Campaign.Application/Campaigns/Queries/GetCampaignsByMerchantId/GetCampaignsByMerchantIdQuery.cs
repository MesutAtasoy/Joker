using System;
using Campaign.Application.Campaigns.Dto;
using Joker.Extensions.Models;
using MediatR;

namespace Campaign.Application.Campaigns.Queries.GetCampaignsByMerchantId
{
    public class GetCampaignsByMerchantIdQuery : IRequest<PagedList<CampaignDto>>
    {
        public GetCampaignsByMerchantIdQuery(Guid merchantId, PaginationFilter filter)
        {
            MerchantId = merchantId;
            Filter = filter;
        }

        public Guid MerchantId { get; }
        public PaginationFilter Filter { get; }
    }
}