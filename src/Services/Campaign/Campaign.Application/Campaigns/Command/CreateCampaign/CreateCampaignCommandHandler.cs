using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Campaign.Application.Campaigns.Dto;
using Campaign.Domain.CampaignAggregate;
using Campaign.Domain.CampaignAggregate.Repositories;
using Campaign.Domain.Refs;
using Campaign.Infrastructure.Factories;
using MediatR;

namespace Campaign.Application.Campaigns.Command.CreateCampaign
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, CampaignListDto>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;
        
        public CreateCampaignCommandHandler(ICampaignRepository campaignRepository, 
            IMapper mapper)
        {
            _campaignRepository = campaignRepository;
            _mapper = mapper;
        }
        
        public async Task<CampaignListDto> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaignId = IdGenerationFactory.Create();

            var campaignGalleries = new List<CampaignGallery>();

            if (request.Galleries != null && request.Galleries.Any())
            {
                campaignGalleries = request.Galleries
                    .Select(x => new CampaignGallery(x.ImageUrl, x.Order))
                    .ToList();
            }

            var businessDirectoryRef = BusinessDirectoryRef.Create(request.BusinessDirectory.RefId,
                request.BusinessDirectory.Name);

            var storeRef = StoreRef.Create(request.Store.RefId, 
                request.Store.Name);
            
            var campaign = Domain.CampaignAggregate.Campaign.Create(campaignId,
                storeRef,
                businessDirectoryRef,
                request.Title,
                request.Code,
                request.Description,
                request.Condition,
                request.PreviewImageUrl,
                request.StartTime,
                request.EndTime,
                request.Channel,
                campaignGalleries);

            await _campaignRepository.AddAsync(campaign);
            
            return _mapper.Map<CampaignListDto>(campaign);
        }
    }
}