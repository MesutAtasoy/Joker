using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Campaign.Application.Campaigns.Dto;
using Campaign.Domain.CampaignAggregate.Repositories;
using MediatR;

namespace Campaign.Application.Campaigns.Queries.GetCampaignById
{
    public class GetCampaignByIdQueryHandler : IRequestHandler<GetCampaignByIdQuery, CampaignDto>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;
        
        public GetCampaignByIdQueryHandler(ICampaignRepository campaignRepository, IMapper mapper)
        {
            _campaignRepository = campaignRepository;
            _mapper = mapper;
        }
        public async Task<CampaignDto> Handle(GetCampaignByIdQuery request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.GetByIdAsync(request.Id);
            return _mapper.Map<CampaignDto>(campaign);
        }
    }
}