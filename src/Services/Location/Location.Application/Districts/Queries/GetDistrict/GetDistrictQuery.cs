using System;
using System.Collections.Generic;
using Location.Core.Entities;
using MediatR;

namespace Location.Application.Districts.Queries.GetDistrict
{
    public class GetDistrictQuery : IRequest<List<District>>
    {
        public Guid CityId { get; set; }
    }
}
