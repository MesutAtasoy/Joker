using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Location.Core.Entities;
using Location.Core.Repositories;
using MediatR;

namespace Location.Application.Cities.Queries.GetCity
{
    public class GetCityQueryHandler : IRequestHandler<GetCityQuery, List<City>>
    {
        private readonly ICityRepository _cityRepository;

        public GetCityQueryHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<List<City>> Handle(GetCityQuery request, CancellationToken cancellationToken)
        {
            return await _cityRepository.ByCountryIdAsync(request.CountryId);
        }
    }
}