
using Aggregator.StoreFront.Api.Models.Campaign;

namespace Aggregator.StoreFront.Api.Services.Campaign;

public interface ICampaignService
{
    Task<CampaignModel> GetByIdAsync(Guid id);
}