using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Campaign.Application.Campaigns.Dto;
using Campaign.Domain.CampaignAggregate.Repositories;
using Joker.Exceptions;
using MediatR;

namespace Campaign.Application.Campaigns.Command.UpdateCampaign
{
    public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, CampaignListDto>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;
        
        public UpdateCampaignCommandHandler(ICampaignRepository campaignRepository, 
            IMapper mapper)
        {
            _campaignRepository = campaignRepository;
            _mapper = mapper;
        }
        public async Task<CampaignListDto> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.GetByIdAsync(request.CampaignId);

            if (campaign == null)
            {
                throw new NotFoundException("Campaign is not found");
            }
            
            campaign.Update(request.Campaign.Title,
                request.Campaign.Code,
                request.Campaign.Description,
                request.Campaign.Condition,
                request.Campaign.PreviewImageUrl,
                request.Campaign.StartTime,
                request.Campaign.EndTime);

            await _campaignRepository.UpdateAsync(campaign.Id, campaign);

            return _mapper.Map<CampaignListDto>(campaign);
        }
    }
}