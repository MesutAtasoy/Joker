using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Joker.CAP.IntegrationEvent;
using Nest;
using Search.Application.Shared.Events;
using Search.Core.Constants;
using Search.Core.IndexManagers.Campaign;
using Search.Core.IndexManagers.Store;
using Search.Core.IndexModels;

namespace Search.Application.Shared.EventHandlers
{
    public class StoreNameUpdatedEventHandler: CAPIntegrationEventHandler<StoreNameUpdatedEvent>
    {
        private readonly IElasticClient _elasticClient;
        private readonly IStoreIndexManager _storeIndexManager;
        private readonly ICampaignIndexManager _campaignIndexManager;

        public StoreNameUpdatedEventHandler(IElasticClient elasticClient,
            IStoreIndexManager storeIndexManager,
            ICampaignIndexManager campaignIndexManager)
        {
            _elasticClient = elasticClient;
            _storeIndexManager = storeIndexManager;
            _campaignIndexManager = campaignIndexManager;
        }

        [CapSubscribe(nameof(StoreNameUpdatedEvent))]
        public override async Task Handle(StoreNameUpdatedEvent @event)
        {
            var storeSearchResponse = await _elasticClient.SearchAsync<StoreIndexModel>(s => s
                .Query(q =>
                    {
                        QueryContainer queryContainer = q.Term(t =>
                            t.Field(ff => ff.Id.Suffix("keyword")).Value(@event.StoreId));
                        return queryContainer;
                    }
                )
                .Index(IndexConstants.StoreIndex));

            if (storeSearchResponse.Documents.Any())
            {
                foreach (var document in storeSearchResponse.Documents)
                {
                    document.Name = @event.NewName;
                    await _storeIndexManager.AddOrUpdateAsync(document);
                }
            }
            
            var campaignSearchResponse = await _elasticClient.SearchAsync<CampaignIndexModel>(s => s
                .Query(q =>
                    {
                        QueryContainer queryContainer = q.Term(t =>
                            t.Field(ff => ff.StoreId.Suffix("keyword")).Value(@event.StoreId));
                        return queryContainer;
                    }
                )
                .Index(IndexConstants.CampaignIndex));

            if (campaignSearchResponse.Documents.Any())
            {
                foreach (var document in campaignSearchResponse.Documents)
                {
                    document.StoreName = @event.NewName;
                    await _campaignIndexManager.AddOrUpdateAsync(document);
                }
            }
        }
    }
}