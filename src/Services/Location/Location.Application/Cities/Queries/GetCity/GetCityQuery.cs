using System;
using System.Collections.Generic;
using Location.Core.Entities;
using MediatR;

namespace Location.Application.Cities.Queries.GetCity
{
    public class GetCityQuery : IRequest<List<City>>
    {
        public Guid CountryId { get; set; }
    }
}