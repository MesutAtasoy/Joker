using Search.Core.IndexModels;

namespace Search.Core.IndexManagers.Campaign;

public interface ICampaignIndexManager
{
    Task BulkAddOrUpdateAsync(List<CampaignIndexModel> list, int bulkNum = 1000);
    Task CreateIndexAsync();
    Task AddOrUpdateAsync(CampaignIndexModel model);
    Task DeleteAsync(CampaignIndexModel model);
    Task DeleteAsync(Guid campaignId);
    Task ReIndexAsync();
}