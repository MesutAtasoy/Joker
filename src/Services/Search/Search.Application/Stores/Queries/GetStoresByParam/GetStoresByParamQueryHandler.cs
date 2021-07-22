using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nest;
using Search.Application.Shared.Dto.Request;
using Search.Core.Constants;
using Search.Core.IndexModels;

namespace Search.Application.Stores.Queries.GetStoresByParam
{
    public class GetStoresByParamQueryHandler : IRequestHandler<GetStoresByParamQuery, SearchBaseResponse<StoreIndexModel>>
    {
        private readonly IElasticClient _elasticClient;

        public GetStoresByParamQueryHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<SearchBaseResponse<StoreIndexModel>> Handle(GetStoresByParamQuery request,
            CancellationToken cancellationToken)
        {
            request.Page ??= 0;
            request.PageSize ??= 50;

            var searchResponse = await _elasticClient.SearchAsync<StoreIndexModel>(s => s
                .Query(q =>
                    {
                        QueryContainer queryContainer = null;

                        if (!string.IsNullOrEmpty(request.q))
                        {
                            queryContainer &= q.MultiMatch(m => m.Fields(f => f.Field(x => x.Description, 4.0)
                                    .Field(x => x.Name, 4.0)
                                    .Field(x => x.Slogan, 3.0)
                                    .Field(x => x.Address, 1.0)
                                )
                                .Query(request.q)
                                .Type(TextQueryType.BestFields)
                                .Operator(Operator.Or));
                        }

                        if (request.StoreId.HasValue)
                        {
                            queryContainer &= q.Term(t =>
                                t.Field(ff => ff.Id.Suffix("keyword")).Value(request.StoreId));
                        }

                        if (request.CountryId.HasValue)
                        {
                            queryContainer &= q.Term(t =>
                                t.Field(ff => ff.CountryId.Suffix("keyword")).Value(request.CountryId));
                        }

                        if (request.CityId.HasValue)
                        {
                            queryContainer &= q.Term(t =>
                                t.Field(ff => ff.CityId.Suffix("keyword")).Value(request.CityId));
                        }

                        if (request.DistrictId.HasValue)
                        {
                            queryContainer &= q.Term(t =>
                                t.Field(ff => ff.DistrictId.Suffix("keyword")).Value(request.DistrictId));
                        }

                        if (request.NeighborhoodId.HasValue)
                        {
                            queryContainer &= q.Term(t =>
                                t.Field(ff => ff.NeighborhoodId.Suffix("keyword")).Value(request.NeighborhoodId));
                        }

                        if (request.QuarterId.HasValue)
                        {
                            queryContainer &= q.Term(t =>
                                t.Field(ff => ff.QuarterId.Suffix("keyword")).Value(request.QuarterId));
                        }

                        return queryContainer;
                    }
                )
                .Index(IndexConstants.StoreIndex)
                .Skip(request.Page * request.PageSize)
                .Take(request.PageSize)
            );

            return new SearchBaseResponse<StoreIndexModel>(searchResponse.Took, request.Page.Value,
                request.PageSize.Value,
                searchResponse.Total,
                searchResponse.Documents.ToList());
        }
    }
}