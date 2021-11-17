using MediatR;

namespace Campaign.Application.Campaigns.Command.DeleteCampaign;

public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand, bool>
{
    private readonly CampaignManager _campaignManager;

    public DeleteCampaignCommandHandler(CampaignManager campaignManager)
    {
        _campaignManager = campaignManager;
    }

    public async Task<bool> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
    {
        return await _campaignManager.DeleteAsync(request);
    }
}