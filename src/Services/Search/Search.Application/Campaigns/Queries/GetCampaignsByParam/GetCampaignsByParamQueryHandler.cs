using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nest;
using Search.Application.Shared.Dto.Request;
using Search.Core.Constants;
using Search.Core.IndexModels;

namespace Search.Application.Campaigns.Queries.GetCampaignsByParam
{
    public class GetCampaignsByParamQueryHandler : IRequestHandler<GetCampaignsByParamQuery,
            SearchBaseResponse<CampaignIndexModel>>
    {
        private readonly IElasticClient _elasticClient;

        public GetCampaignsByParamQueryHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<SearchBaseResponse<CampaignIndexModel>> Handle(GetCampaignsByParamQuery request,
            CancellationToken cancellationToken)
        {
            request.Page ??= 0;
            request.PageSize ??= 50;

            var searchResponse = await _elasticClient.SearchAsync<CampaignIndexModel>(s => s
                .Query(q =>
                    {
                        QueryContainer queryContainer = null;

                        if (!string.IsNullOrEmpty(request.q))
                        {
                            queryContainer &= q.MultiMatch(m => m.Fields(f => f.Field(x => x.Description, 4.0)
                                    .Field(x => x.Title, 4.0)
                                    .Field(x => x.Description, 3.0)
                                    .Field(x => x.Code, 1.0)
                                )
                                .Query(request.q)
                                .Type(TextQueryType.BestFields)
                                .Operator(Operator.Or));
                        }

                        if (!string.IsNullOrEmpty(request.StoreId))
                        {
                            queryContainer &= q.Term(t => 
                                t.Field(ff => ff.StoreId.Suffix("keyword")).Value(request.StoreId));
                        }

                        if (!string.IsNullOrEmpty(request.StoreName))
                        {
                            queryContainer &= q.Term(t =>
                                t.Field(ff => ff.StoreName.Suffix("keyword")).Value(request.StoreName));
                        }

                        if (!string.IsNullOrEmpty(request.SlugKey))
                        {
                            queryContainer &= q.Term(t =>
                                t.Field(ff => ff.SlugKey.Suffix("keyword")).Value(request.SlugKey));
                        }

                        if (!string.IsNullOrEmpty(request.Slug))
                        {
                            queryContainer &= q.Term(t =>
                                t.Field(ff => ff.Slug.Suffix("keyword")).Value(request.Slug));
                        }

                        return queryContainer;
                    }
                )
                .Index(IndexConstants.CampaignIndex)
                .Skip(request.Page * request.PageSize)
                .Take(request.PageSize), cancellationToken);

            return new SearchBaseResponse<CampaignIndexModel>(searchResponse.Took, request.Page.Value,
                request.PageSize.Value,
                searchResponse.Total,
                searchResponse.Documents.ToList());
        }
    }
}