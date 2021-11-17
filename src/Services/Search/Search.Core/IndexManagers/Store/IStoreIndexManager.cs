using Search.Core.IndexModels;

namespace Search.Core.IndexManagers.Store;

public interface IStoreIndexManager
{
    Task BulkAddOrUpdateAsync(List<StoreIndexModel> list);
    Task CreateIndexAsync();
    Task AddOrUpdateAsync(StoreIndexModel model);
    Task DeleteAsync(StoreIndexModel model);
    Task DeleteAsync(Guid storeId);
    Task ReIndexAsync();
}