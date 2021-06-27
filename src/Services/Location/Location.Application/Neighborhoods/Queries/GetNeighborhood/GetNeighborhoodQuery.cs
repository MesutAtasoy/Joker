using System;
using System.Collections.Generic;
using Location.Core.Entities;
using MediatR;

namespace Location.Application.Neighborhoods.Queries.GetNeighborhood
{
    public class GetNeighborhoodQuery : IRequest<List<Neighborhood>>
    {
        public Guid DistrictId { get; set; }
    }
}