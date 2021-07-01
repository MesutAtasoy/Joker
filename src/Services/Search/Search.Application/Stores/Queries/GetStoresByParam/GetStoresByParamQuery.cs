using System;
using MediatR;
using Search.Application.Shared.Dto.Request;
using Search.Core.IndexModels;

namespace Search.Application.Stores.Queries.GetStoresByParam
{
    public class GetStoresByParamQuery : SearchBaseRequest, IRequest<SearchBaseResponse<StoreIndexModel>>
    {
        public Guid? CountryId { get; set; }
        public Guid? CityId { get; set; }
        public Guid? DistrictId { get; set; }
        public Guid? NeighborhoodId { get; set; }
        public Guid? QuarterId { get; set; }
    }
}