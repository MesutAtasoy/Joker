using System;
using Campaign.Application.Campaigns.Dto;
using Campaign.Application.Shared.Dto;
using MediatR;

namespace Campaign.Application.Campaigns.Command.CreateCampaign
{
    public class CreateCampaignCommand : IRequest<CampaignDto>
    {
        public IdNameDto Store { get; set; }
        public IdNameDto BusinessDirectory { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Condition { get; set; }
        public string PreviewImageUrl { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Channel { get; set; }
    }
}