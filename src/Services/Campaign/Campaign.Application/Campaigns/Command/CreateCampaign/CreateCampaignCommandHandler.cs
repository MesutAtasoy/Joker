using System.Threading;
using System.Threading.Tasks;
using Campaign.Application.Campaigns.Dto;
using MediatR;

namespace Campaign.Application.Campaigns.Command.CreateCampaign
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, CampaignListDto>
    {
        private readonly CampaignManager _campaignManager;
        
        public CreateCampaignCommandHandler(CampaignManager campaignManager)
        {
            _campaignManager = campaignManager;
        }
        
        public async Task<CampaignListDto> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            return await _campaignManager.CreateAsync(request);
        }
    }
}