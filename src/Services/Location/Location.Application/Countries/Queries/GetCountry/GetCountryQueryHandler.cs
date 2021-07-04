using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Location.Application.Countries.Dto;
using Location.Core.Repositories;
using MediatR;

namespace Location.Application.Countries.Queries.GetCountry
{
    public class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, CountryDto>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public GetCountryQueryHandler(ICountryRepository countryRepository,
            IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<CountryDto> Handle(GetCountryQuery request, CancellationToken cancellationToken)
        {
            var country = await _countryRepository.GetDefaultCountryAsync();
            return _mapper.Map<CountryDto>(country);
        }
    }
}