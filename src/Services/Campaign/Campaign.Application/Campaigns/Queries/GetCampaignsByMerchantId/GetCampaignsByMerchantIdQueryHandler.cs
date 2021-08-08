using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Campaign.Application.Campaigns.Dto;
using Campaign.Domain.CampaignAggregate.Repositories;
using Joker.Extensions;
using Joker.Extensions.Models;
using MediatR;

namespace Campaign.Application.Campaigns.Queries.GetCampaignsByMerchantId
{
    public class GetCampaignsByMerchantIdQueryHandler : IRequestHandler<GetCampaignsByMerchantIdQuery, PagedList<CampaignDto>>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;

        public GetCampaignsByMerchantIdQueryHandler(ICampaignRepository campaignRepository, IMapper mapper)
        {
            _campaignRepository = campaignRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<CampaignDto>> Handle(GetCampaignsByMerchantIdQuery request, CancellationToken cancellationToken)
        {
            var campaigns = _campaignRepository.Get()
                .Where(x => !x.IsDeleted && x.Merchant.RefId == request.MerchantId)
                .ProjectTo<CampaignDto>(_mapper.ConfigurationProvider)
                .ToPagedList(request.Filter);
            
            return campaigns;
        }
    }
}