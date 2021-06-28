using System;
using MediatR;

namespace Campaign.Application.Campaigns.Command.DeleteCampaign
{
    public class DeleteCampaignCommand : IRequest<bool>
    {
        public DeleteCampaignCommand(Guid campaignId)
        {
            CampaignId = campaignId;
        }

        public Guid CampaignId { get; }
    }
}