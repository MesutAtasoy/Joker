using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Joker.CAP.IntegrationEvent;
using Nest;
using Search.Application.Campaigns.Events;
using Search.Core.Constants;
using Search.Core.IndexManagers.Store;
using Search.Core.IndexModels;

namespace Search.Application.Stores.EventHandlers
{
    public class MerchantNameUpdatedEventHandler : CAPIntegrationEventHandler<MerchantNameUpdatedEvent>
    {
        private readonly IElasticClient _elasticClient;
        private readonly IStoreIndexManager _storeIndexManager;


        public MerchantNameUpdatedEventHandler(IElasticClient elasticClient, IStoreIndexManager storeIndexManager)
        {
            _elasticClient = elasticClient;
            _storeIndexManager = storeIndexManager;
        }

        [CapSubscribe(nameof(MerchantNameUpdatedEvent))]
        public override async Task Handle(MerchantNameUpdatedEvent @event)
        {
            var searchResponse = await _elasticClient.SearchAsync<StoreIndexModel>(s => s
                .Query(q =>
                    {
                        QueryContainer queryContainer = q.Term(t =>
                            t.Field(ff => ff.MerchantId.Suffix("keyword")).Value(@event.MerchantId));
                        return queryContainer;
                    }
                )
                .Index(IndexConstants.CampaignIndex));

            if (searchResponse.Documents.Any())
            {
                foreach (var document in searchResponse.Documents)
                {
                    document.MerchantName = @event.NewName;
                    await _storeIndexManager.AddOrUpdateAsync(document);
                }
            }
        }
    }
}