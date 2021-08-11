using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Joker.CAP.IntegrationEvent;
using Nest;
using Search.Application.Campaigns.Events;
using Search.Core.Constants;
using Search.Core.IndexManagers.Campaign;
using Search.Core.IndexModels;

namespace Search.Application.Campaigns.EventHandlers
{
    public class MerchantNameUpdatedEventHandler : CAPIntegrationEventHandler<MerchantNameUpdatedEvent>
    {
        private readonly IElasticClient _elasticClient;
        private readonly ICampaignIndexManager _campaignIndexManager;


        public MerchantNameUpdatedEventHandler(IElasticClient elasticClient, ICampaignIndexManager campaignIndexManager)
        {
            _elasticClient = elasticClient;
            _campaignIndexManager = campaignIndexManager;
        }

        [CapSubscribe(nameof(MerchantNameUpdatedEvent))]
        public override async Task Handle(MerchantNameUpdatedEvent @event)
        {
            var searchResponse = await _elasticClient.SearchAsync<CampaignIndexModel>(s => s
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
                    await _campaignIndexManager.AddOrUpdateAsync(document);
                }
            }
        }
    }
}