using Aggregator.Api.Models.Campaign;
using Joker.Response;

namespace Aggregator.Api.Services.Campaign;

public interface ICampaignService
{
    Task<JokerBaseResponse<CampaignModel>> CreateAsync(CreateCampaignModel request);
    Task<JokerBaseResponse<CampaignModel>> UpdateAsync(UpdateCampaignModel request);
    Task<JokerBaseResponse<bool>> DeleteAsync(Guid id);
    Task<CampaignModel> GetByIdAsync(Guid id);
}