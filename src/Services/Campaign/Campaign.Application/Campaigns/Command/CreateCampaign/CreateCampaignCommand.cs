using System;
using System.Collections.Generic;
using Campaign.Application.Campaigns.Dto;
using Campaign.Application.Shared.Dto;
using MediatR;

namespace Campaign.Application.Campaigns.Command.CreateCampaign
{
    public class CreateCampaignCommand : IRequest<CampaignListDto>
    {
        public IdNameDto Store { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Condition { get; set; }
        public string PreviewImageUrl { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Channel { get; set; }
        public List<CampaignGalleryDto> Galleries { get; set; }
    }
}