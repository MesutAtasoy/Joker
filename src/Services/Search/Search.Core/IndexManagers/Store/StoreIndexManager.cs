using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Joker.ElasticSearch.Service;
using Search.Core.Constants;
using Search.Core.IndexModels;

namespace Search.Core.IndexManagers.Store
{
    public class StoreIndexManager : IStoreIndexManager
    {
        private readonly IElasticSearchManager _indexManager;

        public StoreIndexManager(IElasticSearchManager indexManager)
        {
            _indexManager = indexManager;
        }

        public async Task BulkAddOrUpdateAsync(List<StoreIndexModel> list)
        {
            await _indexManager.BulkAddOrUpdateAsync<StoreIndexModel, Guid>(IndexConstants.StoreIndex, list);
        }

        public async Task CreateIndexAsync()
        {
            await _indexManager.CrateIndexAsync(IndexConstants.StoreIndex);
        }

        public async Task AddOrUpdateAsync(StoreIndexModel model)
        {
            await _indexManager.AddOrUpdateAsync<StoreIndexModel, Guid>(IndexConstants.StoreIndex, model);
        }

        public async Task DeleteAsync(StoreIndexModel model)
        {
            await _indexManager.DeleteAsync<StoreIndexModel, Guid>(IndexConstants.StoreIndex, null, model);
        }

        public async Task DeleteAsync(Guid storeId)
        {
            await DeleteAsync(new StoreIndexModel {Id = storeId});
        }

        public async Task ReIndexAsync()
        {
            await _indexManager.ReIndex<StoreIndexModel, Guid>(IndexConstants.StoreIndex);
        }
    }
}