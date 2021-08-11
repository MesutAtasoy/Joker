using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Joker.CAP.IntegrationEvent;
using Nest;
using Search.Core.Constants;
using Search.Core.IndexManagers.Store;
using Search.Core.IndexModels;
using Search.Application.Stores.Events;

namespace Search.Application.Stores.EventHandlers
{
    public class StoreNameUpdatedEventHandler: CAPIntegrationEventHandler<StoreNameUpdatedEvent>
    {
        private readonly IElasticClient _elasticClient;
        private readonly IStoreIndexManager _storeIndexManager;


        public StoreNameUpdatedEventHandler(IElasticClient elasticClient, IStoreIndexManager storeIndexManager)
        {
            _elasticClient = elasticClient;
            _storeIndexManager = storeIndexManager;
        }

        [CapSubscribe(nameof(StoreNameUpdatedEvent))]
        public override async Task Handle(StoreNameUpdatedEvent @event)
        {
            var searchResponse = await _elasticClient.SearchAsync<StoreIndexModel>(s => s
                .Query(q =>
                    {
                        QueryContainer queryContainer = q.Term(t =>
                            t.Field(ff => ff.Id.Suffix("keyword")).Value(@event.StoreId));
                        return queryContainer;
                    }
                )
                .Index(IndexConstants.StoreIndex));

            if (searchResponse.Documents.Any())
            {
                foreach (var document in searchResponse.Documents)
                {
                    document.Name = @event.NewName;
                    await _storeIndexManager.AddOrUpdateAsync(document);
                }
            }
        }
    }
}