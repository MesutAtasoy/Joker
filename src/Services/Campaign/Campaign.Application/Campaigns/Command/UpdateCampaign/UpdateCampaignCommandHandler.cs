using System.Threading;
using System.Threading.Tasks;
using Campaign.Application.Campaigns.Dto;
using MediatR;

namespace Campaign.Application.Campaigns.Command.UpdateCampaign
{
    public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, CampaignListDto>
    {
        private readonly CampaignManager _campaignManager;
        
        public UpdateCampaignCommandHandler(CampaignManager campaignManager)
        {
            _campaignManager = campaignManager;
        }
        public async Task<CampaignListDto> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            return await _campaignManager.UpdateAsync(request);
        }
    }
}