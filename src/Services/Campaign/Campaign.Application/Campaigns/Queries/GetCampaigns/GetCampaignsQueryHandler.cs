using AutoMapper;
using AutoMapper.QueryableExtensions;
using Campaign.Application.Campaigns.Dto;
using Campaign.Application.Services;
using Campaign.Domain.CampaignAggregate.Repositories;
using Joker.Extensions;
using Joker.Extensions.Models;
using MediatR;

namespace Campaign.Application.Campaigns.Queries.GetCampaigns;

public class GetCampaignsQueryHandler : IRequestHandler<GetCampaignsQuery, PagedList<CampaignDto>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetCampaignsQueryHandler(ICampaignRepository campaignRepository,
        IMapper mapper, IUserService userService)
    {
        _campaignRepository = campaignRepository;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<PagedList<CampaignDto>> Handle(GetCampaignsQuery request,
        CancellationToken cancellationToken)
    {
        var organizationId = _userService.GetOrganizationId();
        
        var campaigns = _campaignRepository.Get()
            .Where(x => !x.IsDeleted && x.OrganizationId == organizationId)
            .ProjectTo<CampaignDto>(_mapper.ConfigurationProvider)
            .ToPagedList(request.Filter);

        return campaigns;
    }
}