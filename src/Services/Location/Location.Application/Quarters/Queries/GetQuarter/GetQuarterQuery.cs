using System;
using System.Collections.Generic;
using Location.Core.Entities;
using MediatR;

namespace Location.Application.Quarters.Queries.GetQuarter
{
    public class GetQuarterQuery : IRequest<List<Quarter>>
    {
        public Guid NeighborhoodId { get; set; }
    }
}