using System.Threading;
using System.Threading.Tasks;
using Location.Core.Entities;
using Location.Core.Repositories;
using MediatR;

namespace Location.Application.Countries.Queries.GetCountry
{
    public class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, Country>
    {
        private readonly ICountryRepository _countryRepository;

        public GetCountryQueryHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<Country> Handle(GetCountryQuery request, CancellationToken cancellationToken)
        {
            var country = await _countryRepository.GetDefaultCountryAsync();
            return country;
        }
    }
}