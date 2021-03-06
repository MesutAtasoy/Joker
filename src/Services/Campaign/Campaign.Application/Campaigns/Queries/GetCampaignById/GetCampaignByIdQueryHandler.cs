using Campaign.Application.Campaigns.Dto;
using MediatR;

namespace Campaign.Application.Campaigns.Queries.GetCampaignById;

public class GetCampaignByIdQueryHandler : IRequestHandler<GetCampaignByIdQuery, CampaignDto>
{
    private readonly CampaignManager _campaignManager;
        
    public GetCampaignByIdQueryHandler(CampaignManager campaignManager)
    {
        _campaignManager = campaignManager;
    }
    public async Task<CampaignDto> Handle(GetCampaignByIdQuery request, CancellationToken cancellationToken)
    {
        return await _campaignManager.GetByIdAsync(request.Id);
    }
}