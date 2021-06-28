using System.Threading;
using System.Threading.Tasks;
using Campaign.Domain.CampaignAggregate.Repositories;
using Joker.Exceptions;
using MediatR;

namespace Campaign.Application.Campaigns.Command.DeleteCampaign
{
    public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand, bool>
    {
        private readonly ICampaignRepository _campaignRepository;

        public DeleteCampaignCommandHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<bool> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.GetByIdAsync(request.CampaignId);

            if (campaign == null)
            {
                throw new NotFoundException("Campaign is not found");
            }
            
            campaign.MarkAsDeleted();

            await _campaignRepository.UpdateAsync(campaign.Id, campaign);
            
            return true;
        }
    }
}