using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Search.Core.IndexManagers.Store;

namespace Search.Application.Stores.Initializers
{
    public class StoreInitializer : IStoreInitializer
    {
        private readonly IStoreIndexManager _indexManager;
        private readonly ILogger<StoreInitializer> _logger;

        public StoreInitializer(IStoreIndexManager indexManager,
            ILogger<StoreInitializer> logger)
        {
            _indexManager = indexManager;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            await _indexManager.CreateIndexAsync();
            _logger.LogInformation("Store Index Created");
        }
    }
}