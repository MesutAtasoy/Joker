using System;
using System.Threading.Tasks;
using Aggregator.Api.Models.Campaign;

namespace Aggregator.Api.Services.Campaign
{
    public interface ICampaignService
    {
        Task<CampaignModel> CreateAsync(CreateCampaignModel request);
        Task<CampaignModel> UpdateAsync(UpdateCampaignModel request);
        Task<bool> DeleteAsync(Guid id);
        Task<CampaignModel> GetByIdAsync(Guid id);
    }
}