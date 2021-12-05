using Campaign.Application.Campaigns.Dto;
using Joker.Extensions.Models;
using MediatR;

namespace Campaign.Application.Campaigns.Queries.GetCampaigns;

public class GetCampaignsQuery : IRequest<PagedList<CampaignDto>>
{
    public GetCampaignsQuery(PaginationFilter filter)
    {
        Filter = filter;
    }
    
    public PaginationFilter Filter { get; }
}