using System;
using Campaign.Application.Campaigns.Dto;
using Campaign.Application.Campaigns.Dto.Request;
using MediatR;

namespace Campaign.Application.Campaigns.Command.UpdateCampaign
{
    public class UpdateCampaignCommand : IRequest<CampaignDto>
    {
        public UpdateCampaignCommand(Guid campaignId, 
            UpdateCampaignDto campaign)
        {
            CampaignId = campaignId;
            Campaign = campaign;
        }

        public Guid CampaignId { get; }
        public UpdateCampaignDto Campaign { get; }
    }
}