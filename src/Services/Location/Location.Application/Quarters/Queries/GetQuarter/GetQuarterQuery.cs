using System;
using System.Collections.Generic;
using Location.Application.Quarters.Dto;
using MediatR;

namespace Location.Application.Quarters.Queries.GetQuarter
{
    public class GetQuarterQuery : IRequest<List<QuarterDto>>
    {
        public Guid NeighborhoodId { get; set; }
    }
}