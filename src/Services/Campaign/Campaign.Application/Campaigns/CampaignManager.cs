using System;
using System.Threading.Tasks;
using AutoMapper;
using Campaign.Application.Campaigns.Command.CreateCampaign;
using Campaign.Application.Campaigns.Command.DeleteCampaign;
using Campaign.Application.Campaigns.Command.UpdateCampaign;
using Campaign.Application.Campaigns.Dto;
using Campaign.Domain.CampaignAggregate.Repositories;
using Campaign.Domain.Refs;
using Campaign.Infrastructure.Factories;
using Joker.Exceptions;

namespace Campaign.Application.Campaigns
{
    public class CampaignManager
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;

        public CampaignManager(ICampaignRepository campaignRepository,
            IMapper mapper)
        {
            _campaignRepository = campaignRepository;
            _mapper = mapper;
        }

        public async Task<CampaignDto> CreateAsync(CreateCampaignCommand request)
        {
            var campaignId = IdGenerationFactory.Create();
            
            var businessDirectoryRef = BusinessDirectoryRef.Create(request.BusinessDirectory.RefId,
                request.BusinessDirectory.Name);

            var storeRef = StoreRef.Create(request.Store.RefId, 
                request.Store.Name);

            var merchantRef = MerchantRef.Create(request.Merchant.RefId,
                request.Merchant.Name);
            
            var campaign = Domain.CampaignAggregate.Campaign.Create(campaignId,
                storeRef,
                merchantRef,
                businessDirectoryRef,
                request.Title,
                request.Code,
                request.Description,
                request.Condition,
                request.PreviewImageUrl,
                request.StartTime,
                request.EndTime,
                request.Channel);

            await _campaignRepository.AddAsync(campaign);
            
            return _mapper.Map<CampaignDto>(campaign);
        }

        public async Task<CampaignDto> UpdateAsync(UpdateCampaignCommand request)
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

            return _mapper.Map<CampaignDto>(campaign);
        }

        public async Task<bool> DeleteAsync(DeleteCampaignCommand request)
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

        public async Task<CampaignDto> GetByIdAsync(Guid id)
        {
            var campaign = await _campaignRepository.GetByIdAsync(id);
            
            if (campaign == null)
            {
                throw new NotFoundException("Campaign is not found");
            }

            return _mapper.Map<CampaignDto>(campaign);
        }
    }
}