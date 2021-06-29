using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Joker.ElasticSearch.Service;
using Nest;
using Search.Core.Constants;
using Search.Core.IndexModels;

namespace Search.Core.IndexManagers.Campaign
{
    public class CampaignIndexManager : ICampaignIndexManager
    {
        private readonly IElasticSearchManager _indexManager;
        private readonly IElasticClient _elasticClient;

        public CampaignIndexManager(IElasticSearchManager indexManager,
            IElasticClient elasticClient)
        {
            _indexManager = indexManager;
            _elasticClient = elasticClient;
        }

        public async Task BulkAddOrUpdateAsync(List<CampaignIndexModel> list, int bulkNum = 1000)
        {
            await _indexManager.BulkAddOrUpdateAsync<CampaignIndexModel, Guid>(IndexConstants.CampaignIndex,
                list, bulkNum);
        }

        public async Task CreateIndexAsync()
        {
            await _indexManager.CrateIndexAsync(IndexConstants.CampaignIndex);
        }

        public async Task AddOrUpdateAsync(CampaignIndexModel model)
        {
            await _indexManager.AddOrUpdateAsync<CampaignIndexModel, Guid>(IndexConstants.CampaignIndex, model);
        }

        public async Task DeleteAsync(CampaignIndexModel model)
        {
            await _indexManager.DeleteAsync<CampaignIndexModel, Guid>(IndexConstants.CampaignIndex, null, model);
        }

        public async Task DeleteAsync(Guid campaignId)
        {
            await DeleteAsync(new CampaignIndexModel {Id = campaignId});
        }

        public async Task ReIndexAsync()
        {
            await _indexManager.ReIndex<CampaignIndexModel, Guid>(IndexConstants.CampaignIndex);
        }
    }
}